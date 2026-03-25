using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceLoader : MonoBehaviour
{
    public Material defaultMaterial;
    private Dictionary<string, Material> materialCache = new Dictionary<string, Material>();
    private Dictionary<string, DatMesh> meshLookup = new Dictionary<string, DatMesh>();

    void Start()
    {
        TwtModule twtModule = new TwtModule();
        PixiesModule pixiesModule = new PixiesModule();
        TwtFile twtFile = twtModule.LoadTwt("D:\\SteamLibrary\\steamapps\\common\\Carmageddon2\\data\\RACES\\newcity1.TWT");
        TwtFile texturesTwtFile = twtModule.LoadTwt("D:\\SteamLibrary\\steamapps\\common\\Carmageddon2\\data\\RACES\\NEWC.TWT");
        DatFile datFile = new DatModule().LoadDat(twtFile.GetFile("newcity1.dat"));
        MatFile matFile = new MatModule().LoadMat(twtFile.GetFile("newcity1.mat"));
        ActFile actFile = new ActModule().LoadAct(twtFile.GetFile("newcity1.act"));
        Dictionary<string, MatFileMaterial> materialsByName = matFile.GetMaterialsByName();
        PixiesFile pixiesFile = pixiesModule.LoadPixies(texturesTwtFile.GetFile("PIXIES.P16"));

        // Build mesh lookup by name
        foreach (DatMesh mesh in datFile.meshes)
        {
            meshLookup[mesh.name] = mesh;
        }

        // Build scene from ACT hierarchy
        foreach (ActFileActor root in actFile.roots)
        {
            BuildActorHierarchy(root, transform, materialsByName, pixiesFile);
        }
    }

    private void BuildActorHierarchy(ActFileActor actor, Transform parentTransform, Dictionary<string, MatFileMaterial> materialsByName, PixiesFile pixiesFile)
    {
        GameObject go = new GameObject(actor.identifier);
        go.transform.SetParent(parentTransform, false);

        // Apply the actor's 3x4 transform matrix
        ApplyMatrix3D(go.transform, actor.transform);

        // If this actor references a model, attach the mesh
        if (actor.model != null && meshLookup.ContainsKey(actor.model))
        {
            AttachMesh(go, meshLookup[actor.model], materialsByName, pixiesFile);
        }

        // Recurse into children
        foreach (ActFileActor child in actor.children)
        {
            BuildActorHierarchy(child, go.transform, materialsByName, pixiesFile);
        }
    }

    private void ApplyMatrix3D(Transform t, Matrix3D m)
    {
        if (m == null) return;

        // BRender (right-handed, row-vector) → Unity (left-handed, column-vector)
        // M_unity = S * M_br^T * S, where S = diag(-1,1,1,1) flips X axis
        Matrix4x4 mat = new Matrix4x4(
            new Vector4( m.M11, -m.M12, -m.M13, 0),
            new Vector4(-m.M21,  m.M22,  m.M23, 0),
            new Vector4(-m.M31,  m.M32,  m.M33, 0),
            new Vector4(-m.M41,  m.M42,  m.M43, 1)
        );

        t.localPosition = mat.GetColumn(3);
        t.localRotation = mat.rotation;
        t.localScale = new Vector3(mat.GetColumn(0).magnitude, mat.GetColumn(1).magnitude, mat.GetColumn(2).magnitude);
    }

    private void AttachMesh(GameObject go, DatMesh datMesh, Dictionary<string, MatFileMaterial> materialsByName, PixiesFile pixiesFile)
    {
        MeshFilter meshFilter = go.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();

        var facesByMaterial = datMesh.faces
            .Where(f => f.materialId > 0)
            .GroupBy(f => f.materialId)
            .OrderBy(g => g.Key)
            .ToList();

        Mesh unityMesh = new Mesh
        {
            vertices = System.Array.ConvertAll(datMesh.vertices, v => new Vector3(-v.X, v.Y, v.Z)),
            uv = System.Array.ConvertAll(datMesh.uvs, uv => new Vector2(uv.X, 1f - uv.Y)),
            subMeshCount = facesByMaterial.Count
        };

        for (int i = 0; i < facesByMaterial.Count; i++)
        {
            int[] triangles = facesByMaterial[i]
                .SelectMany(f => new int[] { f.v1, f.v3, f.v2 })
                .ToArray();
            unityMesh.SetTriangles(triangles, i);
        }

        unityMesh.RecalculateNormals();
        unityMesh.RecalculateBounds();

        Material[] materials = new Material[facesByMaterial.Count];
        for (int i = 0; i < facesByMaterial.Count; i++)
        {
            int matIndex = facesByMaterial[i].Key - 1;
            if (datMesh.materials != null && matIndex >= 0 && matIndex < datMesh.materials.Length)
            {
                string matName = datMesh.materials[matIndex];
                materials[i] = GetOrCreateMaterial(matName, materialsByName, pixiesFile);
            }
            else
            {
                materials[i] = defaultMaterial;
            }
        }

        meshFilter.mesh = unityMesh;
        meshRenderer.materials = materials;
    }

    private Material GetOrCreateMaterial(string matName, Dictionary<string, MatFileMaterial> materialsByName, PixiesFile pixiesFile)
    {
        if (materialCache.ContainsKey(matName))
            return materialCache[matName];

        MatFileMaterial c2mat = materialsByName.ContainsKey(matName) ? materialsByName[matName] : null;
        Material mat = new Material(defaultMaterial);
        mat.color = c2mat != null ? new Color32(c2mat.diffuseColor[0], c2mat.diffuseColor[1], c2mat.diffuseColor[2], c2mat.diffuseColor[3]) : Color.white;

        if (c2mat != null && c2mat.flags.HasFlag(MatFileMaterial.Settings.Two_Sided))
        {
            mat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        }

        if (mat.color.a < 1f)
        {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }

        string texture = c2mat != null ? c2mat.texture : null;
        mat.SetTexture("_MainTex", pixiesFile.GetFile(texture)?.bitmap);

        materialCache[matName] = mat;
        return mat;
    }

    void Update()
    {
    }
}

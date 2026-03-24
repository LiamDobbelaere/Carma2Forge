using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaceLoader : MonoBehaviour
{
    public Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        TwtModule twtModule = new TwtModule();
        PixiesModule pixiesModule = new PixiesModule();
        TwtFile twtFile = twtModule.LoadTwt("D:\\SteamLibrary\\steamapps\\common\\Carmageddon2\\data\\RACES\\newcity1.TWT");
        TwtFile texturesTwtFile = twtModule.LoadTwt("D:\\SteamLibrary\\steamapps\\common\\Carmageddon2\\data\\RACES\\NEWC.TWT");
        DatFile datFile = new DatModule().LoadDat(twtFile.GetFile("newcity1.dat"));
        MatFile matFile = new MatModule().LoadMat(twtFile.GetFile("newcity1.mat"));
        Dictionary<string, MatFileMaterial> materialsByName = matFile.GetMaterialsByName();

        PixiesFile pixiesFile = pixiesModule.LoadPixies(texturesTwtFile.GetFile("PIXIES.P16"));

        foreach (DatMesh mesh in datFile.meshes)
        {
            // create a new unity gameobject as child
            GameObject meshObj = new GameObject(mesh.name);

            // add a mesh filter and renderer
            MeshFilter meshFilter = meshObj.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = meshObj.AddComponent<MeshRenderer>();

            // Group faces by materialId for submeshes
            // materialId is 1-based in BRender (0 = no material assigned)
            var facesByMaterial = mesh.faces
                .Where(f => f.materialId > 0)
                .GroupBy(f => f.materialId)
                .OrderBy(g => g.Key)
                .ToList();

            // convert dat mesh to unity mesh
            Mesh unityMesh = new Mesh
            {
                vertices = System.Array.ConvertAll(mesh.vertices, v => new Vector3(v.X, v.Y, v.Z)),
                uv = System.Array.ConvertAll(mesh.uvs, uv => new Vector2(uv.X, uv.Y)),
                subMeshCount = facesByMaterial.Count
            };

            for (int i = 0; i < facesByMaterial.Count; i++)
            {
                int[] triangles = facesByMaterial[i]
                    .SelectMany(f => new int[] { f.v1, f.v2, f.v3 })
                    .ToArray();
                unityMesh.SetTriangles(triangles, i);
            }

            unityMesh.RecalculateNormals();
            unityMesh.RecalculateBounds();

            // Assign a material per submesh
            Material[] materials = new Material[facesByMaterial.Count];
            for (int i = 0; i < facesByMaterial.Count; i++)
            {
                int matIndex = facesByMaterial[i].Key - 1; // materialId is 1-based
                if (mesh.materials != null && matIndex >= 0 && matIndex < mesh.materials.Length)
                {
                    string matName = mesh.materials[matIndex];
                    MatFileMaterial c2mat = materialsByName.ContainsKey(matName) ? materialsByName[matName] : null;

                    Material mat = new Material(defaultMaterial);
                    //mat.color = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());

                    /*foreach (PixiesFileEntry entry in pixiesFile.entries)
                    {
                        Debug.Log($"Pixies file entry: {entry.filename}");
                        Debug.Log($"Looking for {matName.ToLower()} in pixies file entries");
                    }*/

                    mat.color = c2mat != null ? new Color32(c2mat.diffuseColor[0], c2mat.diffuseColor[1], c2mat.diffuseColor[2], c2mat.diffuseColor[3]) : Color.white;
                    if (mat.color.a < 1f)
                    {
                        // set material to translucent rendering mode
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
                    materials[i] = mat;
                }
                else
                {
                    materials[i] = defaultMaterial;
                }
            }
            meshFilter.mesh = unityMesh;
            meshRenderer.materials = materials;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

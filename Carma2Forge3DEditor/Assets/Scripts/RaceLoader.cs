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
            var facesByMaterial = mesh.faces
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
                ushort matId = facesByMaterial[i].Key;
                if (mesh.materials != null && matId < mesh.materials.Length)
                {
                    // TODO: load actual material/texture by name: mesh.materials[matId]
                    // For now, make a random color based on the mesh.materials[matId] name
                    string matName = mesh.materials[matId];
                    MatFileMaterial c2mat = materialsByName.ContainsKey(matName) ? materialsByName[matName] : null;

                    Material mat = new Material(defaultMaterial);
                    //mat.color = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble());

                    /*foreach (PixiesFileEntry entry in pixiesFile.entries)
                    {
                        Debug.Log($"Pixies file entry: {entry.filename}");
                        Debug.Log($"Looking for {matName.ToLower()} in pixies file entries");
                    }*/

                    mat.color = c2mat != null ? new Color32(c2mat.diffuseColor[0], c2mat.diffuseColor[1], c2mat.diffuseColor[2], c2mat.diffuseColor[3]) : Color.white;
                    if (c2mat != null && c2mat.diffuseColor[3] < 255)
                    {
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        mat.SetInt("_ZWrite", 0);
                        mat.DisableKeyword("_ALPHATEST_ON");
                        mat.EnableKeyword("_ALPHABLEND_ON");
                        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = 3000;
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
            meshRenderer.materials = materials.Reverse().ToArray(); // reverse because unity applies materials in reverse order for some reason

            meshFilter.mesh = unityMesh;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

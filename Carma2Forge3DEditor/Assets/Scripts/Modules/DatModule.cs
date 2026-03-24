using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

public class DatMesh {
    public ushort flags;
    public string name;
    public Vector3[] vertices;
    public Vector2[] uvs;
    public DatFace[] faces;
    public string[] materials;
  }

  public class DatFace {
    public ushort v1;
    public ushort v2;
    public ushort v3;
    public ushort smoothingGroup;
    public byte flags;
    public ushort materialId;
  }

  public class DatFile {
    public List<DatMesh> meshes = new List<DatMesh>();
  }

  public enum DatChunkType {
    Materials = 22,
    Vertices = 23,
    UVs = 24,
    FaceMaterial = 26,
    Actor = 35,
    Model = 36,
    Transform = 37,
    Material = 38,
    Light = 39,
    CameraOld = 40,
    Bounds = 41,
    AddChild = 42,
    Matrix = 43,
    BoundingBox = 50,
    LightOld = 51,
    Camera = 52,
    Faces = 53,
    ModelOld2 = 54,
    EOF = 0
  }

  public class DatModule {
    private static UInt32[] datHeader = { 0x12, 0x8, 0xface, 0x2 };

    public DatFile LoadDat(TwtFileEntry twtFileEntry) {
      MemoryStream stream = new MemoryStream(twtFileEntry.data);
      DatFile dat = new DatFile();
      DatMesh mesh = new DatMesh();

      using (BinaryReader br = new BinaryReader(stream)) {
        ValidateDatHeader(br);

        while (br.BaseStream.Position < br.BaseStream.Length) {
          DatChunkType chunkType = (DatChunkType)br.ReadUInt32BE();
          int _ = br.ReadInt32BE(); // chunk size, not needed since we can just read until the next chunk

          switch (chunkType) {
            case DatChunkType.ModelOld2:
              mesh = new DatMesh {
                flags = br.ReadUInt16BE(),
                name = br.ReadNullTerminatedString()
              };
              break;
            case DatChunkType.Vertices:
              uint vertexCount = br.ReadUInt32BE();
              mesh.vertices = new Vector3[vertexCount];
              for (int i = 0; i < vertexCount; i++) {
                float x = br.ReadSingleBE();
                float y = br.ReadSingleBE();
                float z = br.ReadSingleBE();

                mesh.vertices[i] = new Vector3(x, y, z);
              }
              break;
            case DatChunkType.UVs:
              uint uvCount = br.ReadUInt32BE();
              mesh.uvs = new Vector2[uvCount];
              for (int i = 0; i < uvCount; i++) {
                float u = br.ReadSingleBE();
                float v = br.ReadSingleBE();
                mesh.uvs[i] = new Vector2(u, v);
              }
              break;
            case DatChunkType.Faces:
              uint faceCount = br.ReadUInt32BE();
              mesh.faces = new DatFace[faceCount];
              for (int i = 0; i < faceCount; i++) {
                ushort v1 = br.ReadUInt16BE();
                ushort v2 = br.ReadUInt16BE();
                ushort v3 = br.ReadUInt16BE();
                ushort smoothingGroup = br.ReadUInt16BE();
                byte flags = br.ReadByte();
                DatFace face = new DatFace {
                  v1 = v1,
                  v2 = v2,
                  v3 = v3,
                  smoothingGroup = smoothingGroup,
                  flags = flags
                };
                mesh.faces[i] = face;
              }
              break;
            case DatChunkType.Materials:
              uint materialCount = br.ReadUInt32BE();
              mesh.materials = new string[materialCount];
              for (int i = 0; i < materialCount; i++) {
                mesh.materials[i] = br.ReadNullTerminatedString();
              }
              break;
            case DatChunkType.FaceMaterial:
              uint faceMatCount = br.ReadUInt32BE();
              br.ReadBytes(4);

              for (int i = 0; i < faceMatCount; i++) {
                mesh.faces[i].materialId = br.ReadUInt16BE();
              }
              break;
            case DatChunkType.EOF:
              dat.meshes.Add(mesh);
              break;
            default:
              throw new Exception($"Unsupported chunk type {chunkType} in DAT file");
          }
        }
      }

      // here come dat file o shit waddup
      return dat;
    }

    private void ValidateDatHeader(BinaryReader reader) {
      for (int i = 0; i < datHeader.Length; i++) {
        uint value = reader.ReadUInt32BE();
        if (datHeader[i] != value) {
          throw new Exception($"Unexpected value {value} where {datHeader[i]} was expected");
        }
      }
    }
  }
using System.IO;
using System.Numerics;
using System.Text;
using Carma2ForgeLib.Modules.TwtModule;
using Carma2ForgeLib.Utilities;

namespace Carma2ForgeLib.Modules.MatModule {
  public class Matrix2D : IEquatable<Matrix2D> {
    public float M11;
    public float M12;
    public float M21;
    public float M22;
    public float M31;
    public float M32;

    public static Matrix2D Identity => new Matrix2D(1.0f, 0, 0, 1.0f, 0, 0);

    public Matrix2D(float m11, float m12, float m21, float m22, float m31, float m32) {
      M11 = m11;
      M12 = m12;
      M21 = m21;
      M22 = m22;
      M31 = m31;
      M32 = m32;
    }

    public override string ToString() {
      return "{ {M11:" + M11 + " M12:" + M12 + "} {M21:" + M21 + " M22:" + M22 + "} {M31:" + M31 + " M32:" + M32 + "} }";
    }

    public static bool operator ==(Matrix2D x, Matrix2D y) {
      return x.Equals(y);
    }

    public static bool operator !=(Matrix2D x, Matrix2D y) {
      return !x.Equals(y);
    }

    public bool Equals(Matrix2D other) {
      return (M11 == other.M11 && M12 == other.M12 && M21 == other.M21 && M22 == other.M22 && M31 == other.M31 && M32 == other.M32);
    }

    public override bool Equals(object obj) {
      Matrix2D m = obj as Matrix2D;

      if (m == null) { return false; }
      return (this == m);
    }

    public override int GetHashCode() {
      return M11.GetHashCode() ^
             M12.GetHashCode() ^
             M21.GetHashCode() ^
             M22.GetHashCode() ^
             M31.GetHashCode() ^
             M32.GetHashCode();
    }
  }

  public class MatFileMaterial {
    [Flags]
    public enum Settings {
      Light = 1,
      PreLit = 2,
      Smooth = 4,
      Environment = 8,
      Environment_Local = 16,
      Perspective = 32,
      Decal = 64,
      IFromU = 128,
      IFromV = 256,
      UFromI = 512,
      VFromI = 1024,
      AlwaysVisible = 2048,
      Two_Sided = 4096,
      ForceFront = 8192,
      Dither = 16384,
      Custom = 32768,
      MapAntialiasing = 65536,
      MapInterpolation = 131072,
      MipInterpolation = 262144,
      Fog_Local = 524288,
      Subdivide = 1048576,
      ZTransparency = 2097152,
    }

    public byte[] diffuseColor = new byte[] { 255, 255, 255, 255 };
    public float ambientLighting;
    public float directionalLighting;
    public float specularLighting;
    public float specularPower;
    public Settings flags = Settings.Light | Settings.Perspective;
    public Matrix2D uvMatrix;
    public byte indexBase;
    public byte indexRange;
    public string name;
    public string texture;
    public string shadeTable;
  }

  public class MatFile {
    public List<MatFileMaterial> materials = new List<MatFileMaterial>();
  }

  public enum MatChunkType {
    C1Mat = 0x4,
    C2Mat = 0x3c,
    ColorMap = 0x1c,
    ShadeTable = 0x1f,
    Commit = 0x0
  }


  public class MatModule : BaseModule {
    private static UInt32[] matHeader = { 0x12, 0x8, 0x5, 0x2 };
    private Carma2ForgeConfig config;

    public void Initialize(Carma2ForgeConfig config) {
      this.config = config;
    }

    public MatFile LoadMat(TwtFileEntry twtFileEntry) {
      MemoryStream stream = new MemoryStream(twtFileEntry.data);
      MatFile mat = new MatFile();
      MatFileMaterial newMaterial = new MatFileMaterial();

      using (BinaryReader br = new BinaryReader(stream)) {
        ValidateMatHeader(br);
        // we don't support ascii mat files in this household >:(

        while (br.BaseStream.Position < br.BaseStream.Length) {
          int chunkType = (int) br.ReadUInt32BE();
          int length = (int) br.ReadUInt32BE();

          switch (chunkType) {
            case (int)MatChunkType.C1Mat:
              newMaterial = new MatFileMaterial();

              newMaterial.diffuseColor[0] = br.ReadByte();
              newMaterial.diffuseColor[1] = br.ReadByte();
              newMaterial.diffuseColor[2] = br.ReadByte();
              newMaterial.diffuseColor[3] = br.ReadByte();
              newMaterial.ambientLighting = br.ReadSingleBE();
              newMaterial.directionalLighting = br.ReadSingleBE();
              newMaterial.specularLighting = br.ReadSingleBE();
              newMaterial.specularPower = br.ReadSingleBE();
              newMaterial.flags = (MatFileMaterial.Settings)br.ReadUInt16BE();
              newMaterial.uvMatrix = new Matrix2D(br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE());
              newMaterial.indexBase = br.ReadByte();
              newMaterial.indexRange = br.ReadByte();
              newMaterial.name = br.ReadNullTerminatedString();
              break;
            case (int)MatChunkType.C2Mat:
              newMaterial = new MatFileMaterial();

              newMaterial.diffuseColor[0] = br.ReadByte();
              newMaterial.diffuseColor[1] = br.ReadByte();
              newMaterial.diffuseColor[2] = br.ReadByte();
              newMaterial.diffuseColor[3] = br.ReadByte();
              newMaterial.ambientLighting = br.ReadSingleBE();
              newMaterial.directionalLighting = br.ReadSingleBE();
              newMaterial.specularLighting = br.ReadSingleBE();
              newMaterial.specularPower = br.ReadSingleBE();
              newMaterial.flags = (MatFileMaterial.Settings)br.ReadUInt32BE();
              newMaterial.uvMatrix = new Matrix2D(br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE());
              if (br.ReadUInt32BE() != 169803776) {
                //throw new Exception("Expected 169803776 after UV matrix in C2Mat chunk");
              }
              br.ReadBytes(13); // 13 bytes of nothing
              newMaterial.name = br.ReadNullTerminatedString();
              break;
            case (int)MatChunkType.ColorMap:
              newMaterial.texture = br.ReadNullTerminatedString();
              break;
            case (int)MatChunkType.ShadeTable:
              newMaterial.shadeTable = br.ReadNullTerminatedString();
              break;
            case (int)MatChunkType.Commit:
              mat.materials.Add(newMaterial);
              break;
            default:
              throw new Exception($"Unsupported chunk type {chunkType} in MAT file");
          }
        }
      }

      return mat;
    }

    private void ValidateMatHeader(BinaryReader reader) {
      for (int i = 0; i < matHeader.Length; i++) {
        uint value = reader.ReadUInt32BE();
        if (matHeader[i] != value) {
          throw new Exception($"Unexpected value {value} where {matHeader[i]} was expected");
        }
      }
    }
  }
}

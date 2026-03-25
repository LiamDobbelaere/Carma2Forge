using System.Drawing;
using System.IO;
using System.Numerics;
using System.Text;
using Carma2ForgeLib.Modules.TwtModule;
using Carma2ForgeLib.Utilities;

namespace Carma2ForgeLib.Modules.MatModule {
  public class ActFileActor {
    public ActorType type;
    public RenderStyle renderStyle;
    public string identifier;
    public string model;
    public string material;
    public Matrix3D transform;
    public MeshExtents? bounds;
    public Light light;
    public Camera camera;
    public ActFileActor parent;
    public List<ActFileActor> children = new();
  }

  public class ActFile {
    public List<ActFileActor> roots = new List<ActFileActor>();
  }

  public enum CameraType {

  }

  public class Camera {
    public CameraType type;
    public ushort fov;
    public float hitherZ;
    public float yonZ;
    public float aspect;
  }

  public enum LightType {

  }

  public class Light {
    public LightType type;
    public Color color;
    public Vector3 attenuation;
    public ushort coneInner;
    public ushort coneOuter;
  }

  public enum ActChunkType {
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
    EOF = 0
  }
  public enum ActorType {
    BR_ACTOR_NONE,
    BR_ACTOR_MODEL,
    BR_ACTOR_LIGHT,
    BR_ACTOR_CAMERA,
    BR_ACTOR_BOUNDS,
    BR_ACTOR_BOUNDS_CORRECT,
    BR_ACTOR_CLIP_PLANE
  }
  public enum RenderStyle {
    BR_RSTYLE_DEFAULT,
    BR_RSTYLE_NONE,
    BR_RSTYLE_POINTS,
    BR_RSTYLE_EDGES,
    BR_RSTYLE_FACES = 0x4,
    BR_RSTYLE_BOUNDING_POINTS,
    BR_RSTYLE_BOUNDING_EDGES,
    BR_RSTYLE_BOUNDING_FACES
  }

  public class Matrix3D {
    public float M11;
    public float M12;
    public float M13;
    public float M21;
    public float M22;
    public float M23;
    public float M31;
    public float M32;
    public float M33;
    public float M41;
    public float M42;
    public float M43;

    public static Matrix3D Identity => new Matrix3D(1.0f, 0, 0, 0, 1.0f, 0, 0, 0, 1.0f, 0, 0, 0);

    public Matrix3D(float M11, float M12, float M13, float M21, float M22, float M23, float M31, float M32, float M33, float M41, float M42, float M43) {
      this.M11 = M11;
      this.M12 = M12;
      this.M13 = M13;
      this.M21 = M21;
      this.M22 = M22;
      this.M23 = M23;
      this.M31 = M31;
      this.M32 = M32;
      this.M33 = M33;
      this.M41 = M41;
      this.M42 = M42;
      this.M43 = M43;
    }

    public Matrix3D(Vector3 Position) {
      M11 = 1.0f;
      M12 = 0;
      M13 = 0;
      M21 = 0;
      M22 = 1.0f;
      M23 = 0;
      M31 = 0;
      M32 = 0;
      M33 = 1.0f;
      M41 = Position.X;
      M42 = Position.Y;
      M43 = Position.Z;
    }

    public Matrix3D(Vector3 row1, Vector3 row2, Vector3 row3, Vector3 row4) {
      M11 = row1.X;
      M12 = row1.Y;
      M13 = row1.Z;
      M21 = row2.X;
      M22 = row2.Y;
      M23 = row2.Z;
      M31 = row3.X;
      M32 = row3.Y;
      M33 = row3.Z;
      M41 = row4.X;
      M42 = row4.Y;
      M43 = row4.Z;
    }

    static float toRads = (float)Math.PI / 180;

    public static Matrix3D CreateRotationX(float Degrees) {
      Degrees *= toRads;

      Matrix3D m = Matrix3D.Identity;
      m.M22 = (float)Math.Cos(Degrees);
      m.M23 = (float)Math.Sin(Degrees);
      m.M32 = -(float)Math.Sin(Degrees);
      m.M33 = (float)Math.Cos(Degrees);
      return m;
    }

    public static Matrix3D CreateRotationY(float Degrees) {
      Degrees *= toRads;

      Matrix3D m = Matrix3D.Identity;
      m.M11 = (float)Math.Cos(Degrees);
      m.M13 = -(float)Math.Sin(Degrees);
      m.M31 = (float)Math.Sin(Degrees);
      m.M33 = (float)Math.Cos(Degrees);
      return m;
    }

    public static Matrix3D CreateRotationZ(float Degrees) {
      Degrees *= toRads;

      Matrix3D m = Matrix3D.Identity;
      m.M11 = (float)Math.Cos(Degrees);
      m.M12 = (float)Math.Sin(Degrees);
      m.M21 = -(float)Math.Sin(Degrees);
      m.M22 = (float)Math.Cos(Degrees);
      return m;
    }

    public static Matrix3D CreateScale(float scale) {
      return CreateScale(scale, scale, scale);
    }

    public static Matrix3D CreateScale(float x, float y, float z) {
      Matrix3D m = Matrix3D.Identity;
      m.M11 = x;
      m.M22 = y;
      m.M33 = z;
      return m;
    }

    public Vector3 Position {
      get => new Vector3(M41, M42, M43);
      set {
        M41 = value.X;
        M42 = value.Y;
        M43 = value.Z;
      }
    }

    public float Scale {
      set {
        M11 = value;
        M22 = value;
        M33 = value;
      }
    }

    public static Matrix3D operator *(Matrix3D x, Matrix3D y) {
      Vector3 p = x.Position + y.Position;

      return new Matrix3D(
          (x.M11 * y.M11) + (x.M12 * y.M21) + (x.M13 * y.M31), (x.M11 * y.M12) + (x.M12 * y.M22) + (x.M13 * y.M32), (x.M11 * y.M13) + (x.M12 * y.M23) + (x.M13 * y.M33),
          (x.M21 * y.M11) + (x.M22 * y.M21) + (x.M23 * y.M31), (x.M21 * y.M12) + (x.M22 * y.M22) + (x.M23 * y.M32), (x.M21 * y.M13) + (x.M22 * y.M23) + (x.M23 * y.M33),
          (x.M31 * y.M11) + (x.M32 * y.M21) + (x.M33 * y.M31), (x.M31 * y.M12) + (x.M32 * y.M22) + (x.M33 * y.M32), (x.M31 * y.M13) + (x.M32 * y.M23) + (x.M33 * y.M33),
          p.X, p.Y, p.Z
      );
    }

    public override bool Equals(object obj) {
      Matrix3D other = (Matrix3D)obj;

      if (other == null) { return false; }

      return M11 == other.M11 && M12 == other.M12 && M13 == other.M13 &&
             M21 == other.M21 && M22 == other.M22 && M23 == other.M23 &&
             M31 == other.M31 && M32 == other.M32 && M33 == other.M33 &&
             M41 == other.M41 && M42 == other.M42 && M43 == other.M43;
    }

    public override string ToString() {
      return "{ {M11:" + M11 + " M12:" + M12 + " M13:" + M13 + "} {M21:" + M21 + " M22:" + M22 + " M23:" + M23 + "} {M31:" + M31 + " M32:" + M32 + " M33:" + M33 + "} {M41:" + M41 + " M42:" + M42 + " M43:" + M43 + "} }";
    }
  }
  public struct MeshExtents {
    public Vector3 Min;
    public Vector3 Max;

    public MeshExtents(Vector3 Min, Vector3 Max) {
      this.Min = Min;
      this.Max = Max;
    }

    public override string ToString() {
      return "Min : " + Min.ToString() + " Max : " + Max.ToString();
    }
  }

  public class ActModule : BaseModule {
    private static UInt32[] actHeader = { 0x12, 0x8, 0x1, 0x2 };
    private Carma2ForgeConfig config;

    public void Initialize(Carma2ForgeConfig config) {
      this.config = config;
    }

    public ActFile LoadAct(TwtFileEntry twtFileEntry) {
      MemoryStream stream = new MemoryStream(twtFileEntry.data);
      ActFile act = new ActFile();
      Stack<object> stack = new Stack<object>();

      using (BinaryReader br = new BinaryReader(stream)) {
        ValidateActHeader(br);

        while (br.BaseStream.Position < br.BaseStream.Length) {
          ActChunkType chunkType = (ActChunkType)br.ReadUInt32BE();
          int length = (int)br.ReadUInt32BE();

          switch (chunkType) {
            case ActChunkType.Actor: {
              ActFileActor actor = new ActFileActor();
              actor.type = (ActorType)br.ReadByte();
              actor.renderStyle = (RenderStyle)br.ReadByte();
              actor.identifier = br.ReadNullTerminatedString();
              if (actor.identifier == string.Empty) {
                actor.identifier = actor.type.ToString();
              }
              actor.transform = Matrix3D.Identity;
              stack.Push(actor);
              break;
            }

            case ActChunkType.Model:
              ((ActFileActor)stack.Peek()).model = br.ReadNullTerminatedString();
              break;

            case ActChunkType.Material:
              ((ActFileActor)stack.Peek()).material = br.ReadNullTerminatedString();
              break;

            case ActChunkType.Matrix:
              stack.Push(new Matrix3D(
                br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(),
                br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(),
                br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE()
              ));
              break;

            case ActChunkType.Transform: {
              Matrix3D transform = (Matrix3D)stack.Pop();
              ((ActFileActor)stack.Peek()).transform = transform;
              break;
            }

            case ActChunkType.BoundingBox:
              stack.Push(new MeshExtents(
                new Vector3(br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE()),
                new Vector3(br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE())
              ));
              break;

            case ActChunkType.Bounds: {
              MeshExtents bounds = (MeshExtents)stack.Pop();
              ((ActFileActor)stack.Peek()).bounds = bounds;
              break;
            }

            case ActChunkType.LightOld:
              stack.Push(new Light {
                type = (LightType)br.ReadByte(),
                color = Color.FromArgb(br.ReadByte(), br.ReadByte(), br.ReadByte()),
                attenuation = new Vector3(br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE()),
                coneInner = br.ReadUInt16BE(),
                coneOuter = br.ReadUInt16BE()
              });
              break;

            case ActChunkType.Light: {
              Light light = (Light)stack.Pop();
              ((ActFileActor)stack.Peek()).light = light;
              break;
            }

            case ActChunkType.Camera:
              stack.Push(new Camera {
                type = (CameraType)br.ReadByte(),
                fov = br.ReadUInt16BE(),
                hitherZ = br.ReadSingleBE(),
                yonZ = br.ReadSingleBE(),
                aspect = br.ReadSingleBE()
              });
              break;

            case ActChunkType.CameraOld: {
              Camera camera = (Camera)stack.Pop();
              ((ActFileActor)stack.Peek()).camera = camera;
              break;
            }

            case ActChunkType.AddChild: {
              ActFileActor child = (ActFileActor)stack.Pop();
              ActFileActor parent = (ActFileActor)stack.Peek();
              child.parent = parent;
              parent.children.Add(child);
              break;
            }

            case ActChunkType.EOF:
              if (stack.Count > 0 && stack.Peek() is ActFileActor root) {
                stack.Pop();
                act.roots.Add(root);
              }
              break;

            default:
              throw new Exception($"Unsupported chunk type {chunkType} in ACT file");
          }
        }
      }

      return act;
    }

    private void ValidateActHeader(BinaryReader reader) {
      for (int i = 0; i < actHeader.Length; i++) {
        uint value = reader.ReadUInt32BE();
        if (actHeader[i] != value) {
          throw new Exception($"Unexpected value {value} where {actHeader[i]} was expected");
        }
      }
    }
  }
}

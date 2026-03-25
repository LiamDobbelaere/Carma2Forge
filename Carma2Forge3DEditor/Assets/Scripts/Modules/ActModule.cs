using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

public class ActFileActor {
  public ActorType type;
  public RenderStyle renderStyle;
  public string identifier;
  public string model;
  public string material;
  public Matrix3D transform;
  public MeshExtents? bounds;
  public ActLight light;
  public ActCamera camera;
  public ActFileActor parent;
  public List<ActFileActor> children = new List<ActFileActor>();
}

public class ActFile {
  public List<ActFileActor> roots = new List<ActFileActor>();
}

public enum ActCameraType {
}

public class ActCamera {
  public ActCameraType type;
  public ushort fov;
  public float hitherZ;
  public float yonZ;
  public float aspect;
}

public enum ActLightType {
}

public class ActLight {
  public ActLightType type;
  public byte r, g, b;
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
  public float M11, M12, M13;
  public float M21, M22, M23;
  public float M31, M32, M33;
  public float M41, M42, M43;

  public static Matrix3D Identity => new Matrix3D(1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0);

  public Matrix3D(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33, float m41, float m42, float m43) {
    M11 = m11; M12 = m12; M13 = m13;
    M21 = m21; M22 = m22; M23 = m23;
    M31 = m31; M32 = m32; M33 = m33;
    M41 = m41; M42 = m42; M43 = m43;
  }
}

public struct MeshExtents {
  public Vector3 Min;
  public Vector3 Max;

  public MeshExtents(Vector3 min, Vector3 max) {
    Min = min;
    Max = max;
  }
}

public class ActModule {
  private static UInt32[] actHeader = { 0x12, 0x8, 0x1, 0x2 };

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
            stack.Push(new ActLight {
              type = (ActLightType)br.ReadByte(),
              r = br.ReadByte(),
              g = br.ReadByte(),
              b = br.ReadByte(),
              attenuation = new Vector3(br.ReadSingleBE(), br.ReadSingleBE(), br.ReadSingleBE()),
              coneInner = br.ReadUInt16BE(),
              coneOuter = br.ReadUInt16BE()
            });
            break;

          case ActChunkType.Light: {
            ActLight light = (ActLight)stack.Pop();
            ((ActFileActor)stack.Peek()).light = light;
            break;
          }

          case ActChunkType.Camera:
            stack.Push(new ActCamera {
              type = (ActCameraType)br.ReadByte(),
              fov = br.ReadUInt16BE(),
              hitherZ = br.ReadSingleBE(),
              yonZ = br.ReadSingleBE(),
              aspect = br.ReadSingleBE()
            });
            break;

          case ActChunkType.CameraOld: {
            ActCamera camera = (ActCamera)stack.Pop();
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

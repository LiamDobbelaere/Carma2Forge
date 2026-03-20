using System.Drawing;
using System.Globalization;
using System.Numerics;

namespace Carma2ForgeLib.Utilities {
  public class TxtEnumerator : IDisposable {
    private bool hasPeeked;
    private string peekedValue;

    private readonly IEnumerator<string> source;

    public TxtEnumerator(IEnumerable<string> source) {
      this.source = source.GetEnumerator();
    }

    public void Dispose() {
      source.Dispose();
    }

    public string Peek() {
      if (!hasPeeked) {
        if (!source.MoveNext())
          throw new InvalidOperationException("No more elements");

        peekedValue = source.Current;
        hasPeeked = true;
      }

      return peekedValue;
    }

    public string Next() {
      if (hasPeeked) {
        hasPeeked = false;
        return peekedValue;
      }

      if (!source.MoveNext())
        throw new InvalidOperationException("No more elements");

      return source.Current;
    }

    public int[] NextIntArray() {
      Next();
      return AsIntArray();
    }

    public int[] AsIntArray() {
      string[] nextLineSplit = source.Current.Split(',');
      int[] ints = new int[nextLineSplit.Length];
      for (int i = 0; i < nextLineSplit.Length; i++) {
        ints[i] = int.Parse(nextLineSplit[i]);
      }
      return ints;
    }

    public float[] NextFloatArray() {
      Next();
      return AsFloatArray();
    }

    public float[] AsFloatArray() {
      string[] nextLineSplit = source.Current.Split(',');
      float[] floats = new float[nextLineSplit.Length];
      for (int i = 0; i < nextLineSplit.Length; i++) {
        floats[i] = float.Parse(nextLineSplit[i], CultureInfo.InvariantCulture);
      }
      return floats;
    }

    public byte[] NextByteArray() {
      Next();
      return AsByteArray();
    }

    public byte[] AsByteArray() {
      string[] nextLineSplit = source.Current.Split(',');
      byte[] bytes = new byte[nextLineSplit.Length];
      for (int i = 0; i < nextLineSplit.Length; i++) {
        bytes[i] = byte.Parse(nextLineSplit[i]);
      }
      return bytes;
    }
    public Color NextColorRGB() {
      Next();
      return AsColorRGB();
    }

    public Color AsColorRGB() {
      byte[] bytes = AsByteArray();
      if (bytes.Length != 3) {
        throw new FormatException("Invalid color format - too colorful");
      }

      return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
    }


    public Color NextColorARGB() {
      Next();
      return AsColorARGB();
    }

    public Color AsColorARGB() {
      byte[] bytes = AsByteArray();
      switch (bytes.Length) {
        case 3:
          return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        case 4:
          return Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
      }
      throw new FormatException("Invalid color format");
    }

    public Color NextColorRGBA() {
      Next();
      return AsColorRGBA();
    }

    public Color AsColorRGBA() {
      byte[] bytes = AsByteArray();
      switch (bytes.Length) {
        case 3:
          return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        case 4:
          return Color.FromArgb(bytes[3], bytes[0], bytes[1], bytes[2]);
      }
      throw new FormatException("Invalid color format");
    }

    public Vector2 NextVector2() {
      Next();
      return AsVector2();
    }

    public Vector2 AsVector2() {
      float[] floats = AsFloatArray();
      if (floats.Length != 2)
        throw new FormatException("Invalid vector format");
      return new Vector2(floats[0], floats[1]);
    }

    public Vector3 NextVector3() {
      Next();
      return AsVector3();
    }

    public Vector3 AsVector3() {
      float[] floats = AsFloatArray();
      if (floats.Length != 3)
        throw new FormatException("Invalid vector format");
      return new Vector3(floats[0], floats[1], floats[2]);
    }

    public Vector4 NextVector4() {
      Next();
      return AsVector4();
    }

    public Vector4 AsVector4() {
      float[] floats = AsFloatArray();
      if (floats.Length != 4)
        throw new FormatException("Invalid vector format");
      return new Vector4(floats[0], floats[1], floats[2], floats[3]);
    }

    public int NextInt() {
      Next();
      return AsInt();
    }

    public int AsInt() {
      string nextLine = source.Current;
      return int.Parse(nextLine);
    }

    public string AsString() {
      return source.Current;
    }

    public float NextFloat() {
      Next();
      return AsFloat();
    }

    public float AsFloat() {
      string nextLine = source.Current;
      return float.Parse(nextLine, CultureInfo.InvariantCulture);
    }

    public bool HasNext() {
      if (hasPeeked)
        return true;

      if (source.MoveNext()) {
        peekedValue = source.Current;
        hasPeeked = true;
        return true;
      }

      return false;
    }
  }
}

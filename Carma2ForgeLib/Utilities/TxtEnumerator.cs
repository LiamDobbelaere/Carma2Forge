using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
      string[] nextLineSplit = Next().Split(',');

      int[] ints = new int[nextLineSplit.Length];
      for (int i = 0; i < nextLineSplit.Length; i++) {
        ints[i] = int.Parse(nextLineSplit[i]);
      }

      return ints;
    }

    public float[] NextFloatArray() {
      string[] nextLineSplit = Next().Split(',');

      float[] floats = new float[nextLineSplit.Length];
      for (int i = 0; i < nextLineSplit.Length; i++) {
        floats[i] = float.Parse(nextLineSplit[i]);
      }

      return floats;
    }

    public byte[] NextByteArray() {
      string[] nextLineSplit = Next().Split(',');

      byte[] bytes = new byte[nextLineSplit.Length];
      for (int i = 0; i < nextLineSplit.Length; i++) {
        bytes[i] = byte.Parse(nextLineSplit[i]);
      }

      return bytes;
    }

    public Color NextColorARGB() {
      byte[] bytes = NextByteArray();

      switch (bytes.Length) {
        case 3:
          return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        case 4:
          return Color.FromArgb(bytes[0], bytes[1], bytes[2], bytes[3]);
      }

      throw new FormatException("Invalid color format");
    }

    public Color NextColorRGBA() {
      byte[] bytes = NextByteArray();

      switch (bytes.Length) {
        case 3:
          return Color.FromArgb(bytes[0], bytes[1], bytes[2]);
        case 4:
          return Color.FromArgb(bytes[3], bytes[0], bytes[1], bytes[2]);
      }

      throw new FormatException("Invalid color format");
    }

    public Vector2 NextVector2() {
      float[] floats = NextFloatArray();
      if (floats.Length != 2)
        throw new FormatException("Invalid vector format");
      
      return new Vector2(floats[0], floats[1]);
    }

    public Vector3 NextVector3() {
      float[] floats = NextFloatArray();
      if (floats.Length != 3)
        throw new FormatException("Invalid vector format");

      return new Vector3(floats[0], floats[1], floats[2]);
    }

    public Vector4 NextVector4() {
      float[] floats = NextFloatArray();
      if (floats.Length != 4)
        throw new FormatException("Invalid vector format");
      return new Vector4(floats[0], floats[1], floats[2], floats[3]);
    }

    public int NextInt() {
      string nextLine = Next();
      return int.Parse(nextLine);
    }

    public float NextFloat() {
      string nextLine = Next();
      return float.Parse(nextLine);
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

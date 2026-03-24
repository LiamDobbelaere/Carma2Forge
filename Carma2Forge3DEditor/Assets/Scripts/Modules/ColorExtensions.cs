using UnityEngine;

public static class ColorExtensions {
    public static Color32 FromR8G8B8(int i) {
      byte r = (byte)((i & 0xFF0000) >> 16);
      byte g = (byte)((i & 0xFF00) >> 8);
      byte b = (byte)(i & 0xFF);

      return new Color32(r, g, b, 0);
    }

    public static Color32 FromR5G6B5(int i) {
      byte r = (byte)(((i & 0xF800) >> 11) << 3);
      byte g = (byte)(((i & 0x7E0) >> 5) << 2);
      byte b = (byte)((i & 0x1F) << 3);

      return new Color32(r, g, b, 255);
    }

    public static Color32 FromA4R4G4B4(int i) {
      byte a = (byte)(((i & 0xF000) >> 12) * 17);
      byte r = (byte)(((i & 0xF00) >> 8) * 17);
      byte g = (byte)(((i & 0xF0) >> 4) * 17);
      byte b = (byte)((i & 0xF) * 17);

      return new Color32(r, g, b, a);
    }
  }

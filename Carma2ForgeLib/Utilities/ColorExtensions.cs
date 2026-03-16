using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carma2ForgeLib.Utilities {
  public static class ColorExtensions {
    public static Color FromR8G8B8(int i) {
      int a = 0;
      int r = (i & 0xFF0000) >> 16;
      int g = (i & 0xFF00) >> 8;
      int b = (i & 0xFF);

      return Color.FromArgb(a, r, g, b);
    }

    public static Color FromR5G6B5(int i) {
      int r = (i & 0xF800) << 8;
      int g = (i & 0x7E0) << 5;
      int b = (i & 0x1F) << 3;

      return Color.FromArgb(r | g | b);
    }

    public static Color FromA4R4G4B4(int i) {
      int a = (i & 0xF000) >> 12;
      int r = (i & 0xF00) >> 8;
      int g = (i & 0xF0) >> 4;
      int b = (i & 0xF);

      return Color.FromArgb(a * 17, r * 17, g * 17, b * 17);
    }
  }
}

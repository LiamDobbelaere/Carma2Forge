using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carma2ForgeLib.Enums;

namespace Carma2ForgeLib.Modules.FontModule {
  public class FontColEntry {
    public string name;
    public byte id;
    public Color topLeft;
    public Color topRight;
    public Color bottomLeft;
    public Color bottomRight;
  }

  public class FontModule : BaseModule {
    private Carma2ForgeConfig config;
    private List<FontColEntry> fontColDefs = new List<FontColEntry>();

    public void Initialize(Carma2ForgeConfig config) {
      this.config = config;

      LoadFontColEntries();
    }

    public FontColEntry[] GetFontColEntries() {
      return fontColDefs.ToArray();
    }

    public void LoadFontColEntries() {
      fontColDefs.Clear();
      using IEnumerator<string> fontColLines = config.ReadTxt(Carma2File.FontCol).GetEnumerator();

      while (fontColLines.MoveNext()) {
        string line = fontColLines.Current;

        if (line.StartsWith("//#define")) {
          string[] definitionParts = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
          string name = definitionParts[1].Trim();
          byte id = byte.Parse(definitionParts[2].Trim());

          fontColLines.MoveNext();
          string[] topLeftRgb = fontColLines.Current.Split(',');
          Color topLeft = Color.FromArgb(
            byte.Parse(topLeftRgb[0]),
            byte.Parse(topLeftRgb[1]),
            byte.Parse(topLeftRgb[2])
          );

          fontColLines.MoveNext();
          string[] topRightRgb = fontColLines.Current.Split(',');
          Color topRight = Color.FromArgb(
            byte.Parse(topRightRgb[0]),
            byte.Parse(topRightRgb[1]),
            byte.Parse(topRightRgb[2])
          );

          fontColLines.MoveNext();
          string[] bottomLeftRgb = fontColLines.Current.Split(',');
          Color bottomLeft = Color.FromArgb(
            byte.Parse(bottomLeftRgb[0]),
            byte.Parse(bottomLeftRgb[1]),
            byte.Parse(bottomLeftRgb[2])
          );

          fontColLines.MoveNext();
          string[] bottomRightRgb = fontColLines.Current.Split(',');
          Color bottomRight = Color.FromArgb(
            byte.Parse(bottomRightRgb[0]),
            byte.Parse(bottomRightRgb[1]),
            byte.Parse(bottomRightRgb[2])
          );

          FontColEntry newFontColDef = new FontColEntry {
            name = name,
            id = id,
            topLeft = topLeft,
            topRight = topRight,
            bottomLeft = bottomLeft,
            bottomRight = bottomRight
          };

          fontColDefs.Add(newFontColDef);
        }
      }
    }

    public void SaveFontColEntries() {
      List<string> linesToWrite = new List<string>();
      foreach (FontColEntry entry in fontColDefs) {
        linesToWrite.Add($"//#define {entry.name} {entry.id}");
        linesToWrite.Add($"{entry.topLeft.R},{entry.topLeft.G},{entry.topLeft.B}");
        linesToWrite.Add($"{entry.topRight.R},{entry.topRight.G},{entry.topRight.B}");
        linesToWrite.Add($"{entry.bottomLeft.R},{entry.bottomLeft.G},{entry.bottomLeft.B}");
        linesToWrite.Add($"{entry.bottomRight.R},{entry.bottomRight.G},{entry.bottomRight.B}");
        linesToWrite.Add(string.Empty);
      }

      config.WriteTxt(Carma2File.FontCol, linesToWrite);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carma2ForgeLib.Enums;
using Carma2ForgeLib.Modules.TwtModule;
using Carma2ForgeLib.Utilities;

namespace Carma2ForgeLib.Modules.PixiesModule {
  public class PixiesModule : BaseModule {
    public enum PixBlockType {
      C2 = 0x3D,
      C1 = 0x03,
      ImageData = 0x21,
      OriginalPix = 0x22,
      Mastro = 0x12,
      EOF = 0x00
    }

    public enum PixSubType {
      None = 0x0,
      Bit8 = 0x3,
      Bit16 = 0x5,
      Bit24 = 0x6,
      Bit16Alpha = 0x12,
      ColorPalette = 0x7
    }


    public class PixiesFileEntry {
      public string filename;
      public Bitmap bitmap;
    }

    public class PixiesFile {
      public required PixiesFileEntry[] entries;

      public PixiesFileEntry GetFile(string filename) {
        foreach (PixiesFileEntry entry in entries) {
          if (entry.filename.ToLower() == filename.ToLower()) {
            return entry;
          }
        }

        throw new Exception($"No entry with filename {filename} found in pixies file");
      }
    }


    static readonly int[] pixHeader = { 0x12, 0x8, 0x2, 0x2 };

    private Carma2ForgeConfig config;
    private Color[] colorPalette;

    public void Initialize(Carma2ForgeConfig config) {
      int[] palette = { 16711935, 6095619, 9634306, 13172993, 16711680, 16728128, 16744319, 16760767, 4002056, 7214854, 10362372, 13575170, 16722432, 16736321, 16684930, 16698819, 4332553, 7416327, 10565893, 13649666, 16733440, 16743482, 16687732, 16697774, 5911309, 8603146, 11360519, 14052355, 16744192, 16752189, 16694395, 16702392, 4862728, 7819270, 10841604, 13798146, 16754688, 16760384, 16766081, 16771777, 4865801, 7824647, 10848773, 13807618, 16766208, 16703299, 16640645, 16577736, 5394954, 8224008, 11118853, 13947907, 16776960, 16711244, 16711319, 16645603, 4608526, 6978827, 9349383, 11654148, 14024448, 14745158, 15400331, 16121041, 2966528, 5010176, 7053824, 9031680, 11075328, 12648263, 14220941, 15793876, 5131591, 7367765, 9538404, 11774322, 13944960, 14669212, 15328184, 16052436, 1591296, 2586112, 3580672, 4575488, 5570048, 8257085, 10943867, 13630904, 402944, 2640929, 4813377, 7051362, 9223810, 10736538, 12314802, 13827530, 16917, 28965, 41269, 53316, 65364, 4259711, 8388265, 12582612, 17185, 1925439, 3899228, 5807226, 7715479, 9554605, 11393732, 13232858, 15143, 27719, 40296, 52872, 65448, 4194238, 8323027, 12451817, 10016, 934457, 1793362, 2717802, 3576707, 5810330, 8109490, 10343113, 13878, 26728, 39835, 52685, 65535, 4194046, 8388350, 12516861, 10031, 21347, 32407, 43723, 54783, 4186110, 8382974, 12514301, 9786, 18283, 26525, 35022, 43263, 3980799, 7983615, 11921151, 6712, 12906, 18844, 25037, 30975, 3248895, 6532607, 9750527, 60, 109, 158, 206, 255, 3816191, 7566335, 11382271, 8072704, 9652509, 11232571, 12746840, 14326645, 15120283, 15979712, 16773350, 2230272, 4988432, 7746848, 10439472, 13197632, 13270115, 13342341, 13414824, 5321499, 6503453, 7751199, 8933153, 10115107, 12282666, 14515505, 16683064, 6176801, 7620633, 9064466, 10442506, 11886338, 13468725, 15116904, 16699291, 3745551, 5061411, 6443064, 7758924, 9074784, 10588282, 12102036, 13615534, 2827800, 3946022, 5064244, 6182465, 7300687, 9537649, 11708818, 13945780, 5423, 1189439, 2438991, 3623006, 4807022, 6911883, 8951463, 11056324, 1315860, 2171169, 3026478, 3947324, 4802889, 5658198, 6513507, 7368816, 8289662, 9144971, 10000280, 10855589, 11711154, 12632000, 13487309, 14342618, 0, 1119273, 2237752, 3355976, 4474455, 5592934, 6711413, 7829893, 8948116, 10066595, 11185074, 12303553, 13421777, 14540256, 15658735, 16777215 };
      
      this.colorPalette = new Color[256];
      for (int i = 0; i < this.colorPalette.Length; i++) {
        this.colorPalette[i] = ColorExtensions.FromR8G8B8(palette[i]);
      }

      this.config = config;
    }

    public PixiesFile ReadPixies(TwtFileEntry entry) {
      BinaryReader pixReader = new BinaryReader(new MemoryStream(entry.data));
      if (!IsValidPixHeader(pixReader)) {
        throw new Exception("Invalid Pixies file header");
      }

      List<PixiesFileEntry> pixiesFileEntries = new List<PixiesFileEntry>();

      // state kept while reading the file
      bool c1 = false;
      PixSubType currentSubType = PixSubType.None;
      int currentRowSize = 0;
      int currentWidth = 0;
      int currentHeight = 0;
      string currentFileName = "";

      while (pixReader.BaseStream.Position < pixReader.BaseStream.Length) {
        if (pixReader.BaseStream.Position + 8 > pixReader.BaseStream.Length) {
          throw new Exception("Unexpected end of Pixies file while reading image header");
        }

        PixBlockType blockType = (PixBlockType)pixReader.ReadInt32BE();
        int blockSize = pixReader.ReadInt32BE();

        switch (blockType) {
          case PixBlockType.C2:
            currentSubType = (PixSubType)pixReader.ReadByte();
            currentRowSize = pixReader.ReadUInt16BE();
            currentWidth = pixReader.ReadUInt16BE();
            currentHeight = pixReader.ReadUInt16BE();

            for (int i = 0; i < 6; i++) { 
              if (pixReader.ReadByte() != 0) {
                throw new Exception("Expected 6 null bytes after C2 block header");
              } 
            }

            currentFileName = pixReader.ReadNullTerminatedString();

            break;
          case PixBlockType.C1:
            throw new Exception("C1 blocks are not supported");
          case PixBlockType.ImageData:
            int pixelCount = pixReader.ReadInt32BE();
            bool useRowSize = false;
            if (pixelCount != (currentWidth * currentHeight) || currentRowSize > currentWidth) {
              useRowSize = true;
            }
            int pixelSize = pixReader.ReadInt32BE();

            if (useRowSize) {
              currentRowSize /= pixelSize;
            }

            if ((c1 || currentSubType == PixSubType.ColorPalette)) {
              throw new Exception("Image data blocks with C1 or ColorPalette subtype are not supported");
            }

            Bitmap bmp = new Bitmap(currentWidth, currentHeight, ToPixelFormat(currentSubType));

            for (int y = 0; y < currentHeight; y++) {
              for (int x = 0; x < currentRowSize; x++) {
                Color c = ReadColor(pixReader, currentSubType);

                if (x < currentWidth) {
                  bmp.SetPixel(x, y, c);
                }
              }
            }

            pixiesFileEntries.Add(new PixiesFileEntry {
              filename = currentFileName,
              bitmap = bmp
            });

            break;
          case PixBlockType.OriginalPix:
            break;
          case PixBlockType.Mastro:
            // Mastro malformed pix
            pixReader.ReadBytes(8);
            break;
          case PixBlockType.EOF:
            break;
          default:
            throw new Exception($"Unknown Pixies block type {blockType} at {pixReader.BaseStream.Position}");
        }
      }

      return new PixiesFile {
        entries = pixiesFileEntries.ToArray()
      };
    }

    private Color ReadColor(BinaryReader pixReader, PixSubType subType) {
      switch (subType) {
        case PixSubType.Bit8:
          return colorPalette[pixReader.ReadByte()];
        case PixSubType.Bit24:
          return Color.FromArgb(0, pixReader.ReadByte(), pixReader.ReadByte(), pixReader.ReadByte());
        case PixSubType.ColorPalette:
          return Color.FromArgb(pixReader.ReadByte(), pixReader.ReadByte(), pixReader.ReadByte(), pixReader.ReadByte());
        case PixSubType.Bit16:
          return ColorExtensions.FromR5G6B5(pixReader.ReadUInt16BE());
        case PixSubType.Bit16Alpha:
          return ColorExtensions.FromA4R4G4B4(pixReader.ReadUInt16BE());
        default:
          throw new Exception($"Unsupported Pixies pixel format {subType}");
      }
    }

    private PixelFormat ToPixelFormat(PixSubType subType) {
      switch (subType) {
        //case PixSubType.Bit8:
        //  return PixelFormat.Format8bppIndexed;
        case PixSubType.Bit16Alpha:
          return PixelFormat.Format32bppArgb;
        default:
          return PixelFormat.Format16bppRgb565;
      }
    }

    private bool IsValidPixHeader(BinaryReader br) {
      for (int i = 0; i < pixHeader.Length; i++) {
        int v = br.ReadInt32BE();
        if (v != pixHeader[i]) {
          return false;
        }
      }

      return true;
    }
  }
}

using System.Text;

namespace Carma2ForgeLib.Modules.TwtModule {
  public class TwtFileEntry {
    public required string filename;
    public required byte[] data;
  }

  public class TwtFile {
    public required TwtFileEntry[] entries;

    public TwtFileEntry GetFile(string filename) {
      foreach (TwtFileEntry entry in entries) {
        if (entry.filename.ToLower() == filename.ToLower()) {
          return entry;
        }
      }

      throw new Exception($"No entry with filename {filename} found in TWT file");
    }
  }

  public class TwtModule : BaseModule {
    private Carma2ForgeConfig config;

    public void Initialize(Carma2ForgeConfig config) {
      this.config = config;
    }

    public TwtFile LoadTwt(string relativePath) {
      // Taken from https://github.com/MaxxWyndham/Trixx/blob/master/TRixx/Program.cs
      BinaryReader twtReader = config.ReadBinary(relativePath);

      int totalSize = twtReader.ReadInt32();
      int totalFiles = twtReader.ReadInt32();

      int[] fileSizes = new int[totalFiles];
      string[] fileNames = new string[totalFiles];

      for (int i = 0; i < totalFiles; i++) {
        fileSizes[i] = twtReader.ReadInt32();
        fileNames[i] = ToFileName(twtReader.ReadBytes(52));
      }

      TwtFileEntry[] twtFileEntries = new TwtFileEntry[totalFiles];

      for (int i = 0; i < totalFiles; i++) {
        byte[] fileData = twtReader.ReadBytes(fileSizes[i]);

        twtFileEntries[i] = new TwtFileEntry {
          filename = fileNames[i],
          data = fileData
        };

        // blocks are padded to nearest multiple of 4
        int paddingBytes = (4 - (fileSizes[i] % 4)) % 4;
        if (paddingBytes > 0) {
          twtReader.ReadBytes(paddingBytes);
        }
      }

      return new TwtFile {
        entries = twtFileEntries
      };
    }

    private string ToFileName(byte[] bytes) {
      StringBuilder sb = new StringBuilder();

      foreach (byte b in bytes) {
        if (b == 0) {
          break;
        }
        sb.Append((char)b);
      }

      return sb.ToString();
    }
  }
}

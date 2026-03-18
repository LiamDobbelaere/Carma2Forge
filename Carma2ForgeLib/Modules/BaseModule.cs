using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carma2ForgeLib.Enums;
using Carma2ForgeLib.Modules.TwtModule;

namespace Carma2ForgeLib.Modules {
  public class Carma2ForgeConfig {
    public required string Carma2Path { get; set; }
    public required string DataPath { get; set; }

    public IEnumerable<string> ReadTxt(TwtFileEntry entry) {
      IEnumerable<string> fileLinesEnumerable = Encoding.UTF8.GetString(entry.data).Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
      
      return ReadTxt(fileLinesEnumerable);
    }

    public IEnumerable<string> ReadTxt(string relativePath) {
      IEnumerable <string> fileLinesEnumerable = File.ReadLines(Path.Combine(Carma2Path, DataPath, relativePath));

      return ReadTxt(fileLinesEnumerable);
    }

    public IEnumerable<string> ReadTxt(IEnumerable<string> fileLinesEnumerable) {
      using IEnumerator<string> fileLines = fileLinesEnumerable.GetEnumerator();

      while (fileLines.MoveNext()) {
        string trimmedLine = fileLines.Current.Trim();
        if (trimmedLine == string.Empty || (trimmedLine.StartsWith("//") && !trimmedLine.StartsWith("//#define"))) {
          continue;
        }

        // remove everything after //
        if (!trimmedLine.StartsWith("//#define") && trimmedLine.Contains("//")) {
          trimmedLine = trimmedLine.Split(new string[] { "//" }, StringSplitOptions.None)[0].Trim();
        }

        yield return trimmedLine;
      }
    }

    public BinaryReader ReadBinary(string relativePath) {
      return new BinaryReader(File.OpenRead(Path.Combine(Carma2Path, DataPath, relativePath)));
    }

    public void WriteTxt(string relativePath, IEnumerable<string> lines) {
      File.WriteAllLines(Path.Combine(Carma2Path, DataPath, relativePath), lines);
    }
  }

  public interface BaseModule {
    void Initialize(Carma2ForgeConfig config);
  }
}

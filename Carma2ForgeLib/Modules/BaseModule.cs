using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carma2ForgeLib.Modules {
  public class Carma2ForgeConfig {
    public required string Carma2Path { get; set; }
    public required string DataPath { get; set; }

    public IEnumerable<string> ReadTxt(string relativePath) {
      return File.ReadLines(Path.Combine(Carma2Path, DataPath, relativePath));
    }

    public void WriteTxt(string relativePath, IEnumerable<string> lines) {
      File.WriteAllLines(Path.Combine(Carma2Path, DataPath, relativePath), lines);
    }
  }

  public interface BaseModule {
    void Initialize(Carma2ForgeConfig config);
  }
}

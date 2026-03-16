using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carma2ForgeLib.Enums;

namespace Carma2ForgeLib.Modules.OpponentModule {
  public enum NetworkAvailability {
    Eagle,
    All
  }

  public class OpponentEntry {
    public int id;
    public required string longName;
    public required string shortName;
    public required string carName;
    public sbyte strengthRating; // 1-5 or -1 for eagle
    public uint cost;
    public NetworkAvailability networkAvailability;
    public required string vehicleFileName;
    public required string topSpeed;
    public required string weight;
    public required string zeroToSixty;
    public required string description;

    public string CanonicalName {
      get {
        return vehicleFileName.Split('.')[0].ToLower();
      }
    }

    public string CarImagePath {
      get {
        return $"INTRFACE/CarImage/{CanonicalName}CI.TWT";
      }
    }
  }

  public class OpponentModule : BaseModule {
    private Carma2ForgeConfig config;
    private List<OpponentEntry> opponentEntries = new List<OpponentEntry>();

    public void Initialize(Carma2ForgeConfig config) {
      this.config = config;

      LoadOpponentEntries();
    }

    public OpponentEntry[] GetOpponentEntries() {
      return opponentEntries.ToArray();
    }

    public void LoadOpponentEntries() {
      opponentEntries.Clear();
      using IEnumerator<string> opponentEntryLines = config.ReadTxt(Carma2File.Opponent).GetEnumerator();

      bool firstLine = true;
      while (opponentEntryLines.MoveNext()) {
        string line = opponentEntryLines.Current;

        // skip opponent count
        if (firstLine) {
          firstLine = false;
          continue;
        }

        if (line == "END") {
          continue;
        }

        int id = opponentEntries.Count;
        string longName = opponentEntryLines.Current;
        opponentEntryLines.MoveNext();
        string shortName = opponentEntryLines.Current;
        opponentEntryLines.MoveNext();
        string carName = opponentEntryLines.Current;
        opponentEntryLines.MoveNext();
        sbyte strengthRating = sbyte.Parse(opponentEntryLines.Current);
        opponentEntryLines.MoveNext();
        uint cost = uint.Parse(opponentEntryLines.Current);
        opponentEntryLines.MoveNext();
        NetworkAvailability networkAvailability = opponentEntryLines.Current == "eagle" ? NetworkAvailability.Eagle : NetworkAvailability.All;
        opponentEntryLines.MoveNext();
        string vehicleFileName = opponentEntryLines.Current;
        opponentEntryLines.MoveNext();
        string topSpeed = opponentEntryLines.Current;
        opponentEntryLines.MoveNext();
        string weight = opponentEntryLines.Current;
        opponentEntryLines.MoveNext();
        string zeroToSixty = opponentEntryLines.Current;
        opponentEntryLines.MoveNext();
        string description = opponentEntryLines.Current;

        opponentEntries.Add(new OpponentEntry {
          id = id,
          longName = longName,
          shortName = shortName,
          carName = carName,
          strengthRating = strengthRating,
          cost = cost,
          networkAvailability = networkAvailability,
          vehicleFileName = vehicleFileName,
          topSpeed = topSpeed,
          weight = weight,
          zeroToSixty = zeroToSixty,
          description = description
        });
      }
    }

    public void SaveOpponentEntries() {
      /*List<string> linesToWrite = new List<string>();
      foreach (FontColEntry entry in fontColDefs) {
        linesToWrite.Add($"//#define {entry.name} {entry.id}");
        linesToWrite.Add($"{entry.topLeft.R},{entry.topLeft.G},{entry.topLeft.B}");
        linesToWrite.Add($"{entry.topRight.R},{entry.topRight.G},{entry.topRight.B}");
        linesToWrite.Add($"{entry.bottomLeft.R},{entry.bottomLeft.G},{entry.bottomLeft.B}");
        linesToWrite.Add($"{entry.bottomRight.R},{entry.bottomRight.G},{entry.bottomRight.B}");
        linesToWrite.Add(string.Empty);
      }

      config.WriteTxt(Carma2File.FontCol, linesToWrite);*/
    }
  }
}

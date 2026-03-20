using System.Globalization;
using Carma2ForgeLib.Enums;

namespace Carma2ForgeLib.Modules.RaceModule {
  internal enum RacesFileBlockType {
    FirstRaceDev,
    DefaultParams,
    Race
  }

  public enum RaceType {
    Carma1,
    Cars,
    Peds,
    Checkpoints,
    Smash,
    SmashAndPeds
  }

  public class RaceEntry {
    public int group;
    public required string name;
    public required string fileName;
    public required string interfaceElement;
    public int opponentCount;
    public required int[] explicitOpponents;
    public int opponentNastiness;
    public required int[] powerupExclusions;
    public bool disableTimeAwards;
    public bool boundaryRace;
    public RaceType raceType;
    public required int[] initialTimerCounts;
    public int numberOfOpponentsThatMustBeKilled;
    public required int[] opponentsThatMustBeKilled;
    public int numberOfPedGroups;
    public required int[] pedGroups;
    public int laps;
    public int smashVariableNumber;
    public int smashVariableTarget;
    public int pedGroupIndexForRequiredKills;
    public required int[] raceCompletionBonus;
    public required int[] raceCompletionBonusPeds;
    public required int[] raceCompletionBonusOpponents;
    public required string raceDescription;
    public bool isExpansion;
  }

  public class RacesFile {
    public int firstRaceDevOnly;
    public RacesDefaultParams firstRaceDefaults = new RacesDefaultParams();
    public RacesDefaultParams lastRaceDefaults = new RacesDefaultParams();
    public RaceEntry[] entries = Array.Empty<RaceEntry>();
  }

  public class RacesDefaultParams {
    public int opponentCount;
    public float opponentSoftness;
    public int opponentMaxRank;
    public float opponentNastiness;
  }

  public class RaceModule : BaseModule {
    private Carma2ForgeConfig config;
    private RacesFile racesFile;

    public void Initialize(Carma2ForgeConfig config) {
      this.config = config;

      LoadRacesFile();
    }

    public RacesFile GetRacesFile() {
      return racesFile;
    }

    public RacesFile LoadRacesFile() {
      using IEnumerator<string> racesTxtLines = config.ReadTxt(Carma2File.Races).GetEnumerator();

      this.racesFile = new RacesFile();
      List<RaceEntry> raceEntries = new List<RaceEntry>();

      RacesFileBlockType currentBlockType = RacesFileBlockType.FirstRaceDev;
      int currentGroup = 1;

      while (racesTxtLines.MoveNext()) {
        string line = racesTxtLines.Current;

        if (line == "END") {
          continue;
        }


        switch (currentBlockType) {
          case RacesFileBlockType.FirstRaceDev:
            racesFile.firstRaceDevOnly = int.Parse(line);
            currentBlockType = RacesFileBlockType.DefaultParams;
            break;
          case RacesFileBlockType.DefaultParams:
            racesFile.firstRaceDefaults.opponentCount = int.Parse(line);
            racesTxtLines.MoveNext();
            racesFile.lastRaceDefaults.opponentCount = int.Parse(racesTxtLines.Current);

            racesTxtLines.MoveNext();
            string[] splitLine = racesTxtLines.Current.Split(',');
            racesFile.firstRaceDefaults.opponentSoftness = float.Parse(splitLine[0], CultureInfo.InvariantCulture);
            racesFile.firstRaceDefaults.opponentMaxRank = int.Parse(splitLine[1]);

            racesTxtLines.MoveNext();
            splitLine = racesTxtLines.Current.Split(',');
            racesFile.lastRaceDefaults.opponentSoftness = float.Parse(splitLine[0], CultureInfo.InvariantCulture);
            racesFile.lastRaceDefaults.opponentMaxRank = int.Parse(splitLine[1]);

            racesTxtLines.MoveNext();
            racesFile.firstRaceDefaults.opponentNastiness = float.Parse(racesTxtLines.Current, CultureInfo.InvariantCulture);
            racesTxtLines.MoveNext();
            racesFile.lastRaceDefaults.opponentNastiness = float.Parse(racesTxtLines.Current, CultureInfo.InvariantCulture);

            currentBlockType = RacesFileBlockType.Race;
            break;
          case RacesFileBlockType.Race:
            string name = line;

            racesTxtLines.MoveNext();
            string fileName = racesTxtLines.Current;

            racesTxtLines.MoveNext();
            string interfaceElement = racesTxtLines.Current;

            racesTxtLines.MoveNext();
            int opponentCount = int.Parse(racesTxtLines.Current);

            racesTxtLines.MoveNext();
            int explicitOpponentCount = int.Parse(racesTxtLines.Current);
            int[] explicitOpponents = Array.Empty<int>();
            if (explicitOpponentCount > 0) {
              explicitOpponents = new int[explicitOpponentCount];
              for (int i = 0; i < opponentCount; i++) {
                racesTxtLines.MoveNext();
                explicitOpponents[i] = int.Parse(racesTxtLines.Current);
              }
            }

            racesTxtLines.MoveNext();
            int opponentNastiness = int.Parse(racesTxtLines.Current);

            racesTxtLines.MoveNext();
            string powerupExclusionsString = racesTxtLines.Current;
            int[] powerupExclusions = Array.Empty<int>();
            if (powerupExclusionsString != "-1") {
              string[] powerupExclusionsSplit = powerupExclusionsString.Split(',');
              powerupExclusions = new int[powerupExclusionsSplit.Length];
              for (int i = 0; i < powerupExclusionsSplit.Length; i++) {
                powerupExclusions[i] = int.Parse(powerupExclusionsSplit[i]);
              }
            }

            racesTxtLines.MoveNext();
            bool disableTimeAwards = racesTxtLines.Current == "1";

            racesTxtLines.MoveNext();
            bool boundaryRace = racesTxtLines.Current == "1";

            racesTxtLines.MoveNext();
            RaceType raceType = (RaceType)Enum.Parse(typeof(RaceType), racesTxtLines.Current);

            racesTxtLines.MoveNext();
            string[] initialTimerCountsString = racesTxtLines.Current.Split(',');
            int[] initialTimerCounts = new int[initialTimerCountsString.Length];
            for (int i = 0; i < initialTimerCountsString.Length; i++) {
              initialTimerCounts[i] = int.Parse(initialTimerCountsString[i]);
            }

            int smashVariableNumber = 0;
            int smashVariableTarget = 0;
            int pedGroupIndexForRequiredKills = 0;
            if (raceType == RaceType.Smash || raceType == RaceType.SmashAndPeds) {
              racesTxtLines.MoveNext();
              smashVariableNumber = int.Parse(racesTxtLines.Current);
              racesTxtLines.MoveNext();
              smashVariableTarget = int.Parse(racesTxtLines.Current);

              if (raceType == RaceType.SmashAndPeds) {
                racesTxtLines.MoveNext();
                pedGroupIndexForRequiredKills = int.Parse(racesTxtLines.Current);
              }
            }

            int numberOfOpponentsThatMustBeKilled = 0;
            int[] opponentsThatMustBeKilled = Array.Empty<int>();
            if (raceType == RaceType.Cars) {
              racesTxtLines.MoveNext();
              numberOfOpponentsThatMustBeKilled = int.Parse(racesTxtLines.Current);

              if (numberOfOpponentsThatMustBeKilled != -1) {
                opponentsThatMustBeKilled = new int[numberOfOpponentsThatMustBeKilled];
                for (int i = 0; i < numberOfOpponentsThatMustBeKilled; i++) {
                  racesTxtLines.MoveNext();
                  opponentsThatMustBeKilled[i] = int.Parse(racesTxtLines.Current);
                }
              }
            }

            int laps = 0;
            if (raceType == RaceType.Checkpoints || raceType == RaceType.Carma1) {
              racesTxtLines.MoveNext();
              laps = int.Parse(racesTxtLines.Current);
            }

            int numberOfPedGroups = -1;
            int[] pedGroups = Array.Empty<int>();
            if (raceType == RaceType.Peds) {
              racesTxtLines.MoveNext();
              numberOfPedGroups = int.Parse(racesTxtLines.Current);
              pedGroups = new int[numberOfPedGroups];
              for (int i = 0; i < numberOfPedGroups; i++) {
                racesTxtLines.MoveNext();
                pedGroups[i] = int.Parse(racesTxtLines.Current);
              }
            }

            racesTxtLines.MoveNext();
            string[] raceCompletionBonusesString = racesTxtLines.Current.Split(',');
            int[] raceCompletionBonuses = new int[raceCompletionBonusesString.Length];
            for (int i = 0; i < raceCompletionBonusesString.Length; i++) {
              raceCompletionBonuses[i] = int.Parse(raceCompletionBonusesString[i]);
            }

            int[] raceCompletionBonusesPeds = Array.Empty<int>();
            int[] raceCompletionBonusesOpponents = Array.Empty<int>();
            if (raceType == RaceType.Carma1) {
              racesTxtLines.MoveNext();
              string[] raceCompletionBonusesPedsString = racesTxtLines.Current.Split(',');
              raceCompletionBonusesPeds = new int[raceCompletionBonusesPedsString.Length];
              for (int i = 0; i < raceCompletionBonusesPedsString.Length; i++) {
                raceCompletionBonusesPeds[i] = int.Parse(raceCompletionBonusesPedsString[i]);
              }

              racesTxtLines.MoveNext();
              string[] raceCompletionBonusesOpponentsString = racesTxtLines.Current.Split(',');
              raceCompletionBonusesOpponents = new int[raceCompletionBonusesOpponentsString.Length];
              for (int i = 0; i < raceCompletionBonusesOpponentsString.Length; i++) {
                raceCompletionBonusesOpponents[i] = int.Parse(raceCompletionBonusesOpponentsString[i]);
              }
            }

            racesTxtLines.MoveNext();
            string description = racesTxtLines.Current;

            racesTxtLines.MoveNext();
            bool isExpansion = racesTxtLines.Current == "1";

            raceEntries.Add(new RaceEntry {
              group = currentGroup,
              name = name,
              fileName = fileName,
              interfaceElement = interfaceElement,
              opponentCount = opponentCount,
              explicitOpponents = explicitOpponents,
              opponentNastiness = opponentNastiness,
              powerupExclusions = powerupExclusions,
              disableTimeAwards = disableTimeAwards,
              boundaryRace = boundaryRace,
              raceType = raceType,
              initialTimerCounts = initialTimerCounts,
              numberOfOpponentsThatMustBeKilled = numberOfOpponentsThatMustBeKilled,
              opponentsThatMustBeKilled = opponentsThatMustBeKilled,
              numberOfPedGroups = numberOfPedGroups,
              pedGroups = pedGroups,
              laps = laps,
              smashVariableNumber = smashVariableNumber,
              smashVariableTarget = smashVariableTarget,
              pedGroupIndexForRequiredKills = pedGroupIndexForRequiredKills,
              raceCompletionBonus = raceCompletionBonuses,
              raceCompletionBonusPeds = raceCompletionBonusesPeds,
              raceCompletionBonusOpponents = raceCompletionBonusesOpponents,
              raceDescription = description,
              isExpansion = isExpansion
            });

            if (boundaryRace) {
              currentGroup++;
            }
            break;
          default:
            throw new Exception($"Unknown RacesFileBlockType: {currentBlockType}");
        }
      }

      racesFile.entries = raceEntries.ToArray();

      return racesFile;
    }

    public void SaveRacesFile() {
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

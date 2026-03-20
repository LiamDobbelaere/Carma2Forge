using Carma2ForgeLib.Modules;
using Carma2ForgeLib.Modules.MapModule;
using Carma2ForgeLib.Modules.PixiesModule;
using Carma2ForgeLib.Modules.RaceModule;
using Carma2ForgeLib.Modules.TwtModule;

namespace Carma2Forge {
  public partial class RaceEditor : Form {
    private RaceModule raceModule = new RaceModule();
    private TwtModule twtModule = new TwtModule();
    private MapModule mapModule = new MapModule();
    private PixiesModule pixiesModule = new PixiesModule();
    private Carma2ForgeConfig config;

    public RaceEditor(Carma2ForgeConfig config) {
      this.config = config;

      raceModule.Initialize(config);
      twtModule.Initialize(config);
      pixiesModule.Initialize(config);
      mapModule.Initialize(config);

      InitializeComponent();
    }

    private void RaceEditor_Load(object sender, EventArgs e) {
      RacesFile racesFile = raceModule.GetRacesFile();
      RaceEntry[] raceEntries = racesFile.entries;

      // get all groups and turn them into lvRaces groups:
      List<string> uniqueGroups = new List<string>();
      foreach (RaceEntry raceEntry in raceEntries) {
        string uniqueGroupName = "Group " + raceEntry.group.ToString();
        if (!uniqueGroups.Contains(uniqueGroupName)) {
          uniqueGroups.Add(uniqueGroupName);
        }
      }

      lvRaces.Groups.Clear();
      foreach (string uniqueGroup in uniqueGroups) {
        lvRaces.Groups.Add(uniqueGroup, uniqueGroup);
      }

      foreach (RaceEntry raceEntry in raceEntries) {
        ListViewItem newListViewItem = new ListViewItem { Text = raceEntry.name };
        newListViewItem.Tag = raceEntry;

        if (raceEntry.boundaryRace) {
          newListViewItem.ForeColor = Color.Red;
        } else {
          newListViewItem.ForeColor = Color.Green;
        }

        newListViewItem.Group = lvRaces.Groups["Group " + raceEntry.group.ToString()];

        lvRaces.Items.Add(newListViewItem);
      }
    }

    private void lvRaces_SelectedIndexChanged(object sender, EventArgs e) {
      if (lvRaces.SelectedItems.Count == 0) {
        return;
      }

      RaceEntry selectedRaceEntry = (RaceEntry)lvRaces.SelectedItems[0].Tag;
      if (selectedRaceEntry == null) {
        return;
      }

      try {
        TwtFile raceTwt = twtModule.LoadTwt("RACES/" + selectedRaceEntry.CanonicalName + ".TWT");
        MapFile parsedMap = mapModule.LoadMapFile(raceTwt.GetFile(selectedRaceEntry.CanonicalName + ".TXT"));
        PixiesModule.PixiesFile pf = pixiesModule.ReadPixies(raceTwt.GetFile("PIXIES.P16"));
        foreach (PixiesModule.PixiesFileEntry entry in pf.entries) {
          if (entry.filename.ToLower() == parsedMap.minimap.mapPixelmapName.Split('.')[0].ToLower()) {
            pictureBox1.Image = entry.bitmap;
            break;
          }
        }
      } catch (Exception ex) {
        // TODO: maps like quarry1 cause a crash because of an uncommented Point #2 in the txt - make parsing more lenient for such cases
        MessageBox.Show("There was a problem parsing the map file - " + ex.ToString());
      }
    }

    private void btnEditMap_Click(object sender, EventArgs e) {
      RaceEntry selectedRaceEntry = (RaceEntry)lvRaces.SelectedItems[0].Tag;
      if (selectedRaceEntry == null) {
        return;
      }

      new MapEditor(this.config, selectedRaceEntry.fileName).Show();
    }
  }
}

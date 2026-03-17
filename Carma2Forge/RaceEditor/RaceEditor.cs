using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Carma2ForgeLib.Modules;
using Carma2ForgeLib.Modules.RaceModule;

namespace Carma2Forge {
  public partial class RaceEditor : Form {
    private RaceModule raceModule = new RaceModule();

    public RaceEditor(Carma2ForgeConfig config) {
      raceModule.Initialize(config);

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
  }
}

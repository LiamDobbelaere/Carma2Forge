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
using Carma2ForgeLib.Modules.OpponentModule;

namespace Carma2Forge {
  public partial class OpponentEditor : Form {
    private OpponentModule opponentModule = new OpponentModule();

    private bool _hasChanges = false;
    private bool HasChanges {
      get {
        return _hasChanges;
      }
      set {
        _hasChanges = value;
        this.Text = "Opponent Editor" + (value ? " *" : "");
        //btnSave.Enabled = value;
      }
    }

    public OpponentEditor(Carma2ForgeConfig config) {
      opponentModule.Initialize(config);

      InitializeComponent();
    }

    private void OpponentEditor_Load(object sender, EventArgs e) {
      OpponentEntry[] opponentEntries = opponentModule.GetOpponentEntries();

      foreach (OpponentEntry opp in opponentEntries) {
        ListViewItem newListViewItem = new ListViewItem { Text = opp.carName }; //, ImageKey = fcd.id.ToString() };
        newListViewItem.SubItems.Add(opp.id.ToString());
        newListViewItem.Tag = opp;
        lvOpponents.Items.Add(newListViewItem);
      }
    }
  }
}

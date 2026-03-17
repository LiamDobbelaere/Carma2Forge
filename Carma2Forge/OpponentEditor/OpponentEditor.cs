using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Carma2ForgeLib.Modules;
using Carma2ForgeLib.Modules.OpponentModule;
using Carma2ForgeLib.Modules.PixiesModule;
using Carma2ForgeLib.Modules.TwtModule;
using static Carma2ForgeLib.Modules.PixiesModule.PixiesModule;

namespace Carma2Forge {
  public partial class OpponentEditor : Form {
    private TwtModule twtModule = new TwtModule();
    private PixiesModule pixiesModule = new PixiesModule();
    private OpponentModule opponentModule = new OpponentModule();

    private Bitmap bmpCurrentCarImage = new Bitmap(64 * 3, 64 * 2);
    private Bitmap bmpCurrentDriverImage = new Bitmap(64, 64);
    private Graphics gCurrentCarImage;

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
      twtModule.Initialize(config);
      pixiesModule.Initialize(config);
      opponentModule.Initialize(config);

      gCurrentCarImage = Graphics.FromImage(bmpCurrentCarImage);

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

      TwtFile twtFile = twtModule.LoadTwt("INTRFACE/Backdrop/ccar.TWT");
      PixiesFile pf = pixiesModule.ReadPixies(twtFile.GetFile("PIXIES.P16"));
      Bitmap carInterface = pf.GetFile("ccarl").bitmap;

      pbxCarInterface.Image = carInterface;

      ReparentForTransparency(lblCarName, pbxCarInterface);
      ReparentForTransparency(lblDriverName, pbxCarInterface);
      ReparentForTransparency(lblCarStats, pbxCarInterface);
      ReparentForTransparency(lblCarDescription, pbxCarInterface);

      ReparentForTransparency(pbxCar, pbxCarInterface);
      ReparentForTransparency(pbxDriver, pbxCarInterface);

      pbxDriver.BringToFront();
    }

    private void ReparentForTransparency(Control source, PictureBox newParent) {
      source.Location = newParent.PointToClient(source.PointToScreen(Point.Empty));
      source.Parent = newParent;
    }

    private void lvOpponents_SelectedIndexChanged(object sender, EventArgs e) {
      if (lvOpponents.SelectedItems.Count == 0) {
        return;
      }

      OpponentEntry selectedOpponent = (OpponentEntry)lvOpponents.SelectedItems[0].Tag;
      string canonicalName = selectedOpponent.CanonicalName;

      lblCarName.Text = selectedOpponent.carName;
      lblDriverName.Text = selectedOpponent.longName;
      lblCarStats.Text = $"{selectedOpponent.topSpeed}\n{selectedOpponent.weight}\n{selectedOpponent.zeroToSixty}";
      lblCarDescription.Text = selectedOpponent.description;

      TwtFile twtFile;
      PixiesFile pf;
      try {
        twtFile = twtModule.LoadTwt(selectedOpponent.CarImagePath);
        pf = pixiesModule.ReadPixies(twtFile.GetFile("PIXIES.P16"));
      } catch (Exception ex) {
        pbxCar.Image = null;
        pbxDriver.Image = null;
        return;
      }

      string[] pictureSegments = new string[] {
        $"{canonicalName}a",
        $"{canonicalName}b",
        $"{canonicalName}c",
        $"{canonicalName}d",
        $"{canonicalName}e",
        $"{canonicalName}f",
      };

      int xOffset;
      int yOffset;
      foreach (string pictureSegment in pictureSegments) {
        PixiesFileEntry pixEntry = pf.GetFile(pictureSegment);
        Bitmap pixBitmap = pixEntry.bitmap;
        switch (pictureSegment.Last()) {
          case 'a':
            xOffset = 0;
            yOffset = 0;
            break;
          case 'b':
            xOffset = 64;
            yOffset = 0;
            break;
          case 'c':
            xOffset = 128;
            yOffset = 0;
            break;
          case 'd':
            xOffset = 0;
            yOffset = 64;
            break;
          case 'e':
            xOffset = 64;
            yOffset = 64;
            break;
          case 'f':
            xOffset = 128;
            yOffset = 64;
            break;
          default:
            throw new Exception($"Unexpected picture segment {pictureSegment}");
        }
        gCurrentCarImage.DrawImage(pixBitmap, xOffset, yOffset);
      }

      // find the driver image by finding first entry that isn't in pictureSegments:
      foreach (PixiesFileEntry entry in pf.entries) {
        if (!pictureSegments.Contains(entry.filename)) {
          bmpCurrentDriverImage = entry.bitmap;
          break;
        }
      }

      pbxCar.Image = bmpCurrentCarImage;
      pbxDriver.Image = bmpCurrentDriverImage;
    }
  }
}

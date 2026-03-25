using Carma2ForgeLib.Modules;
using Carma2ForgeLib.Modules.DatModule;
using Carma2ForgeLib.Modules.MatModule;
using Carma2ForgeLib.Modules.TwtModule;

namespace Carma2Forge {
  public partial class MainForm : Form {
    private Carma2ForgeConfig config;

    public MainForm() {
      InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e) {
      if (!HasCarma2PathConfigured()) {
        StartCarma2PathConfigurationFlow();
      }

      config = new Carma2ForgeConfig {
        Carma2Path = Properties.Settings.Default.Carma2Path,
        DataPath = "data"
      };

      // new MapEditor(config, "newcity1").Show();

      TwtModule twtModule = new TwtModule();
      twtModule.Initialize(config);
      DatModule datModule = new DatModule();
      datModule.Initialize(config);
      MatModule matModule = new MatModule();
      matModule.Initialize(config);
      ActModule actModule = new ActModule();
      actModule.Initialize(config);

      TwtFile newcity1 = twtModule.LoadTwt("RACES/newcity1.TWT");
      DatFile newcity1Dat = datModule.LoadDat(newcity1.GetFile("newcity1.dat"));
      MatFile newcity1Mat = matModule.LoadMat(newcity1.GetFile("newcity1.mat"));
      ActFile newcity1Act = actModule.LoadAct(newcity1.GetFile("newcity1.act"));
    }

    private bool HasCarma2PathConfigured() {
      return Properties.Settings.Default.Carma2Path.Trim() != string.Empty;
    }

    private void StartCarma2PathConfigurationFlow() {
      MessageBox.Show(
        "Looks like there is no Carmageddon 2 directory configured yet, please point me to your Carmageddon 2 folder.",
        "Select your Carmageddon 2 directory",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
      );
      DialogResult dlrCarmaPath = fbdCarmaPath.ShowDialog();

      if (dlrCarmaPath != DialogResult.OK) {
        DialogResult dlrCarmaPathRetry = MessageBox.Show(
          "You didn't select a Carmageddon 2 folder, give up and close?",
          "Oops",
          MessageBoxButtons.RetryCancel,
          MessageBoxIcon.Exclamation
        );

        if (dlrCarmaPathRetry == DialogResult.Retry) {
          StartCarma2PathConfigurationFlow();
          return;
        }

        Environment.Exit(69);
      }

      string carma2Path = fbdCarmaPath.SelectedPath;
      if (!IsValidCarma2Path(carma2Path)) {
        DialogResult dlrCarmaPathInvalid = MessageBox.Show(
          "The selected folder doesn't seem to contain CARMA2_HW.EXE, try again?",
          "Invalid Carmageddon 2 directory",
          MessageBoxButtons.RetryCancel,
          MessageBoxIcon.Error
        );

        if (dlrCarmaPathInvalid == DialogResult.Retry) {
          StartCarma2PathConfigurationFlow();
          return;
        }

        Environment.Exit(69);
      }

      Properties.Settings.Default.Carma2Path = carma2Path;
      Properties.Settings.Default.Save();
    }

    private bool IsValidCarma2Path(string path) {
      return File.Exists(Path.Combine(path, "CARMA2_HW.EXE"));
    }

    private void btnFontEditing_Click(object sender, EventArgs e) {
      new FontEditor(config).Show();
    }

    private void btnOpponentEditor_Click(object sender, EventArgs e) {
      new OpponentEditor(config).Show();
    }

    private void btnTwtEditor_Click(object sender, EventArgs e) {
      new TwtEditor(config).Show();
    }

    private void btnRaceEditor_Click(object sender, EventArgs e) {
      new RaceEditor(config).Show();
    }
  }
}
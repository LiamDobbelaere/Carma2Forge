using Carma2ForgeLib.Modules;
using Carma2ForgeLib.Modules.PixiesModule;
using Carma2ForgeLib.Modules.TwtModule;
using static Carma2ForgeLib.Modules.PixiesModule.PixiesModule;

namespace Carma2Forge {
  public partial class TwtEditor : Form {
    private TwtModule twtModule = new TwtModule();
    private PixiesModule pixiesModule = new PixiesModule();

    public TwtEditor(Carma2ForgeConfig config) {
      twtModule.Initialize(config);
      pixiesModule.Initialize(config);

      InitializeComponent();
    }

    private void TwtEditor_Load(object sender, EventArgs e) {
      TwtFile twtFile = twtModule.LoadTwt("INTRFACE/CarImage/buzzCI.TWT");

      foreach (TwtFileEntry entry in twtFile.entries) {
        rtbTest.AppendText($"Filename: {entry.filename}, Data Length: {entry.data.Length}\n");
      }

      PixiesFile pf = pixiesModule.ReadPixies(twtFile.GetFile("PIXIES.P16"));
      foreach (PixiesFileEntry entry in pf.entries) {
        rtbTest.AppendText($"Filename: {entry.filename}, Bitmap Size: {entry.bitmap.Size}\n");
      }

      pictureBox1.Image = pf.entries[1].bitmap;
    }
  }
}

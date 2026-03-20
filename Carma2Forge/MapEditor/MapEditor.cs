using Carma2ForgeLib.Modules;
using Carma2ForgeLib.Modules.MapModule;
using Carma2ForgeLib.Modules.TwtModule;

namespace Carma2Forge {
  public partial class MapEditor : Form {
    private TwtModule twtModule = new TwtModule();
    private MapModule mapModule = new MapModule();
    private TwtFile raceTwt;

    public MapEditor(Carma2ForgeConfig config, string mapName) {
      twtModule.Initialize(config);
      mapModule.Initialize(config);

      InitializeComponent();

      // TODO: do this in a cleaner way
      raceTwt = twtModule.LoadTwt("RACES/" + mapName + ".TWT");
      mapModule.LoadMapFile(raceTwt.GetFile(mapName + ".TXT"));
    }
  }
}

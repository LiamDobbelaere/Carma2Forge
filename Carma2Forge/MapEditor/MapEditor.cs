using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;
using Carma2ForgeLib.Modules;
using Carma2ForgeLib.Modules.MapModule;
using Carma2ForgeLib.Modules.PixiesModule;
using Carma2ForgeLib.Modules.TwtModule;

namespace Carma2Forge {
  public partial class MapEditor : Form {
    private TwtModule twtModule = new TwtModule();
    private MapModule mapModule = new MapModule();
    private PixiesModule pixiesModule = new PixiesModule();
    private TwtFile raceTwt;
    private MapFile mapFile;

    public MapEditor(Carma2ForgeConfig config, string mapName) {
      twtModule.Initialize(config);
      mapModule.Initialize(config);
      pixiesModule.Initialize(config);

      InitializeComponent();

      // TODO: do this in a cleaner way
      raceTwt = twtModule.LoadTwt("RACES/" + mapName + ".TWT");
      mapModule.LoadMapFile(raceTwt.GetFile(mapName + ".TXT"));

      raceTwt = twtModule.LoadTwt("RACES/" + mapName + ".TWT");
      mapFile = mapModule.LoadMapFile(raceTwt.GetFile(mapName + ".TXT"));
      PixiesModule.PixiesFile pf = pixiesModule.ReadPixies(raceTwt.GetFile("PIXIES.P16"));
      foreach (PixiesModule.PixiesFileEntry entry in pf.entries) {
        if (entry.filename.ToLower() == mapFile.minimap.mapPixelmapName.Split('.')[0].ToLower()) {
          pbxMap.Image = entry.bitmap;
          break;
        }
      }

      MapMinimap mapMinimap = mapFile.minimap;
      
      Bitmap mapImage = pbxMap.Image as Bitmap;
      Graphics g = Graphics.FromImage(mapImage);

 

      // visualize special volumes:
      MapSpecialEffectsVolume[] specialEffectVolumes = mapFile.specialEffectsVolumes;
      foreach (MapSpecialEffectsVolume volume in specialEffectVolumes) {
        if (volume.type != "BOX")
          continue;

        Vector3 center = volume.p4;
        Vector2 centerOnMap = mapMinimap.WorldToMap(center);

        g.DrawEllipse(new Pen(Brushes.Pink, 2), centerOnMap.X - 5, centerOnMap.Y - 5, 10, 10);

        // calculate the 4 corners of the box:
        /*Vector3 halfExtents = volume.p3 / 2;
        Vector3[] corners = new Vector3[4];
        corners[0] = center + new Vector3(-halfExtents.X, -halfExtents.Y, 0);
        corners[1] = center + new Vector3(halfExtents.X, -halfExtents.Y, 0);
        corners[2] = center + new Vector3(halfExtents.X, halfExtents.Y, 0);
        corners[3] = center + new Vector3(-halfExtents.X, halfExtents.Y, 0);

        // draw the box on the map:
        for (int i = 0; i < corners.Length; i++) {
          Vector2 cornerOnMap = mapMinimap.WorldToMap(corners[i]);
          Vector2 nextCornerOnMap = mapMinimap.WorldToMap(corners[(i + 1) % corners.Length]);
          g.DrawLine(new Pen(Brushes.Red, 2), (PointF)cornerOnMap, (PointF)nextCornerOnMap);
        }*/
      }


      pbxMap.Refresh();
    }
  }
}

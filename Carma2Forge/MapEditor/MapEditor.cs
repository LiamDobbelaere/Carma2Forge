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

      // draw opponent path
      MapOpponentPaths opponentPaths = mapFile.opponentPaths;
      foreach (Vector3 node in opponentPaths.nodes) {
        Vector2 nodeOnMap = mapMinimap.WorldToMap(node);
        g.DrawEllipse(new Pen(Brushes.Orange, 2), nodeOnMap.X - 3, nodeOnMap.Y - 3, 6, 6);
      }

      // connect opponent path nodes by reading out sections
      MapOpponentPathSection[] sections = opponentPaths.sections;
      foreach (MapOpponentPathSection section in sections) {
        Vector3 startNode = opponentPaths.nodes[section.from];
        Vector3 endNode = opponentPaths.nodes[section.to];

        Vector2 startNodeOnMap = mapMinimap.WorldToMap(startNode);
        Vector2 endNodeOnMap = mapMinimap.WorldToMap(endNode);

        Vector2 arrowEnd = endNodeOnMap;
        Vector2 direction = Vector2.Normalize(endNodeOnMap - startNodeOnMap);

        // draw it as a yellow-to-red gradient line
        using (LinearGradientBrush brush = new LinearGradientBrush((PointF)startNodeOnMap, (PointF)endNodeOnMap, Color.Yellow, Color.Red)) {
          g.DrawLine(new Pen(brush, 2), (PointF)startNodeOnMap, (PointF)endNodeOnMap);
        }
      }

      foreach (MapOpponentPathSection section in sections) {
        Vector3 startNode = opponentPaths.nodes[section.from];
        Vector3 endNode = opponentPaths.nodes[section.to];

        Vector2 startNodeOnMap = mapMinimap.WorldToMap(startNode);
        Vector2 endNodeOnMap = mapMinimap.WorldToMap(endNode);

        Vector2 arrowEnd = endNodeOnMap;
        Vector2 direction = Vector2.Normalize(endNodeOnMap - startNodeOnMap);

        // draw arrow head:
        float arrowHeadSize = 6;
        Vector2 perpendicular = new Vector2(-direction.Y, direction.X);
        Vector2 arrowHeadPoint1 = arrowEnd - direction * arrowHeadSize + perpendicular * arrowHeadSize / 2;
        Vector2 arrowHeadPoint2 = arrowEnd - direction * arrowHeadSize - perpendicular * arrowHeadSize / 2;

        // draw filled arrow head:
        PointF[] arrowHeadPoints = new PointF[] { (PointF)arrowEnd, (PointF)arrowHeadPoint1, (PointF)arrowHeadPoint2 };
        g.FillPolygon(Brushes.White, arrowHeadPoints);
      }


      /*MapDronePath[] dronePaths = mapFile.dronePaths.paths;
      // only draw their positions for now
      foreach (MapDronePath path in dronePaths) {
        Vector2 droneOnMap = mapMinimap.WorldToMap(path.position);

        g.DrawEllipse(new Pen(Brushes.Purple, 2), droneOnMap.X - 3, droneOnMap.Y - 3, 6, 6);
      }*/

      /*MapNetworkStartPoint[] networkStartPoints = mapFile.networkStartPoints;
      foreach (MapNetworkStartPoint networkStartPoint in networkStartPoints) {
        MessageBox.Show(networkStartPoint.position.ToString());
        Vector2 networkStartPointOnMap = mapMinimap.WorldToMap(networkStartPoint.position);
        g.DrawEllipse(new Pen(Brushes.CadetBlue, 2), networkStartPointOnMap.X - 3, networkStartPointOnMap.Y - 3, 6, 6);
      }*/

      Vector2 startingGridMapPos = mapMinimap.WorldToMap(mapFile.startingGrid.gridPosition);
      g.FillEllipse(Brushes.Yellow, startingGridMapPos.X - 5, startingGridMapPos.Y - 5, 10, 10);

      // draw an arrow using startingGridMapPos direction, where 0 is right:
      float arrowLength = 20;
      float arrowAngle = mapFile.startingGrid.direction;
      Vector2 arrowEndPos = new Vector2(
        startingGridMapPos.X + arrowLength * (float)Math.Cos(arrowAngle),
        startingGridMapPos.Y + arrowLength * (float)Math.Sin(arrowAngle)
      );
      g.DrawLine(new Pen(Brushes.Yellow, 2), (PointF)startingGridMapPos, (PointF)arrowEndPos);

      int checkpointNumber = 0;
      foreach (MapCheckpoint checkpoint in mapFile.checkpoints) {
        checkpointNumber++;
        Vector3 checkpointPos = checkpoint.quads[0].Center;
        Vector2 posOnMap = mapMinimap.WorldToMap(checkpointPos); ;

        g.FillEllipse(Brushes.Blue, posOnMap.X - 5, posOnMap.Y - 5, 10, 10);

        // draw number inside the ellipse:
        g.DrawString(checkpointNumber.ToString(), new Font("Arial", 8), Brushes.White, posOnMap.X - 4, posOnMap.Y - 7);
      }

      // visualize special volumes:
      /*MapSpecialEffectsVolume[] specialEffectVolumes = mapFile.specialEffectsVolumes;
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
        }
      }*/


      pbxMap.Refresh();
    }
  }
}

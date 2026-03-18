using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Carma2ForgeLib.Enums;
using Carma2ForgeLib.Modules.TwtModule;
using Carma2ForgeLib.Utilities;

namespace Carma2ForgeLib.Modules.MapModule{
  public enum SmokeColumnSmokiness {
    Fire,
    Black,
    Grey,
    White
  }

  public class MapLighting {
    public Color mainDirectionalLight;
    public Vector2 zeroAmbientDiffuseLight;
    public Vector2 oneAmbientDiffuseLight;
    public Vector2 otherAmbientDiffuseLight;
  }

  public class MapStartingGrid {
    public Vector3 gridPosition;
    public int direction;
  }

  public class MapCheckpoint {
    public required int[] pedTimerIncrements;
    public int quadCount;
    public required Vector3[] quadPoints;
  }

  public class MapSmashable {
    public int flags;
    public required string triggerModel;
    public required string mode; // TODO: enum/constants, can be replacemodel, texturechange
    public required string intactPixelmap;
    public int numberOfLevels;
    public float triggerThreshold; // texturechange only
    public int textureChangeFlags; // texturechange only
    public required string collisionTypeSolidEdge; // texturechange only, can be edges, solid, passthrough
    public int removalThreshold;
    public required int[] possibleSounds;
    public required MapSmashableShrapnel[] shrapnel;
    public int fireSmokeChance;
    public int smokeColumnCount;
    public SmokeColumnSmokiness minSmokiness;
    public SmokeColumnSmokiness maxSmokiness;
    public required string actorFile;
    public required string[] separateActor;
    public required string[] nonCarsSeparateActors;
    public required MapSmashableExplosionGroup[] explosionGroups;
    public required string slickMaterial;
    public int activatedNonCarCuboids;
    public int unknown1; // 1
    public required int[] unknown2; // 1,3
    public required string unknown3; // *.DAT
    public required string unknown4; // relative
    public required int[] unknown5; // -1,-1,-1
    public required int[] unknown6; // 1,1,1
    public required string unknown7; // away
    public int unknown8; // -10
    public int extensionsFlags;
    public int roomTurnOnCode; 
    public required string awardCode; // singleshot
    public int unknown9; // 3000
    public int unknown10; // 20
    public int unknown11; // 0
    public int unknown12; // 0
    public int unknown13; // 0
    public int runtimeVariableChanges;
    public required string newModel;
    public int fireChance;
    public int unknown14; // 1
    public required int[] unknown15; // 0,0
    public required string[] pixelMaps; // ghostparts only
    public int reserved1;
    public int reserved2;
    public int reserved3;
    public int reserved4;
  }

  public class MapSmashableShrapnel {
    public required string shrapnelType; // noncars, ghostparts
    public float minSpeed;
    public float maxSpeed;
    public float impacteeVelocityFactor;
    public float maxRandomVelocity;
    public float maxRandomUpVelocity;
    public float maxRandomNormalVelocity;
    public float maxRandomSpinRate;
    public required string initialPlacementMode;
    public float minNumber;
    public float maxNumber;
    public int count; // -1
    public int unknown1; // -1
    public required string shrapnelActor; // ghostparts only
  }

  public class MapSmashableExplosionGroup {
    public int minCount;
    public int maxCount;
    public float minStartDelay;
    public float maxStartDelay;
    public Vector3 offset;
    public Vector3 minFactor;
    public Vector3 maxFactor;
    public int minFramerate;
    public int maxFramerate;
    public int minScalingFactor;
    public int maxScalingFactor;
    public required string rotateMode; // possibilities: randomRotate
    public required MapSmashableExplosionGroupFrame[] frames;
  }

  public class MapSmashableExplosionGroupFrame {
    public int opacity;
    public required string framePixName;
  }
  
  public class MapFile {
    private MapLighting lighting;
    private MapStartingGrid startingGrid;
    private MapCheckpoint[] checkpoints;
    private MapSmashable[] smashables;
    // ped specs was next
  }

  public enum MapBlockType {
    Version,
    Lighting,
    StartingGrid,
    Checkpoints,
    Smashables,
    Peds,
    AdditionalActor,
    Horizon,
    SpecialEffectsVolumes,
    SoundGenerators,
    ReflectiveWindscreenSpecs,
    Minimap,
    Funks,
    Grooves,
    OpponentPaths,
    DronePaths,
    MaterialModifiers,
    NonCarObjects,
    DustShadeTables,
    NetworkStartPoints,
    SplashFiles,
    SelfReference
  }

  public class MapModule : BaseModule {
    private Carma2ForgeConfig config;

    public void Initialize(Carma2ForgeConfig config) {
      this.config = config;
    }

    public void LoadMapFile(TwtFileEntry entry) {
      using TxtEnumerator mapTxtLines = new TxtEnumerator(config.ReadTxt(entry));

      while (mapTxtLines.HasNext()) {
        string line = mapTxtLines.Next();


      }
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

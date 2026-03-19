using System;
using System.Collections.Generic;
using System.ComponentModel;
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

  public class MapPed {
    public required string materialName;
    public int movementIndex;
    public int groupIndex;
    public int pedsPer100Sqm;
    public required MapPedExclusionMaterial[] exclusionMaterials;
  }

  public class MapPedExclusionMaterial {
    public int flags;
    public required string materialName;
  }

  public class MapPedExceptionMaterial {
    // TODO, it'll probably crash at some point and I'll have to fix it then :')
  }

  public class MapHorizon {
    public required string skyTextureName;
    public int horizontalRepetitions;
    public int verticalSizeDegrees;
    public int horizonPositionBelowTop; // in pixels
    public required string depthCueMode; // none, dark, fog, colour
    public int fogAmount;
    public int darknessAmount;
    public Color depthCueColor;
  }

  public class MapSpecialEffectVolume {
    public required string type; // DEFAULT or BOX
    public Vector3 p1; // ? maybe part of a transformation matrix, not sure
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;
    public float gravityMultiplier;
    public float viscosityMultiplier;
    public float carDamagePerMs;
    public float pedDamagePerMs;
    public int cameraEffectIndex;
    public int skyColor;
    public required string windscreenTexture;
    public int entrySoundId;
    public int exitSoundId;
    public int engineNoiseIndex;
    public int materialIndex;
    public required string soundType; // none or SCATTERED
    public required string scatterMode; // RANDOM
    public float minVolume;
    public float maxVolume;
    public int count; // 15, TODO: not sure what this is
    public required int[] scatterSounds;
  }

  public class MapSoundGenerator {
    // TODO?
  }

  public class MapReflectiveWindscreenSpecs {
    public required string defaultScreenMaterial;
    public required string darknessScreenMaterial;
    public required string fogScreenMaterial;
    public int areasWithDifferentScreens; // (ignore)
  }

  public class MapMinimap {
    public required string mapPixelmapName;
    public Vector3 worldMapTransformationX;
    public Vector3 worldMapTransformationY;
    public Vector3 worldMapTransformationZ;
    public Vector3 worldMapTransformationW;
  }

  public class MapFunk {
    // TODO
  }

  public class MapGroove {
    // TODO
  }

  public class MapOpponentPathSection {
    public int from;
    public int to;
    public int unknown1; // 0
    public int unknown2; // 255
    public int unknown3; // 0
    public int unknown4; // 255
    public float unknown5; // 1.0 - speed?
    public bool unknown6; // idk - TODO
  }

  public class MapOpponentPaths {
    public required Vector3[] nodes;
    public required MapOpponentPathSection[] sections;
    public int copStartPoints;
  }

  public class MapDronePaths {
    public int version;
    public required MapDronePath[] paths;
  }

  public class MapDronePath {
    public Vector3 position;
    public required string droneName;
    public int unknown1; // 0
    public required Vector4[] unknown2;
  }

  public class MapMaterialModifier {
    public float carWallFriction;
    public float tireWallFriction;
    public float downForce;
    public float bumpiness;
    public int tireSoundIndex;
    public int crashSoundIndex;
    public int scrapeSoundIndex;
    public float sparkiness;
    public int roomForExpansion;
    public required string skidmarkMaterial; // can be 'none'
  }

  public class MapDustShadeTable {
    public Color rgb;
    public Vector3 strength;
  }

  public class MapNetworkStartPoint {
    public Vector3 position;
    public int rotationDegrees;
  }

  public class MapFile {
    public required MapLighting lighting;
    public required MapStartingGrid startingGrid;
    public required MapCheckpoint[] checkpoints;
    public required MapSmashable[] smashables;
    public required MapPed[] peds;
    public required string additionalActor;
    public required MapHorizon horizon;
    public int defaultEngineNoise;
    public required MapSpecialEffectVolume[] specialEffectsVolumes;
    public required MapSoundGenerator[] soundGenerators;
    public required MapReflectiveWindscreenSpecs reflectiveWindscreenSpecs;
    public required MapMinimap minimap;
    public required MapFunk[] funks;
    public required MapGroove[] grooves;
    public required MapOpponentPaths opponentPaths;
    public required MapDronePaths dronePaths;
    public required MapMaterialModifier[] materialModifiers;
    public required string[] nonCarObjects;
    public required MapDustShadeTable[] dustShadeTables;
    public required MapNetworkStartPoint[] networkStartPoints;
    public required string[] splashFiles;
    public required string[] mapTxtReferences;
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

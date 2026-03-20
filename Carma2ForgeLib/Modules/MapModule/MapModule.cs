using System.Drawing;
using System.Numerics;
using Carma2ForgeLib.Modules.TwtModule;
using Carma2ForgeLib.Utilities;

namespace Carma2ForgeLib.Modules.MapModule {
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

  public class MapQuad {
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;

    public Vector3 Center {
      get {
        return new Vector3(
          (p1.X + p2.X + p3.X + p4.X) / 4,
          (p1.Y + p2.Y + p3.Y + p4.Y) / 4,
          (p1.Z + p2.Z + p3.Z + p4.Z) / 4
        );
      }
    }
  }

  public class MapCheckpoint {
    public int[] pedTimerIncrements;
    public MapQuad[] quads;
  }

  public class MapConnotations {
    public int[] sounds;
    public MapSmashableShrapnel[] shrapnel;
    public MapSmashableExplosion[] explosions;
    public string slickMaterial;
    public MapSmashableNonCarCuboid[] nonCarCuboids;
    public MapSmashableActivationCuboid[] activationCuboids;
    public int extensionFlags;
    public int roomTurnOnCode;
    public string awardCode;
    public int pointsAwarded;
    public int timeAwarded;
    public int hudIndex;
    public int fancyHudIndex;
    public string[] runtimeVariableChanges;
  }

  public class MapSmashableCuboid {
    public string cuboidCoordinateSystem;
    public Vector2 delay;
    public Vector3 min;
    public Vector3 max;
  }

  public class MapSmashableNonCarCuboid : MapSmashableCuboid {
    public int nonCarNumber;
    public float minSpeed;
    public float maxSpeed;
    public float impacteeVelocityFactor;
    public float maxRandomVelocity;
    public float maxRandomUpVelocity;
    public float maxRandomNormalVelocity;
    public float maxRandomSpinRate;
  }

  public class MapSmashableActivationCuboid : MapSmashableCuboid {
    public string name;
    public string impactDirection;
    public float impactStrength;
  }

  public class MapSmashableExplosion {
    public int[] count;
    public Vector2 startDelay;
    public Vector3 offset;
    public Vector2 xFactor;
    public Vector2 yFactor;
    public Vector2 zFactor;
    public Vector2 framerate;
    public Vector2 scalingFactor;
    public string rotationMode;
    public MapSmashableExplosionFrame[] frames;
  }

  public class MapSmashableExplosionFrame {
    public int opacity;
    public string pixelmap;
  }

  public class MapSmashableTextureLevel {
    public float triggerThreshold;
    public int flags;
    public string collisionType;
    public MapConnotations connotations;
    public string[] pixelMaps;
  }

  public class MapSmashable {
    public int flags;
    public string triggerModel;
    public int triggerFlags;
    public string triggerMode; // TODO: enum/constants, can be replacemodel, texturechange
    public string intactPixelmap;
    public int numberOfTextureLevels;
    public MapSmashableTextureLevel[] textureLevels;
    public float triggerThreshold; // texturechange only
    public MapConnotations connotations;
    public int textureChangeFlags; // texturechange only
    public string collisionTypeSolidEdge; // texturechange only, can be edges, solid, passthrough
    public float removalThreshold;
    public int[] possibleSounds;
    public MapSmashableShrapnel[] shrapnel;
    public int fireSmokeChance;
    public int smokeColumnCount;
    public SmokeColumnSmokiness minSmokiness;
    public SmokeColumnSmokiness maxSmokiness;
    public string actorFile;
    public string[] separateActors;
    public string[] nonCarsSeparateActors;
    public MapSmashableExplosionGroup[] explosionGroups;
    public string slickMaterial;
    public int activatedNonCarCuboids;
    public int unknown1; // 1
    public int[] unknown2; // 1,3
    public string unknown3; // *.DAT
    public string unknown4; // relative
    public int[] unknown5; // -1,-1,-1
    public int[] unknown6; // 1,1,1
    public string unknown7; // away
    public int unknown8; // -10
    public int extensionsFlags;
    public int roomTurnOnCode;
    public string awardCode; // singleshot
    public int unknown9; // 3000
    public int unknown10; // 20
    public int unknown11; // 0
    public int unknown12; // 0
    public int runtimeVariableChanges;
    public string newModel;
    public int fireChance;
    public int unknown13; // 1
    public int[] unknown14; // 0,0
    public string[] pixelMaps; // ghostparts only
    public int reserved1;
    public int reserved2;
    public int reserved3;
    public int reserved4;
  }

  public class MapSmashableShrapnel {
    public string shrapnelType; // noncars, ghostparts
    public float minSpeed;
    public float maxSpeed;
    public float impacteeVelocityFactor;
    public float maxRandomVelocity;
    public float maxRandomUpVelocity;
    public float maxRandomNormalVelocity;
    public float maxRandomSpinRate;
    public string initialPlacementMode;
    public float clumpoingRadius;
    public string clumpingCenter;
    public float minNumber;
    public float maxNumber;
    public Vector2 time;
    public float cutLength;
    public int flags;
    public string materialName;
    public int count; // -1
    public int unknown1; // -1
    public string[] ghostPartActors;
    public int fireChance;
    public int smokeColumnCount;
    public SmokeColumnSmokiness minSmokiness;
    public SmokeColumnSmokiness maxSmokiness;
    public string actor;
    public MapSmashableShrapnelActor[] actors;
  }

  public class MapSmashableShrapnelActor {
    public string name;
    public string fileName;
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
    public string rotateMode; // possibilities: randomRotate
    public MapSmashableExplosionGroupFrame[] frames;
  }

  public class MapSmashableExplosionGroupFrame {
    public int opacity;
    public string framePixName;
  }

  public class MapPed {
    public required string materialName;
    public int movementIndex;
    public int groupIndex;
    public float pedsPer100Sqm;
    public MapPedExclusionMaterial[] exclusionMaterials;
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

  public class MapSpecialEffectsVolume {
    public string type; // DEFAULT or BOX
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
    public string windscreenTexture;
    public int entrySoundId;
    public int exitSoundId;
    public int engineNoiseIndex;
    public int materialIndex;
    public string soundType; // none or SCATTERED
    public string scatterMode; // RANDOM
    public float minVolume;
    public float maxVolume;
    public int count; // 15, TODO: not sure what this is
    public int[] scatterSounds;
  }

  public class MapSoundGenerator {
    // TODO?
  }

  public class MapReflectiveWindscreenSpecs {
    public string defaultScreenMaterial;
    public string darknessScreenMaterial;
    public string fogScreenMaterial;
    public int areasWithDifferentScreens; // (ignore)
  }

  public class MapMinimap {
    public string mapPixelmapName;
    public Vector3 worldMapTransformationRow0;
    public Vector3 worldMapTransformationRow1;
    public Vector3 worldMapTransformationRow2;
    public Vector3 worldMapTransformationOffset;

    public Vector2 WorldToMap(Vector3 p) {
      float mapX =
          worldMapTransformationRow0.X * p.X +
          worldMapTransformationRow1.X * p.Y +
          worldMapTransformationRow2.X * p.Z +
          worldMapTransformationOffset.X;

      float mapY =
          worldMapTransformationRow0.Y * p.X +
          worldMapTransformationRow1.Y * p.Y +
          worldMapTransformationRow2.Y * p.Z +
          worldMapTransformationOffset.Y;

      return new Vector2(mapX, mapY);
    }
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
    public Vector3[] nodes;
    public MapOpponentPathSection[] sections;
    public int copStartPoints;
  }

  public class MapDronePaths {
    public int version;
    public MapDronePath[] paths;
  }

  public class MapDronePath {
    public Vector3 position;
    public string droneName;
    public int unknown1; // 0
    public Vector4[] unknown2;
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
    public string skidmarkMaterial; // can be 'none'
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
    public MapLighting lighting;
    public MapStartingGrid startingGrid;
    public MapCheckpoint[] checkpoints;
    public MapSmashable[] smashables;
    public MapPed[] peds;
    public string additionalActor;
    public MapHorizon horizon;
    public int defaultEngineNoise;
    public MapSpecialEffectsVolume[] specialEffectsVolumes;
    public MapSoundGenerator[] soundGenerators;
    public MapReflectiveWindscreenSpecs reflectiveWindscreenSpecs;
    public MapMinimap minimap;
    public MapFunk[] funks;
    public MapGroove[] grooves;
    public MapOpponentPaths opponentPaths;
    public MapDronePaths dronePaths;
    public MapMaterialModifier[] materialModifiers;
    public string[] nonCarObjects;
    public MapDustShadeTable[] dustShadeTables;
    public MapNetworkStartPoint[] networkStartPoints;
    public string[] splashFiles;
    public string[] mapTxtReferences;
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
    DefaultEngineNoise,
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

    public MapFile LoadMapFile(TwtFileEntry entry) {
      using TxtEnumerator mapTxtLines = new TxtEnumerator(config.ReadTxt(entry));

      MapBlockType currentBlock = MapBlockType.Version;
      MapFile mapFile = new MapFile();

      while (mapTxtLines.HasNext()) {
        string line = mapTxtLines.Next();

        switch (currentBlock) {
          case MapBlockType.Version:
            if (line != "VERSION 1") {
              throw new Exception($"Unsupported map file version: {line}");
            }
            break;
          case MapBlockType.Lighting:
            MapLighting lighting = new MapLighting();
            lighting.mainDirectionalLight = mapTxtLines.AsColorRGB();
            lighting.zeroAmbientDiffuseLight = mapTxtLines.NextVector2();
            lighting.oneAmbientDiffuseLight = mapTxtLines.NextVector2();
            lighting.otherAmbientDiffuseLight = mapTxtLines.NextVector2();
            break;
          case MapBlockType.StartingGrid:
            mapFile.startingGrid = new MapStartingGrid();
            mapFile.startingGrid.gridPosition = mapTxtLines.AsVector3();
            mapFile.startingGrid.direction = mapTxtLines.NextInt();
            break;
          case MapBlockType.Checkpoints:
            int checkpointCount = mapTxtLines.AsInt();
            mapFile.checkpoints = new MapCheckpoint[checkpointCount];
            for (int i = 0; i < checkpointCount; i++) {
              MapCheckpoint checkpoint = new MapCheckpoint();

              checkpoint.pedTimerIncrements = mapTxtLines.NextIntArray();

              int quadCount = mapTxtLines.NextInt();
              checkpoint.quads = new MapQuad[quadCount];
              for (int j = 0; j < quadCount; j++) {
                MapQuad quad = new MapQuad();
                quad.p1 = mapTxtLines.NextVector3();
                quad.p2 = mapTxtLines.NextVector3();
                quad.p3 = mapTxtLines.NextVector3();
                quad.p4 = mapTxtLines.NextVector3();
                checkpoint.quads[j] = quad;
              }

              mapFile.checkpoints[i] = checkpoint;
            }
            break;
          case MapBlockType.Smashables:
            int smashableCount = mapTxtLines.AsInt();
            mapFile.smashables = new MapSmashable[smashableCount];

            for (int i = 0; i < smashableCount; i++) {
              MapSmashable mapSmashable = new MapSmashable();

              mapSmashable.flags = mapTxtLines.NextInt();
              mapSmashable.triggerModel = mapTxtLines.Next();
              if (mapSmashable.triggerModel.Length == 3 && mapSmashable.triggerModel.StartsWith("&")) {
                mapSmashable.triggerFlags = mapTxtLines.NextInt();
              }

              mapSmashable.triggerMode = mapTxtLines.Next();

              switch (mapSmashable.triggerMode) {
                case "nochange":
                case "remove":
                  mapSmashable.removalThreshold = mapTxtLines.NextFloat();
                  mapSmashable.connotations = LoadConnotations(mapTxtLines);
                  break;
                case "replacemodel":
                  mapSmashable.removalThreshold = mapTxtLines.NextFloat();
                  mapSmashable.connotations = LoadConnotations(mapTxtLines);

                  mapSmashable.newModel = mapTxtLines.Next();
                  mapSmashable.fireChance = mapTxtLines.NextInt();

                  if (mapSmashable.fireChance > 0) {
                    mapSmashable.smokeColumnCount = mapTxtLines.NextInt();
                    int[] smokinessLevels = mapTxtLines.NextIntArray();
                    mapSmashable.minSmokiness = (SmokeColumnSmokiness)smokinessLevels[0];
                    mapSmashable.maxSmokiness = (SmokeColumnSmokiness)smokinessLevels[1];
                  }
                  break;
                case "texturechange":
                  mapSmashable.intactPixelmap = mapTxtLines.Next();
                  mapSmashable.numberOfTextureLevels = mapTxtLines.NextInt();
                  mapSmashable.textureLevels = new MapSmashableTextureLevel[mapSmashable.numberOfTextureLevels];
                  for (int j = 0; j < mapSmashable.numberOfTextureLevels; j++) {
                    MapSmashableTextureLevel mapSmashableTextureLevel = new MapSmashableTextureLevel();
                    mapSmashableTextureLevel.triggerThreshold = mapTxtLines.NextFloat();
                    mapSmashableTextureLevel.flags = mapTxtLines.NextInt();
                    mapSmashableTextureLevel.collisionType = mapTxtLines.Next();

                    mapSmashableTextureLevel.connotations = LoadConnotations(mapTxtLines);

                    int pixelmapCount = mapTxtLines.NextInt();
                    mapSmashableTextureLevel.pixelMaps = new string[pixelmapCount];
                    for (int k = 0; k < pixelmapCount; k++) {
                      mapSmashableTextureLevel.pixelMaps[k] = mapTxtLines.Next();
                    }

                    mapSmashable.textureLevels[j] = mapSmashableTextureLevel;
                  }
                  break;
                default:
                  throw new NotImplementedException($"Unsupported trigger mode: {mapSmashable.triggerMode}");
              }

              mapSmashable.reserved1 = mapTxtLines.NextInt();
              mapSmashable.reserved2 = mapTxtLines.NextInt();
              mapSmashable.reserved3 = mapTxtLines.NextInt();
              mapSmashable.reserved4 = mapTxtLines.NextInt();

              mapFile.smashables[i] = mapSmashable;
            }
            break;
          case MapBlockType.Peds:
            int pedCount = mapTxtLines.AsInt();
            mapFile.peds = new MapPed[pedCount];

            for (int i = 0; i < pedCount; i++) {
              mapFile.peds[i] = new MapPed {
                materialName = mapTxtLines.Next(),
                movementIndex = mapTxtLines.NextInt(),
                groupIndex = mapTxtLines.NextInt(),
                pedsPer100Sqm = mapTxtLines.NextFloat(),
              };

              int exclusionMaterialCount = mapTxtLines.NextInt();
              mapFile.peds[i].exclusionMaterials = new MapPedExclusionMaterial[exclusionMaterialCount];
              for (int j = 0; j < exclusionMaterialCount; j++) {
                mapFile.peds[i].exclusionMaterials[j] = new MapPedExclusionMaterial {
                  flags = mapTxtLines.NextInt(),
                  materialName = mapTxtLines.Next()
                };
              }

              // TODO: missing exception materials
              int exceptionMaterialCount = mapTxtLines.NextInt();
            }
            break;
          case MapBlockType.AdditionalActor:
            mapFile.additionalActor = mapTxtLines.AsString();
            break;
          case MapBlockType.Horizon:
            mapFile.horizon = new MapHorizon {
              skyTextureName = mapTxtLines.AsString(),
              horizontalRepetitions = mapTxtLines.NextInt(),
              verticalSizeDegrees = mapTxtLines.NextInt(),
              horizonPositionBelowTop = mapTxtLines.NextInt(),
              depthCueMode = mapTxtLines.Next(),
            };

            float[] fogDarkness = mapTxtLines.NextFloatArray();
            mapFile.horizon.fogAmount = (int)fogDarkness[0];
            mapFile.horizon.darknessAmount = (int)fogDarkness[1];

            mapFile.horizon.depthCueColor = mapTxtLines.NextColorRGB();
            break;
          case MapBlockType.DefaultEngineNoise:
            mapFile.defaultEngineNoise = mapTxtLines.AsInt();
            break;
          case MapBlockType.SpecialEffectsVolumes:
            int specialEffectVolumeCount = mapTxtLines.AsInt();
            mapFile.specialEffectsVolumes = new MapSpecialEffectsVolume[specialEffectVolumeCount];

            for (int i = 0; i < specialEffectVolumeCount; i++) {
              MapSpecialEffectsVolume volume = new MapSpecialEffectsVolume();
              volume.type = mapTxtLines.Next();
              if (volume.type == "BOX") {
                volume.p1 = mapTxtLines.NextVector3();
                volume.p2 = mapTxtLines.NextVector3();
                volume.p3 = mapTxtLines.NextVector3();
                volume.p4 = mapTxtLines.NextVector3();
              }

              volume.gravityMultiplier = mapTxtLines.NextFloat();
              volume.viscosityMultiplier = mapTxtLines.NextFloat();
              volume.carDamagePerMs = mapTxtLines.NextFloat();
              volume.pedDamagePerMs = mapTxtLines.NextFloat();
              volume.cameraEffectIndex = mapTxtLines.NextInt();
              volume.skyColor = mapTxtLines.NextInt();
              volume.windscreenTexture = mapTxtLines.Next();
              volume.entrySoundId = mapTxtLines.NextInt();
              volume.exitSoundId = mapTxtLines.NextInt();
              volume.engineNoiseIndex = mapTxtLines.NextInt();
              volume.materialIndex = mapTxtLines.NextInt();

              if (volume.type != "DEFAULT") {
                volume.soundType = mapTxtLines.Next();
                if (volume.soundType == "SCATTERED") {
                  volume.scatterMode = mapTxtLines.Next();

                  float[] minMaxVolume = mapTxtLines.NextFloatArray();
                  volume.minVolume = minMaxVolume[0];
                  volume.maxVolume = minMaxVolume[1];

                  volume.count = mapTxtLines.NextInt();

                  int scatterSoundCount = mapTxtLines.NextInt();
                  volume.scatterSounds = new int[scatterSoundCount];
                  for (int j = 0; j < scatterSoundCount; j++) {
                    volume.scatterSounds[j] = mapTxtLines.NextInt();
                  }
                }
              }

              mapFile.specialEffectsVolumes[i] = volume;
            }
            break;
          case MapBlockType.SoundGenerators:
            int soundGeneratorCount = mapTxtLines.AsInt();
            break;
          case MapBlockType.ReflectiveWindscreenSpecs:
            mapFile.reflectiveWindscreenSpecs = new MapReflectiveWindscreenSpecs();
            mapFile.reflectiveWindscreenSpecs.defaultScreenMaterial = mapTxtLines.AsString();
            mapFile.reflectiveWindscreenSpecs.darknessScreenMaterial = mapTxtLines.Next();
            mapFile.reflectiveWindscreenSpecs.fogScreenMaterial = mapTxtLines.Next();
            mapFile.reflectiveWindscreenSpecs.areasWithDifferentScreens = mapTxtLines.NextInt();
            break;
          case MapBlockType.Minimap:
            mapFile.minimap = new MapMinimap();
            mapFile.minimap.mapPixelmapName = mapTxtLines.AsString();
            mapFile.minimap.worldMapTransformationRow0 = mapTxtLines.NextVector3();
            mapFile.minimap.worldMapTransformationRow1 = mapTxtLines.NextVector3();
            mapFile.minimap.worldMapTransformationRow2 = mapTxtLines.NextVector3();
            mapFile.minimap.worldMapTransformationOffset = mapTxtLines.NextVector3();
            break;
          case MapBlockType.Funks:
            if (mapTxtLines.AsString() != "START OF FUNK") {
              throw new Exception("Expected START OF FUNK");
            }

            string currentLine = mapTxtLines.Next();
            while (currentLine != "END OF FUNK") {
              currentLine = mapTxtLines.Next();
            }

            break;
          case MapBlockType.Grooves:
            if (mapTxtLines.AsString() != "START OF GROOVE") {
              throw new Exception("Expected START OF GROOVE");
            }
            currentLine = mapTxtLines.Next();
            while (currentLine != "END OF GROOVE") {
              currentLine = mapTxtLines.Next();
            }
            break;
          case MapBlockType.OpponentPaths:
            mapFile.opponentPaths = new MapOpponentPaths();

            if (mapTxtLines.AsString() != "START OF OPPONENT PATHS") {
              throw new Exception("Expected START OF OPPONENT PATHS");
            }

            int pathNodesCount = mapTxtLines.NextInt();
            mapFile.opponentPaths.nodes = new Vector3[pathNodesCount];
            for (int i = 0; i < pathNodesCount; i++) {
              mapFile.opponentPaths.nodes[i] = mapTxtLines.NextVector3();
            }

            int pathSectionCount = mapTxtLines.NextInt();
            mapFile.opponentPaths.sections = new MapOpponentPathSection[pathSectionCount];
            for (int i = 0; i < pathSectionCount; i++) {
              MapOpponentPathSection section = new MapOpponentPathSection();
              float[] values = mapTxtLines.NextFloatArray();

              section.from = (int)values[0];
              section.to = (int)values[1];
              section.unknown1 = (int)values[2];
              section.unknown2 = (int)values[3];
              section.unknown3 = (int)values[4];
              section.unknown4 = (int)values[5];
              section.unknown5 = values[6];
              section.unknown6 = values[7] != 0;

              mapFile.opponentPaths.sections[i] = section;
            }

            mapFile.opponentPaths.copStartPoints = mapTxtLines.NextInt();

            if (mapTxtLines.Next() != "END OF OPPONENT PATHS") {
              throw new Exception("Expected END OF OPPONENT PATHS");
            }
            break;
          case MapBlockType.DronePaths:
            mapFile.dronePaths = new MapDronePaths();

            if (mapTxtLines.AsString() != "START OF DRONE PATHS") {
              throw new Exception("Expected START OF DRONE PATHS");
            }

            mapFile.dronePaths.version = mapTxtLines.NextInt();

            int dronePathCount = mapTxtLines.NextInt();
            mapFile.dronePaths.paths = new MapDronePath[dronePathCount];
            for (int i = 0; i < dronePathCount; i++) {
              MapDronePath path = new MapDronePath();
              path.position = mapTxtLines.NextVector3();
              path.droneName = mapTxtLines.Next();
              path.unknown1 = mapTxtLines.NextInt();
              int unknown2Count = mapTxtLines.NextInt();
              path.unknown2 = new Vector4[unknown2Count];
              for (int j = 0; j < unknown2Count; j++) {
                path.unknown2[j] = mapTxtLines.NextVector4();
              }
              mapFile.dronePaths.paths[i] = path;
            }
            if (mapTxtLines.Next() != "END OF DRONE PATHS") {
              throw new Exception("Expected END OF DRONE PATHS");
            }
            break;
          case MapBlockType.MaterialModifiers:
            int materialModifierCount = mapTxtLines.AsInt();
            mapFile.materialModifiers = new MapMaterialModifier[materialModifierCount];
            for (int i = 0; i < materialModifierCount; i++) {
              MapMaterialModifier modifier = new MapMaterialModifier();
              modifier.carWallFriction = mapTxtLines.NextFloat();
              modifier.tireWallFriction = mapTxtLines.NextFloat();
              modifier.downForce = mapTxtLines.NextFloat();
              modifier.bumpiness = mapTxtLines.NextFloat();
              modifier.tireSoundIndex = mapTxtLines.NextInt();
              modifier.crashSoundIndex = mapTxtLines.NextInt();
              modifier.scrapeSoundIndex = mapTxtLines.NextInt();
              modifier.sparkiness = mapTxtLines.NextFloat();
              modifier.roomForExpansion = mapTxtLines.NextInt();
              modifier.skidmarkMaterial = mapTxtLines.Next();
              mapFile.materialModifiers[i] = modifier;
            }
            break;
          case MapBlockType.NonCarObjects:
            int nonCarObjectCount = mapTxtLines.AsInt();
            mapFile.nonCarObjects = new string[nonCarObjectCount];
            for (int i = 0; i < nonCarObjectCount; i++) {
              mapFile.nonCarObjects[i] = mapTxtLines.Next();
            }
            break;
          case MapBlockType.DustShadeTables:
            int dustShadeTableCount = mapTxtLines.AsInt();
            mapFile.dustShadeTables = new MapDustShadeTable[dustShadeTableCount];
            for (int i = 0; i < dustShadeTableCount; i++) {
              MapDustShadeTable table = new MapDustShadeTable();
              table.rgb = mapTxtLines.NextColorRGB();
              table.strength = mapTxtLines.NextVector3();
              mapFile.dustShadeTables[i] = table;
            }
            break;
          case MapBlockType.NetworkStartPoints:
            int networkStartPointCount = mapTxtLines.AsInt();
            mapFile.networkStartPoints = new MapNetworkStartPoint[networkStartPointCount];
            for (int i = 0; i < networkStartPointCount; i++) {
              MapNetworkStartPoint startPoint = new MapNetworkStartPoint();
              startPoint.position = mapTxtLines.NextVector3();
              startPoint.rotationDegrees = mapTxtLines.NextInt();
              mapFile.networkStartPoints[i] = startPoint;
            }
            break;
          case MapBlockType.SplashFiles:
            int splashFileCount = mapTxtLines.AsInt();
            mapFile.splashFiles = new string[splashFileCount];
            for (int i = 0; i < splashFileCount; i++) {
              mapFile.splashFiles[i] = mapTxtLines.Next();
            }
            break;
          case MapBlockType.SelfReference:
            int mapTxtReferenceCount = mapTxtLines.AsInt();
            mapFile.mapTxtReferences = new string[mapTxtReferenceCount];
            for (int i = 0; i < mapTxtReferenceCount; i++) {
              mapFile.mapTxtReferences[i] = mapTxtLines.Next();
            }
            break;
        }

        currentBlock++;
      }

      return mapFile;
    }

    private MapSmashableExplosion LoadExplosion(TxtEnumerator mapTxtLines) {
      MapSmashableExplosion explosion = new MapSmashableExplosion();
      explosion.count = mapTxtLines.NextIntArray();
      explosion.startDelay = mapTxtLines.NextVector2();
      explosion.offset = mapTxtLines.NextVector3();
      explosion.xFactor = mapTxtLines.NextVector2();
      explosion.yFactor = mapTxtLines.NextVector2();
      explosion.zFactor = mapTxtLines.NextVector2();
      explosion.framerate = mapTxtLines.NextVector2();
      explosion.scalingFactor = mapTxtLines.NextVector2();
      explosion.rotationMode = mapTxtLines.Next();

      int frameCount = mapTxtLines.NextInt();
      explosion.frames = new MapSmashableExplosionFrame[frameCount];
      for (int i = 0; i < frameCount; i++) {
        MapSmashableExplosionFrame frame = new MapSmashableExplosionFrame();
        frame.opacity = mapTxtLines.NextInt();
        frame.pixelmap = mapTxtLines.Next();
        explosion.frames[i] = frame;
      }

      return explosion;
    }

    private MapSmashableNonCarCuboid LoadSmashableNonCarCuboid(TxtEnumerator mapTxtLines) {
      MapSmashableNonCarCuboid cuboid = new MapSmashableNonCarCuboid();
      cuboid.delay = mapTxtLines.NextVector2();
      cuboid.cuboidCoordinateSystem = mapTxtLines.Next();
      cuboid.nonCarNumber = mapTxtLines.NextInt();
      cuboid.min = mapTxtLines.NextVector3();
      cuboid.max = mapTxtLines.NextVector3();

      int[] minMaxSpeed = mapTxtLines.NextIntArray();
      cuboid.minSpeed = minMaxSpeed[0];
      cuboid.maxSpeed = minMaxSpeed[1];

      cuboid.impacteeVelocityFactor = mapTxtLines.NextFloat();
      cuboid.maxRandomVelocity = mapTxtLines.NextFloat();
      cuboid.maxRandomUpVelocity = mapTxtLines.NextFloat();
      cuboid.maxRandomNormalVelocity = mapTxtLines.NextFloat();
      cuboid.maxRandomSpinRate = mapTxtLines.NextFloat();
      return cuboid;
    }

    private MapSmashableActivationCuboid LoadSmashableActivationCuboid(TxtEnumerator mapTxtLines) {
      MapSmashableActivationCuboid cuboid = new MapSmashableActivationCuboid();
      cuboid.delay = mapTxtLines.NextVector2();
      cuboid.name = mapTxtLines.Next();
      cuboid.cuboidCoordinateSystem = mapTxtLines.Next();
      cuboid.min = mapTxtLines.NextVector3();
      cuboid.max = mapTxtLines.NextVector3();
      cuboid.impactDirection = mapTxtLines.Next();
      cuboid.impactStrength = mapTxtLines.NextFloat();
      return cuboid;
    }

    private MapConnotations LoadConnotations(TxtEnumerator mapTxtLines) {
      MapConnotations connotations = new MapConnotations();
      int soundCount = mapTxtLines.NextInt();
      connotations.sounds = new int[soundCount];
      for (int i = 0; i < soundCount; i++) {
        connotations.sounds[i] = mapTxtLines.NextInt();
      }

      int shrapnelCount = mapTxtLines.NextInt();
      connotations.shrapnel = new MapSmashableShrapnel[shrapnelCount];
      for (int i = 0; i < shrapnelCount; i++) {
        MapSmashableShrapnel shrapnel = new MapSmashableShrapnel();
        shrapnel.shrapnelType = mapTxtLines.Next();

        float[] minMaxSpeed = mapTxtLines.NextFloatArray();
        shrapnel.minSpeed = minMaxSpeed[0];
        shrapnel.maxSpeed = minMaxSpeed[1];

        shrapnel.impacteeVelocityFactor = mapTxtLines.NextFloat();
        shrapnel.maxRandomVelocity = mapTxtLines.NextFloat();
        shrapnel.maxRandomUpVelocity = mapTxtLines.NextFloat();
        shrapnel.maxRandomNormalVelocity = mapTxtLines.NextFloat();
        shrapnel.maxRandomSpinRate = mapTxtLines.NextFloat();

        if (shrapnel.shrapnelType != "shards") {
          shrapnel.initialPlacementMode = mapTxtLines.Next();

          if (shrapnel.initialPlacementMode == "sphereclumped") {
            shrapnel.clumpoingRadius = mapTxtLines.NextFloat();
            shrapnel.clumpingCenter = mapTxtLines.Next();
          }
        }

        if (shrapnel.shrapnelType != "noncars") {
          shrapnel.time = mapTxtLines.NextVector2();
        }

        if (shrapnel.shrapnelType == "shards") {
          shrapnel.cutLength = mapTxtLines.NextFloat();
          shrapnel.flags = mapTxtLines.NextInt();
          shrapnel.materialName = mapTxtLines.Next();
        } else if (shrapnel.shrapnelType == "ghostparts") {
          int[] counts = mapTxtLines.NextIntArray();
          shrapnel.minNumber = counts[0];
          shrapnel.maxNumber = counts.Length == 2 ? counts[1] : counts[0];

          int actorCount = mapTxtLines.NextInt();
          List<string> ghostPartActors = new List<string>();
          if (actorCount > 0) {
            for (int j = 0; j < actorCount; j++) {
              ghostPartActors.Add(mapTxtLines.Next());
            }
          } else {
            ghostPartActors.Add(mapTxtLines.Next());
          }

          shrapnel.ghostPartActors = ghostPartActors.ToArray();
        } else if (shrapnel.shrapnelType == "noncars") {
          int[] count = mapTxtLines.NextIntArray();
          shrapnel.minNumber = count[0];
          shrapnel.maxNumber = count[1];
          shrapnel.fireChance = mapTxtLines.NextInt();

          if (shrapnel.fireChance > 0) {
            shrapnel.smokeColumnCount = mapTxtLines.NextInt();
            int[] smokinessLevels = mapTxtLines.NextIntArray();
            shrapnel.minSmokiness = (SmokeColumnSmokiness)smokinessLevels[0];
            shrapnel.maxSmokiness = (SmokeColumnSmokiness)smokinessLevels[1];
          }

          shrapnel.actor = mapTxtLines.Next();
          int actorCount = mapTxtLines.NextInt();
          shrapnel.actors = new MapSmashableShrapnelActor[actorCount];
          for (int j = 0; j < actorCount; j++) {
            MapSmashableShrapnelActor actor = new MapSmashableShrapnelActor();
            actor.name = mapTxtLines.Next();
            actor.fileName = mapTxtLines.Next();
            shrapnel.actors[j] = actor;
          }
        } else {
          shrapnel.minNumber = mapTxtLines.NextInt();
          shrapnel.maxNumber = mapTxtLines.NextInt();
          shrapnel.actor = mapTxtLines.Next();
        }

        connotations.shrapnel[i] = shrapnel;
      }

      int explosionCount = mapTxtLines.NextInt();
      connotations.explosions = new MapSmashableExplosion[explosionCount];
      for (int j = 0; j < explosionCount; j++) {
        connotations.explosions[j] = LoadExplosion(mapTxtLines);
      }

      connotations.slickMaterial = mapTxtLines.Next();

      int nonCarCuboidCount = mapTxtLines.NextInt();
      connotations.nonCarCuboids = new MapSmashableNonCarCuboid[nonCarCuboidCount];
      for (int i = 0; i < nonCarCuboidCount; i++) {
        connotations.nonCarCuboids[i] = LoadSmashableNonCarCuboid(mapTxtLines);
      }

      int activationCuboidCount = mapTxtLines.NextInt();
      connotations.activationCuboids = new MapSmashableActivationCuboid[activationCuboidCount];
      for (int i = 0; i < activationCuboidCount; i++) {
        connotations.activationCuboids[i] = LoadSmashableActivationCuboid(mapTxtLines);
      }

      connotations.extensionFlags = mapTxtLines.NextInt();
      connotations.roomTurnOnCode = mapTxtLines.NextInt();
      connotations.awardCode = mapTxtLines.Next();

      if (connotations.awardCode != "none") {
        connotations.pointsAwarded = mapTxtLines.NextInt();
        connotations.timeAwarded = mapTxtLines.NextInt();
        connotations.hudIndex = mapTxtLines.NextInt();
        connotations.fancyHudIndex = mapTxtLines.NextInt();
      }

      int runtimeVariableChangeCount = mapTxtLines.NextInt();
      connotations.runtimeVariableChanges = new string[runtimeVariableChangeCount];
      for (int i = 0; i < runtimeVariableChangeCount; i++) {
        connotations.runtimeVariableChanges[i] = mapTxtLines.Next();
      }

      return connotations;
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

﻿
namespace TuxPlanetSpeedrunAnyPercentLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class LevelConfiguration_Level5 : ILevelConfiguration
	{
		private List<CompositeTilemap.TilemapWithOffset> normalizedTilemaps;
		private int startingXMibi;
		private IBackground background;
		private Difficulty difficulty;

		private const string LEVEL_SUBFOLDER = "Level5/";

		public LevelConfiguration_Level5(
			Difficulty difficulty,
			IReadOnlyDictionary<string, MapDataHelper.Map> mapInfo, 
			IDTDeterministicRandom random)
		{
			Tuple<List<CompositeTilemap.TilemapWithOffset>, int> result = ConstructUnnormalizedTilemaps(
				difficulty: difficulty,
				mapInfo: mapInfo,
				random: random);

			List<CompositeTilemap.TilemapWithOffset> unnormalizedTilemaps = result.Item1;

			this.normalizedTilemaps = new List<CompositeTilemap.TilemapWithOffset>(CompositeTilemap.NormalizeTilemaps(tilemaps: unnormalizedTilemaps));

			this.startingXMibi = result.Item2;

			this.background = BackgroundUtil.GetRandomBackground(random: random);

			this.difficulty = difficulty;
		}

		private static Tuple<List<CompositeTilemap.TilemapWithOffset>, int> ConstructUnnormalizedTilemaps(
			Difficulty difficulty,
			IReadOnlyDictionary<string, MapDataHelper.Map> mapInfo,
			IDTDeterministicRandom random)
		{
			GameMusic gameMusic = LevelConfigurationHelper.GetRandomGameMusic(random: random);

			EnemyIdGenerator enemyIdGenerator = new EnemyIdGenerator();

			List<CompositeTilemap.TilemapWithOffset> list = new List<CompositeTilemap.TilemapWithOffset>();

			CompositeTilemap.TilemapWithOffset startTilemap = new CompositeTilemap.TilemapWithOffset(
				tilemap: MapDataTilemapGenerator.GetTilemap(
					data: mapInfo[LEVEL_SUBFOLDER + "A_Start"],
					enemyIdGenerator: enemyIdGenerator,
					cutsceneName: null,
					scalingFactorScaled: 3 * 128,
					gameMusic: gameMusic),
				xOffset: 0,
				yOffset: 0,
				alwaysIncludeTilemap: false);

			list.Add(startTilemap);

			int xOffset = startTilemap.Tilemap.GetWidth() + (3 + random.NextInt(5)) * 16 * 3;

			int yOffset = 3 * 16 * 3;

			bool hasAddedCheckpoint1 = false;
			bool hasAddedCheckpoint2 = false;
			bool hasAddedCheckpoint3 = false;

			List<string> mapInfoNames;

			switch (difficulty)
			{
				case Difficulty.Easy:
					mapInfoNames = new List<string>()
					{
						"B_Fragment1Easy",
						"B_Fragment2EasyNormal",
						"B_Fragment3EasyNormal",
						"B_Fragment4Easy",
						"B_Fragment5Easy",
						"B_Fragment6EasyNormal",
						"B_Fragment7Easy",
						"B_Fragment8EasyNormal",
						"B_Fragment9Easy",
						"B_Fragment10EasyNormalHard",
						"B_Fragment11Easy",
						"B_Fragment12EasyNormalHard"
					};
					break;
				case Difficulty.Normal:
					mapInfoNames = new List<string>()
					{
						"B_Fragment1NormalHard",
						"B_Fragment2EasyNormal",
						"B_Fragment3EasyNormal",
						"B_Fragment4Normal",
						"B_Fragment5NormalHard",
						"B_Fragment6EasyNormal",
						"B_Fragment7Normal",
						"B_Fragment8EasyNormal",
						"B_Fragment9Normal",
						"B_Fragment10EasyNormalHard",
						"B_Fragment11Normal",
						"B_Fragment12EasyNormalHard"
					};
					break;
				case Difficulty.Hard:
					mapInfoNames = new List<string>()
					{
						"B_Fragment1NormalHard",
						"B_Fragment2Hard",
						"B_Fragment3Hard",
						"B_Fragment4Hard",
						"B_Fragment5NormalHard",
						"B_Fragment6Hard",
						"B_Fragment7Hard",
						"B_Fragment8Hard",
						"B_Fragment9Hard",
						"B_Fragment10EasyNormalHard",
						"B_Fragment11Hard",
						"B_Fragment12EasyNormalHard"
					};
					break;
				default:
					throw new Exception();
			}

			while (true)
			{
				if (xOffset >= 450 * 16 * 3)
					break;

				string mapInfoName = LEVEL_SUBFOLDER + mapInfoNames[random.NextInt(mapInfoNames.Count)];

				Tilemap fragmentTilemap;

				if (xOffset >= 130 * 16 * 3 && !hasAddedCheckpoint1)
				{
					hasAddedCheckpoint1 = true;
					fragmentTilemap = MapDataTilemapGenerator.GetTilemap(
						data: mapInfo[LEVEL_SUBFOLDER + "B_Checkpoint"],
						enemyIdGenerator: enemyIdGenerator,
						cutsceneName: null,
						scalingFactorScaled: 3 * 128,
						gameMusic: gameMusic);
				}
				else if (xOffset >= 235 * 16 * 3 && !hasAddedCheckpoint2)
				{
					hasAddedCheckpoint2 = true;
					fragmentTilemap = MapDataTilemapGenerator.GetTilemap(
						data: mapInfo[LEVEL_SUBFOLDER + "B_Checkpoint"],
						enemyIdGenerator: enemyIdGenerator,
						cutsceneName: null,
						scalingFactorScaled: 3 * 128,
						gameMusic: gameMusic);
				}
				else if (xOffset >= 340 * 16 * 3 && !hasAddedCheckpoint3)
				{
					hasAddedCheckpoint3 = true;
					fragmentTilemap = MapDataTilemapGenerator.GetTilemap(
						data: mapInfo[LEVEL_SUBFOLDER + "B_Checkpoint"],
						enemyIdGenerator: enemyIdGenerator,
						cutsceneName: null,
						scalingFactorScaled: 3 * 128,
						gameMusic: gameMusic);
				}
				else
				{
					fragmentTilemap = MapDataTilemapGenerator.GetTilemap(
						data: mapInfo[mapInfoName],
						enemyIdGenerator: enemyIdGenerator,
						cutsceneName: null,
						scalingFactorScaled: 3 * 128,
						gameMusic: gameMusic);
				}

				CompositeTilemap.TilemapWithOffset fragmentTilemapWithOffset = new CompositeTilemap.TilemapWithOffset(
					tilemap: fragmentTilemap,
					xOffset: xOffset,
					yOffset: yOffset,
					alwaysIncludeTilemap: false);

				list.Add(fragmentTilemapWithOffset);

				xOffset += fragmentTilemap.GetWidth() + (3 + random.NextInt(5)) * 16 * 3;

				if (yOffset == 5 * 16 * 3)
					yOffset += (random.NextInt(3) - 2) * 16 * 3;
				else if (yOffset == 0)
					yOffset += random.NextInt(3) * 16 * 3;
				else
					yOffset += (random.NextInt(5) - 2) * 16 * 3;

				if (yOffset > 5 * 16 * 3)
					yOffset = 5 * 16 * 3;

				if (yOffset < 0)
					yOffset = 0;
			}

			CompositeTilemap.TilemapWithOffset finishTilemap = new CompositeTilemap.TilemapWithOffset(
				tilemap: MapDataTilemapGenerator.GetTilemap(
					data: mapInfo[LEVEL_SUBFOLDER + "C_Finish"],
					enemyIdGenerator: enemyIdGenerator,
					cutsceneName: null,
					scalingFactorScaled: 3 * 128,
					gameMusic: gameMusic),
				xOffset: xOffset,
				yOffset: 0,
				alwaysIncludeTilemap: false);

			list.Add(finishTilemap);

			int startingXMibi = finishTilemap.XOffset * 1024;

			return new Tuple<List<CompositeTilemap.TilemapWithOffset>, int>(list, startingXMibi);
		}

		public IReadOnlyDictionary<string, string> GetCustomLevelInfo()
		{
			return new Dictionary<string, string>();
		}

		public IBackground GetBackground()
		{
			return this.background;
		}

		public ITilemap GetTilemap(int? tuxX, int? tuxY, int? cameraX, int? cameraY, int windowWidth, int windowHeight, IReadOnlyList<string> levelFlags, MapKeyState mapKeyState)
		{
			ITilemap tilemap = LevelConfigurationHelper.GetTilemap(
				normalizedTilemaps: this.normalizedTilemaps,
				tuxX: tuxX,
				tuxY: tuxY,
				cameraX: cameraX,
				cameraY: cameraY,
				mapKeyState: mapKeyState,
				windowWidth: windowWidth,
				windowHeight: windowHeight);

			return new Level5Tilemap(
				mapTilemap: tilemap,
				startingXMibiOfFirstSpike: (windowWidth * 3) << 10,
				startingXMibi: this.startingXMibi,
				endingXMibi: -5 * 48 * 1024,
				difficulty: this.difficulty);
		}

		public CameraState GetCameraState(
			int tuxXMibi,
			int tuxYMibi,
			Tuple<int, int> tuxTeleportStartingLocation,
			int? tuxTeleportInProgressElapsedMicros,
			ITilemap tilemap,
			int windowWidth,
			int windowHeight,
			IReadOnlyList<string> levelFlags)
		{
			CameraState cameraState = CameraStateProcessing.ComputeCameraState(
				tuxXMibi: tuxXMibi,
				tuxYMibi: tuxYMibi,
				tuxTeleportStartingLocation: tuxTeleportStartingLocation,
				tuxTeleportInProgressElapsedMicros: tuxTeleportInProgressElapsedMicros,
				tilemap: tilemap,
				windowWidth: windowWidth,
				windowHeight: windowHeight);

			return CameraState.GetCameraState(
				x: cameraState.X,
				y: windowHeight >> 1);
		}
	}
}

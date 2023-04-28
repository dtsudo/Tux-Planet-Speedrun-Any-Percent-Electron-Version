﻿
namespace TuxPlanetSpeedrunAnyPercentLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class GameFrame : IFrame<GameImage, GameFont, GameSound, GameMusic>
	{
		private GlobalState globalState;
		private SessionState sessionState;

		private GameLogicState gameLogic;
		private bool hasStartedLevelTransition;
		private GameLogicState savedGameLogicState;

		private int extraElapsedMicros;

		private int numberOfSkippedFrames;

		private Dictionary<int, Move> moveHistory;
		private int lastMoveOfHistory;
		private bool useSavedMoves;

		private GameFrame(
			GlobalState globalState, 
			SessionState sessionState,
			GameLogicState gameLogicState)
		{
			this.globalState = globalState;
			this.sessionState = sessionState;
			this.gameLogic = gameLogicState;
			this.hasStartedLevelTransition = false;
			this.savedGameLogicState = null;
			this.extraElapsedMicros = 0;
			this.numberOfSkippedFrames = 0;
			this.moveHistory = new Dictionary<int, Move>();
			this.lastMoveOfHistory = 0;
			this.useSavedMoves = false;
		}

		public static IFrame<GameImage, GameFont, GameSound, GameMusic> GetGameFrame(
			GlobalState globalState,
			SessionState sessionState,
			GameLogicState gameLogicState,
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			GameFrame gameFrame = new GameFrame(globalState: globalState, sessionState: sessionState, gameLogicState: gameLogicState);
			gameFrame.GetNextFrame(
				keyboardInput: new EmptyKeyboard(),
				mouseInput: new EmptyMouse(),
				previousKeyboardInput: new EmptyKeyboard(),
				previousMouseInput: new EmptyMouse(),
				displayProcessing: displayProcessing,
				soundOutput: soundOutput,
				musicProcessing: musicProcessing);

			return gameFrame;
		}

		public IFrame<GameImage, GameFont, GameSound, GameMusic> GetNextFrame(
			IKeyboard keyboardInput, 
			IMouse mouseInput, 
			IKeyboard previousKeyboardInput, 
			IMouse previousMouseInput, 
			IDisplayProcessing<GameImage> displayProcessing,
			ISoundOutput<GameSound> soundOutput,
			IMusicProcessing musicProcessing)
		{
			this.sessionState.AddElapsedMillis(this.globalState.ElapsedMicrosPerFrame / 1000);

			this.extraElapsedMicros = this.extraElapsedMicros + (this.globalState.ElapsedMicrosPerFrame % 1000);
			if (this.extraElapsedMicros >= 1000)
			{
				this.extraElapsedMicros -= 1000;
				this.sessionState.AddElapsedMillis(1);
			}

			if (keyboardInput.IsPressed(Key.Esc) && !previousKeyboardInput.IsPressed(Key.Esc) && !this.gameLogic.Tux.IsDead)
				return new PauseMenuFrame(
					globalState: this.globalState, 
					sessionState: this.sessionState, 
					underlyingFrame: this, 
					currentLevelForRestartLevelOption: this.gameLogic.Level,
					currentDifficultyForRestartLevelOption: this.gameLogic.Difficulty,
					showRestartLevelOption: true, 
					showBackToMapOption: true,
					showToggleInputReplayFunctionalityOption: this.gameLogic.CanUseSaveStates,
					showBackToTitleScreenOption: false);

			Move move = new Move(
				jumped: keyboardInput.IsPressed(Key.Z),
				teleported: keyboardInput.IsPressed(Key.X),
				arrowLeft: keyboardInput.IsPressed(Key.LeftArrow),
				arrowRight: keyboardInput.IsPressed(Key.RightArrow),
				arrowUp: keyboardInput.IsPressed(Key.UpArrow),
				arrowDown: keyboardInput.IsPressed(Key.DownArrow),
				respawn: keyboardInput.IsPressed(Key.Esc));

			if (move.Jumped)
				this.sessionState.AddRandomSeed(7);
			if (move.ArrowLeft)
				this.sessionState.AddRandomSeed(11);
			if (move.ArrowRight)
				this.sessionState.AddRandomSeed(17);

			bool shouldExecuteFrame;
			bool shouldEndLevel;

			if (keyboardInput.IsPressed(Key.Shift) && this.gameLogic.CanUseTimeSlowdown)
			{
				this.numberOfSkippedFrames++;
				if (this.numberOfSkippedFrames == 5)
				{
					this.numberOfSkippedFrames = 0;
					shouldExecuteFrame = true;
				}
				else
					shouldExecuteFrame = false;
			}
			else
				shouldExecuteFrame = true;

			if (shouldExecuteFrame)
			{
				Move moveToUse;

				bool didUserPressKey = move.Jumped || move.Teleported || move.ArrowLeft || move.ArrowRight || move.ArrowUp || move.ArrowDown || move.Respawn;

				if (this.useSavedMoves && !didUserPressKey && this.sessionState.ShouldReplayInputAfterLoadingSaveState && this.lastMoveOfHistory >= this.gameLogic.FrameCounter && this.moveHistory.ContainsKey(this.gameLogic.FrameCounter))
					moveToUse = this.moveHistory[this.gameLogic.FrameCounter];
				else
				{
					this.moveHistory[this.gameLogic.FrameCounter] = move;
					this.lastMoveOfHistory = this.gameLogic.FrameCounter;
					moveToUse = move;
					this.useSavedMoves = false;
				}

				GameLogicStateProcessing.Result result = GameLogicStateProcessing.ProcessFrame(
					gameLogicState: this.gameLogic,
					move: moveToUse,
					debugMode: this.globalState.DebugMode,
					debug_tuxInvulnerable: this.globalState.Debug_TuxInvulnerable,
					debugKeyboardInput: keyboardInput,
					debugPreviousKeyboardInput: previousKeyboardInput,
					displayProcessing: displayProcessing,
					soundOutput: soundOutput,
					elapsedMicrosPerFrame: this.globalState.ElapsedMicrosPerFrame);

				this.gameLogic = result.NewGameLogicState;

				if (result.PlayMusic != null)
					this.globalState.MusicPlayer.SetMusic(music: result.PlayMusic.Value, volume: 100);

				if (result.ShouldStopMusic)
					this.globalState.MusicPlayer.StopMusic();

				shouldEndLevel = result.EndLevel;
			}
			else
				shouldEndLevel = false;

			if (keyboardInput.IsPressed(Key.S) && !previousKeyboardInput.IsPressed(Key.S) && this.gameLogic.CanUseSaveStates && !this.gameLogic.Tux.IsDead)
				this.savedGameLogicState = this.gameLogic;

			if (keyboardInput.IsPressed(Key.A) && !previousKeyboardInput.IsPressed(Key.A) && this.gameLogic.CanUseSaveStates && !shouldEndLevel && !this.hasStartedLevelTransition)
			{
				if (this.savedGameLogicState != null)
				{
					this.gameLogic = this.savedGameLogicState;
					this.useSavedMoves = true;
				}
			}

			if (this.globalState.DebugMode)
			{
				if (keyboardInput.IsPressed(Key.H) && !previousKeyboardInput.IsPressed(Key.H))
					this.globalState.Debug_ShowHitBoxes = !this.globalState.Debug_ShowHitBoxes;

				if (keyboardInput.IsPressed(Key.I) && !previousKeyboardInput.IsPressed(Key.I))
					this.globalState.Debug_TuxInvulnerable = !this.globalState.Debug_TuxInvulnerable;
			}

			if (shouldEndLevel && !this.hasStartedLevelTransition)
			{
				this.hasStartedLevelTransition = true;
				
				Level level = this.gameLogic.Level;
				Difficulty difficulty = this.gameLogic.Difficulty;

				bool isLastLevel = level.IsLastLevel();

				bool canUseSaveStatesAtStartOfLevel = this.sessionState.CanUseSaveStates;
				bool canUseTimeSlowdownAtStartOfLevel = this.sessionState.CanUseTimeSlowdown;
				bool canUseTeleportAtStartOfLevel = this.sessionState.CanUseTeleport;

				List<Move> moves = new List<Move>();

				for (int i = 0; i < this.gameLogic.FrameCounter; i++)
				{
					Move moveAtThisFrame;
					if (this.moveHistory.ContainsKey(i))
						moveAtThisFrame = this.moveHistory[i];
					else
						moveAtThisFrame = Move.EmptyMove();
					moves.Add(moveAtThisFrame);
				}

				Replay replay = new Replay(
					level: level,
					difficulty: difficulty,
					windowWidth: this.globalState.WindowWidth,
					windowHeight: this.globalState.WindowHeight,
					rngForGeneratingLevel: this.sessionState.GetRngUsedForGeneratingLevel(level: level, difficulty: difficulty),
					canUseSaveStatesAtStartOfLevel: canUseSaveStatesAtStartOfLevel,
					canUseTimeSlowdownAtStartOfLevel: canUseTimeSlowdownAtStartOfLevel,
					canUseTeleportAtStartOfLevel: canUseTeleportAtStartOfLevel,
					moves: moves);

				this.sessionState.CompleteLevel(
					level: level,
					difficulty: difficulty,
					replay: replay,
					canUseSaveStates: this.gameLogic.CanUseSaveStates,
					canUseTimeSlowdown: this.gameLogic.CanUseTimeSlowdown,
					canUseTeleport: this.gameLogic.CanUseTeleport);

				this.globalState.SaveData(sessionState: this.sessionState, soundVolume: soundOutput.GetSoundVolume());

				if (!isLastLevel)
				{
					return new LevelCompleteFrame(
						globalState: this.globalState,
						sessionState: this.sessionState,
						level: level,
						difficulty: difficulty,
						replay: replay,
						gameFrame: this);
				}

				return new LevelTransitionFrame(
					globalState: this.globalState,
					previousFrame: this,
					newFrame: new VictoryScreenFrame(globalState: this.globalState, sessionState: this.sessionState));
			}

			return this;
		}

		public void ProcessExtraTime(int milliseconds)
		{
		}

		public string GetClickUrl()
		{
			return null;
		}

		public HashSet<string> GetCompletedAchievements()
		{
			return null;
		}

		public string GetScore()
		{
			return null;
		}

		public void ProcessMusic()
		{
			this.globalState.ProcessMusic();
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			GameLogicStateProcessing.Render(
				gameLogicState: this.gameLogic, 
				displayOutput: displayOutput, 
				elapsedMillis: this.sessionState.ElapsedMillis,
				showElapsedTime: true,
				debug_showHitboxes: this.globalState.Debug_ShowHitBoxes);
		}

		public void RenderMusic(IMusicOutput<GameMusic> musicOutput)
		{
			this.globalState.RenderMusic(musicOutput: musicOutput);
		}
	}
}

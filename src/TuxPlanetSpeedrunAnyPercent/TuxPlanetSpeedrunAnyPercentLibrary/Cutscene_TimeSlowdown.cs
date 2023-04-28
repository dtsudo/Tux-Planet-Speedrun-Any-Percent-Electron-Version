﻿
namespace TuxPlanetSpeedrunAnyPercentLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class Cutscene_TimeSlowdown : ICutscene
	{
		private enum Status
		{
			A_Camera,
			B_Dialogue,
			C_KonqiDisappear,
			D_Camera
		}

		private bool isFirstFrame;
		private Status status;
		private int konqiDisappearElapsedMicros;
		private DialogueList dialogueList;

		private const int KONQI_DISAPPEAR_WAIT_TIME = 500 * 1000;

		private Cutscene_TimeSlowdown(
			bool isFirstFrame,
			Status status,
			int konqiDisappearElapsedMicros,
			DialogueList dialogueList)
		{
			this.isFirstFrame = isFirstFrame;
			this.status = status;
			this.konqiDisappearElapsedMicros = konqiDisappearElapsedMicros;
			this.dialogueList = dialogueList;
		}

		public static ICutscene GetCutscene()
		{
			List<Dialogue> dialogues = new List<Dialogue>();

			dialogues.Add(Dialogue.GetDialogue(
				x: 500,
				y: 460,
				width: 490,
				height: 40,
				text: "Hello again!"));

			dialogues.Add(Dialogue.GetDialogue(
				x: 10,
				y: 460,
				width: 172,
				height: 40,
				text: "Hello Konqi!"));

			dialogues.Add(Dialogue.GetDialogue(
				x: 500,
				y: 400,
				width: 490,
				height: 60,
				text: "I've brought a new power that \nshould help -- time dilation!"));

			dialogues.Add(Dialogue.GetDialogue(
				x: 500,
				y: 400,
				width: 490,
				height: 60,
				text: "Hold the left shift key to \nslow down time!"));

			DialogueList dialogueList = new DialogueList(dialogues: dialogues);

			return new Cutscene_TimeSlowdown(
				isFirstFrame: true,
				status: Status.A_Camera,
				konqiDisappearElapsedMicros: 0,
				dialogueList: dialogueList);
		}

		public string GetCutsceneName()
		{
			return CutsceneProcessing.TIME_SLOWDOWN_CUTSCENE;
		}

		public CutsceneProcessing.Result ProcessFrame(
			Move move,
			int tuxXMibi,
			int tuxYMibi,
			CameraState cameraState,
			int elapsedMicrosPerFrame,
			int windowWidth,
			int windowHeight,
			ITilemap tilemap,
			Difficulty difficulty,
			IReadOnlyList<IEnemy> enemies,
			IReadOnlyList<string> levelFlags)
		{
			CameraState newCameraState;
			DialogueList newDialogueList;
			Status newStatus;
			int newKonqiDisappearElapsedMicros = this.konqiDisappearElapsedMicros;
			List<IEnemy> newEnemies = new List<IEnemy>(enemies);
			List<string> newlyAddedLevelFlags = null;

			switch (this.status)
			{
				case Status.A_Camera:
					{
						CameraState destinationCameraState = CameraState.GetCameraState(
							x: Math.Min(
								(tuxXMibi >> 10) + 440,
								tilemap.GetWidth() - windowWidth / 2),
							y: tuxYMibi >> 10);
							
						newDialogueList = this.dialogueList;

						if (cameraState.X >= destinationCameraState.X && Math.Abs(cameraState.Y - destinationCameraState.Y) <= 5)
						{
							newCameraState = cameraState;
							newStatus = Status.B_Dialogue;
						}
						else
						{
							newCameraState = CameraState.SmoothCameraState(
								currentCamera: cameraState,
								destinationCamera: destinationCameraState,
								elapsedMicrosPerFrame: elapsedMicrosPerFrame,
								cameraSpeedInPixelsPerSecond: CameraState.CUTSCENE_CAMERA_SPEED);
							newStatus = Status.A_Camera;
						}

						break;
					}
				case Status.B_Dialogue:
					DialogueList.Result dialogueListResult = this.dialogueList.ProcessFrame(
						move: move,
						elapsedMicrosPerFrame: elapsedMicrosPerFrame);

					newCameraState = cameraState;
					newDialogueList = dialogueListResult.DialogueList;

					if (dialogueListResult.IsDone)
					{
						newStatus = Status.C_KonqiDisappear;
						if (newlyAddedLevelFlags == null)
							newlyAddedLevelFlags = new List<string>();
						newlyAddedLevelFlags.Add(EnemyKonqiCutscene.SHOULD_TELEPORT_OUT_DEFAULT_LEVEL_FLAG);
					}
					else
					{
						newStatus = Status.B_Dialogue;
					}

					break;
				case Status.C_KonqiDisappear:
					newKonqiDisappearElapsedMicros += elapsedMicrosPerFrame;

					newCameraState = cameraState;
					newDialogueList = this.dialogueList;

					if (newKonqiDisappearElapsedMicros >= KONQI_DISAPPEAR_WAIT_TIME)
						newStatus = Status.D_Camera;
					else
						newStatus = Status.C_KonqiDisappear;

					break;
				case Status.D_Camera:
					{
						CameraState destinationCameraState = CameraStateProcessing.ComputeCameraState(
							tuxXMibi: tuxXMibi,
							tuxYMibi: tuxYMibi,
							tuxTeleportStartingLocation: null,
							tuxTeleportInProgressElapsedMicros: null,
							tilemap: tilemap,
							windowWidth: windowWidth,
							windowHeight: windowHeight);

						newDialogueList = this.dialogueList;
						newStatus = Status.D_Camera;

						if (cameraState.X <= destinationCameraState.X)
						{
							return new CutsceneProcessing.Result(
								move: Move.EmptyMove(),
								cameraState: cameraState,
								enemies: newEnemies,
								newlyAddedLevelFlags: newlyAddedLevelFlags,
								cutscene: null,
								shouldGrantSaveStatePower: false,
								shouldGrantTimeSlowdownPower: true,
								shouldGrantTeleportPower: false);
						}
						else
						{
							newCameraState = CameraState.SmoothCameraState(
								currentCamera: cameraState,
								destinationCamera: destinationCameraState,
								elapsedMicrosPerFrame: elapsedMicrosPerFrame,
								cameraSpeedInPixelsPerSecond: CameraState.CUTSCENE_CAMERA_SPEED);
						}

						break;
					}
				default:
					throw new Exception();
			}

			return new CutsceneProcessing.Result(
				move: this.isFirstFrame
					? new Move(jumped: false, teleported: false, arrowLeft: false, arrowRight: true, arrowUp: false, arrowDown: false, respawn: false)
					: Move.EmptyMove(),
				cameraState: newCameraState,
				enemies: newEnemies,
				newlyAddedLevelFlags: newlyAddedLevelFlags,
				cutscene: new Cutscene_TimeSlowdown(isFirstFrame: false, status: newStatus, konqiDisappearElapsedMicros: newKonqiDisappearElapsedMicros, dialogueList: newDialogueList),
				shouldGrantSaveStatePower: false,
				shouldGrantTimeSlowdownPower: this.status == Status.C_KonqiDisappear || this.status == Status.D_Camera,
				shouldGrantTeleportPower: false);
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput, int windowWidth, int windowHeight)
		{
			if (this.status == Status.B_Dialogue)
				this.dialogueList.Render(displayOutput: displayOutput, windowWidth: windowWidth, windowHeight: windowHeight);
		}
	}
}

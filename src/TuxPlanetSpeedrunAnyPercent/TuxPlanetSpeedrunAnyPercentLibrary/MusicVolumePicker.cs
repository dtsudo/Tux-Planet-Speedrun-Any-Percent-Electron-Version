﻿
namespace TuxPlanetSpeedrunAnyPercentLibrary
{
	using DTLibrary;
	using System;

	public class MusicVolumePicker
	{
		private int _xPos;
		private int _yPos;

		private int _currentVolume;
		private int _unmuteVolume;

		private bool _isDraggingVolumeSlider;

		private SoundAndMusicVolumePicker.Color _color;

		public MusicVolumePicker(int xPos, int yPos, int initialVolume, SoundAndMusicVolumePicker.Color color)
		{
			this._xPos = xPos;
			this._yPos = yPos;

			this._currentVolume = initialVolume;
			this._unmuteVolume = this._currentVolume;

			this._isDraggingVolumeSlider = false;

			this._color = color;
		}

		public void ProcessFrame(
			IMouse mouseInput,
			IMouse previousMouseInput)
		{
			int mouseX = mouseInput.GetX();
			int mouseY = mouseInput.GetY();

			if (mouseInput.IsLeftMouseButtonPressed()
				&& !previousMouseInput.IsLeftMouseButtonPressed()
				&& this._xPos <= mouseX
				&& mouseX <= this._xPos + 40
				&& this._yPos <= mouseY
				&& mouseY <= this._yPos + 50)
			{
				if (this._currentVolume == 0)
				{
					this._currentVolume = this._unmuteVolume == 0 ? GlobalState.DEFAULT_VOLUME : this._unmuteVolume;
					this._unmuteVolume = this._currentVolume;
				}
				else
				{
					this._unmuteVolume = this._currentVolume;
					this._currentVolume = 0;
				}
			}

			if (mouseInput.IsLeftMouseButtonPressed()
				&& !previousMouseInput.IsLeftMouseButtonPressed()
				&& this._xPos + 50 <= mouseX
				&& mouseX <= this._xPos + 150
				&& this._yPos + 10 <= mouseY
				&& mouseY <= this._yPos + 40)
			{
				this._isDraggingVolumeSlider = true;
			}

			if (this._isDraggingVolumeSlider && mouseInput.IsLeftMouseButtonPressed())
			{
				int volume = mouseX - (this._xPos + 50);
				if (volume < 0)
					volume = 0;
				if (volume > 100)
					volume = 100;

				this._currentVolume = volume;
				this._unmuteVolume = this._currentVolume;
			}

			if (!mouseInput.IsLeftMouseButtonPressed())
				this._isDraggingVolumeSlider = false;
		}

		/// <summary>
		/// Returns a number from 0 to 100 (both inclusive)
		/// </summary>
		public int GetCurrentMusicVolume()
		{
			return this._currentVolume;
		}

		public void Render(IDisplayOutput<GameImage, GameFont> displayOutput)
		{
			GameImage image;
			DTColor color;

			switch (this._color)
			{
				case SoundAndMusicVolumePicker.Color.Black:
					image = this._currentVolume > 0 ? GameImage.MusicOn_Black : GameImage.MusicOff_Black;
					color = DTColor.Black();
					break;
				case SoundAndMusicVolumePicker.Color.White:
					image = this._currentVolume > 0 ? GameImage.MusicOn_White : GameImage.MusicOff_White;
					color = DTColor.White();
					break;
				default:
					throw new Exception();
			}

			displayOutput.DrawImage(image, this._xPos, this._yPos);

			displayOutput.DrawRectangle(
				x: this._xPos + 50,
				y: this._yPos + 10,
				width: 101,
				height: 31,
				color: color,
				fill: false);

			if (this._currentVolume > 0)
				displayOutput.DrawRectangle(
					x: this._xPos + 50,
					y: this._yPos + 10,
					width: this._currentVolume,
					height: 31,
					color: color,
					fill: true);
		}
	}
}

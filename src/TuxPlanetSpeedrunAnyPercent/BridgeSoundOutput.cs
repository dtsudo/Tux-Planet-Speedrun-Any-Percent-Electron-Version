
namespace TuxPlanetSpeedrunAnyPercent
{
	using Bridge;
	using DTLibrary;
	using System;
	using System.Collections.Generic;
	using TuxPlanetSpeedrunAnyPercentLibrary;

	public class BridgeSoundOutput : ISoundOutput<GameSound>
	{	
		private int desiredSoundVolume;
		private int currentSoundVolume;
		private int elapsedMicrosPerFrame;
	
		public BridgeSoundOutput(int elapsedMicrosPerFrame)
		{
			this.desiredSoundVolume = GlobalState.DEFAULT_VOLUME;
			this.currentSoundVolume = this.desiredSoundVolume;
			this.elapsedMicrosPerFrame = elapsedMicrosPerFrame;
			Script.Eval(@"
				window.BridgeSoundOutputJavascript = ((function () {
					'use strict';
						
					var soundDictionary = {};
					
					var numberOfAudioObjectsLoaded = 0;
					
					var loadSounds = function (soundNames) {
						var soundNamesArray = soundNames.split(',');
						
						var numberOfAudioObjects = soundNamesArray.length * 10;
						
						for (var i = 0; i < soundNamesArray.length; i++) {
							var soundName = soundNamesArray[i];
							
							if (soundDictionary[soundName])
								continue;
							
							soundDictionary[soundName] = [];
							
							var soundPath = 'Data/Sound/' + soundName + '?doNotCache=' + Date.now().toString();
							for (var j = 0; j < 10; j++) {
								var audio = new Audio(soundPath);
								audio.addEventListener('canplaythrough', function () {
									numberOfAudioObjectsLoaded++;
								});
								
								soundDictionary[soundName].push(audio);
							}
						}
						
						return numberOfAudioObjects === numberOfAudioObjectsLoaded;
					};
					
					var playSound = function (soundName, volume) {
						var sound = soundDictionary[soundName];
						
						if (volume > 1.0)
							volume = 1.0;
						if (volume < 0.0)
							volume = 0.0;
						
						var audio = sound[0];
						
						for (var i = 0; i < sound.length; i++) {
							if (i === sound.length - 1)
								sound[i] = audio;
							else
								sound[i] = sound[i+1];
						}
						
						audio.volume = volume;
						audio.play();
					};
					
					return {
						loadSounds: loadSounds,
						playSound: playSound
					};
				})());
			");
		}
	
		public bool LoadSounds()
		{			
			string soundNames = "";
			bool isFirst = true;
			foreach (GameSound gameSound in Enum.GetValues(typeof(GameSound)))
			{
				if (isFirst)
					isFirst = false;
				else
					soundNames = soundNames + ",";
				
				string soundFilename = gameSound.GetSoundFilename().DefaultFilename;
				soundNames = soundNames + soundFilename;
			}
			
			if (soundNames == "")
				return true;
			
			bool result = Script.Eval<bool>("window.BridgeSoundOutputJavascript.loadSounds('" + soundNames + "')");
			
			return result;
		}
		
		/// <summary>
		/// Volume ranges from 0 to 100 (both inclusive)
		/// </summary>
		public void SetSoundVolume(int volume)
		{
			if (volume < 0)
				throw new Exception();

			if (volume > 100)
				throw new Exception();

			this.desiredSoundVolume = volume;
		}

		public int GetSoundVolume()
		{
			return this.desiredSoundVolume;
		}

		public void ProcessFrame()
		{
			this.currentSoundVolume = VolumeUtil.GetVolumeSmoothed(
				elapsedMicrosPerFrame: this.elapsedMicrosPerFrame,
				currentVolume: this.currentSoundVolume,
				desiredVolume: this.desiredSoundVolume);
		}
		
		public void PlaySound(GameSound sound)
		{
			double finalVolume = (sound.GetSoundVolume() / 100.0) * (this.currentSoundVolume / 100.0);
			if (finalVolume > 1.0)
				finalVolume = 1.0;
			if (finalVolume < 0.0)
				finalVolume = 0.0;
			
			if (finalVolume > 0.0)
			{
				string soundFilename = sound.GetSoundFilename().DefaultFilename;
				Script.Call("window.BridgeSoundOutputJavascript.playSound", soundFilename, finalVolume);
			}
		}
		
		public void DisposeSounds()
		{
		}
	}
}

﻿
namespace TuxPlanetSpeedrunAnyPercentLibrary
{
	using DTLibrary;
	using System;
	using System.Collections.Generic;

	public class PerformanceTestFrameKeyboard : IKeyboard
	{
		private static string[] _inputArray =
		{
			"0000000",
			"0000010",
			"0000010",
			"0000010",
			"0000010",
			"0000010",
			"0000000",
			"0000000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100010",
			"0100010",
			"0100010",
			"0100010",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100001",
			"0100001",
			"0100001",
			"0100001",
			"0100001",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100001",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100010",
			"0100010",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100010",
			"0100010",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100001",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100001",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100010",
			"0100010",
			"0100000",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100000",
			"0100001",
			"0100000",
			"0100000",
			"0100000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000",
			"0000000"
		};

		private int frameCounter;

		public PerformanceTestFrameKeyboard(int frameCounter)
		{
			this.frameCounter = frameCounter;
		}

		public bool IsPressed(Key key)
		{
			switch (key)
			{
				case Key.LeftArrow:
				case Key.RightArrow:
				case Key.UpArrow:
				case Key.DownArrow:
				case Key.Esc:
				case Key.Z:
				case Key.X:
					if (_inputArray.Length <= this.frameCounter)
						return false;

					string str = _inputArray[this.frameCounter];
					int index;
					if (key == Key.LeftArrow)
						index = 0;
					else if (key == Key.RightArrow)
						index = 1;
					else if (key == Key.UpArrow)
						index = 2;
					else if (key == Key.DownArrow)
						index = 3;
					else if (key == Key.Esc)
						index = 4;
					else if (key == Key.Z)
						index = 5;
					else if (key == Key.X)
						index = 6;
					else
						throw new Exception();

					char c = str[index];

					return c == '1';
				default:
					return false;
			}
		}
	}
}

﻿
namespace TuxPlanetSpeedrunAnyPercentLibrary
{
	using DTLibrary;

	public class Credits_Font
	{
		public static void Render(IDisplayOutput<GameImage, GameFont> displayOutput, int width, int height)
		{
			string text = "The font used in this game was generated by metaflop and then \n"
				+ "slightly modified by dtsudo. \n"
				+ "https://www.metaflop.com/modulator \n"
				+ "\n"
				+ "The font is licensed under SIL Open Font License v1.1 \n"
				+ "See the source code for more details about the license. \n";

			displayOutput.DrawText(
				x: 10,
				y: height - 10,
				text: text,
				font: GameFont.DTSimpleFont20Pt,
				color: DTColor.Black());
		}
	}
}

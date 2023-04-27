
namespace TuxPlanetSpeedrunAnyPercentLibrary
{
	using DTLibrary;
	using System;

	public class Credits_DesignAndCoding
	{
		private static string GetDesktopVersionText()
		{
			return "";
		}

		private static string GetWebVersionText()
		{
			return "Design and coding by dtsudo: \n"
				+ "\n"
				+ "This game is a fangame of SuperTux and SuperTux Advance. \n"
				+ "\n"
				+ "This game is open source, licensed under GPL 3.0. \n"
				+ "(Code dependencies and images/font/sound/music licensed under \n"
				+ "other open source licenses.) \n"
				+ "\n"
				+ "The source code is written in C# and transpiled to javascript using \n"
				+ "Bridge.NET. \n"
				+ "\n"
				+ "See the source code for more information (including licensing \n"
				+ "details).";
		}

		private static string GetElectronVersionText()
		{
			return "Design and coding by dtsudo (https://github.com/dtsudo) \n"
				+ "\n"
				+ "This game is a fangame of SuperTux and SuperTux Advance. \n"
				+ "\n"
				+ "This game is open source, licensed under GPL 3.0. \n"
				+ "(Code dependencies and images/font/sound/music licensed under \n"
				+ "other open source licenses.) \n"
				+ "\n"
				+ "The source code is written in C# and transpiled to javascript using \n"
				+ "Bridge.NET. \n"
				+ "\n"
				+ "This game uses the Electron framework (https://www.electronjs.org) \n"
				+ "\n"
				+ "See the source code for more information (including licensing \n"
				+ "details).";
		}

		public static bool IsHoverOverGitHubUrl(IMouse mouse, BuildType buildType, int width, int height)
		{
			if (buildType == BuildType.Desktop || buildType == BuildType.Electron)
				return false;

			int mouseX = mouse.GetX();
			int mouseY = mouse.GetY();

			return 394 <= mouseX && mouseX <= 394 + 351
				&& height - 38 <= mouseY && mouseY <= height - 13;
		}

		public static void Render(
			IDisplayOutput<GameImage, GameFont> displayOutput, 
			bool isHoverOverGitHubUrl,
			BuildType buildType, 
			int width, 
			int height)
		{
			if (buildType == BuildType.Desktop)
			{
				string text = GetDesktopVersionText();

				displayOutput.DrawText(
					x: 10,
					y: height - 10,
					text: text,
					font: GameFont.DTSimpleFont20Pt,
					color: DTColor.Black());
			}
			else if (buildType == BuildType.WebStandAlone || buildType == BuildType.WebEmbedded)
			{
				string text = GetWebVersionText();

				displayOutput.DrawText(
					x: 10,
					y: height - 10,
					text: text,
					font: GameFont.DTSimpleFont20Pt,
					color: DTColor.Black());

				displayOutput.DrawText(
					x: 395,
					y: height - 10,
					text: "https://github.com/dtsudo",
					font: GameFont.DTSimpleFont20Pt,
					color: isHoverOverGitHubUrl ? new DTColor(0, 0, 255) : DTColor.Black());
			}
			else if (buildType == BuildType.Electron)
			{
				string text = GetElectronVersionText();

				displayOutput.DrawText(
					x: 10,
					y: height - 10,
					text: text,
					font: GameFont.DTSimpleFont20Pt,
					color: DTColor.Black());
			}
			else
			{
				throw new Exception();
			}
		}
	}
}

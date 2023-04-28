
namespace TuxPlanetSpeedrunAnyPercent
{
	using DTLibrary;
	using TuxPlanetSpeedrunAnyPercentLibrary;

	public class BridgeDisplay : DTDisplay<GameImage, GameFont>
	{
		private BridgeDisplayRectangle bridgeDisplayRectangle;
		private BridgeDisplayImages bridgeDisplayImages;
		private BridgeDisplayFont bridgeDisplayFont;
		
		public BridgeDisplay(int windowHeight)
		{
			this.bridgeDisplayRectangle = new BridgeDisplayRectangle(windowHeight: windowHeight);
			this.bridgeDisplayImages = new BridgeDisplayImages(windowHeight: windowHeight);
			this.bridgeDisplayFont = new BridgeDisplayFont(windowHeight: windowHeight);
		}
		
		public override void DrawInitialLoadingScreen()
		{
		}

		public override void DrawRectangle(int x, int y, int width, int height, DTColor color, bool fill)
		{
			this.bridgeDisplayRectangle.DrawRectangle(
				x: x,
				y: y,
				width: width,
				height: height,
				color: color,
				fill: fill);
		}
		
		public override bool LoadImages()
		{
			bool finishedLoadingImages = this.bridgeDisplayImages.LoadImages();
			
			if (!finishedLoadingImages)
				return false;
			
			return this.bridgeDisplayFont.LoadFonts();
		}
		
		public override void DrawImageRotatedClockwise(GameImage image, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			this.bridgeDisplayImages.DrawImageRotatedClockwise(
				image: image,
				x: x,
				y: y,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
		}
		
		public override void DrawImageRotatedClockwise(GameImage image, int imageX, int imageY, int imageWidth, int imageHeight, int x, int y, int degreesScaled, int scalingFactorScaled)
		{
			this.bridgeDisplayImages.DrawImageRotatedClockwise(
				image: image,
				imageX: imageX,
				imageY: imageY,
				imageWidth: imageWidth,
				imageHeight: imageHeight,
				x: x,
				y: y,
				degreesScaled: degreesScaled,
				scalingFactorScaled: scalingFactorScaled);
		}
		
		public override int GetWidth(GameImage image)
		{
			return this.bridgeDisplayImages.GetWidth(image: image);
		}
		
		public override int GetHeight(GameImage image)
		{
			return this.bridgeDisplayImages.GetHeight(image: image);
		}

		public override void DrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			this.bridgeDisplayFont.DrawText(
				x: x,
				y: y,
				text: text,
				font: font,
				color: color);
		}

		public override void TryDrawText(int x, int y, string text, GameFont font, DTColor color)
		{
			this.bridgeDisplayFont.TryDrawText(
				x: x,
				y: y,
				text: text,
				font: font,
				color: color);
		}
		
		public override void DisposeImages()
		{
		}
	}
}

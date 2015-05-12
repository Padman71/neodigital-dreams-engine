//
// NDDGraphicSettings.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using Microsoft.Xna.Framework;

namespace NeoDigitalDreamsEngine
{
	public class NDDGraphicSettings
	{
		private NDDEngine engine;
		private GraphicsDeviceManager graphics;
		private Color backgroundColor;

		public NDDGraphicSettings (NDDEngine engine)
		{
			this.engine = engine;
			graphics = new GraphicsDeviceManager (this.engine);
			graphics.IsFullScreen = false;
			graphics.PreferredBackBufferWidth = 800;
			graphics.PreferredBackBufferHeight = 600;
			backgroundColor = Color.Black;
			graphics.SynchronizeWithVerticalRetrace = false;
		}

		public void SetResolution (int width, int height)
		{
			graphics.PreferredBackBufferWidth = width;
			graphics.PreferredBackBufferHeight = height;
		}

		public void SetFullScreen (bool isFullscreen)
		{
			graphics.IsFullScreen = isFullscreen;
		}

		public void SetBackgroundColor (Color backgroundColor)
		{
			this.backgroundColor = backgroundColor;
		}

		public void Clear ()
		{
			graphics.GraphicsDevice.Clear (this.backgroundColor);
		}
	}
}

//
// NDDText.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NeoDigitalDreamsEngine
{
	public class NDDTextPrinter
	{
		SpriteBatch spriteBatch;
		SpriteFont font;
		Color fontColor = Color.White;

		public NDDTextPrinter (NDDEngine engine, SpriteFont font, Color fontColor)
		{
			this.spriteBatch = new SpriteBatch(engine.GraphicsDevice);
			if (font == null) {
				this.font = engine.Content.Load<SpriteFont> ("DefaultFont");     
			} else {
				this.font = font;
			}
			if (fontColor == null) {
				this.fontColor = Color.White;
			} else {
				this.fontColor = fontColor;
			}
		}

		public void Print(string text, Vector2 position)
		{
			spriteBatch.Begin();
			spriteBatch.DrawString(font, text, position, fontColor);
			spriteBatch.End();
		}
	}
}


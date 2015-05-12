//
// NewScene.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NeoDigitalDreamsEngine;

namespace TestGame
{
	public class NewScene : NDDScene
	{
		NDDCamera camera;

		public NewScene (NDDEngine engine, string name) : base (engine, name)
		{
		}

		public override void Initialize ()
		{
			camera = new NDDCamera (this.engine);
			engine.SetCurrentCamera (camera);
		}

		public override void Load ()
		{            
		}

		public override void Unload ()
		{
		}

		public override void Draw ()
		{
		}

		public override void Update (GameTime gameTime)
		{
			camera.Update (gameTime);
		}
	}
}


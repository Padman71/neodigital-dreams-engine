//
// TestScene.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using NeoDigitalDreamsEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
	public class TestScene : NDDScene
	{
		private CubeMesh cubeMesh;
		private BasicEffect cubeEffect;
		private Color AmbientLightColor;
		private float ambientLightIntensity = 1.0f;
		NDDCamera camera;
		NDDObject Helicopter;

		public TestScene (NDDEngine engine, string name) : base (engine, name)
		{
			AmbientLightColor = Color.White;
		}

		public override void Initialize ()
		{
			camera = new NDDCamera (this.engine);
			engine.SetCurrentCamera (camera);
			// Helper mesh creation and setup
			//cubeMesh = new CubeMesh (2.0f, this.engine.GraphicsDevice);

			// Let's create an effect to display our mesh
			//cubeEffect = new BasicEffect (this.engine.GraphicsDevice);
			//cubeEffect.EnableDefaultLighting ();

			// Setup a front looking camera
			//cubeEffect.Projection = camera.projectionMatrix;
			//cubeEffect.View = camera.viewMatrix;
			//cubeEffect.World = camera.worldMatrix;
			//UpdateAmbientLight ();
		}

		public override void Load ()
		{            
			//Helicopter.model = engine.Content.Load<Model> ("Helicopter");
		}

		public override void Unload ()
		{
		}

		public override void Draw ()
		{
			//cubeMesh.Draw (cubeEffect);
			//Helicopter.Draw (camera.projectionMatrix, camera.viewMatrix, camera.projectionMatrix);
		}

		public override void Update (GameTime gameTime)
		{
			// Let's create an effect to display our mesh
			/*cubeEffect = new BasicEffect (this.engine.GraphicsDevice);
			cubeEffect.EnableDefaultLighting ();*/

			// Setup a front looking camera
			/*cubeEffect.Projection = camera.projectionMatrix;
			cubeEffect.View = camera.viewMatrix;
			cubeEffect.World = camera.worldMatrix;*/
			//UpdateAmbientLight ();
			camera.Update (gameTime);
		}

		public void UpdateAmbientLight ()
		{
			//cubeEffect.AmbientLightColor = AmbientLightColor.ToVector3 () * ambientLightIntensity;
		}
	}
}


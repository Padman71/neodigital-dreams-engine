//
// NDDEngine.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace NeoDigitalDreamsEngine
{
	public class NDDEngine : Game
	{
		private List<NDDScene> scenes;
		private NDDScene currentScene;
		private NDDCamera currentCamera;
		public NDDGraphicSettings Graphics;
		private NDDTextPrinter textPrinter;
		int frameRate = 0;
		int frameCounter = 0;
		TimeSpan elapsedTime = TimeSpan.Zero;
		public bool IsInitialized = false;

		public NDDEngine ()
		{
			Graphics = new NDDGraphicSettings (this);
			this.IsFixedTimeStep = false;
			Content.RootDirectory = "Datas";	            
			IsMouseVisible = true;
			IsMouseVisible = false; 
			scenes = new List<NDDScene> ();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			NDDInitialize ();
			base.Initialize ();
		}
		public void NDDInitialize()
		{
			this.currentScene.Initialize ();
			IsInitialized = true;
			// TODO: Add your initialization logic here
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			textPrinter = new NDDTextPrinter (this, null, Color.White);
			this.currentScene.Load ();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			NDDUpdate (gameTime);
			base.Update (gameTime);
		}

		public void NDDUpdate(GameTime gameTime)
		{
			elapsedTime += gameTime.ElapsedGameTime;
			if (elapsedTime > TimeSpan.FromSeconds (1)) {
				elapsedTime -= TimeSpan.FromSeconds (1);
				frameRate = frameCounter;
				frameCounter = 0;
			}
			if (this.currentScene != null)
				this.currentScene.Update (gameTime);
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif
			// TODO: Add your update logic here		
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			NDDDraw (gameTime);
			base.Draw (gameTime);
		}

		public void NDDDraw(GameTime gameTime)
		{
			frameCounter++;
			Graphics.Clear ();
			if (currentCamera != null) {
				if (this.currentScene != null)
					this.currentScene.Draw ();
				showDebugLog ();
			}
			//TODO: Add your drawing code here
		}

		public void showDebugLog ()
		{
			textPrinter.Print (frameRate + " FPS", new Vector2 (0, 0));
			textPrinter.Print ("Camera position : X=" + currentCamera.position.X + " Y=" + currentCamera.position.Y + " Z=" + currentCamera.position.Z, new Vector2 (0, 14));
			textPrinter.Print ("Has focus :" + ((this.IsActive == true) ? "true" : "false"), new Vector2 (0, 28));
		}

		protected override void BeginRun ()
		{
			base.BeginRun ();
		}

		protected override void OnExiting (object sender, EventArgs args)
		{
			base.OnExiting (sender, args);
		}

		public void Cleanup ()
		{
		}

		public void SetGameTitle (string title)
		{
			this.Window.Title = title;
		}

		public void AddScene (NDDScene scene)
		{
			scenes.Add (scene);
		}

		public void SetCurrentScene (string name)
		{
			if (this.currentScene != null) {
				if (this.currentScene.name != name) {
					this.currentScene.Unload ();
				}
			}
			if (this.IsInitialized) {
				if (this.currentScene.name != name) {
					this.currentScene.Initialize ();
					this.currentScene.Load ();
				}
			}
			this.currentScene = scenes.Find (o => o.name.Equals (name));
		}

		public void SetCurrentCamera (NDDCamera camera)
		{
			this.currentCamera = camera;
		}

		protected override void UnloadContent ()
		{
			base.UnloadContent ();
			this.currentScene.Unload ();
		}
	}
}


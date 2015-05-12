using System;
using Eto.Forms;
using Microsoft.Xna.Framework.Graphics;
using Eto.Gl;
using NeoDigitalDreamsEngine;
using Microsoft.Xna.Framework;

namespace NDDEditor.Desktop
{
	public class NDDViewport : GameControl
	{
		private static NDDEngine engine;

		public NDDViewport () : base()
		{
			//set splashscreen to launch
			/*TestScene testScene = new TestScene (engine, "Test scene");
			engine.AddScene (testScene);
			engine.SetCurrentScene ("Test scene");*/
			//engine.Run ();
		}

		protected override void Initialize ()
		{
			base.Initialize();

			engine = new NDDEngine();
			engine.NDDInitialize ();
		}

		protected override void Update (GameTime gameTime)
		{
			engine.NDDUpdate(gameTime);
		}

		protected override void Draw (GameTime gameTime)
		{
			engine.NDDDraw(gameTime);
		}
	}
}


//
// NDDScene.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;

namespace NeoDigitalDreamsEngine
{
	public abstract class NDDScene
	{
		protected NDDEngine engine;
		public string name = "Unamed scene";
		protected NDDSceneFile sceneFile;

		public NDDScene (NDDEngine engine, string name)
		{
			this.engine = engine;
			this.name = name;
		}

		public abstract void Initialize ();

		public abstract void Load ();

		public abstract void Unload ();

		public abstract void Update (GameTime gameTime);

		public abstract void Draw ();

		public bool SaveScene ()
		{
			XmlSerializer xs = new XmlSerializer (typeof(NDDSceneFile));
			using (StreamWriter wr = new StreamWriter (this.name + ".nddscene")) {
				xs.Serialize (wr, sceneFile);
				return true;
			}
		}

		public bool LoadScene ()
		{
			XmlSerializer xs = new XmlSerializer (typeof(NDDSceneFile));
			using (StreamReader rd = new StreamReader (this.name + ".nddscene")) {
				sceneFile = xs.Deserialize (rd) as NDDSceneFile;
				return true;
			}
		}

		public void AddIntoScene (NDDObject anObject)
		{
			sceneFile.objectsInScene.Add (anObject);
		}
	}
}


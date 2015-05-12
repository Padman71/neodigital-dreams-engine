//
// NDDObject.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NeoDigitalDreamsEngine
{
	public class NDDObject
	{
		public int ID;
		public Vector3 position;
		public Vector3 rotation;
		public Model model;

		public NDDObject (Model model = null)
		{
			if (model != null) {
				model = new Model ();
			}
			position = new Vector3 ();
			rotation = new Vector3 ();
		}

		public void Draw (Matrix world, Matrix view, Matrix projection)
		{
			model.Draw (world, view, projection);
		}
	}
}


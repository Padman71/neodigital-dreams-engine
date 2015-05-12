//
// NDDCamera.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NeoDigitalDreamsEngine
{
	public class NDDCamera
	{
		public Vector3 position;
		public Vector3 target;
		public Matrix worldMatrix;
		public Matrix viewMatrix;
		public Matrix projectionMatrix;
		private NDDEngine engine;
		private Matrix cameraRotation;
		float leftrightRot = MathHelper.PiOver2;
		float updownRot = -MathHelper.Pi / 10.0f;
		const float rotationSpeed = 0.3f;
		const float moveSpeed = 0.001f;
		MouseState originalMouseState;
		bool hasFocus = false;
		bool prevHasFocus = false;
		bool wantToLooseFocus = false;

		public NDDCamera (NDDEngine engine)
		{
			this.engine = engine;
			ResetCamera ();
		}

		public void ResetCamera ()
		{
			Matrix cameraRotation = Matrix.CreateRotationX (updownRot) * Matrix.CreateRotationY (leftrightRot);

			Vector3 cameraOriginalTarget = new Vector3 (0, 0, -1);
			Vector3 cameraRotatedTarget = Vector3.Transform (cameraOriginalTarget, cameraRotation);
			Vector3 cameraFinalTarget = position + cameraRotatedTarget;

			Vector3 cameraOriginalUpVector = new Vector3 (0, 1, 0);
			Vector3 cameraRotatedUpVector = Vector3.Transform (cameraOriginalUpVector, cameraRotation);
			position = new Vector3 (0, 0, 4f);
			viewMatrix = Matrix.CreateLookAt (position, cameraFinalTarget, cameraRotatedUpVector);

			Mouse.SetPosition (this.engine.GraphicsDevice.Viewport.Width / 2, this.engine.GraphicsDevice.Viewport.Height / 2);
			originalMouseState = Mouse.GetState ();
		}

		public void Update (GameTime gameTime)
		{
			float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;
			HandleInput (timeDifference);
			this.engine.IsMouseVisible = false;
			UpdateViewMatrix ();
		}

		private void UpdateViewMatrix ()
		{
			Matrix cameraRotation = Matrix.CreateRotationX (updownRot) * Matrix.CreateRotationY (leftrightRot);

			Vector3 cameraOriginalTarget = new Vector3 (0, 0, -1);
			Vector3 cameraRotatedTarget = Vector3.Transform (cameraOriginalTarget, cameraRotation);
			Vector3 cameraFinalTarget = position + cameraRotatedTarget;

			Vector3 cameraOriginalUpVector = new Vector3 (0, 1, 0);
			Vector3 cameraRotatedUpVector = Vector3.Transform (cameraOriginalUpVector, cameraRotation);

			viewMatrix = Matrix.CreateLookAt (position, cameraFinalTarget, cameraRotatedUpVector);
		}

		private void HandleInput (float amount)
		{
			MouseState currentMouseState = Mouse.GetState ();
			if (currentMouseState.MiddleButton == ButtonState.Pressed) {
				if (currentMouseState != originalMouseState) {
					float xDifference = currentMouseState.X - originalMouseState.X;
					float yDifference = currentMouseState.Y - originalMouseState.Y;
					leftrightRot -= rotationSpeed * xDifference * amount;
					updownRot -= rotationSpeed * yDifference * amount;
					Mouse.SetPosition (this.engine.GraphicsDevice.Viewport.Width / 2, this.engine.GraphicsDevice.Viewport.Height / 2);
					UpdateViewMatrix ();
				}
			}
			Vector3 moveVector = new Vector3 (0, 0, 0);
			KeyboardState keyState = Keyboard.GetState ();
			if (keyState.IsKeyDown (Keys.Z))
				moveVector += new Vector3 (0, 0, -1);
			if (keyState.IsKeyDown (Keys.S))
				moveVector += new Vector3 (0, 0, 1);
			if (keyState.IsKeyDown (Keys.D))
				moveVector += new Vector3 (1, 0, 0);
			if (keyState.IsKeyDown (Keys.Q))
				moveVector += new Vector3 (-1, 0, 0);
			if (keyState.IsKeyDown (Keys.A))
				moveVector += new Vector3 (0, 1, 0);
			if (keyState.IsKeyDown (Keys.W))
				moveVector += new Vector3 (0, -1, 0);
			AddToCameraPosition (moveVector * amount);
		}

		private void AddToCameraPosition (Vector3 vectorToAdd)
		{
			Matrix cameraRotation = Matrix.CreateRotationX (updownRot) * Matrix.CreateRotationY (leftrightRot);
			Vector3 rotatedVector = Vector3.Transform (vectorToAdd, cameraRotation);
			position += moveSpeed * rotatedVector;
			UpdateViewMatrix ();
		}
	}
}


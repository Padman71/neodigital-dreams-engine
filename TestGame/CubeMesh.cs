//
// CubeMesh.cs
//
// Author:
//       padman <padman@neodigital-dreams.net>
//
// Copyright (c) 2014 padman
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TestGame
{
	public class CubeMesh
	{
		public float Side { get; private set; }

		protected float halfSide;
		protected VertexBuffer vb;
		protected IndexBuffer ib;
		protected GraphicsDevice graphics;

		public CubeMesh (float side, GraphicsDevice gd)
		{
			graphics = gd;
			Side = side;
			halfSide = Side / 2.0f;

			Vector3[] vertexPositions = new Vector3[8];
			Vector3[] sideNormals = new Vector3[6];
			Vector2[] vertexUVs = new Vector2[4];
			int[] sideVertexIndices = new int[24];

			vb = new VertexBuffer (graphics, typeof(VertexPositionNormalTexture), 24, BufferUsage.WriteOnly);
			ib = new IndexBuffer (graphics, IndexElementSize.SixteenBits, 36, BufferUsage.WriteOnly);

			VertexPositionNormalTexture[] vertexData = new VertexPositionNormalTexture[24];
			ushort[] indexData = new ushort[36];

			vertexPositions [0] = new Vector3 (-halfSide, -halfSide, -halfSide);
			vertexPositions [1] = new Vector3 (halfSide, -halfSide, -halfSide);
			vertexPositions [2] = new Vector3 (halfSide, -halfSide, halfSide);
			vertexPositions [3] = new Vector3 (-halfSide, -halfSide, halfSide);
			vertexPositions [4] = new Vector3 (-halfSide, halfSide, -halfSide);
			vertexPositions [5] = new Vector3 (halfSide, halfSide, -halfSide);
			vertexPositions [6] = new Vector3 (halfSide, halfSide, halfSide);
			vertexPositions [7] = new Vector3 (-halfSide, halfSide, halfSide);

			sideNormals [0] = Vector3.UnitZ;
			sideNormals [1] = -Vector3.UnitX;
			sideNormals [2] = -Vector3.UnitZ;
			sideNormals [3] = Vector3.UnitX;
			sideNormals [4] = Vector3.UnitY;
			sideNormals [5] = -Vector3.UnitY;

			vertexUVs [0] = new Vector2 (0.0f, 0.0f);
			vertexUVs [1] = new Vector2 (1.0f, 0.0f);
			vertexUVs [2] = new Vector2 (1.0f, 1.0f);
			vertexUVs [3] = new Vector2 (0.0f, 1.0f);

			sideVertexIndices [0] = 7;
			sideVertexIndices [1] = 6;
			sideVertexIndices [2] = 2;
			sideVertexIndices [3] = 3;

			sideVertexIndices [4] = 4;
			sideVertexIndices [5] = 7;
			sideVertexIndices [6] = 3;
			sideVertexIndices [7] = 0;

			sideVertexIndices [8] = 5;
			sideVertexIndices [9] = 4;
			sideVertexIndices [10] = 0;
			sideVertexIndices [11] = 1;

			sideVertexIndices [12] = 6;
			sideVertexIndices [13] = 5;
			sideVertexIndices [14] = 1;
			sideVertexIndices [15] = 2;

			sideVertexIndices [16] = 4;
			sideVertexIndices [17] = 5;
			sideVertexIndices [18] = 6;
			sideVertexIndices [19] = 7;

			sideVertexIndices [20] = 3;
			sideVertexIndices [21] = 2;
			sideVertexIndices [22] = 1;
			sideVertexIndices [23] = 0;

			int currentVertex = 0;
			for (int s = 0; s < 6; s++) {
				for (int v = 0; v < 4; v++) {
					vertexData [currentVertex].Position = vertexPositions [sideVertexIndices [currentVertex]];
					vertexData [currentVertex].Normal = sideNormals [s];
					vertexData [currentVertex].TextureCoordinate = vertexUVs [v];
					currentVertex++;
				}

				int firstVertex = s * 4;
				indexData [s * 6 + 0] = (ushort)(firstVertex + 3);
				indexData [s * 6 + 1] = (ushort)(firstVertex + 0);
				indexData [s * 6 + 2] = (ushort)(firstVertex + 1);

				indexData [s * 6 + 3] = (ushort)(firstVertex + 3);
				indexData [s * 6 + 4] = (ushort)(firstVertex + 1);
				indexData [s * 6 + 5] = (ushort)(firstVertex + 2);
			}

			vb.SetData<VertexPositionNormalTexture> (vertexData);
			ib.SetData<ushort> (indexData);
		}

		public void Draw (Effect e)
		{
			graphics.Indices = ib;
			graphics.SetVertexBuffer (vb);

			foreach (EffectPass pass in e.CurrentTechnique.Passes) {
				pass.Apply ();				
				graphics.DrawIndexedPrimitives (PrimitiveType.TriangleList, 0, 0, 24, 0, 12);
			}
		}
	}
}


using Godot;
using System;
using System.Collections.Generic;
using System.Threading;

public partial class TerrainSegment : Node2D
{
	[Export]
	public Ground? Ground { get; set; }

	const int NUM_POINTS = 50;
	const int MAX_HEIGHT = 150;
	const int MIN_HEIGHT = 0;
	const int BASE_HEIGHT = 500;

	public int BuildPolygon(int startHeight, int width)
	{
		ArgumentNullException.ThrowIfNull(this.Ground);
		ArgumentNullException.ThrowIfNull(this.Ground.Poly);
		ArgumentNullException.ThrowIfNull(this.Ground.CollisionPoly);

		List<Vector2> vectorList = new();

		int lastHeight = startHeight;

		for (int i = 0; i <= width; i += width / NUM_POINTS)
		{
			lastHeight += GD.RandRange(-3, 3);

			if (lastHeight > MAX_HEIGHT) { lastHeight = MAX_HEIGHT; }
			if (lastHeight < MIN_HEIGHT) { lastHeight = MIN_HEIGHT; }

			vectorList.Add(new Vector2(i, lastHeight));
		}

		vectorList.Add(new Vector2(width, 150));
		vectorList.Add(new Vector2(0, 150));

		Ground.Poly.Polygon = vectorList.ToArray();
		Ground.CollisionPoly.Polygon = vectorList.ToArray();

		return lastHeight;
	}
}

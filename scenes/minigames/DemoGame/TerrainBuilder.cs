using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class TerrainBuilder : Node2D
{
	[Export]
	public PackedScene? TerrainSegment;
	
	public Queue<TerrainSegment> TerrainSegments = new();

	const int NUM_SEGMENTS = 5;
	const int SEGMENT_WIDTH = 500;

	int startHeight = 0;

	public override void _Ready()
	{
		ArgumentNullException.ThrowIfNull(this.TerrainSegment);

		for (int i = 0; i < NUM_SEGMENTS; i++)
		{
			var terrainSegment = TerrainSegment.Instantiate<TerrainSegment>();
			terrainSegment.Position = new Vector2(i * SEGMENT_WIDTH, 0);

			if (i >= 2)
			{
				startHeight = terrainSegment.BuildPolygon(startHeight, SEGMENT_WIDTH);
			}

			AddChild(terrainSegment);
			TerrainSegments.Enqueue(terrainSegment);
		}
	}

	public void RotateSegments()
	{
		TerrainSegment terrainSegment = TerrainSegments.Dequeue();
		terrainSegment.Position = new Vector2(terrainSegment.Position.X + NUM_SEGMENTS * SEGMENT_WIDTH, 0);
		startHeight = terrainSegment.BuildPolygon(startHeight, SEGMENT_WIDTH);
		TerrainSegments.Enqueue(terrainSegment);
	}

}

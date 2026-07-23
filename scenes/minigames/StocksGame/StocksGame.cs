using Godot;
using System;
using System.Collections.Generic;

public partial class StocksGame : Node
{
	[Export]
	public Line2D? Line;
	[Export]
	public Node2D? Player;
	[Export]
	public Camera2D? Camera;

	const int START_POINTS = 100;
	const int POINT_GAP = 20;
	const int MIN_NUM = 100;
	const int MAX_NUM = 550;

	public Queue<Vector2> points = new();
	public int lastPointX;
	public int lastPointY = 400;

	public FastNoiseLite noise;

	public override void _Ready()
	{
		ArgumentNullException.ThrowIfNull(this.Line);

		GD.Randomize();

		noise = new FastNoiseLite();
		noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
		noise.Seed = (int) GD.Randi();
		noise.Frequency = 0.002f;
		noise.FractalOctaves = 4;

		for (lastPointX = 0; lastPointX < START_POINTS * POINT_GAP; lastPointX += POINT_GAP)
		{
			int noiseValue = (int) (Mathf.Tanh(noise.GetNoise2D(lastPointX - 600, 0) * 3.0f) * 300.0f);
			lastPointY = noiseValue + GD.RandRange(-30, 30) + 324;
			
			// if (lastPointY > MAX_NUM) { lastPointY = MAX_NUM; }
			// if (lastPointY < MIN_NUM) { lastPointY = MIN_NUM; }

			points.Enqueue(new Vector2(lastPointX, lastPointY));

			GD.Print("First point: " + lastPointX);
		}

		Line.Points = points.ToArray();
	}

	public override void _Process(double delta)
	{
		ArgumentNullException.ThrowIfNull(this.Line);
		ArgumentNullException.ThrowIfNull(this.Player);
		ArgumentNullException.ThrowIfNull(this.Camera);

		Player.Position = new Vector2(Player.Position.X + 1, Player.Position.Y);
		Camera.Position = new Vector2(Player.Position.X - 500, 324);

	if (Player.Position.X > lastPointX - 600)
		{
			int noiseValue = (int) (Mathf.Tanh(noise.GetNoise2D(lastPointX - 600, 0) * 3.0f) * 300.0f);
			lastPointY = noiseValue + GD.RandRange(-30, 30) + 324;
			// if (lastPointY > MAX_NUM) { lastPointY = MAX_NUM; }
			// if (lastPointY < MIN_NUM) { lastPointY = MIN_NUM; }
			points.Enqueue(new Vector2(lastPointX, lastPointY));
			lastPointX += POINT_GAP;

			points.Dequeue();

			GD.Print("First point in process: " + lastPointX);
		}

		Line.Points = points.ToArray();
	}


}

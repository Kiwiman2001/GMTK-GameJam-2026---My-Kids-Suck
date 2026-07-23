using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class StocksGame : Node
{
	[Export]
	public Line2D? Line;
	[Export]
	public Node2D? Player;
	[Export]
	public Camera2D? Camera;
	[Export]
	public Sprite2D? Marker;

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

		for (lastPointX = -100; lastPointX <= START_POINTS * POINT_GAP; lastPointX += POINT_GAP)
		{
			int noiseValue = (int) (Mathf.Tanh(noise.GetNoise2D(lastPointX - 600, 0) * 3.0f) * 300.0f);
			lastPointY = noiseValue + GD.RandRange(-30, 30) + 324;

			points.Enqueue(new Vector2(lastPointX, lastPointY));
		}

		Line.Points = points.ToArray();
	}

	public override void _Process(double delta)
	{
		ArgumentNullException.ThrowIfNull(this.Line);
		ArgumentNullException.ThrowIfNull(this.Player);
		ArgumentNullException.ThrowIfNull(this.Camera);
		ArgumentNullException.ThrowIfNull(this.Marker);

		Player.Position = new Vector2(Player.Position.X + 1, Player.Position.Y);
		Camera.Position = new Vector2(Player.Position.X - 500, 324);

	if (Player.Position.X > points.First().X + 1100)
		{
			int noiseValue = (int) (Mathf.Tanh(noise.GetNoise2D(lastPointX - 600, 0) * 3.0f) * 300.0f);
			lastPointY = noiseValue + GD.RandRange(-30, 30) + 324;
			points.Enqueue(new Vector2(lastPointX, lastPointY));
			lastPointX += POINT_GAP;

			points.Dequeue();
		}
		else
		{
			float yValue1 = points.ElementAt(54).Y;
			float yValue2 = points.ElementAt(55).Y;


			float index = Player.Position.X % 20;
			float yFinal = yValue1 - ((yValue1 - yValue2) * (index / 20));

			// GD.Print("Left Y Value: " + yValue1 + "\tRight Y Value: " + yValue2 + "\tIndex: " + index + "\tFinal Y Value: " + yFinal);
			Marker.Position = new Vector2(0, yFinal);
		}

		Line.Points = points.ToArray();
	}
}

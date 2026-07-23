using Godot;
using System;
using System.ComponentModel;

public partial class DemoGame : Node
{
	[Export]
	public Car? PlayerCar { get; set; }

	[Export]
	public TerrainBuilder? TerrainBuilder { get; set; }

	public int checkpoint = 1000;

	public override void _Ready()
	{
		GD.Randomize();
	}


	public override void _Process(double delta)
	{
		ArgumentNullException.ThrowIfNull(this.PlayerCar);
		ArgumentNullException.ThrowIfNull(this.PlayerCar.Body);
		ArgumentNullException.ThrowIfNull(this.TerrainBuilder);

		GD.Print("Position.X of car: " + PlayerCar.Body.Position.X);

		if (PlayerCar.Body.Position.X > checkpoint)
		{
			checkpoint += 500;
			TerrainBuilder.RotateSegments();
			GD.Print("Rotating Segments");
		}
	}
}

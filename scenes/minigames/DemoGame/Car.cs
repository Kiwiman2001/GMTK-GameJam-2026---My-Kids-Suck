using Godot;
using System;

public partial class Car : Node2D
{
	public static StringName Accelerate = new("car_move_forward");

	[Export]
	public RigidBody2D? Body { get; set;}
	[Export]
	public RigidBody2D? FrontWheel { get; set;}
	[Export]
	public RigidBody2D? BackWheel { get; set;}


	[Export]
	public Camera2D? Camera { get; set; }

	public const int DRIVE_TORQUE = 25000;



	public override void _PhysicsProcess(double delta)
	{
		ArgumentNullException.ThrowIfNull(this.FrontWheel);
		ArgumentNullException.ThrowIfNull(this.BackWheel);
		ArgumentNullException.ThrowIfNull(this.Camera);

		if (Input.IsActionPressed(Accelerate))
		{
			FrontWheel.ApplyTorque(DRIVE_TORQUE);
			BackWheel.ApplyTorque(DRIVE_TORQUE);
		}

		Camera.Position = new Vector2(500, -100);
	}

}

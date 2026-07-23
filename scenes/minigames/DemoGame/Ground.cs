using Godot;
using System;

public partial class Ground : StaticBody2D
{
    [Export]
	public Polygon2D? Poly { get; set; }
    [Export]
	public CollisionPolygon2D? CollisionPoly { get; set; }
}

using Godot;
using System;

// A single spike. Level.cs listens to everything in the "hazards" group,
// so you can drop as many Spike scenes into a level as you like.
public partial class Spike : Area2D
{
    public override void _Ready()
    {
        AddToGroup("hazards");
    }
}

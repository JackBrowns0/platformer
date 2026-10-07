using Godot;
using System;

public enum PickupType { Coin, Gem, Key }

public partial class Player : CharacterBody2D
{

	[Export] public float Speed = 300.0f;
    [Export] public float Gravity = 1200.0f;
    [Export] public float JumpVelocity = -550.0f;

    public int Score { get; private set; } = 0;
    public bool HasKey { get; private set; } = false;
    public bool IsActive { get; set; } = true;

    public override void _PhysicsProcess(double delta)
    {
    if (!IsActive)
    {
        Velocity = Vector2.Zero;
        return;
    }

    Vector2 velocity = Velocity;
    if (!IsOnFloor())
        velocity.Y += Gravity * (float)delta;

    if (Input.IsActionJustPressed("jump") && IsOnFloor())
    velocity.Y = JumpVelocity;
    velocity.X = Input.GetAxis("move_left", "move_right") * Speed;
    Velocity = velocity;
    MoveAndSlide();
    }   

    public void OnPickup(PickupType type)
    {
        if (!IsActive) return;

        switch (type)
        {
            case PickupType.Coin:
                Score += 1;
                break;
            case PickupType.Gem:
                Score += 5;
                break;
            case PickupType.Key:
                HasKey = true;
                break;
        }

        GD.Print($"Punkti: {Score}; atslēga: {HasKey}");
    }
}

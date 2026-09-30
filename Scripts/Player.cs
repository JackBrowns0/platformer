using Godot;
using System;

public enum PickupType { Coin, Gem, Key }

public partial class Player : CharacterBody2D
{

	[Export] public float Speed = 300.0f;

    public int Score { get; private set; } = 0;
    public bool HasKey { get; private set; } = false;
    public bool IsActive { get; set; } = true;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Vector2.Zero;
        if (Input.IsActionPressed("move_left")) direction.X -= 1;
        if (Input.IsActionPressed("move_right")) direction.X += 1;
        if (Input.IsActionPressed("move_up")) direction.Y -= 1;
        if (Input.IsActionPressed("move_down")) direction.Y += 1;

        Velocity = direction.Normalized() * Speed;
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

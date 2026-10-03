using Godot;
using System;

public partial class Pickup : Area2D
{
    [Export] public PickupType Type = PickupType.Coin;
    private bool collected = false;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (collected || body is not Player player || !player.IsActive)
            return;

        collected = true;
        player.OnPickup(Type);
        QueueFree();
    }
}
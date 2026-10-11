using Godot;
using System;

public partial class Level : Node2D
{
    [Export] public string GameTitle = "The Cyan Block's Great Quest";
    [Export] public string Mission = "Get the key and get out!";
    [Export] public string WinText = "you got the key and got out, GG.";
    [Export] public string LoseText = "You died! LLLLLL";
    [Export] public string LockedText = "You didnt get the key yet bozo.";

    private Player player;
    private Label status;
    private Label message;
    public bool GameEnded { get; private set; } = false;

    public override void _Ready()
    {
        player = GetNode<Player>("Player");
        status = GetNode<Label>("HUD/Status");
        message = GetNode<Label>("HUD/Message");
        GetNode<Label>("HUD/Instructions").Text =
            $"{GameTitle} | A/D or ←/→: Movement | Space: Jump | R: Restart";
        message.Text = Mission;
        GetNode<Area2D>("Finish").BodyEntered += OnFinishEntered;
        foreach (Node hazard in GetTree().GetNodesInGroup("hazards"))
        {
            if (hazard is Area2D area)
                area.BodyEntered += OnHazardEntered;
        }
        UpdateHud();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("restart"))
        {
            GetTree().ReloadCurrentScene();
            return;
        }

        if (!GameEnded && player.GlobalPosition.Y > 800)
            EndGame(false);

        UpdateHud();
    }

    private void UpdateHud()
    {
        string keyStatus = player.HasKey ? "Acquired" : "Nuh uh";
        status.Text = $"Points: {player.Score} | Key: {keyStatus}";
    }

    private void OnFinishEntered(Node2D body)
    {
        if (body != player || GameEnded) return;

        if (player.HasKey)
            EndGame(true);
        else
            message.Text = LockedText;
    }

    private void OnHazardEntered(Node2D body)
    {
        if (body == player) EndGame(false);
    }

    private void EndGame(bool won)
    {
        if (GameEnded) return;
        GameEnded = true;
        player.IsActive = false;
        player.Velocity = Vector2.Zero;
        message.Text = (won ? WinText : LoseText) + " | Press R to play again";
        UpdateHud();
    }
}

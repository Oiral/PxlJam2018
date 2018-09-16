using Godot;
using System;

public class GuiScript : Control
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    private Label moveLabel;

    public int moves;
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        moveLabel = (Label)GetNode("Panel/Number");
        moveLabel.Text = "0";

    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_right")){
            UpdateMove();
        }

    }


    public void OnPausePress(){
        var nodeToLoad = (PackedScene) ResourceLoader.Load("res://Scenes/PauseMenu.tscn");
        var node = nodeToLoad.Instance();
        GetNode("/root/GameNode").AddChild(node);
    }

    public void UpdateMove(){
        moves += 1;
        moveLabel.Text = moves.ToString();
    }
}

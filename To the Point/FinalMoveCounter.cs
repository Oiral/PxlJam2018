using Godot;
using System;

public class FinalMoveCounter : Label
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
        GuiScript guiScript = (GuiScript)GetNode("/root/GameNode/ObjLevelLoader/InGameGui");
        Text = guiScript.moves.ToString();

    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}

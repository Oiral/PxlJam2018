using Godot;
using System;

public class MainMenuScript : Control
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public void OnStartPressed(){
        GD.Print("Start the game!");
        Game gameScript = (Game)GetNode("/root/GameNode");
        //Game gameScript = (Game)GetParent().GetParent();
        
        gameScript.LoadNextLevel();
    }

    public void OnQuitPressed(){
        GD.Print("Good bye old friend");
        GetTree().Quit();
    }
}

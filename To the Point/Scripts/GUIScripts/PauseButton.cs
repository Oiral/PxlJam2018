using Godot;
using System;

public class PauseButton : Control
{

    public void OnResumePress(){
        GD.Print("Resume");
        //Delete the pause node
        //RemoveChild(this);
        QueueFree();
    }

    public void OnRestartPress(){
        
        //Call restart
        Game gameScript = (Game)GetNode("/root/GameNode");
        gameScript.LoadLevel(gameScript.levelNum);

        //Reset move counter
        GuiScript guiScript = (GuiScript)GetNode("/root/GameNode/ObjLevelLoader/InGameGui");
        guiScript.moves = 0;

        //Delete the pause node
        QueueFree();
    }

    public void OnMainMenuPress(){
        Game gameScript = (Game)GetNode("/root/GameNode");
        gameScript.LoadMainMenu();

        QueueFree();
    }

    public void OnFinishGamePress(){
        //Load main menu
        Game gameScript = (Game)GetNode("/root/GameNode");
        gameScript.LoadMainMenu();

        //Remove this screen
        Node loadedNode = GetParent().GetParent();
        RemoveChild(loadedNode);
        loadedNode.CallDeferred("free");
    }

    public void OnQuitPress(){
        GetTree().Quit();
    }
}

using Godot;
using System;

public class Game : Node
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public int levelNum = 0;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here

        //Load the Main Menu

        levelNum = 0;
        //Add in the main menu
        var nodeToLoad = (PackedScene) ResourceLoader.Load("res://MainMenu.tscn");
        var node = nodeToLoad.Instance();
        AddChild(node);
    }

    public override void _Process(float delta){
        if ( Input.IsActionJustPressed("ui.up")){
            GD.Print("ajsbkda");
            
            //LoadNextLevel();
        }
    }

    public void LoadNextLevel(){
        //We are in the main menu load the first level and delete the main menu scene
        GD.Print("Loading Level");
        Node loadedNode;
        if (levelNum == 0){
            //Remove the main menu
            loadedNode = GetNode("MainMenu");
        }else{
            //Remove the previous level
            loadedNode = GetNode("Level" + levelNum);
        }

        RemoveChild(loadedNode);
        //loadedNode.CallDeferred("free");

        //add on to the current level
        levelNum += 1;

        //Load in the next level
        var nodeToLoad = (PackedScene) ResourceLoader.Load("res://Levels/Level" + levelNum + ".tscn");
        var node = nodeToLoad.Instance();
        AddChild(node);

    }
    public void LoadMainMenu(){
        //we are coming from a level so delete the level node
        if (levelNum > 0){
            Node loadedNode = GetNode("Level" + levelNum);
            RemoveChild(loadedNode);
            loadedNode.CallDeferred("free");
        }
        levelNum = 0;
        //Add in the main menu
        var nodeToLoad = (PackedScene) ResourceLoader.Load("res://MainMenu.tscn");
        var node = nodeToLoad.Instance();
        AddChild(node);
    }
}

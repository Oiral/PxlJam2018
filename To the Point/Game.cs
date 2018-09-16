using Godot;
using System;

public class Game : Node
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";


    public int levelNum = 1;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here

        //Load the Main Menu
        //Add in the main menu
        var nodeToLoad = (PackedScene) ResourceLoader.Load("res://MainMenu.tscn");
        var node = nodeToLoad.Instance();
        AddChild(node);

        //Start playing music

    }

    public override void _Process(float delta){
        if ( Input.IsActionJustPressed("ui_up")){
            GD.Print("ajsbkda");
            LoadNextLevel();
        }
    }

    public void LoadNextLevel(){
        GD.Print("Loading Level");

        LoadLevel(levelNum);

        levelNum += 1;
        
    }

    public void LoadLevel(int levelNumber){
        //Clean up the level
        ObjLevelLoader objLevelLoader = (ObjLevelLoader) GetNode("ObjLevelLoader");
        objLevelLoader.LoadLevel("Levels/Level" + levelNum + "Edges.obj");

        //Get the level loader
        LevelLoader loader = (LevelLoader) GetNode("ObjLevelLoader/LevelLoader");

        //Load the next level
        loader.spawnLevel();
    }

    public void StartGame(){
        GD.Print("Starting the game");
        //Remove the main menu
        Node mainMenuNode = GetNode("MainMenu");
        RemoveChild(mainMenuNode);
        mainMenuNode.CallDeferred("free");

        //Create the Level instance
        var LevelNodeToLoad = (PackedScene) ResourceLoader.Load("res://Scenes/LevelLoader.scn");
        var LevelParentNode = LevelNodeToLoad.Instance();
        AddChild(LevelParentNode);

        LoadNextLevel();
    }

    public void LoadMainMenu(){
        //we are coming from a level so delete the level node
        if (levelNum > 0){
            Node loadedNode = GetNode("ObjLevelLoader");
            RemoveChild(loadedNode);
            loadedNode.CallDeferred("free");
        }
        levelNum = 1;
        //Add in the main menu
        var nodeToLoad = (PackedScene) ResourceLoader.Load("res://MainMenu.tscn");
        var node = nodeToLoad.Instance();
        AddChild(node);
    }
}

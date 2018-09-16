using Godot;
using System;

public class TestLoader : Node
{
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }
    /* 
	public override void _Process(float delta)
	{
        
	    if (Input.IsActionJustReleased("ui_select"))
	    {   
            ObjLevelLoader objLevelLoader = (ObjLevelLoader) GetNode("/root/ObjLevelLoader");
            objLevelLoader.LoadLevel("Levels/LevelPlaneEdges.obj");
            LevelLoader levelParent = (LevelLoader) GetNode("/root/ObjLevelLoader/LevelLoader");
            GD.Print(levelParent.Name);
            GD.Print("Start Node = " + levelParent.StartVert.Id.ToString());
            GD.Print("Spawning Level...");
            levelParent.spawnLevel();
            
	    }
	}
    */
}

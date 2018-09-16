using Godot;
using System;

public class Player : Spatial
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    
    public LvlVert currentVert;

    public Vector3 targetPos;
    public Vector3 startingPos;

    [Export]
    public float travelTime = 1;
    private float elapsedTime;

    public bool traveling = false;
    
    public override void _Ready()
    {
        
    }
    
    public override void _Process(float delta)
    {
        if (traveling == true){
            if (elapsedTime >= travelTime){
                Translation.Set(targetPos);
                traveling = false;
                LevelLoader levelLoader = GetNode<LevelLoader>("/root/GameNode/ObjLevelLoader/LevelLoader");
                levelLoader.enableVertHighlights();
                levelLoader.checkFinished();
                return;
            }   
            elapsedTime += delta;

            var intermediate = (targetPos-startingPos) / (travelTime/elapsedTime) + startingPos;
            SetTranslation(intermediate);
        }
        
        // if (Input.IsActionJustPressed("ui_right")){
        //     GD.Print("Attempting to move");
        //     SetLerpPos(new Vector3(0,10,0));
        // }
    }

    public void SetLerpPos(LvlVert vert){
        GD.Print(GetTranslation() + " Something");
        elapsedTime = 0;
        targetPos = vert.Vertex;
        traveling = true;
        currentVert = vert;
        GuiScript gui = GetNode<GuiScript>("/root/GameNode/ObjLevelLoader/InGameGui");
        gui.UpdateMove();
        startingPos = GetTranslation();
    }
}

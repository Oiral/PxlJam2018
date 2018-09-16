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
    public override void _Process(float delta)
    {
        if (traveling == true){
            if (elapsedTime >= travelTime){
                Translation.Set(targetPos);
                traveling = false;
                GD.Print("Finished Traveling");
                return;
            }   
            GD.Print(Translation);
            elapsedTime += delta;

            var intermediate = (targetPos-startingPos) / (travelTime/elapsedTime) + startingPos;
            SetTranslation(intermediate);
        }
        
        if (Input.IsActionJustPressed("ui_right")){
            GD.Print("Attempting to move");
            SetLerpPos(new Vector3(0,10,0));
        }
    }

    public void SetLerpPos(Vector3 pos){
        GD.Print(GetTranslation() + " Something");
        elapsedTime = 0;
        targetPos = pos;
        traveling = true;
        startingPos = GetTranslation();
    }
}

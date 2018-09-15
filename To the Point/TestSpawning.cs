using Godot;
using System;

public class TestSpawning : Spatial
{
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
    }

    public override void _Process(float delta)
    {
        // Called every frame. Delta is time since last frame.
        // Update game logic here.

        if(Input.IsActionJustPressed("ui_left")){
            PackedScene vertexScene = (PackedScene) ResourceLoader.Load("res://Vertex.tscn");
            //var vertex = vertexScene.Instance();
            //AddChild(vertex);
            


            for (int i = 0; i < 3; i++){
                //Node vertHolder = GetNode("VertHolder");
                Node vertex = vertexScene.Instance();
                //vertHolder.AddChild(vertex);
                AddChild(vertex);
                Spatial vertSpacial = (Spatial) vertex;
                vertSpacial.Translation = (new Vector3(i,i,0));
                //vertex as PackedScene
                GD.Print(vertex.Name + " --- " + i.ToString());
            }
        }
    }
}

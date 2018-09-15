using Godot;
using System;

public class TestLoader : Node
{
    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

	public override void _Process(float delta)
	{
	    if (Input.IsActionJustReleased("ui_select"))
	    {
            
            LevelLoader levelParent = (LevelLoader) GetNode("/root/Node/LevelParent");
            GD.Print("Start Node = " + levelParent.StartVert.Id.ToString());
            GD.Print("Spawning Level...");
            levelParent.spawnLevel();
            /*
            GD.Print("ChildCount = " +levelParent.GetChildCount());
            foreach(Node child in levelParent.GetChildren()) {
                GD.Print(child.Name);
            }
            GD.Print("VertNodes = ");
            foreach(var edge in levelParent.Edges) {
                GD.Print(edge.Vert1.VertNode.Name + " - " + edge.Vert2.VertNode.Name );
            }
            */

            /*
            SceneTree tree = GetTree();

            //Node parent = GetNode("/root/Node/LevelParent");
            PackedScene vertMesh = (PackedScene)ResourceLoader.Load("res://Scenes/VertMesh.tscn");
            Node newVert = (Node)vertMesh.Instance();
            
            Spatial newVertSpatial = (Spatial) newVert;
            newVertSpatial.SetTranslation(new Vector3(1,1,2));
            newVertSpatial.SetScale(new Vector3(0.1f, 0.1f, 0.1f));
            //parent.AddChild(newVert, true);
            //AddChild(newVert);
            tree.Root.AddChild(newVert);
            */
	    }
	}
}

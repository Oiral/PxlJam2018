using Godot;
using System;
using System.Collections.Generic;

public class Level : Node
{
    
    private List<LvlVert> verts;
    private List<LvlEdge> edges;
    private LvlVert startVert;
    private List<LvlVert> endVerts;

    public List<LvlVert> Verts {
        get {return this.verts;}
    }

    public List<LvlEdge> Edges {
        get {return this.edges;}
    }

    public LvlVert StartVert {
        get {return this.startVert;}
    }

    public List<LvlVert> EndVerts {
        get {return this.endVerts;}
    }

    public Level(List<LvlVert> verts, List<LvlEdge> edges, LvlVert startVert, List<LvlVert> endVerts) {
        this.verts = verts;
        this.edges = edges;
        this.startVert = startVert;
        this.endVerts = endVerts; 
    }

    public void spawnLevel() {
        PackedScene vertMesh = (PackedScene)ResourceLoader.Load("res://Scenes/VertMesh.tscn");

        Node parent = GetNode("/root/Node/LevelParent");

        foreach (var vert in verts) {
            GD.Print("Spawning vert: " + vert.Vertex.ToString());
            Node newVert = (Node)vertMesh.Instance();
            newVert.Name = vert.Id.ToString();
            Spatial newVertSpatial = (Spatial) newVert;
            newVertSpatial.SetTranslation(vert.Vertex);
            newVertSpatial.Translate(new Vector3(0,0,2));
            //newVertSpatial.SetScale(new Vector3(0.1f, 0.1f, 0.1f));

            parent.AddChild(newVert, true);
        }
    }
}

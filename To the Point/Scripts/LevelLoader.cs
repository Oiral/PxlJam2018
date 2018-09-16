using Godot;
using System;
using System.Collections.Generic;

public class LevelLoader : Spatial
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        
    }

    private bool isSetup = false;
    [Export]
    private float nodeScale = 0.1f;
    private List<LvlVert> verts;
    private List<LvlEdge> edges;
    private LvlVert startVert;
    private List<LvlVert> endVerts;

    public bool IsSetup {
        get {return this.isSetup;}
    }
    public float NodeScale {
        get {return this.nodeScale;}
        set {this.nodeScale = value;}
    }
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

    public void ResetLevel() {
        foreach (Node child in this.GetChildren()) {
            child.Dispose();
        }
        this.verts = new List<LvlVert>();
        this.edges = new List<LvlEdge>();
        this.startVert = null;
        this.endVerts = new List<LvlVert>();
        this.isSetup = false;
        GD.Print("Level has been reset!");
    }
 
    public void Setup(List<LvlVert> verts, List<LvlEdge> edges, LvlVert startVert, List<LvlVert> endVerts) {
        this.verts = verts;
        this.edges = edges;
        this.startVert = startVert;
        this.endVerts = endVerts; 
        isSetup = true;
        GD.Print("Level Setup Complete!");
    }

    public void spawnLevel() {
        if (!isSetup){
            GD.Print("Unable to spawn level, LevelLoader is not set up yet");
            return;
        }
        spawnVerts();
        spawnEdges();
    }

    private void spawnVerts() {
        PackedScene vertMesh = (PackedScene)ResourceLoader.Load("res://Scenes/VertMesh.tscn");

        foreach (var vert in verts) {
            //GD.Print("Spawning vert: " + vert.Vertex.ToString());
            Node newVert = (Node)vertMesh.Instance();
            newVert.Name = vert.Id.ToString();
            Spatial newVertSpatial = (Spatial) newVert;
            newVertSpatial.SetTranslation(vert.Vertex);
            //newVertSpatial.Translate(new Vector3(0,0,2));
            newVertSpatial.SetScale(new Vector3(nodeScale, nodeScale, nodeScale));

            vert.VertNode = newVert;
            this.AddChild(newVert);
        }
    }

    private void spawnEdges() {
        PackedScene edgeMesh = (PackedScene)ResourceLoader.Load("res://Scenes/EdgeMesh.tscn");

        foreach (var edge in edges) {
            Node newEdge = (Node)edgeMesh.Instance();
            newEdge.Name = edge.Id.ToString();
            Spatial newEdgeSpatial = (Spatial) newEdge;
            //newEdgeSpatial.Translate(new Vector3(0,0,2));
            Vector3 edgeTranslation = edge.Vert1.Vertex - edge.Vert2.Vertex;
            //GD.Print(edgeTranslation.Normalized().ToString());
            //newEdgeSpatial.LookAtFromPosition(edge.Vert1.Vertex, edge.Vert2.Vertex, edgeTranslation.Normalized());
            //newEdgeSpatial.RotateObjectLocal(new Vector3(0,0,1), 45f);
            //newEdgeSpatial.SetTranslation(edge.Vert1.Vertex);
            newEdgeSpatial.SetScale(new Vector3(nodeScale, nodeScale, nodeScale));

            edge.EdgeNode = newEdge;
            this.AddChild(newEdge);
        }
    }
}

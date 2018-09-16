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
    private String meshPath;
    private Player player;

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
 
    public void Setup(List<LvlVert> verts, List<LvlEdge> edges, LvlVert startVert, List<LvlVert> endVerts, String mesh) {
        this.verts = verts;
        this.edges = edges;
        this.startVert = startVert;
        this.endVerts = endVerts; 
        this.meshPath = mesh;
        isSetup = true;
        GD.Print("Level Setup Complete!");
    }

    public void spawnLevel() {
        if (!isSetup){
            GD.Print("Unable to spawn level, LevelLoader is not set up yet");
            return;
        }
        spawnVerts();
        spawnMesh();
        spawnPlayer();
        //spawnEdges();
        enableVertHighlights();
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

    private void spawnMesh() {
        Mesh meshObj = (Mesh) ResourceLoader.Load(meshPath);
        MeshInstance mesh = new MeshInstance();
        mesh.SetMesh(meshObj);
        this.AddChild(mesh);
    }

    private void spawnEdges() {
        PackedScene edgeMesh = (PackedScene)ResourceLoader.Load("res://Scenes/EdgeMesh.tscn");

        foreach (var edge in edges) {
            Node newEdge = (Node)edgeMesh.Instance();
            newEdge.Name = edge.Id.ToString();
            this.AddChild(newEdge);
            Spatial newEdgeSpatial = (Spatial) newEdge;
            //newEdgeSpatial.Translate(new Vector3(0,0,2));
            Vector3 edgeTranslation = edge.Vert1.Vertex - edge.Vert2.Vertex;
            //GD.Print(edgeTranslation.Normalized().ToString());
            //newEdgeSpatial.LookAtFromPosition(edge.Vert1.Vertex, edge.Vert2.Vertex, edgeTranslation.Normalized());
            //newEdgeSpatial.RotateObjectLocal(new Vector3(0,0,1), 45f);
            //newEdgeSpatial.SetTranslation(edge.Vert1.Vertex);
            newEdgeSpatial.SetScale(new Vector3(nodeScale, nodeScale, nodeScale));

            edge.EdgeNode = newEdge;
        }
    }
    
    private void spawnPlayer() {
        PackedScene playerScene = (PackedScene)ResourceLoader.Load("res://Scenes/Player.tscn");
        
        player = (Player)playerScene.Instance();
        this.AddChild(player);
        player.Name = "SamiPlayer";
        player.SetTranslation(startVert.Vertex);
        float playerScale = nodeScale * 1.3f;
        player.SetScale(new Vector3(playerScale, playerScale, playerScale));
        player.currentVert = startVert;
        
        // foreach (var vert in verts) {
        //     //GD.Print("Spawning vert: " + vert.Vertex.ToString());
        //     Node newVert = (Node)playerMesh.Instance();
        //     newVert.Name = vert.Id.ToString();
        //     Spatial newVertSpatial = (Spatial) newVert;
        //     newVertSpatial.SetTranslation(vert.Vertex);
        //     //newVertSpatial.Translate(new Vector3(0,0,2));
        //     newVertSpatial.SetScale(new Vector3(nodeScale, nodeScale, nodeScale));

        //     vert.VertNode = newVert;
        //     this.AddChild(newVert);
        // }
    }

    public List<LvlVert> getEligibleMoves() {
        List<LvlVert> verts = new List<LvlVert>();

        foreach(var edge in edges) {
            if (player.currentVert.Id == edge.Vert1.Id){
                verts.Add(edge.Vert2);
            }

            if (player.currentVert.Id == edge.Vert2.Id){
                verts.Add(edge.Vert1);
            }
        }

        return verts;
    }

    public void enableVertHighlights() {
        foreach (var vert in getEligibleMoves()) {
            Spatial node = vert.VertNode.GetNode("HighlightArea") as Spatial;
            node.SetVisible(true);
        }
    }

    public void disableVertHighlights() {
        foreach (var vert in verts) {
            Spatial node = vert.VertNode.GetNode("HighlightArea") as Spatial;
            node.SetVisible(false);
        }
    }
    
    public void movePlayerTo(string id)
    {
        LvlVert vert = verts[0];
        
        foreach (LvlVert v in verts)
        {
            if (v.Id.ToString().Equals(id))
            {
                vert = v;
                break;
            }
        }
        
        disableVertHighlights();
        player.SetLerpPos(vert);
    }
}

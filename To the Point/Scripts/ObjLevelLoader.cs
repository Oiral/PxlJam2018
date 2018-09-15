using Godot;
using System;
using System.Collections.Generic;

public class ObjLevelLoader : Node
{
	private Level level;
	[Export]
	private String levelObject;
    [Export]
    private bool debugPrint = false;
    private List<LvlVert> rawVertexes = new List<LvlVert>();
    private List<LvlVert> compressedVertexes = new List<LvlVert>();
    private List<LvlEdge> edges = new List<LvlEdge>();
    private LvlVert startVert;
    private List<LvlVert> endVerts;
    
    public override void _Ready()
    {
        String[] objText = System.IO.File.ReadAllLines(levelObject);

        String startFace = "";
        String endFace1 = "";
        String endFace2 = "";
        String endFace3 = "";
        String endFace4 = "";

        for (int i = 0; i < objText.Length; i++) {
            //Print everything except comments
            /*
            if (!line.BeginsWith("#")) {
                GD.Print(line);
            }
            */

            if (objText[i].BeginsWith("v")) {
                rawVertexes.Add(readVertex(objText[i]));
            }

            if (objText[i].BeginsWith("l")) {
                edges.Add(readEdge(objText[i]));
            }

            //Find the start point
            if (objText[i].BeginsWith("g Start")) {
                startFace = objText[i+1];
            }

            //Find the start point
            if (objText[i].BeginsWith("g End1")) {
                endFace1 = objText[i+1];
            }

            //Find the start point
            if (objText[i].BeginsWith("g End2")) {
                endFace2 = objText[i+1];
            }

            //Find the start point
            if (objText[i].BeginsWith("g End3")) {
                endFace3 = objText[i+1];
            }

            //Find the start point
            if (objText[i].BeginsWith("g End4")) {
                endFace4 = objText[i+1];
            }
        }

        //Calculate the start vert
        startVert = getVertByFaceNum(startFace);

        endVerts = new List<LvlVert>();

        if (!string.IsNullOrEmpty(endFace1)) {
            endVerts.Add(getVertByFaceNum(endFace1));
        }

        if (!string.IsNullOrEmpty(endFace2)) {
            endVerts.Add(getVertByFaceNum(endFace2));
        }

        if (!string.IsNullOrEmpty(endFace3)) {
            endVerts.Add(getVertByFaceNum(endFace3));
        }

        if (!string.IsNullOrEmpty(endFace4)) {
            endVerts.Add(getVertByFaceNum(endFace4));
        }

        if (debugPrint) {
            GD.Print("Start vert = " + startVert.Id.ToString());

            GD.Print("End verts = ");
            foreach (var vert in endVerts) {
                GD.Print(vert.Id.ToString());
            }

            GD.Print("End verts = ");
            foreach (var vert in endVerts) {
                GD.Print(vert.Id.ToString());
            }
        }

        compressVerts();

        if (debugPrint) {
            GD.Print("Compressed verts count = " + compressedVertexes.Count);
            GD.Print("Verts:");
            foreach(var vert in compressedVertexes) {
                GD.Print(vert.Id.ToString() + " - " + vert.Vertex.ToString());
            }

            //compressEdges();
            GD.Print("Edge count = " + edges.Count);
            GD.Print("Edges:");
            foreach(var edge in edges) {
                GD.Print(edge.Vert1.Id.ToString() + " - " + edge.Vert2.Id.ToString());
            }
        }
        level = new Level(compressedVertexes, edges, startVert, endVerts);
    }

	public override void _Process(float delta)
	{
        try{
            LevelLoader levelLoader = (LevelLoader) GetNode("/root/Node/LevelParent");
            if (!levelLoader.IsSetup){
                levelLoader.Setup(compressedVertexes,edges, startVert, endVerts);
            }
        } catch {
            GD.Print("Couldnt find LevelParent");
        }
    }

    private LvlVert readVertex(String vertex) {
        string[] vertexes = vertex.Split(' ');
        return new LvlVert(new Vector3(float.Parse(vertexes[2]), float.Parse(vertexes[3]), float.Parse(vertexes[4])));
    }

    private LvlEdge readEdge(String edge) {
        string[] edgeVerts = edge.Split(' ');

        LvlVert vert1 = rawVertexes[int.Parse(edgeVerts[1]) - 1];
        LvlVert vert2 = rawVertexes[int.Parse(edgeVerts[2]) - 1];

        return new LvlEdge(vert1, vert2);
    }

    private void compressVerts() {
        foreach(var currentVert in rawVertexes) {
            //check to see if this vert is already in the compressed list
            bool isCompressed = false;
            foreach(var compressedVert in compressedVertexes) {
                if (currentVert.Vertex == compressedVert.Vertex) {
                    isCompressed = true;
                    break;
                }
            }

            //if not, add it to the compressed list
            if (!isCompressed) {
                compressedVertexes.Add(currentVert);

                //Update edges to point use newly compressed verts
                foreach(var edge in edges) {
                    if (edge.Vert1.Vertex == currentVert.Vertex){
                        edge.Vert1 = currentVert;
                    }

                    if (edge.Vert2.Vertex == currentVert.Vertex){
                        edge.Vert2 = currentVert;
                    }
                }
            }
        }
    }

    private LvlVert getVertByFaceNum(String faceNum) {
        string[] startVerts = faceNum.Split(' ');

        foreach (var vert in rawVertexes) {
            if (vert.Vertex == rawVertexes[int.Parse(startVerts[1])].Vertex) {
                return vert;
            }
        }
        GD.PrintErr("ERROR: Could not find vert by face!");
        return null;
    }
}

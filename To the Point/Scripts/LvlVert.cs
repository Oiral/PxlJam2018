using Godot;
using System;

public class LvlVert : Node
{
    private Vector3 vertex;
    private Guid id;
    private Node vertNode;

    public Vector3 Vertex {
        get {return this.vertex;}
    }

    public Guid Id {
        get {return this.id;}
    }

    public Node VertNode {
        get {return this.vertNode;}
        set {this.vertNode = value;}
    }

    public LvlVert(Vector3 vert) {
        id = Guid.NewGuid();
        vertex = vert;
    }
}

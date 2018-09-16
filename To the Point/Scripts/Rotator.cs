using Godot;
using System;

public class Rotator : Node
{
    private Vector2 startPos;
    private bool dragging = false;
    private Vector2 dragPos;
    private Vector3 startRot;
    
    public Spatial node;
    
    public override void _Ready()
    {
        GD.Print("something");
        node = GetNode<Spatial>("../LevelLoader");
        GD.Print(node.Name);
    }
    
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseBtn)
        {
            if (eventMouseBtn.Pressed && eventMouseBtn.ButtonIndex == (int)ButtonList.Right)
            {
                startPos = eventMouseBtn.Position;
                dragPos = startPos;
                startRot = node.RotationDegrees;
                dragging = true;
            }
            else
            {
                dragging = false;
            }
        }
        else
        if (@event is InputEventMouseMotion eventMouseMotion)
        {
            if (dragging)
            {
                dragPos = eventMouseMotion.Position;
                
                float degX = dragPos.x - startPos.x;
                degX *= 0.5f;
                float degY = dragPos.y - startPos.y;
                degY *= -0.5f;
                
                node.GlobalRotate(Vector3.Up, degX * (float)(Math.PI / 180));
                node.GlobalRotate(Vector3.Left, degY * (float)(Math.PI / 180));
                
                startPos = dragPos;
            }
        }
    }
}

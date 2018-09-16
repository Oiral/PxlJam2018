using Godot;
using System;

public class VertClick : CollisionShape
{
    private void _on_HighlightArea_input_event(Godot.Object camera, Godot.Object @event, Vector3 clickPosition, Vector3 clickNormal, int shapeIndex)
    {
        if (@event is InputEventMouseButton inputEventMouseBtn)
        {
            Spatial parent = (Spatial)GetParent();
            
            if (parent.Visible && inputEventMouseBtn.Pressed && inputEventMouseBtn.ButtonIndex == (int)ButtonList.Left)
            {
                Game gameNode = GetNode<Game>("/root/GameNode");
                gameNode.PlayMoveSound();
                LevelLoader levelLoader = GetNode<LevelLoader>("/root/GameNode/ObjLevelLoader/LevelLoader");
                levelLoader.movePlayerTo(GetParent().GetParent().Name);
            }
        }
    }
}

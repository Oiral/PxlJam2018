using Godot;
using System;

public class VertClick : CollisionShape
{
    private void _on_HighlightArea_input_event(Godot.Object camera, Godot.Object @event, Vector3 clickPosition, Vector3 clickNormal, int shapeIndex)
    {
        if (@event is InputEventMouseButton inputEventMouseBtn)
        {
            if (inputEventMouseBtn.Pressed && inputEventMouseBtn.ButtonIndex == (int)ButtonList.Left)
            {
                LevelLoader levelLoader = GetNode<LevelLoader>("/root/GameNode/ObjLevelLoader/LevelLoader");
                levelLoader.movePlayerTo(GetParent().GetParent().Name);
                GD.Print(GetParent().GetParent().Name);
            }
        }
    }
}

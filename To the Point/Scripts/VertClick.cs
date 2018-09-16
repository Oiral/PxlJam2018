using Godot;
using System;
using System.Collections.Generic;

public class VertClick : CollisionShape
{
    private void _on_HighlightArea_input_event(Godot.Object camera, Godot.Object @event, Vector3 clickPosition, Vector3 clickNormal, int shapeIndex)
    {
        if (@event is InputEventMouseButton inputEventMouseBtn)
        {
            LevelLoader levelLoader = GetNode<LevelLoader>("/root/GameNode/ObjLevelLoader/LevelLoader");
            bool isEligible = false;
            List<LvlVert> verts = levelLoader.getEligibleMoves();
            
            String vertName = GetParent().GetParent().Name;
            
            foreach (LvlVert v in verts)
            {
                if (v.Id.ToString().Equals(vertName))
                {
                    isEligible = true;
                    break;
                }
            }
            
            if (isEligible && inputEventMouseBtn.Pressed && inputEventMouseBtn.ButtonIndex == (int)ButtonList.Left)
            {
                Game gameNode = GetNode<Game>("/root/GameNode");
                gameNode.PlayMoveSound();
                levelLoader.movePlayerTo(vertName);
            }
        }
    }
}

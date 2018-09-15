using Godot;
using System;

public class PlayerMovement : Area2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";

    [Export]
    public int speed = 3;

    private Vector2 screenSize;

    public override void _Ready()
    {
        
        screenSize = GetViewport().GetSize();
        
    }

    public override void _Process(float delta)
    {
            
        Vector2 velocity = new Vector2();

        if(Input.IsActionPressed("ui_left")){
            velocity.x -= 1;
        }
        if (Input.IsActionPressed("ui_right")){
            velocity.x += 1;
        }
        if (Input.IsActionPressed("ui_down")){
            velocity.y += 1;
        }
        if (Input.IsActionPressed("ui_up")){
            velocity.y -= 1;
        }

        velocity = velocity.Normalized() * speed;

        Position += velocity;

        Position = new Vector2(
            Mathf.Clamp(Position.x,0,screenSize.x),
            Mathf.Clamp(Position.y,0,screenSize.y)
        );

        var animatedSprite = (AnimatedSprite) GetNode("AnimatedSprite");

        if (velocity.Length() == 0){
            animatedSprite.Animation = "Selected";
        }else{
            animatedSprite.Animation = "UnSelected";
        }
    }
}

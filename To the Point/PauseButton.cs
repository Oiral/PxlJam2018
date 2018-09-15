using Godot;
using System;

public class PauseButton : Button
{

    public void _on_Button_pressed(){
        GD.Print("Pause!");
    }
}

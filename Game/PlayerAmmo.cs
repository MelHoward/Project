using twoDTDS.Engine;
using twoDTDS.Game;

/// <summary>
/// Player ammo class that allows player ammo to have its own logic
/// </summary>
public class Playerammo : Ammo
{
    public string direction;

    public Playerammo(Map m, double X, double Y) : base(m)
    {
        this.X = X;
        this.Y = Y;
        Width = 6;
        Height = 15;
        /*/string uri = "http://pixelartmaker.com/art/f59eaa826d4e49f.png";*/
        Sprite = new Rec(Width, Height, Asset.paths[0]);
    }

    public override void OnUpdate()
    {
        if (direction == "up") { Y -= 15; }
        if (direction == "down") { Y += 15; }
        if (direction == "left") { X -= 15; }
        if (direction == "right") { X += 15; }

        if (Y < -100) { ObDied = true; }
    }
}
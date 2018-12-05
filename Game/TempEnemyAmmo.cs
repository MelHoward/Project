using twoDTDS.Engine;
using twoDTDS.Game;

/// <summary>
/// Enemy ammo class that allows enemy ammo to have its own logic
/// </summary>
public class TempEnemyammo : Ammo
{
    public int Damage { get; set; } = 10;

    public TempEnemyammo(Map m, double X, double Y) : base(m)
    {
        this.X = X;
        this.Y = Y;
        Width = 15;
        Height = 15;
        /*string uri = "http://pixelartmaker.com/art/f59eaa826d4e49f.png";*/
        Sprite = new Rec(Width, Height, Asset.paths[2]);
    }

    public override void OnUpdate()
    {
        Y += 5;
        CheckOutOfBounds();
    }

}

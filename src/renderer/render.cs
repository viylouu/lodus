using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;
using System.Numerics;

partial class lodus {
    static void rend(ICanvas c) {
        if(Keyboard.IsKeyPressed(Key.C))
            c.Clear(Color.Black);

        map.rend(c);

        player.move();

        c.Fill(Color.White);
        c.DrawAlignedText($"{map.dat.Count} chunks", 48, Vector2.Zero, Alignment.TopLeft);
    }
}
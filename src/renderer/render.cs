using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;
using System.Numerics;

partial class lodus {
    static void rend(ICanvas c) {
        //if(Keyboard.IsKeyPressed(Key.C))
            c.Clear(Color.Black);

        map.rend(c);

        player.move();
    }
}
using SimulationFramework.Input;
using System.Numerics;

public class player {
    public static Vector3 pos;
    public static float zoom = 1;

    public static void move() {
        if(Mouse.IsButtonDown(MouseButton.Middle))
            pos += new Vector3(Mouse.DeltaPosition.X, 0, Mouse.DeltaPosition.Y);

        zoom -= Mouse.ScrollWheelDelta*0.125f;
        zoom = MathF.Max(zoom,0.125f);
    }
}
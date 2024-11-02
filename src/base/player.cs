using SimulationFramework.Input;
using SimulationFramework;
using System.Numerics;

public class player {
    public static Vector3 pos;
    public static float zoom = 1;

    public static void move(bool map) {
        if(map) {
            if(Mouse.IsButtonDown(MouseButton.Middle))
                pos += new Vector3(Mouse.DeltaPosition.X, 0, Mouse.DeltaPosition.Y);

            zoom -= Mouse.ScrollWheelDelta*0.125f;
            zoom = MathF.Max(zoom,0.125f);
        } else {
            if(Keyboard.IsKeyDown(Key.W)) {
                pos.X -= Time.DeltaTime;
                pos.Y -= Time.DeltaTime;
            }
            if(Keyboard.IsKeyDown(Key.S)) {
                pos.X += Time.DeltaTime;
                pos.Y += Time.DeltaTime;
            }
            if(Keyboard.IsKeyDown(Key.A)) {
                pos.X -= Time.DeltaTime;
                pos.Y += Time.DeltaTime;
            }
            if(Keyboard.IsKeyDown(Key.D)) {
                pos.X += Time.DeltaTime;
                pos.Y -= Time.DeltaTime;
            }
        }
    }
}
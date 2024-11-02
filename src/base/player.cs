using SimulationFramework.Input;
using SimulationFramework;
using System.Numerics;

public class player {
    public static Vector3 pos;
    public static float zoom = 1;
    public static Vector3 wpos;

    public static Vector3 pdcs;

    public static void move(bool inmap) {
        if(inmap) {
            if(Mouse.IsButtonDown(MouseButton.Middle))
                pos += new Vector3(Mouse.DeltaPosition.X, 0, Mouse.DeltaPosition.Y);

            zoom -= Mouse.ScrollWheelDelta*0.125f;
            zoom = math.max(zoom,0.125f);
        } else {
            float speed = 12;

            if(Keyboard.IsKeyDown(Key.W)) {
                pos.X -= Time.DeltaTime*speed;
                pos.Z -= Time.DeltaTime*speed;

                pdcs = pos/g.chksize;
                map.sxP = -pos.X*6+pos.Z*6;
                map.syP = pos.Y*6-pos.X*3-pos.Z*3;
            }
            if(Keyboard.IsKeyDown(Key.S)) {
                pos.X += Time.DeltaTime*speed;
                pos.Z += Time.DeltaTime*speed;

                pdcs = pos/g.chksize;
                map.sxP = -pos.X*6+pos.Z*6;
                map.syP = pos.Y*6-pos.X*3-pos.Z*3;
            }
            if(Keyboard.IsKeyDown(Key.A)) {
                pos.X -= Time.DeltaTime*speed;
                pos.Z += Time.DeltaTime*speed;

                pdcs = pos/g.chksize;
                map.sxP = -pos.X*6+pos.Z*6;
                map.syP = pos.Y*6-pos.X*3-pos.Z*3;
            }
            if(Keyboard.IsKeyDown(Key.D)) {
                pos.X += Time.DeltaTime*speed;
                pos.Z -= Time.DeltaTime*speed;

                pdcs = pos/g.chksize;
                map.sxP = -pos.X*6+pos.Z*6;
                map.syP = pos.Y*6-pos.X*3-pos.Z*3;
            }
        }
    }
}
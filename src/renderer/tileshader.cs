using SimulationFramework.Drawing.Shaders;
using System.Numerics;
using SimulationFramework;
using static SimulationFramework.Drawing.Shaders.ShaderIntrinsics;
using SimulationFramework.Drawing;

public class tileshader : CanvasShader {
    public ITexture atlas;

    public float drawx, drawy;
    public float samplex, sampley;

    public override ColorF GetPixelColor(Vector2 pos) {
        return atlas.Sample(new Vector2(pos.X-drawx+samplex,pos.Y-drawy+sampley));
    }
}
using SimulationFramework.Drawing.Shaders;
using System.Numerics;
using SimulationFramework;
using static SimulationFramework.Drawing.Shaders.ShaderIntrinsics;

public class dithergradient : CanvasShader {
    public int[] dithermatrix = {
        0, 48, 12, 60,  3, 51, 15, 63,
        32, 16, 44, 28, 35, 19, 47, 31,
        8, 56,  4, 52, 11, 59,  7, 55,
        40, 24, 36, 20, 43, 27, 39, 23,
        2, 50, 14, 62,  1, 49, 13, 61,
        34, 18, 46, 30, 33, 17, 45, 29,
        10, 58,  6, 54,  9, 57,  5, 53,
        42, 26, 38, 22, 41, 25, 37, 21
    };

    public int maxmatval = 8 * 8 - 1;

    public Vector2 startpos;
    public Vector2 endpos;

    public ColorF col1;
    public ColorF col2;

    public override ColorF GetPixelColor(Vector2 pos) {
        float distratio = getdistratio(startpos, endpos, pos);
        int intensity = (int)(distratio * maxmatval);

        bool whatcol = intensity < dithermatrix[(int)pos.X%8+(int)pos.Y%8*8];
        
        return whatcol?col1:col2;
    }

    float getdistratio(Vector2 start, Vector2 end, Vector2 pos) {
        float dx = end.X - start.X;
        float dy = end.Y - start.Y;

        float totalDistance = Sqrt(dx * dx + dy * dy);

        float nx = dx / totalDistance;
        float ny = dy / totalDistance;

        float px = pos.X - start.X;
        float py = pos.Y - start.Y;

        float projection = px * nx + py * ny;

        float ratio = projection/totalDistance;
        return Clamp(ratio,0,1);
    }
}
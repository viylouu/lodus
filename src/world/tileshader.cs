using SimulationFramework.Drawing.Shaders;
using System.Numerics;
using SimulationFramework;
using static SimulationFramework.Drawing.Shaders.ShaderIntrinsics;
using SimulationFramework.Drawing;
using ImGuiNET;

public class tileshader : CanvasShader {
    public ITexture atlas;
    public ITexture normals;
    public ITexture highlights;
    public ITexture mappings;

    public ITexture palette;
    public int psx;
    public int psy;

    public Vector3 sun;

    //public tile_s[] tiles;
    public stile[] scrn;

    public override ColorF GetPixelColor(Vector2 pos) {
        ColorF col = new();

        for(int i = scrn.Length-1; i >= 0; i--)
            if(pos.X >= scrn[i].p.X-8 && pos.Y >= scrn[i].p.Y-8 && pos.X <= scrn[i].p.X+8 && pos.Y <= scrn[i].p.Y+8) {
                //math.floor(tiles.t[block].tex*.0625f)*16,
                //tiles.t[block].tex%16*16,

                float sx = pos.X-scrn[i].p.X+8;
                float sy = pos.Y-scrn[i].p.Y+8;

                ColorF s = atlas.Sample(new(sx,sy));

                if(s.A == 0)
                    continue;

                int px = -1;
                int py = 0;

                for(int y = 0; y < psy; y++) {
                    for(int x = 0; x < psx; x++) {
                        ColorF samp = palette.Sample(new(x,y));

                        if(samp.A == 0)
                            break;

                        if(samp != s)
                            continue;

                        px = x;
                        py = y;

                        break;
                    }

                    if(px != -1)
                        break;
                }

                if(highlights.Sample(new(sx,sy)).R != 0)
                    px--;

                if(normals.Sample(new(sx,sy)).R != 0) {
                    if(sun.X == .5f)
                        px++;
                    if(sun.X == 0)
                        px+=2;
                }

                if(normals.Sample(new(sx,sy)).G != 0) {
                    if(sun.Y == .5f)
                        px++;
                    if(sun.Y == 0)
                        px+=2;
                }

                if(normals.Sample(new(sx,sy)).B != 0) {
                    if(sun.Z == .5f)
                        px++;
                    if(sun.Z == 0)
                        px+=2;
                }

                if(px < 1)
                    px = 1;

                if(px > psx-1)
                    px = psx-1;

                col = palette.Sample(new(px,py));
                break;
            }

        if(col.A == 0)
            Discard();

        return new ColorF(col.ToVector4());
    }
}
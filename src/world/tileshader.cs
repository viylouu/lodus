using SimulationFramework.Drawing.Shaders;
using System.Numerics;
using SimulationFramework;
using static SimulationFramework.Drawing.Shaders.ShaderIntrinsics;
using SimulationFramework.Drawing;

public class tileshader : CanvasShader {
    public ITexture atlas;
    public ITexture normals;
    public ITexture highlights;
    public ITexture mappings;

    public ITexture palette;

    public int psx;
    public int psy;

    public Vector3 sun;

    public tile_s[] tiles;
    public stile[] scrn;

    public float t;

    public override ColorF GetPixelColor(Vector2 pos) {
        ColorF col = new();

        float off = 0;

        for(int i = scrn.Length-1; i >= 0; i--) {
            if(tiles[scrn[i].b].liquid == 1) {
                float x = scrn[i].wp.X;
                float z = scrn[i].wp.Z;

                off = Sin(x+t);
                off += .6f * Sin(z-1.5f*t);
                off += .4f * Sin(2*x+2*z+2*t);
                off += .3f * Sin(3*x-z-2.5f*t);
                off *= 1.5f;
                off += 4;
            } else
                off = 0;

            if(pos.X >= scrn[i].p.X-7 && pos.Y >= scrn[i].p.Y-7+off && pos.X <= scrn[i].p.X+7 && pos.Y <= scrn[i].p.Y+7+off) {
                float sx = pos.X-scrn[i].p.X+8;
                float sy = pos.Y-scrn[i].p.Y+8;

                int ox = (tiles[scrn[i].b].tex >> 5) & 0b0011_1111;
                int oy = tiles[scrn[i].b].tex & 0b0001_1111;
                
                ColorF s = atlas.Sample(new(sx+ox*16,Mod(sy-off,16)+oy*16));

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

                if(px == -1) { px = 1; py = psy-1; }

                if(tiles[scrn[i].b].liquid == 0 && highlights.Sample(new(sx,sy-off)).R != 0)
                    px--;

                if(normals.Sample(new(sx,sy-off)).R != 0) {
                    if(sun.X == .5f)
                        px++;
                    if(sun.X == 0)
                        px+=2;
                }

                if(normals.Sample(new(sx,sy-off)).G != 0) {
                    if(sun.Y == .5f)
                        px++;
                    if(sun.Y == 0)
                        px+=2;
                }

                if(normals.Sample(new(sx,sy-off)).B != 0) {
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
        }

        if(col.A == 0)
            Discard();

        return new ColorF(col.ToVector4());
    }
}
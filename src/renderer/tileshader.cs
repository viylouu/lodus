using SimulationFramework.Drawing.Shaders;
using System.Numerics;
using SimulationFramework;
using static SimulationFramework.Drawing.Shaders.ShaderIntrinsics;
using SimulationFramework.Drawing;

public class tileshader : CanvasShader {
    public int[] dithermatrix = {
        15,07,11,09,
        07,05,03,07,
        11,00,13,09,
        09,07,09,05
    };

    /*public int[] dithermatrix = {
        15,15,7,7,11,11,9,9,
        7,7,5,5,3,3,7,7,
        11,11,0,0,13,13,9,9,
        9,9,7,7,9,9,5,5
    };*/

    public int maxmatval = 4 * 4 - 1;

    /*public void applydither(ITexture tex, int width, int height) {
        maxmatval = height * height - 1;

        dithermatrix = new int[height, height];

        int tiles = width / height;

        for(int t = 0; t < tiles; t++) {
            for(int x = 0; x < height; x++)
                for(int y = 0; y < height; y++)
                    if(tex.Sample(new(t * height + x, y)).R != 0)
                        dithermatrix[x,y] += 1;
        }
    }*/

    public ITexture atlas;
    public ITexture normals;
    public ITexture highlights;
    public ITexture mappings;

    public ITexture palette;
    public Vector2[] patlas;

    public int pasx;

    public int psx;
    public int psy;

    public Vector3 sun;

    public Vector2 cam;

    public tile_s[] tiles;
    public stile[] scrn;

    public float t;

    public int dither;

    public bool lit(Vector2 pos, float val) {
        pos += cam;

        int intensity = (int)(val * maxmatval);

        return intensity < dithermatrix[(int)pos.X % 4 + (int)pos.Y % 4 * 4];
    }

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

                int ax = (int)(sx+ox*16);
                int ay = (int)(Mod(sy-off,16)+oy*16);

                ColorF s = atlas.Sample(new(ax,ay));

                if(s.A == 0)
                    continue;

                //wrong
                /*Vector2 p = patlas[ax+ay*pasx];

                int px = (int)p.X;
                int py = (int)p.Y;*/

                //im just gonna use this because its correct, and there are no visible perf difference
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
                //

                if(px == -1) { px = 1; py = psy-1; }
        
                if(tiles[scrn[i].b].liquid == 0 && highlights.Sample(new(sx,sy-off)).R != 0)
                    px--;

                if(normals.Sample(new(sx,sy-off)).R != 0)
                    if(dither==1) {
                        if(sun.X <= 1 && sun.X >= .5f)
                            if(lit(pos, (sun.X-.5f)*2))
                                px++;
                        if(sun.X <= .5f) {
                            if(!lit(pos, sun.X * 2))
                                px++;
                            else
                                px+=2;
                        }
                    } else { 
                        if(math.round(sun.X*2) == 0)
                            px+=2;
                        else if(math.round(sun.X*2)==1)
                            px++;
                    }

                if(normals.Sample(new(sx,sy-off)).G != 0)
                    if(dither==1) {
                        if(sun.Y <= 1 && sun.Y >= .5f)
                            if(lit(pos, (sun.Y-.5f)*2))
                                px++;
                        if(sun.Y <= .5f) {
                            if(!lit(pos, sun.Y*2))
                                px++;
                            else
                                px+=2;
                        }
                    } else {
                        if(math.round(sun.Y*2)==0)
                            px += 2;
                        else if(math.round(sun.Y*2)==1)
                            px++;
                    }

                if(normals.Sample(new(sx,sy-off)).B != 0) 
                    if(dither==1) {
                        if(sun.Z <= 1 && sun.Z >= .5f)
                            if(lit(pos, (sun.Z-.5f)*2))
                                px++;
                        if(sun.Z <= .5f) {
                            if(!lit(pos, sun.Z*2))
                                px++;
                            else
                                px+=2;
                        }
                    } else { 
                        if(math.round(sun.Z*2) == 0)
                            px+=2;
                        else if(math.round(sun.Z*2)==1)
                            px++;
                    }

                if(px < 0)
                    px = 0;

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
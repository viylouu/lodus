using SimulationFramework;
using SimulationFramework.Drawing;

public class worldgen {
    static fnl cont = new(); //continental noise (most influential)
    static fnl b = new(); //big noise
    static fnl lerp = new(); //lerp between inquad big and outquad big

    static int seed = 0;

    static byte maxasync = 8;
    static byte async = 0;

    static Random r = new();

    public static void initnoise(int _seed) {
        seed = _seed;

        cont.SetNoiseType(fnl.NoiseType.Perlin);
        cont.SetFrequency(0.04f);
        cont.SetFractalType(fnl.FractalType.FBm);
        cont.SetSeed(seed);

        b.SetNoiseType(fnl.NoiseType.OpenSimplex2);
        b.SetFrequency(0.01f);
        b.SetSeed(seed);

        lerp.SetNoiseType(fnl.NoiseType.Perlin);
        lerp.SetFrequency(0.0025f);
        lerp.SetSeed(seed);
    }

    public static async void gen(int u, int v, int w) {
        map.genning[u,v,w] = true;
        ushort[,,] dat = new ushort[g.chksize,g.chksize,g.chksize];
        ITexture topview = Graphics.CreateTexture(g.chksize,g.chksize);

        float contxyz = 0;
        float bxyz = 0;
        float lerpxyz = 0;
        float contmul = 0;
        float contxyzm = 0;

        float wx, wy, wz;

        bool empty = true;
        
        for(int x = 0; x < g.chksize; x++) {
            for(int y = 0; y < 1; y++) {
                for(int z = 0; z < g.chksize; z++) {
                    async++;

                    if(async >= maxasync) {
                        async = 0;
                        await Task.Delay(16);
                    }

                    wx = u*g.chksize+x;
                    wy = v*g.chksize+y;
                    wz = w*g.chksize+z;

                    contxyz = (cont.GetNoise(wx,wy,wz)+1)*.5f;
                    bxyz = (b.GetNoise(wx,wy,wz)+1)*.5f;
                    lerpxyz = (lerp.GetNoise(wx,wy,wz)+1)*.5f;
                    contmul = float.Lerp(MathF.Pow(bxyz,6),1-MathF.Pow(1-bxyz,3),lerpxyz);
                    contxyzm = contxyz*contmul;

                    if(contxyz*bxyz >= 0.25f) {
                        dat[x,y,z] = tiles.grass.tex;
                        lock(topview)
                            topview.SetPixel(x,z,Color.Lerp(Color.Black,Color.White, contxyzm));
                        empty = false;
                        continue;
                    }

                    lock(topview)
                        topview.SetPixel(x, z, Color.Lerp(Color.Black, Color.Blue, contxyzm * 2));
                }
            }
        }

        map.dat[u,v,w] = new chunk() { data = dat, birdeye = topview, empty = empty, changed = true, genning = false, genned = true };
        map.genning[u,v,w] = false;
    }
}
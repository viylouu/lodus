using SimulationFramework;
using SimulationFramework.Drawing;

public class worldgen {
    static fnl cont = new(); //continental noise (most influential)
    static fnl b = new(); //big noise
    static fnl lerp = new(); //lerp between inquad big and outquad big

    static int seed = 0;

    static ushort maxasync = 1024;
    static ushort async = 0;

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
        
        for(int x = 0; x < g.chksize; x++)
            for(int z = 0; z < g.chksize; z++) {
                float height = 0;

                for(int y = 0; y < g.chksize; y++) {
                    async++;

                    if(async >= maxasync) {
                        async = 0;
                        await Task.Delay(1);
                    }

                    wx = u*g.chksize+x;
                    wy = v*g.chksize+y;
                    wz = w*g.chksize+z;

                    if(y == 0) {
                        contxyz = (cont.GetNoise(wx,wy,wz)+1)*.5f;
                        bxyz = (b.GetNoise(wx,wy,wz)+1)*.5f;
                        lerpxyz = (lerp.GetNoise(wx,wy,wz)+1)*.5f;
                        contmul = float.Lerp(MathF.Pow(bxyz,10),1-MathF.Pow(1-bxyz,3),lerpxyz);
                        contxyzm = contxyz*contmul;
                        height = contxyzm*g.chksize;
                    }

                    if(contxyzm >= 0.25f && y < height) {
                        dat[x,y,z] = tiles.grass.tex;
                        try {
                            lock(topview)
                                topview.SetPixel(x,z,Color.Lerp(Color.Black,Color.White, (float)y/g.chksize));
                        } catch(Exception e) { Console.WriteLine($"could not write to texture! {e.Message}"); }
                        empty = false;
                        continue;
                    }

                    if(y == 0)
                        try {
                            lock(topview)
                                topview.SetPixel(x, z, Color.Blue);
                        } catch(Exception e) { Console.WriteLine($"could not write to texture! {e.Message}"); }
                }
            }

        map.dat[u,v,w] = new chunk() { data = dat, birdeye = topview, empty = empty, changed = true, genning = false, genned = true };
        map.genning[u,v,w] = false;
    }
}
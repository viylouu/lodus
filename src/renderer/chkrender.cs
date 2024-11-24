using Silk.NET.OpenGL;
using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Drawing.Shaders;
using System.Numerics;

public struct stile { 
    public Vector2 p { get; set; }
    public int b { get; set; }
    public Vector3 wp { get; set; }
}

partial class map {
    static ITexture atlas;
    static ITexture normals;
    static ITexture highlights;
    static ITexture mappings;

    static ITexture palette;

    static int wmsX, wmsY;

    static Vector3 sun = new(.5f,1,0);

    public static tileshader tshader = new();

    //static ITexture dither;

    public static void init() {
        atlas = Graphics.LoadTexture(@"assets\sprites\tiles\atlas.png");
        normals = Graphics.LoadTexture(@"assets\sprites\tiledata\normals.png");
        highlights = Graphics.LoadTexture(@"assets\sprites\tiledata\highlights.png");
        mappings = Graphics.LoadTexture(@"assets\sprites\tiledata\mappings.png");

        palette = Graphics.LoadTexture(@"assets\misc\palette.png");
        //dither = Graphics.LoadTexture(@"assets\misc\dither.png");

        wmsX = (int)math.ceil(dat.GetLength(0)*g.chksize/1024f);
        wmsY = (int)math.ceil(dat.GetLength(2)*g.chksize/1024f);

        Console.WriteLine(wmsX + " maps (x)");
        Console.WriteLine(wmsY + " maps (y)");

        worldmap = new ITexture[wmsX,wmsY];

        for(int x = 0; x < wmsX; x++)
            for(int y = 0; y < wmsY; y++)
                worldmap[x,y] = Graphics.CreateTexture(1024,1024);

        tshader.atlas = atlas;
        tshader.normals = normals;
        tshader.highlights = highlights;
        tshader.mappings = mappings;

        tshader.palette = palette;
        tshader.psx = palette.Width;
        tshader.psy = palette.Height;

        //make patlas

        Vector2[] patlas = new Vector2[atlas.Width*atlas.Height];

        int px = 0, py = 0;

        for(int i = 0; i < atlas.Width; i++)
            for(int j = 0; j < atlas.Height; j++) {
                (px,py) = getp(i, j);

                patlas[i+j*atlas.Width] = new(px,py);
            }

        tshader.patlas = patlas;
        tshader.pasx = atlas.Width;

        tshader.dither = g.dthrlight?1:0;

        //tshader.applydither(dither, dither.Width, dither.Height);
    }

    static (int, int) getp(int i, int j) {
        int px = -1, py = 0;

        Color s = atlas[i, j];

        for(int y = 0; y < palette.Height; y++) {
            for(int x = 0; x < palette.Width; x++) {
                Color samp = palette[x, y];

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

        if(px == -1) { px = 0; py = palette.Height - 1; }

        return (px, py);
    }

    public static float sxP, syP, sxU, syU, sx, sy, ya, za, zb, yb;

    static int minx, miny, minz, maxx, maxy, maxz;

    public static int datLX, datLY, datLZ;

    static int maxTiles = 9*9*9*g.chksize*g.chksize*g.chksize;

    static List<stile> screen = new List<stile>();

    public static void rend(ICanvas c, bool inmap) {
        //c.Fill(tshader);

        fontie.c = c;
        
        if(inmap) { rendermap(c); return; }

        minx = (int)math.clamp(math.floor(player.pdcs.X)-3,0,datLX-1);
        miny = (int)math.clamp(math.floor(player.pdcs.Y)-3,0,datLY-1);
        minz = (int)math.clamp(math.floor(player.pdcs.Z)-3,0,datLZ-1);
        maxx = (int)math.clamp(math.floor(player.pdcs.X)+4,1,datLX);
        maxy = (int)math.clamp(math.floor(player.pdcs.Y)+4,1,datLY);
        maxz = (int)math.clamp(math.floor(player.pdcs.Z)+4,1,datLZ);

        screen.Clear();

        screen.Capacity = maxTiles;

        for(int v = miny; v < maxy; v++) {
        for(int w = minz; w < maxz; w++) {
        for(int u = minx; u < maxx; u++)
        if(u*g.chksize*6-w*g.chksize*6+sxP+320<640+g.chksize*6)
        if(u*g.chksize*6-w*g.chksize*6+sxP+320>g.chksize*-6)
        if(v*g.chksize*-6+w*g.chksize*3+u*g.chksize*3+syP+180<360+g.chksize*6)
        if(v*g.chksize*-6+w*g.chksize*3+u*g.chksize*3+syP+180>g.chksize*-6) {
            if(dat[u,v,w] == null) {
                if(genning[u,v,w])
                    continue;

                worldgen.gen(u,v,w);
                continue;
            }

            sxU = u*g.chksize*6-w*g.chksize*6;
            syU = v*g.chksize*-6+w*g.chksize*3+u*g.chksize*3;

            if(dat[u,v,w].genned && !dat[u,v,w].empty)
                for(int y = 0; y < g.chksize; y++) {
                    ya = y*-6;
                for(int z = 0; z < g.chksize; z++) {
                    za = z*3; zb = z*-6;
                for(int x = 0; x < g.chksize; x++)
                    if(dat[u,v,w].data[x,y,z] != 65535) {
                        if(x < g.chksize - 1 && y < g.chksize - 1 && z < g.chksize - 1 &&
                            dat[u,v,w].data[x+1,y,z] < 1024 && dat[u,v,w].data[x+1,y,z]%1024!=tiles.water.tex &&
                            dat[u,v,w].data[x,y+1,z] < 1024 && dat[u,v,w].data[x,y+1,z]%1024!=tiles.water.tex &&
                            dat[u,v,w].data[x,y,z+1] < 1024 && dat[u,v,w].data[x,y,z+1]%1024!=tiles.water.tex)
                            continue;

                        sx = 320+(sxU+x*6+zb)+sxP;
                        sy = 180+(syU+ya+za+x*3)+syP;

                        if(sx > -16 && sy > -16 && sx < 656 && sy < 656)
                            screen.Add(new() { p = new(math.floor(sx), math.floor(sy)), b = dat[u,v,w].data[x,y,z], wp = new(x+u*g.chksize,y+v*g.chksize,z+w*g.chksize) });
                    }
                }
                }
        }
        }
        }

        c.Fill(tshader);
        tshader.sun = sun;
        tshader.t = Time.TotalTime;
        tshader.scrn = screen.ToArray();
        tshader.cam = new Vector2(math.floor(sxP),math.floor(syP));
        c.DrawRect(0,0,640,360);
    }
}
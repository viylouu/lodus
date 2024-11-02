using Silk.NET.OpenGL;
using SimulationFramework;
using SimulationFramework.Drawing;
using System.Numerics;

partial class map {
    static ITexture atlas;

    static int wmsX, wmsY;

    public enum rendertype { 
        map,
        isometric
    }

    public static void init() {
        atlas = Graphics.LoadTexture(@"assets\sprites\tiles\atlas.png");

        wmsX = (int)math.ceil(dat.GetLength(0)*g.chksize/1024f);
        wmsY = (int)math.ceil(dat.GetLength(2)*g.chksize/1024f);

        Console.WriteLine(wmsX + " maps (x)");
        Console.WriteLine(wmsY + " maps (y)");

        worldmap = new ITexture[wmsX,wmsY];

        for(int x = 0; x < wmsX; x++)
            for(int y = 0; y < wmsY; y++)
                worldmap[x,y] = Graphics.CreateTexture(1024,1024);
    }

    public static void rend(ICanvas c, rendertype r) {
        if(r == rendertype.map) { rendermap(c); return; }

        int minx = (int)math.clamp(math.floor(player.pos.X/g.chksize)-1,0,dat.GetLength(0)-1),
            miny = (int)math.clamp(math.floor(player.pos.Y/g.chksize)-1,0,dat.GetLength(0)-1),
            minz = (int)math.clamp(math.floor(player.pos.Z/g.chksize)-1,0,dat.GetLength(0)-1),
            maxx = (int)math.clamp(math.floor(player.pos.X/g.chksize)+2,1,dat.GetLength(0)),
            maxy = (int)math.clamp(math.floor(player.pos.Y/g.chksize)+2,1,dat.GetLength(0)),
            maxz = (int)math.clamp(math.floor(player.pos.Z/g.chksize)+2,1,dat.GetLength(0));

        for(int v = miny; v < maxy; v++)
        for(int w = minz; w < maxz; w++)
        for(int u = minx; u < maxx; u++) {
            if(dat[u,v,w] == null) {
                if(genning[u, v, w])
                    continue;

                worldgen.gen(u,v,w);
                continue;
            }

            if(dat[u,v,w].genned && !dat[u,v,w].empty)
                for(int y = 0; y < g.chksize; y++) 
                for(int z = 0; z < g.chksize; z++)
                for(int x = 0; x < g.chksize; x++)
                    if(dat[u,v,w].data[x,y,z] != 65535) {
                        if(x < g.chksize-1 && y < g.chksize-1 && z < g.chksize-1) {
                            if(dat[u,v,w].data[x+1,y,z] < 256)
                            if(dat[u,v,w].data[x,y+1,z] < 256)
                            if(dat[u,v,w].data[x,y,z+1] < 256)
                                continue;
                        }

                        float sx = Window.Width/2+(u*g.chksize*6-w*g.chksize*6+x*6-z*6)-player.pos.X;
                        float sy = Window.Height/2+(v*g.chksize*-6+w*g.chksize*3+u*g.chksize*3-y*6+z*3+x*3)-player.pos.Y;

                        byte block = (byte)dat[u,v,w].data[x,y,z];

                        c.DrawTexture(
                            atlas, 
                            new Rectangle(
                                tiles.t[block].tex%16*16,
                                math.floor(tiles.t[block].tex/16)*16,
                                16,16
                            ), 
                            new Rectangle(sx,sy, 16,16, Alignment.Center)
                        );
                    }
        }
    }
}
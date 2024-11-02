using SimulationFramework;
using SimulationFramework.Drawing;

partial class map {
    static ITexture atlas;

    static int wmsX, wmsY;

    static tileshader tshader = new();

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

        tshader.atlas = atlas;
    }

    public static float sxP, syP, sxU, syU, sx, sy, ya, za, zb, yb;

    static int minx, miny, minz, maxx, maxy, maxz;

    public static int datLX, datLY, datLZ;

    public static void rend(ICanvas c, bool inmap) {
        c.Fill(tshader);

        if(inmap) { rendermap(c); return; }

        minx = (int)math.clamp(math.floor(player.pdcs.X)-3,0,datLX-1);
        miny = (int)math.clamp(math.floor(player.pdcs.Y)-3,0,datLY-1);
        minz = (int)math.clamp(math.floor(player.pdcs.Z)-3,0,datLZ-1);
        maxx = (int)math.clamp(math.floor(player.pdcs.X)+4,1,datLX);
        maxy = (int)math.clamp(math.floor(player.pdcs.Y)+4,1,datLY);
        maxz = (int)math.clamp(math.floor(player.pdcs.Z)+4,1,datLZ);

        for(int v = miny; v < maxy; v++)
        for(int w = minz; w < maxz; w++)
        for(int u = minx; u < maxx; u++) {
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
                        if(x < g.chksize-1 && y < g.chksize-1 && z < g.chksize-1) {
                            if(dat[u,v,w].data[x+1,y,z] < 256)
                            if(dat[u,v,w].data[x,y+1,z] < 256)
                            if(dat[u,v,w].data[x,y,z+1] < 256)
                                continue;
                        }

                        sx = 320+(sxU+x*6+zb)+sxP;
                        sy = 180+(syU+ya+za+x*3)+syP;
                        if(sx > -16 && sy > -16 && sx < 656 && sy < 656) {
                            byte block = (byte)dat[u,v,w].data[x,y,z];

                            if(block == tiles.water.tex)
                                yb = math.sin(u*g.chksize+x+Time.TotalTime*.75f)*1.5f+math.cos(w*g.chksize+z+Time.TotalTime)*1.5f;
                            else
                                yb = 0;
                            
                            //new Rectangle(
                            //    math.floor(tiles.t[block].tex*.0625f)*16,
                            //    tiles.t[block].tex%16*16,
                            //    16,16
                            //)

                            tshader.drawx = sx-8;
                            tshader.drawy = sy+yb-8;
                            tshader.samplex = math.floor(tiles.t[block].tex*.0625f)*16;
                            tshader.sampley = tiles.t[block].tex%16*16;

                            c.DrawRect(sx,sy+yb, 16,16, Alignment.Center);
                        }
                    }
                }
                }
        }
    }
}
using SimulationFramework;
using SimulationFramework.Drawing;
using System.Numerics;

partial class map {
    static ITexture atlas;

    static int wmsX, wmsY;

    public static void init() {
        atlas = Graphics.LoadTexture(@"assets\sprites\tiles\atlas.png");

        wmsX = (int)Math.Ceiling(dat.GetLength(0)*g.chksize/1024f);
        wmsY = (int)Math.Ceiling(dat.GetLength(2)*g.chksize/1024f);

        Console.WriteLine(wmsX + " maps (x)");
        Console.WriteLine(wmsY + " maps (y)");

        worldmap = new ITexture[wmsX,wmsY];

        for(int x = 0; x < wmsX; x++)
            for(int y = 0; y < wmsY; y++)
                worldmap[x,y] = Graphics.CreateTexture(1024,1024);
    }

    public static void rend(ICanvas c) {
        //worldmap view

        Vector2 cam = new(player.pos.X,player.pos.Z);

        Vector2 size = new Vector2(1024,1024)/player.zoom;

        for(int x = 0; x < dat.GetLength(0); x++) 
            for(int z = 0; z < dat.GetLength(2); z++) {
                Vector2 wp = cam + new Vector2(x * g.chksize, z * g.chksize)/player.zoom;

                if(wp.X > -g.chksize/player.zoom && wp.Y > -g.chksize/player.zoom && wp.X < Window.Width && wp.Y < Window.Height) {
                    if(dat[x,0,z] != null) {
                        if(!dat[x,0,z].genning && !dat[x,0,z].genned) {
                            dat[x,0,z].genning = true;
                            worldgen.gen(x,0,z);
                        } else {
                            if(dat[x,0,z].changed) {
                                dat[x,0,z].birdeye.ApplyChanges();
                                dat[x,0,z].changed = false;

                                int wx = (int)MathF.Floor(x*g.chksize/1024f),
                                    wy = (int)MathF.Floor(z*g.chksize/1024f);

                                for(int u = 0; u < g.chksize; u++)
                                    for(int v = 0; v < g.chksize; v++) {
                                            Color set = dat[x,0,z].birdeye[u,v];

                                            worldmap[wx,wy][(u+x*g.chksize)%1024,(v+z*g.chksize)%1024] = set;
                                        } //catch(Exception e) { Console.WriteLine($"wx: {wx}, wy: {wy}, wmx: {u+(x*g.chksize)%1024}, wmy: {v+(z*g.chksize)%1024}, x: {x}, z: {z}, u: {u}, v: {v}"); }

                                worldmap[wx,wy].ApplyChanges();
                            }

                            //c.DrawTexture(dat[x,0,z].birdeye, wp, size);
                        }
                    } else {
                        if(!genning[x,0,z])
                            worldgen.gen(x,0,z);
                    }
                }
            }

        for(int x = 0; x < wmsX; x++)
            for(int y = 0; y < wmsY; y++)
                c.DrawTexture(worldmap[x,y], cam + new Vector2(x*1024,y*1024)/player.zoom, size);

        //chunks loaded percentage
        c.Fill(Color.White);
        c.DrawAlignedText($"{Math.Round((float)map.chunksloaded/(float)(map.dat.GetLength(0)*map.dat.GetLength(2))*10000)/100}% explored", 48, new(Window.Width-3,3), Alignment.TopRight);
    }
}
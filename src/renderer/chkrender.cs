using Silk.NET.Core;
using SimulationFramework;
using SimulationFramework.Drawing;
using System.Numerics;

partial class map {
    static int a = 0;

    public static void rend(ICanvas c) {
        //worldmap view

        Vector2 cam = new(player.pos.X,player.pos.Z);

        //Vector2 min = new(0,0);
        //Vector2 max = new();

        //(chunk c, Vector3 p)[] v = dat.ToArray();
        /*.Where(chunkInfo =>
            chunkInfo.p.X >= min.X && chunkInfo.p.X <= max.X &&
            chunkInfo.p.Z >= min.Y && chunkInfo.p.Z <= max.Y
        ).ToArray()*/

        int renders = 0;

        Vector2 size = new Vector2(g.chksize, g.chksize)/player.zoom;

        for(int x = 0; x < dat.GetLength(0); x++) 
            for(int z = 0; z < dat.GetLength(2); z++) {
                Vector2 wp = cam + new Vector2(x * g.chksize, z * g.chksize)/player.zoom;

                if(wp.X > -g.chksize/player.zoom && wp.Y > -g.chksize/player.zoom && wp.X < Window.Width && wp.Y < Window.Height) {
                    if(dat[x,0,z] != null) {
                        if(!dat[x,0,z].genning && !dat[x,0,z].genned) {
                            dat[x,0,z].genning = true;
                            worldgen.gen(x,0,z);
                        } else {
                            if(dat[x,0,z].changed)
                                dat[x,0,z].birdeye.ApplyChanges();

                            c.DrawTexture(dat[x,0,z].birdeye, wp, size);
                            renders++;
                        }
                    } else {
                        c.Fill(Color.Gray);
                        c.DrawRect(wp, size);
                    }
                } else
                    renders++;
            }

        a = (a+renders)%(dat.GetLength(0)*dat.GetLength(2));
    }
}
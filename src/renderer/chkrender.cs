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

        for(int i = a; i < Math.Min(a+512,dat.Count); i++) {
            Vector2 wp = cam + new Vector2(dat[i].p.X * g.chksize, dat[i].p.Z * g.chksize)/player.zoom;

            if(wp.X > -g.chksize/player.zoom && wp.Y > -g.chksize/player.zoom && wp.X < Window.Width && wp.Y < Window.Height) {
                if(dat[i].c != null) {
                    if(!dat[i].c.genning && !dat[i].c.genned) {
                        dat[i].c.genning = true;
                        worldgen.gen(i);
                    } else {
                        if(dat[i].c.changed)
                            dat[i].c.birdeye.ApplyChanges();

                        c.DrawTexture(dat[i].c.birdeye, wp, size);
                        renders++;
                    }
                } else {
                    c.Fill(Color.Gray);
                    c.DrawRect(wp, size);
                }
            } else
                renders++;
        }

        a = (a+renders)%dat.Count;
    }
}
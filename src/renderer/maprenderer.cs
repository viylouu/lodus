﻿using System.Numerics;
using SimulationFramework;
using SimulationFramework.Drawing;

partial class map { 
    static void rendermap(ICanvas c) { 
        Vector2 cam = new(player.pos.X,player.pos.Z);

        float zoom1d = 1 / player.zoom;

        Vector2 size = new Vector2(1024,1024)*zoom1d;

        for(int x = 0; x < dat.GetLength(0); x++) 
            for(int z = 0; z < dat.GetLength(2); z++) {
                Vector2 wp = cam + new Vector2(x * g.chksize, z * g.chksize)*zoom1d;

                if(wp.X > -g.chksize*zoom1d && wp.Y > -g.chksize*zoom1d && wp.X < Window.Width && wp.Y < Window.Height) {
                    if(dat[x,0,z] != null) {
                        if(!dat[x,0,z].genning && !dat[x,0,z].genned) {
                            //dat[x,0,z].genning = true;
                            //worldgen.gen(x,0,z);
                        } else {
                            if(dat[x,0,z].changed) {
                                dat[x,0,z].birdeye.ApplyChanges();
                                dat[x,0,z].changed = false;

                                int wx = (int)math.floor(x*g.chksize*.0009765625f), //divide by 1024
                                    wy = (int)math.floor(z*g.chksize*.0009765625f);

                                for(int u = 0; u < g.chksize; u++)
                                    for(int v = 0; v < g.chksize; v++) {
                                        Color set = dat[x,0,z].birdeye[u,v];

                                        worldmap[wx,wy][(u+x*g.chksize)%1024,(v+z*g.chksize)%1024] = set;
                                    }

                                worldmap[wx,wy].ApplyChanges();
                            }
                        }
                    } /*else {
                        if(!genning[x,0,z])
                            worldgen.gen(x,0,z);
                    }*/
                }
            }

        for(int x = 0; x < wmsX; x++)
            for(int y = 0; y < wmsY; y++)
                c.DrawTexture(worldmap[x,y], cam + new Vector2(x*1024,y*1024)*zoom1d, size);

        //chunks loaded percentage
        string text = $"{math.round((float)chunksloaded/(float)(dat.GetLength(0)*dat.GetLength(2))*10000)/100}% explored";
        fontie.rendertext(fontie.dfont, text, Window.Width-3-fontie.predicttextwidth(fontie.dfont, text), 3, Color.White);
    }
}
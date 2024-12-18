﻿using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;

partial class lodus {
    static List<float> fpsses = new List<float>();
    static float fpsavg;
    static float fpstot;

    static bool inmap = false;

    static bool debug = false;

    static bool ingame = true;

    static void rend(ICanvas c) {
        c.Clear(Color.Black);

        fontie.c = c;

        if(ingame)
            ingameupdate(c);

        if(!ingame)
            menu.mainmenu(c);

        debugupdate(c);
    }

    static void debugupdate(ICanvas c) {
        if(debug)
            debugmenu(c);

        if(Keyboard.IsKeyPressed(Key.F2))
            debug = !debug;
    }

    static void ingameupdate(ICanvas c) {
        if(Keyboard.IsKeyPressed(Key.M)) {
            inmap = !inmap;
            if(inmap) {
                player.wpos = player.pos;
                player.pos *= -1;
            } else
                player.pos = player.wpos;
        }

        map.rend(c, inmap);

        player.move(inmap);
    }

    static void perfgraph(ICanvas c) { 
        float fps = 1 / Time.DeltaTime;
        fpsses.Add(fps);

        if(fpsses.Count > 128)
            fpsses.RemoveAt(0);

        c.Fill(Color.White);

        fpstot = 0;

        for(int i = 0; i < fpsses.Count; i++) {
            c.DrawRect(i, Window.Height, 1, fpsses[i],Alignment.BottomLeft);
            fpstot += fpsses[i];
        }

        fpsavg = fpstot / fpsses.Count;

        c.Fill(Color.LightGray);

        c.DrawRect(fpsses.Count, Window.Height, 1, 60, Alignment.BottomLeft);
        c.DrawRect(0, Window.Height - 60, fpsses.Count, 1);
        c.DrawRect(0, Window.Height - 30, fpsses.Count, 1);
        c.DrawRect(0, Window.Height - fpsavg-1, fpsses.Count, 3);

        c.DrawAlignedText("60", 8, new(fpsses.Count/2, Window.Height - 72), Alignment.CenterLeft);
        c.DrawAlignedText("30", 8, new(fpsses.Count/2, Window.Height - 42), Alignment.CenterLeft);
        c.DrawAlignedText(math.round(fpsavg) + "", 8, new(fpsses.Count/2, Window.Height - fpsavg-12), Alignment.CenterLeft);
    }
}
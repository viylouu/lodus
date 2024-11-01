﻿using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;
using System.Numerics;

partial class lodus {
    static List<float> fpsses = new List<float>();
    static float fpsavg;
    static float fpstot;

    static void rend(ICanvas c) {
        //if(Keyboard.IsKeyPressed(Key.C))
            c.Clear(Color.Black);

        map.rend(c);

        player.move();

        perfgraph(c);
    }

    static void perfgraph(ICanvas c) { 
        float fps = 1 / Time.DeltaTime;
        fpsses.Add(fps);

        if(fpsses.Count > 256)
            fpsses.RemoveAt(0);

        c.Fill(Color.White);

        fpstot = 0;

        for(int i = 0; i < fpsses.Count; i++) {
            c.DrawRect(i, Window.Height, 1, fpsses[i],Alignment.BottomLeft);
            fpstot += fpsses[i];
        }

        fpsavg = fpstot / fpsses.Count;

        c.Fill(Color.LightGray);

        c.DrawRect(fpsses.Count+1, Window.Height, 1, 60, Alignment.BottomLeft);
        c.DrawRect(0, Window.Height - 60, fpsses.Count, 1);
        c.DrawRect(0, Window.Height - 30, fpsses.Count, 1);
        c.DrawRect(0, Window.Height - fpsavg-1, fpsses.Count, 3);

        c.DrawAlignedText("60", 8, new(fpsses.Count/2, Window.Height - 72), Alignment.CenterLeft);
        c.DrawAlignedText("30", 8, new(fpsses.Count/2, Window.Height - 42), Alignment.CenterLeft);
        c.DrawAlignedText(Math.Round(fpsavg) + "", 8, new(fpsses.Count/2, Window.Height - fpsavg-12), Alignment.CenterLeft);
    }
}
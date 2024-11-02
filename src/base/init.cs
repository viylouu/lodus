﻿using SimulationFramework;

partial class lodus {
    static void init() {
        Window.Title = "lodus";

        Console.WriteLine("creating tiles");

        tiles.initarr();

        Console.WriteLine("noise initialisation");

        worldgen.initnoise(new Random().Next(int.MinValue,int.MaxValue));

        Console.WriteLine("initializing large arrays");

        map.datLX = 256; map.datLY = 256; map.datLZ = 256;
        map.dat = new chunk[map.datLX,map.datLY,map.datLZ];
        map.genning = new bool[map.datLX,map.datLY,map.datLZ];

        Console.WriteLine("map initialization");

        map.init();

        Console.WriteLine("finished initialization!");

        Simulation.SetFixedResolution(640,360,Color.Black);
    }
}
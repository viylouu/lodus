using SimulationFramework;

partial class lodus {
    static void init() {
        Window.Title = "lodus";

        Console.WriteLine("creating tiles");

        tiles.initarr();

        Console.WriteLine("noise initialisation");

        worldgen.initnoise(new Random().Next(int.MinValue,int.MaxValue));

        Console.WriteLine("initializing large arrays");

        map.dat = new chunk[256, 256, 256];
        map.genning = new bool[256, 256, 256];

        Console.WriteLine("map initialization");

        map.init();

        Console.WriteLine("finished initialization!");

        //Simulation.SetFixedResolution(640,360,Color.Black);
    }
}
using SimulationFramework;

partial class lodus {
    static void init() {
        Window.Title = "lodus";

        Console.WriteLine("creating tiles");

        tiles.initarr();

        Console.WriteLine("noise initialisation");

        worldgen.initnoise(new Random().Next(int.MinValue,int.MaxValue));

        Console.WriteLine("generating chunks");

        map.dat = new chunk[256,256,256];
        map.genning = new bool[256,256,256];

        //Simulation.SetFixedResolution(640,360,Color.Black);
    }
}
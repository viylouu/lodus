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

        /*int i = 0;

        int total = 128 * 64;

        for(int x = 0; x < 128; x++)
            for(int z = 0; z < 64; z++) {
                worldgen.gen(x,0,z);
                i++;

                if(i%64==0)
                    Console.WriteLine($"generated chunk {i} / {total}");
            }*/

        Simulation.SetFixedResolution(640,360,Color.Black);
    }
}
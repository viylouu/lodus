using SimulationFramework;

partial class lodus {
    static void init() {
        Window.Title = "lodus";

        Console.WriteLine("creating tiles");

        tiles.initarr();

        Console.WriteLine("noise initialisation");

        worldgen.initnoise(new Random().Next(int.MinValue,int.MaxValue));

        Console.WriteLine("initializing large arrays");

        map.datLX = 1024; map.datLY = 1024; map.datLZ = 1024;
        map.dat = new chunk[map.datLX,map.datLY,map.datLZ];
        map.genning = new bool[map.datLX,map.datLY,map.datLZ];

        Console.WriteLine("map initialization");

        map.init();

        Console.WriteLine("misc initialization");

        Simulation.SetFixedResolution(640, 360, Color.Black);

        player.pos = new(map.datLX / 2 * g.chksize, 0, map.datLZ / 2 * g.chksize);

        initdebugmenu();

        menu.init();

        intro.loadintro();

        Console.WriteLine("finished initialization!");
    }
}
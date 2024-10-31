using SimulationFramework.Drawing;

partial class map {
    public static chunk[,,] dat = new chunk[0,0,0];
    public static bool[,,] genning = new bool[0,0,0];
    public static int chunksloaded = 0;
    public static ITexture[,] worldmap;
}
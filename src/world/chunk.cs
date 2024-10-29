using SimulationFramework.Drawing;

public class chunk {
    public ushort[,,] data; //stores the type and shape
    public bool empty = true;
    public ITexture birdeye;
    public bool genning;
    public bool changed;
    public bool genned;
}
using SimulationFramework;
using SimulationFramework.Desktop;

partial class lodus {
    static void Main() {
        Simulation _sim = Simulation.Create(init, rend);
        _sim.Run(new DesktopPlatform());
    }
}
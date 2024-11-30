using SimulationFramework;
using SimulationFramework.Drawing;

public static class simfutils {
    public static void trydispose(this ITexture todisp) {
        if(todisp != null)
            todisp.Dispose();
    }

    public static void trydispose(this ISound todisp) {
        if(todisp != null)
            todisp.Dispose();
    }

    public static void trydispose(this SoundPlayback todisp) {
        if(todisp != null)
            todisp.Dispose();
    }
}
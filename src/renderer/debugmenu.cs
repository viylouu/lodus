using SimulationFramework.Drawing;
using SimulationFramework;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

partial class lodus {
    static Process proc = Process.GetCurrentProcess();

    static float tomeg = 1f / (1024 * 1024);

    static bool windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    static string ver;
    static int verw;

    static string seed;
    static int seedw;

    static string cpu;
    static int cpuw;

    static string gpu;
    static int gpuw;

    static string fps,ram,pos;

    static void initdebugmenu() {
        seed = $"seed {worldgen.seed}";
        ver = "v0.0.1";

        seedw = fontie.predicttextwidth(fontie.dfont, seed);
        verw = fontie.predicttextwidth(fontie.dfont, ver);

        cpu = hwi("Win32_Processor", "Name");
        cpuw = fontie.predicttextwidth(fontie.dfont, cpu);

        gpu = hwi("Win32_VideoController", "Name");
        gpuw = fontie.predicttextwidth(fontie.dfont, gpu);
    }

    static void debugmenu(ICanvas c) {
        fps = $"{math.round(1 / Time.DeltaTime)} fps";
        ram = $"{math.round(proc.WorkingSet64*tomeg*100)/100f} MB used";
        pos = $"({math.round(player.pos.X)} x, {math.round(player.pos.Y)} y, {math.round(player.pos.Z)} z)";

        c.Fill(new ColorF(0,0,0,.25f));

        //left side
        c.DrawRect(1,1,fontie.predicttextwidth(fontie.dfont,fps)+2,fontie.dfont.charh+1);
        c.DrawRect(1,fontie.dfont.charh+2,fontie.predicttextwidth(fontie.dfont,ram)+2,fontie.dfont.charh+1);
        c.DrawRect(1,fontie.dfont.charh*2+3,verw+2,fontie.dfont.charh+1);
        c.DrawRect(1,fontie.dfont.charh*3+4,fontie.predicttextwidth(fontie.dfont,pos)+2,fontie.dfont.charh+1);
        c.DrawRect(1,fontie.dfont.charh*4+5,seedw+2,fontie.dfont.charh+1);

        //right side
        c.DrawRect(639,1,cpuw+2,fontie.dfont.charh+1,Alignment.TopRight);
        c.DrawRect(639,fontie.dfont.charh+2,gpuw+2,fontie.dfont.charh+1,Alignment.TopRight);

        //left side
        fontie.rendertext(fontie.dfont, fps, 2, 2, Color.White);
        fontie.rendertext(fontie.dfont, ram, 2, fontie.dfont.charh+3, Color.White);
        fontie.rendertext(fontie.dfont, ver, 2, fontie.dfont.charh*2+4, Color.White);
        fontie.rendertext(fontie.dfont, pos, 2, fontie.dfont.charh*3+5, Color.White);
        fontie.rendertext(fontie.dfont, seed, 2, fontie.dfont.charh*4+6, Color.White);

        //right side
        fontie.rendertext(fontie.dfont, cpu, 638-cpuw, 2, Color.White);
        fontie.rendertext(fontie.dfont, gpu, 638-gpuw, fontie.dfont.charh + 3, Color.White);
    }
    
    static string hwi(string wmiClass, string prop) {
        string a = "";

        if(windows) {
            try {
                using(var search = new ManagementObjectSearcher($"SELECT {prop} FROM {wmiClass}")) {
                    foreach(ManagementObject obj in search.Get())
                        a += obj[prop]?.ToString() + " ";
                }
            } catch(Exception e) {
                Console.WriteLine($"err: {e.Message}");
            }
        }

        if(a == "")
            a = "cant find hardware";
        else
            a = a.TrimEnd();

        return a;
    }
}
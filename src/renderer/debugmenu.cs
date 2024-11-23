using SimulationFramework.Drawing;
using SimulationFramework;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;

partial class lodus {
    static Process proc = Process.GetCurrentProcess();

    static float tomeg = 1f / (1024 * 1024);

    static bool windows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    static void debugmenu(ICanvas c) {
        string fps = $"{math.round(1 / Time.DeltaTime)} fps";
        string ram = $"{math.round(proc.WorkingSet64*tomeg*100)/100f} MB used";
        string ver = $"v0.0.1";

        string cpu = hwi("Win32_Processor", "Name");
        int cpuw = fontie.predicttextwidth(fontie.dfont, cpu);

        string gpu = hwi("Win32_VideoController", "Name");
        int gpuw = fontie.predicttextwidth(fontie.dfont, gpu);

        c.Fill(new ColorF(0,0,0,.25f));

        //left side
        c.DrawRect(1,1,fontie.predicttextwidth(fontie.dfont,fps)+2,fontie.dfont.charh+1);
        c.DrawRect(1,fontie.dfont.charh+2,fontie.predicttextwidth(fontie.dfont,ram)+2,fontie.dfont.charh+1);
        c.DrawRect(1,fontie.dfont.charh*2+3,fontie.predicttextwidth(fontie.dfont,ver)+2,fontie.dfont.charh+1);

        //right side
        c.DrawRect(639,1,cpuw+2,fontie.dfont.charh+1,Alignment.TopRight);
        c.DrawRect(639,fontie.dfont.charh+2,gpuw+2,fontie.dfont.charh+1,Alignment.TopRight);

        //left side
        fontie.rendertext(c, fontie.dfont, fps, 2, 2, Color.White);
        fontie.rendertext(c, fontie.dfont, ram, 2, fontie.dfont.charh+3, Color.White);
        fontie.rendertext(c, fontie.dfont, ver, 2, fontie.dfont.charh*2+4, Color.White);

        //right side
        fontie.rendertext(c, fontie.dfont, cpu, 638-cpuw, 2, Color.White);
        fontie.rendertext(c, fontie.dfont, gpu, 638-gpuw, fontie.dfont.charh + 3, Color.White);
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
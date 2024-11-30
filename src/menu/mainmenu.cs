using SimulationFramework;
using SimulationFramework.Drawing;

partial class menu {
    static void mainmenurender(ICanvas c) {
        float x = 320 + (sm.X - 320) / 16;
        float y = 64 + (sm.Y - 64) / 16;

        c.DrawTexture(logo, x, y, 154, 36, Alignment.Center);

        rot = math.sin(Time.TotalTime) * .125f;
    }

    static void drawmenulines(ICanvas c) {
        for(int i = 0; i < 64; i++) {
            c.Fill(menupalette[1, 0]);

            float p = i * 24 + Time.TotalTime * 12 % 24 - 32 * 12;

            //represents y = x/(x^2+c) where x is '(mousex-p) * 1/size'

            float x = (sm.X - p) * .06125f;

            p -= x / (x * x + .05f) * 24;

            c.Translate(p, 180);
            c.Rotate(rot);

            c.DrawRect(0, 0, 1, 1024, Alignment.Center);

            c.ResetState();
        }

        for(int i = 0; i < 64; i++) {
            c.Fill(menupalette[1, 0]);

            float p = i * 24 + Time.TotalTime * 12 % 24 - 32 * 12;

            float x = (sm.Y - p) * .06125f;

            p -= x / (x * x + .05f) * 24;

            c.Translate(320, p);
            c.Rotate(rot + float.Pi / 2f);

            c.DrawRect(0, 0, 1, 1024, Alignment.Center);

            c.ResetState();
        }
    }
}
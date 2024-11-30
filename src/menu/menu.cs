using SimulationFramework;
using SimulationFramework.Drawing;
using SimulationFramework.Input;
using System.Numerics;

partial class menu {
    static ISound menusfx;
    static SoundPlayback menumusic;

    static ITexture menupalette;

    static ITexture logo;

    const float musicstarttime = 2.5f;

    static float introendtime = 0;

    const float waittime = 0;

    static bool playedintro = false;

    static Vector2 sm;

    static float rot = 0;

    static mstatus state = mstatus.mainmenu;

    // settings
    static bool playintro = true;

    enum mstatus {
        mainmenu,
        settings,
        ingame
    }

    public static void init() {
        trydisposeassets();
        loadassets();
    }

    public static void mainmenu(ICanvas c) {
        sm += (Mouse.Position - sm) / (16 / (Time.DeltaTime * 60));

        //intro stuff
        if(playintro) {
            if(dointro(c))
                return;
        } else
            introendtime = float.NegativeInfinity;

        bool fade = Time.TotalTime - introendtime <= musicstarttime;
        if(fade) {
            fading(c);
            return;
        }

        drawbg(c);

        switch(state) {
            case mstatus.mainmenu:
                mainmenurender(c);
                break;
        }
    }

    static void drawbg(ICanvas c) {
        if(state != mstatus.ingame) {
            c.Fill(menupalette[0, 0]);
            c.DrawRect(0, 0, 640, 360);

            drawmenulines(c);
        }
    }

    //returns a bool based on if it should continue to the next stuff or not
    static bool dointro(ICanvas c) {
        //start playing intro on the first frame it can
        if(Time.TotalTime > waittime && !playedintro) {
            intro.playintro();
            playedintro = true;
        }

        //do the intro
        if(!intro.introplayed) {
            intro.dointro(c, fontie.dfont);

            // stop intro
            if(intro.introplayed) {
                introendtime = Time.TotalTime;
                menumusic = menusfx.Loop();
                state = mstatus.mainmenu;
            }

            return true;
        }

        return false;
    }

    static void fading(ICanvas c) {
        c.Fill(Color.DarkGray);
        c.DrawRect(0, 0, 640, 360);

        float col = (Time.TotalTime - introendtime) / musicstarttime;
        col = math.pow(col, 12);

        c.Fill(new ColorF(0, 0, 0, 1 - col));
        c.DrawRect(0, 0, 640, 360);

        return;
    }
}
using SimulationFramework;
using SimulationFramework.Drawing;

partial class menu {
    static void trydisposeassets() {
        menusfx.trydispose();
        menumusic.trydispose();

        menupalette.trydispose();
        logo.trydispose();
    }

    static void loadassets() {
        menusfx = Audio.LoadSound(@"assets\audio\music\main menu.wav");

        menupalette = Graphics.LoadTexture(@"assets\misc\menupalette.png");
        logo = Graphics.LoadTexture(@"assets\sprites\menu\title.png");
    }
}
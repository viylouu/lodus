public class tiles {
    /// <summary> stores tile data in a way to easily access it by indexing it with the map's data at i[x,y,z] </summary>
    public static tile[] t;

    public static void initarr() {
        t = new tile[256];

        t[grass.tex] = grass;
        t[dirt.tex] = dirt;
        t[water.tex] = water;
        t[rhyolite.tex] = rhyolite;
        t[andesite.tex] = andesite;
        t[basalt.tex] = basalt;
        t[granite.tex] = granite;
        t[diorite.tex] = diorite;
        t[gabbro.tex] = gabbro;
        t[conglom.tex] = conglom;
        t[breccia.tex] = breccia;
        t[quartzar.tex] = quartzar;
        t[arkose.tex] = arkose;
        t[wacke.tex] = wacke;
        t[siltstn.tex] = siltstn;
        t[shale.tex] = shale;
    }

    //tiles

    public static tile grass = new() {
        tex = 0x00,
        name = "grass"
    };

    public static tile dirt = new() {
        tex = 0x01,
        name = "dirt"
    };

    public static tile water = new() {
        tex = 0x03,
        name = "water"
    };

    public static tile rhyolite = new() {
        tex = 0x20,
        name = "rhyolite"
    };

    public static tile andesite = new() {
        tex = 0x30,
        name = "andesite"
    };

    public static tile basalt = new() {
        tex = 0x40,
        name = "basalt"
    };

    public static tile granite = new() {
        tex = 0x21,
        name = "granite"
    };

    public static tile diorite = new() {
        tex = 0x31,
        name = "diorite"
    };

    public static tile gabbro = new() {
        tex = 0x41,
        name = "gabbro"
    };

    public static tile conglom = new() { 
        tex = 0x60,
        name = "conglomerate"
    };

    public static tile breccia = new() { 
        tex = 0x70,
        name = "breccia"
    };

    public static tile quartzar = new() { 
        tex = 0x61,
        name = "quartz arenite"
    };

    public static tile arkose = new() { 
        tex = 0x71,
        name = "arkose"
    };

    public static tile wacke = new() { 
        tex = 0x81,
        name = "wacke"
    };

    public static tile siltstn = new() {
        tex = 0x62,
        name = "siltstone"
    };

    public static tile shale = new() {
        tex = 0x72,
        name = "shale"
    };
}
public class tiles {
    static ushort toProper(ushort _val) {
        //uncompact it by splitting 8 bits from each side
        byte val_low = (byte)(_val & 0xFF); //least sig byte
        byte val_high = (byte)(_val >> 8); //most sig byte

        //clamp the values
        val_low = (byte)math.clamp(val_low, 0, math.pow(2, 5) - 1);
        val_high = (byte)math.clamp(val_low, 0, math.pow(2, 6) - 1);

        //merge them back and return
        return (ushort)((val_high << 5) | val_low);
    }

    static (byte, byte) uncompact(ushort _val) {
        _val >>= 5; //remove "garbage"

        byte val_low = (byte)(_val & 0b11111);
        byte val_high = (byte)((_val >> 5) & 0b111111);

        return (val_high, val_low);
    }

    /// <summary> stores tile data in a way to easily access it by indexing it with the map's data at i[x,y,z] </summary>
    public static tile[] t;

    public static tile_s[] tarr;

    public static void initarr() {
        t = new tile[256];
        tarr = new tile_s[256];

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
        t[gypsum.tex] = gypsum;
        t[rocksalt.tex] = rocksalt;

        for(int i = 0; i < 256; i++) {
            tarr[i] = new();
            if(t[i] != null) {
                tarr[i].tex = t[i].tex;
            }
        }
    }

    //tiles

    public static tile grass = new() {
        tex = 0x0000,
        name = "grass"
    };

    public static tile dirt = new() {
        tex = 0x0001,
        name = "dirt"
    };

    public static tile water = new() {
        tex = 0x0003,
        name = "water"
    };

    public static tile rhyolite = new() {
        tex = 0x0200,
        name = "rhyolite"
    };

    public static tile andesite = new() {
        tex = 0x0300,
        name = "andesite"
    };

    public static tile basalt = new() {
        tex = 0x0400,
        name = "basalt"
    };

    public static tile granite = new() {
        tex = 0x0201,
        name = "granite"
    };

    public static tile diorite = new() {
        tex = 0x0301,
        name = "diorite"
    };

    public static tile gabbro = new() {
        tex = 0x0401,
        name = "gabbro"
    };

    public static tile conglom = new() { 
        tex = 0x0600,
        name = "conglomerate"
    };

    public static tile breccia = new() { 
        tex = 0x0700,
        name = "breccia"
    };

    public static tile quartzar = new() { 
        tex = 0x0601,
        name = "quartz arenite"
    };

    public static tile arkose = new() { 
        tex = 0x0701,
        name = "arkose"
    };

    public static tile wacke = new() { 
        tex = 0x0801,
        name = "wacke"
    };

    public static tile siltstn = new() {
        tex = 0x0602,
        name = "siltstone"
    };

    public static tile shale = new() {
        tex = 0x0702,
        name = "shale"
    };

    public static tile gypsum = new() { 
        tex = 0x0A00,
        name = "gypsum"
    };

    public static tile rocksalt = new() {
        tex = 0x0A01,
        name = "rock salt"
    };
}
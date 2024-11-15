public class tile {
    public ushort tex;
    //even tho the tex is a ushort,
    //technically we only have 11 bits to work with
    //since an extra 5 bits are being used by the block type from the map data
    //so the max value of this is 2047
    //we can also make a function to convert 0xXXYY to the proper values and convert back

    public string name;
}

public struct tile_s { 
    public int tex;
}

partial class tiles {
    static ushort toProper(ushort _val) {
        //uncompact it by splitting 8 bits from each side
        byte val_low = (byte)(_val & 0xFF); //least sig byte
        byte val_high = (byte)(_val >> 8); //most sig byte

        //clamp the values
        val_low = (byte)math.clamp(val_low, 0, math.pow(2,5)-1);
        val_high = (byte)math.clamp(val_low, 0, math.pow(2,6)-1);

        //merge them back and return
        return (ushort)((val_high << 5) | val_low);
    }

    static (byte, byte) uncompact(ushort _val) {
        _val >>= 5; //remove "garbage"

        byte val_low = (byte)(_val & 0b11111);
        byte val_high = (byte)((_val >> 5) & 0b111111);

        return (val_high,val_low);
    }
}
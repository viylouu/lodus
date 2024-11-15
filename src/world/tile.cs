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
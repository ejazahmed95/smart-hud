namespace RangerRPG.Grids {
    [System.Flags]
    public enum HexDirection {
        None    = 0,
        Q       = 1<<0,
        QNeg    = 1<<1,
        R       = 1<<2,
        RNeg    = 1<<3,
        S       = 1<<4,
        SNeg    = 1<<5,
    }
}
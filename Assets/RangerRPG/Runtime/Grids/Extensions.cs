using System;

namespace RangerRPG.Grids {
    public static class HexDirectionExtensions {
        public static bool Has(this HexDirection current, HexDirection hasDirection) {
            return (current & hasDirection) == hasDirection;
        }

        public static HexDirection Add(this HexDirection current, HexDirection addDirection) {
            return current | addDirection;
        }
        
        public static HexDirection Remove(this HexDirection current, HexDirection removeDirection) {
            return current & (~removeDirection);
        }
        
        public static HexDirection Opposite(this HexDirection current) {
            return current switch {
                HexDirection.None => HexDirection.None,
                HexDirection.Q => HexDirection.QNeg,
                HexDirection.QNeg => HexDirection.Q,
                HexDirection.R => HexDirection.RNeg,
                HexDirection.RNeg => HexDirection.R,
                HexDirection.S => HexDirection.SNeg,
                HexDirection.SNeg => HexDirection.S,
                _ => HexDirection.None,
            };
        }
    }
}
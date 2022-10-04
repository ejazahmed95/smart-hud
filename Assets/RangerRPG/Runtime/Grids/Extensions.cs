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
    }
}

namespace RangerRPG.Grids {
    
    public delegate HexCellInfo CreateCellDelegate(AxialPosition position);
    
    [System.Serializable]
    public struct HexGridConfig {
        public int Width;
        public int Height;

        public HexGridConfig(int width, int height) {
            Width = width;
            Height = height;
        }
    }
}
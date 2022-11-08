using RangerRPG.Grids;

namespace FiaMaze.World {
    public class WorldCellInfo: HexCellInfo {

        public bool IsPlayerStart;
        public bool IsEndCell;
        public bool IsPlayerInside;
       
        
        public WorldCellInfo() {
            
        }

        public void SetPlayerStatus(bool isPlayerInside) {
            IsPlayerInside = isPlayerInside;
            Notify();
        }

        
    }
}
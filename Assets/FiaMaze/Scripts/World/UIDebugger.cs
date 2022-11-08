using RangerRPG.Core;
using TMPro;

namespace FiaMaze.World {
    public class UIDebugger : SingletonBehaviour<UIDebugger> {
        public TMP_Text textRef;

        public void SetInfo(WorldCellInfo info) {
            string text = $"Position = {info.Position.ToString()} \n" +
                          $"Directions = {info.AvailableDirections} \n" +
                          $"Combined = {info.CombinedDirections} \n" +
                          $"RoomId = {info.RoomId} \n";

            textRef.text = text;
        }
    }
}
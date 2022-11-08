using RangerRPG.Grids;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiaMaze.World {
    public class MinimapCell : CellBehavior<WorldCellInfo> {
        [SerializeField] private Image imageRef;
        [SerializeField] private TMP_Text roomIdText;
        
        private RectTransform _rt;

        private void Awake() {
            _rt = GetComponent<RectTransform>();
        }
        
        public override CellBehavior<WorldCellInfo> Init(WorldCellInfo info) {
            base.Init(info);
            //Log.Info($"pos {info.Position}");
            UpdateCellInfo(info);
            // GetComponent<RectTransform>().rect.Set(position.x, position.y, size, size);
            // _rt.anchoredPosition = new Vector3(info.Position.Q, info.Position.R, _rt.position.z);
            // _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10*1.8f);
            // _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 10*1.8f);
            return this;
        }

        protected override void UpdateCellInfo(WorldCellInfo info) {
            base.UpdateCellInfo(info);
            // Log.Debug($"Updating Cell Info {info.Position}");
            var cellColor = Color.white;
            if (info.Enabled == false) cellColor = Color.gray;
            else if (info.IsPlayerStart) cellColor = Color.red;
            else if (info.IsEndCell) cellColor = Color.green;
            else if (info.IsPlayerInside) cellColor = Color.yellow;
            imageRef.color = cellColor;

            if (roomIdText != null && info.RoomId != -1) {
                roomIdText.text = info.RoomId.ToString();
            }
        }
    }
}
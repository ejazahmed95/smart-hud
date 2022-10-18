using RangerRPG.Grids;
using UnityEngine;
using UnityEngine.UI;

namespace FiaMaze.World {
    public class MinimapCell : CellBehavior<WorldCellInfo> {
        [SerializeField] private Image imageRef;

        private RectTransform _rt;

        private void Awake() {
            _rt = GetComponent<RectTransform>();
        }
        public override CellBehavior<WorldCellInfo> Init(WorldCellInfo info) {
            base.Init(info);
            // Log.Info($"pos {position}");
            // GetComponent<RectTransform>().rect.Set(position.x, position.y, size, size);
            _rt.anchoredPosition = new Vector3(info.Position.Q, info.Position.R, _rt.position.z);
            _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10*1.8f);
            _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 10*1.8f);
            return this;
        }

        protected override void UpdateCellInfo(WorldCellInfo info) {
            base.UpdateCellInfo(info);
            
            imageRef.color = info.Enabled? Color.white: Color.gray;
        }
    }
}
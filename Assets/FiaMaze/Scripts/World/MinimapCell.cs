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
        public override void Init(Vector2 position, float size, WorldCellInfo info) {
            base.Init(position, size, info);
            // Log.Info($"pos {position}");
            // GetComponent<RectTransform>().rect.Set(position.x, position.y, size, size);
            _rt.anchoredPosition = new Vector3(position.x, position.y, _rt.position.z);
            _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size*1.8f);
            _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size*1.8f);
        }

        protected override void UpdateCellInfo(WorldCellInfo info) {
            base.UpdateCellInfo(info);
            
            imageRef.color = info.Enabled? Color.white: Color.gray;
        }
    }
}
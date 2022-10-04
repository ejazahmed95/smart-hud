using RangerRPG.Core;
using UnityEngine;

namespace _Developers.Ejaz {
    public class TestUIPosSet : MonoBehaviour {
        private RectTransform _rt;
        [SerializeField] private Vector2 position;
        [SerializeField] private float size;
        
        private void Awake() {
            _rt = GetComponent<RectTransform>();
            Log.Info($"Rect={_rt.rect}");
            Log.Info($"Anchored Position={_rt.anchoredPosition}");
            
            Init(position, size);
            
            
        }
        private void Init(Vector2 position, float size) {
            // Log.Info($"pos {position}");
            _rt.anchoredPosition = new Vector3(position.x, position.y, _rt.position.z);
            _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
            _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        }
    }
}
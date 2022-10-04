using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace RangerRPG.Grids {
    public class CellBehavior<T> : MonoBehaviour where T : HexCellInfo {
        private T _info = null;
        
        public virtual void Init(Vector2 position, float size, T info) {
            _info = info;
            _info.AddListener(OnCellInfoChange);
        }

        protected void OnEnable() {
            _info?.AddListener(OnCellInfoChange);
        }

        protected void OnDisable() {
            _info?.RemoveListener(OnCellInfoChange);
        }

        private void OnCellInfoChange() {
            UpdateCellInfo(_info);
        }

        protected virtual void UpdateCellInfo(T info) {
            
        }
    }
}
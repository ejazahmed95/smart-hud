using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace RangerRPG.Grids {
    public class CellBehavior<T> : MonoBehaviour where T : ICellInfo {
        protected T _info = default(T);
        
        public virtual CellBehavior<T> Init(T info) {
            _info = info;
            _info.AddListener(OnCellInfoChange);
            return this;
        }

        protected void OnEnable() {
            _info?.AddListener(OnCellInfoChange);
            OnCellInfoChange();
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
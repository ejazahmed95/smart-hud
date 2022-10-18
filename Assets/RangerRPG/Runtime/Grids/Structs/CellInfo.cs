﻿using UnityEngine;
using UnityEngine.Events;

namespace RangerRPG.Grids {
    public class CellInfo: ICellInfo {

        public Vector2Int Position { get; private set; }
        private bool _enabled;
        private UnityEvent _onChange;
        
        // Properties
        public bool Enabled {
            get => _enabled;
            set {
                _enabled = value;
                Notify();
            }
        }
        
        // Constructor
        public void Init(Vector2Int position, bool enabled = true) {
            Position = position;
            _enabled = true;
            _onChange = new UnityEvent();
        }
        
        public void AddListener(UnityAction changeListener) {
            _onChange.AddListener(changeListener);
        }

        public void RemoveListener(UnityAction changeListener) {
            _onChange.RemoveListener(changeListener);
        }
        
        protected void Notify() {
            _onChange.Invoke();
        }
    }

    public interface ICellInfo {
        void AddListener(UnityAction changeListener);
        void RemoveListener(UnityAction changeListener);
    }
}
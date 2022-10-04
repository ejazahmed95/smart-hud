
using UnityEngine.Events;

namespace RangerRPG.Grids {
    public class HexCellInfo {
        private AxialPosition _position;
        private bool _enabled;
        
        public bool Enabled {
            get => _enabled;
            set {
                _enabled = value;
                Notify();
            }
        }

        private UnityEvent _onChange = new UnityEvent();
        public AxialPosition Position => _position;

        protected HexCellInfo() { }

        public void Init(AxialPosition axialPosition) {
            _position = axialPosition;
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
}
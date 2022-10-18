
using UnityEngine.Events;

namespace RangerRPG.Grids {
    public class HexCellInfo: ICellInfo {
        
        public AxialPosition Position { get; private set; }
        private bool _enabled;
        private UnityEvent _onChange = new UnityEvent();
        
        public bool Enabled {
            get => _enabled;
            set {
                _enabled = value;
                Notify();
            }
        }
        
        protected HexCellInfo() { }

        public void Init(AxialPosition axialPosition) {
            Position = axialPosition;
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
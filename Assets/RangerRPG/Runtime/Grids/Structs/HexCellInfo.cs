
using UnityEngine.Events;

namespace RangerRPG.Grids {
    public class HexCellInfo: ICellInfo {
        
        public AxialPosition Position { get; private set; }
        public HexDirection AvailableDirections = HexDirection.None;
        public HexDirection CombinedDirections = HexDirection.None;
        public int RoomId = -1;
        
        private bool _enabled = true;
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

        public void AddAvailableDirection(HexDirection direction) {
            AvailableDirections = AvailableDirections.Add(direction);
        }
        
        public void AddCombinedDirection(HexDirection direction) {
            CombinedDirections = CombinedDirections.Add(direction);
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
        
        public void SetRoomId(int id) {
            RoomId = id;
            Notify();
        }
    }
}
using UnityEngine;

namespace RangerRPG.Grids {
    public struct AxialPosition {
        private Vector2Int _position;

        public int Q => _position.x;
        public int R => _position.y;
        public int S => -(_position.x + _position.y);

        public static implicit operator Vector2Int(AxialPosition aPos) {
            return aPos._position;
        }

        public AxialPosition(int q = 0, int r = 0) {
            _position = new Vector2Int(q, r);
        }

        public void SetPosition(Vector2Int newPos) {
            _position = newPos;
        }

        public override string ToString() {
            return _position.ToString();
        }
        
        public AxialPosition Next(HexDirection dir) {
            return dir switch {
                HexDirection.Q => new AxialPosition(Q + 1, R),
                HexDirection.QNeg => new AxialPosition(Q - 1, R),
                HexDirection.R => new AxialPosition(Q, R + 1),
                HexDirection.RNeg => new AxialPosition(Q, R - 1),
                HexDirection.S => new AxialPosition(Q + 1, R - 1),
                HexDirection.SNeg => new AxialPosition(Q - 1, R + 1),
                _ => new AxialPosition(Q, R)
            };
        }
    }
}
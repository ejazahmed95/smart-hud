using UnityEngine;

namespace RangerRPG.Grids {
    public interface ICellView {
        void Init(Vector2 position, float size);
    }
}
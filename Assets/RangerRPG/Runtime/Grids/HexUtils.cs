using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RangerRPG.Grids {
    public class HexUtils {
        public static readonly double Sqrt3 = Math.Sqrt(3);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AxialPosition IndexToAxial(int row, int col) {
            return new AxialPosition(col - row / 2, row);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2Int AxialToIndex(int q, int r) {
            return new Vector2Int(q + r / 2, r);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetPosition(AxialPosition coords, float size, bool xzPlane) {
            if (xzPlane) {
                return new Vector3((float) (Sqrt3 * coords.Q + (Sqrt3 / 2) * coords.R) * size, 0, (float) (1.5 * -coords.R) * size);
            }
            return new Vector3((float)(Sqrt3 * coords.Q + (Sqrt3 / 2) * coords.R) * size, (float)(1.5 * -coords.R) * size, 0);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RangerRPG.Core;
using UnityEngine;

namespace RangerRPG.Grids {
    public class HexGrid<T>: MonoBehaviour where T : HexCellInfo, new() {

        public static readonly double Sqrt3 = Math.Sqrt(3);

        [SerializeField] private HexGridConfig _config;
        private List<List<T>> _cellsInfo = new List<List<T>>();

        public HexGridConfig Config => _config;
        
        private void Awake() {
            Log.Info("Creating Cell Info");
            CreateCells(_config);
        }

        private void CreateCells(HexGridConfig config) {
            for (var row = 0; row < config.Height; row++) {
                List<T> rowCells = new List<T>();
                var offset = IndexToAxial(row, 0).Q;
                for (var col = 0; col < config.Width; col++) {
                    var cell = new T();
                    cell.Init(new AxialPosition(offset + col, row));
                    rowCells.Add(cell);
                }
                _cellsInfo.Add(rowCells);
            }
        }

        private void GenerateView(float cellSize, CreateCellViewDelegate createCellCb) {
            for (int row = 0; row < _cellsInfo.Count; row++) {
                for (int col = 0; col < _cellsInfo.Count; col++) {
                    // var position = 
                }
            }
        }

        public HexDirection GetAvailableDirections(AxialPosition position) {
            HexDirection dir = HexDirection.None;
            if (IsValidCell(position.Next(HexDirection.Q))) dir |= HexDirection.Q;
            if (IsValidCell(position.Next(HexDirection.QNeg))) dir |= HexDirection.QNeg;
            if (IsValidCell(position.Next(HexDirection.R))) dir |= HexDirection.R;
            if (IsValidCell(position.Next(HexDirection.RNeg))) dir |= HexDirection.RNeg;
            if (IsValidCell(position.Next(HexDirection.S))) dir |= HexDirection.S;
            if (IsValidCell(position.Next(HexDirection.SNeg))) dir |= HexDirection.SNeg;
            return dir;
        }

        private bool IsValidCell(AxialPosition position) {
            var index = AxialToIndex(position.Q, position.R);
            if (!(MathUtils.InRange(index.x, 0, _config.Height) && MathUtils.InRange(index.y, 0, _config.Width))) {
                return false;
            }
            return _cellsInfo[index.x][index.y].Enabled;
        }
        
        public T GetCell(AxialPosition pos) {
            var index = AxialToIndex(pos.Q, pos.R);
            return _cellsInfo[index.x][index.y];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private AxialPosition IndexToAxial(int row, int col) {
            return new AxialPosition(col - row / 2, row);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector2Int AxialToIndex(int q, int r) {
            return new Vector2Int(r, q + r / 2);
        }

        public void ForEachCell(Action<T> cbAction) {
            foreach (var rowCells in _cellsInfo) {
                foreach (var t in rowCells) {
                    cbAction(t);
                }
            }
        }
        public Vector2 GetPosition(AxialPosition coords, float size) {
            return new Vector2((float)(Sqrt3 * coords.Q + (Sqrt3 / 2) * coords.R) * size, (float)(1.5 * -coords.R) * size);
        }
    }

    public delegate ICellView CreateCellViewDelegate();
}
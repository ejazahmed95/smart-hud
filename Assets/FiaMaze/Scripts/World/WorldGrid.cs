using FiaMaze.World;
using RangerRPG.Grids;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Mono behaviour to generate the hex grid
/// </summary>
public class WorldGrid : HexGrid2D<WorldCellInfo> {

    [SerializeField] private GameObject mapParent;
    [SerializeField] private GameObject worldParent;
    [SerializeField] private MinimapCell minimapCellPrefab;
    [SerializeField] private WorldRoom worldCellPrefab;

    private HexGridView<WorldCellInfo> minimapView;
    public HexGridView<WorldCellInfo> worldView;
    
    [SerializeField][Range(0.1f, 0.4f)] private float disabledFrequency = 0.1f;
    
    private void Start() {
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    public void InitializeWorldInfo() {
        InitializeGrid();
        RandomDisable();
        SetAvailableDirections();
        RandomizeRoomSizes();
    }

    public void InitializeWorldView() {
        minimapView = CreateView(new HexGridView<WorldCellInfo>.ViewData {
            cellSize = 36,
            parent = mapParent.transform,
            prefab = minimapCellPrefab,
            proceduralGeneration = false,
            xzPlane = false,
        });

        worldView = CreateView(new HexGridView<WorldCellInfo>.ViewData {
            cellSize = 10,
            parent = worldParent.transform,
            prefab = worldCellPrefab,
            proceduralGeneration = true,
            xzPlane = true,
        });
    }

    private void RandomDisable() {
        ForEachCell(cell => {
            if (Random.value < disabledFrequency) cell.Enabled = false;
        });
    }

    public WorldCellInfo GetRandomCell() {
        while (true) {
            var row = Random.Range(0, gridDimensions.y);
            var col = Random.Range(0, gridDimensions.x);
            var cell = GetCell(new Vector2Int(col, row));
            if (cell.Enabled == false || cell.IsPlayerStart || cell.IsEndCell) {
                continue;
            }
            return cell;
        }
        
    }
}
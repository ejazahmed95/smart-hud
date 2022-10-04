using System.Collections;
using FiaMaze.World;
using RangerRPG.Grids;
using UnityEngine;

/// <summary>
/// Mono behaviour to generate the hex grid
/// </summary>
public class WorldGrid : HexGrid<WorldCellInfo> {

    [SerializeField][Range(0.1f, 0.4f)] private float disabledFrequency = 0.1f;
    private IEnumerator Start() {
        yield return new WaitForSeconds(5);
        Random.InitState((int)System.DateTime.Now.Ticks);
        RandomDisable();
    }
    private void RandomDisable() {
        ForEachCell(cell => {
            if (Random.value < disabledFrequency) cell.Enabled = false;
        });
    }
}
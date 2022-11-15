using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RangerRPG.Grids {
    public class HexPrimsCrawler : MonoBehaviour {

        public void CrawlGrid<T>(HexGrid2D<T> grid) where T : HexCellInfo, new() {
            var cell = grid.GetRandomCell();

            cell.Enabled = true;
            List<T> foundWalls = new();
            foundWalls.AddRange(grid.GetNeighbours(cell, HexDirection.All, info => info.Enabled == false));

            int loopCount = 0;
            while (foundWalls.Count != 0 && loopCount++ < 5000) {
                var index = Random.Range(0, foundWalls.Count);
                cell = foundWalls[index];
                foundWalls.RemoveAt(index);
                var neighbourOpenCells = grid.GetNeighbours(cell, HexDirection.All, info => info.Enabled);
                switch (neighbourOpenCells.Count) {
                    case > 2:
                    case > 1 when Random.value < 0.5f:
                        continue;
                    default:
                        cell.Enabled = true;
                        foundWalls.AddRange(grid.GetNeighbours(cell, HexDirection.All, info => info.Enabled == false));
                        break;
                }
            }

            //StartCoroutine(SlowlyUpdateCrawler(foundWalls, grid));
        }
        
        private IEnumerator SlowlyUpdateCrawler<T>(List<T> foundWalls, HexGrid2D<T> grid) where T : HexCellInfo, new() {
            int loopCount = 0;
            while (foundWalls.Count > 0 && loopCount++ < 10000) {
                var index = Random.Range(0, foundWalls.Count);
                var cell = foundWalls[index];
                foundWalls.RemoveAt(index);
                var neighbourOpenCells = grid.GetNeighbours(cell, HexDirection.All, info => info.Enabled);
                foundWalls.AddRange(grid.GetNeighbours(cell, HexDirection.All, info => info.Enabled == false));
                if (neighbourOpenCells.Count > 2) continue;
                cell.Enabled = true;
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
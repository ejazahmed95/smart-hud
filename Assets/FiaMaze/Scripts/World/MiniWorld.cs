using System;
using System.Collections.Generic;
using RangerRPG.Grids;
using UnityEngine;

namespace FiaMaze.World {
    public class MiniWorld : MonoBehaviour {

        [SerializeField] private List<Door> doors; // Doors added in the order Q, QNeg, R, RNeg, 
        
        public MiniWorld Init(WorldCellInfo currentCellInfo, HexDirection availableDirs) {
            EnableDoors(availableDirs);
            return this;
        }

        private void EnableDoors(HexDirection availableDirs) {
            doors[0].IsActive = availableDirs.HasFlag(HexDirection.Q);
            doors[1].IsActive = availableDirs.HasFlag(HexDirection.QNeg);
            doors[2].IsActive = availableDirs.HasFlag(HexDirection.R);
            doors[3].IsActive = availableDirs.HasFlag(HexDirection.RNeg);
            doors[4].IsActive = availableDirs.HasFlag(HexDirection.S);
            doors[5].IsActive = availableDirs.HasFlag(HexDirection.SNeg);
        }

        // TODO: Temp function, remove after doors is added as a dictionary
        private HexDirection IndexToDir(int index) {
            return index switch {
                0 => HexDirection.Q,
                1 => HexDirection.QNeg,
                2 => HexDirection.R,
                3 => HexDirection.RNeg,
                4 => HexDirection.S,
                5 => HexDirection.SNeg,
                _ => HexDirection.None
            };
        }
    }
}


///
/// Create the door script
/// Scene: Create a mini world: Place Six Doors
/// Enabling and disabling door implementation
/// Activating the door
/// Spawning the player in front of the door
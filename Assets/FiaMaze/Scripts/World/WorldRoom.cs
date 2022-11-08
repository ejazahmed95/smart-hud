using System;
using FiaMaze.Characters;
using RangerRPG.Core;
using RangerRPG.Grids;
using UnityEngine;

namespace FiaMaze.World {
    public class WorldRoom : CellBehavior<WorldCellInfo> {
        public WallAssets wallAssets;
        public float size = 10f;
        public Transform wallParent;
        public EnemyBehaviour enemyRef;

        public override CellBehavior<WorldCellInfo> Init(WorldCellInfo info) {
            base.Init(info);
            SetupWalls();
            if (info.IsEndCell) {
                var playerRef = DI.Get<PCGManager>().GetPlayerRef();
                enemyRef.Init(playerRef);
                enemyRef.gameObject.SetActive(true);
            }
            return this;
        }

        private void Start() {
            //SetupWalls();
        }

        private void SetupWalls() {
            AddWall(HexDirection.Q);
            AddWall(HexDirection.QNeg);
            AddWall(HexDirection.R);
            AddWall(HexDirection.RNeg);
            AddWall(HexDirection.S);
            AddWall(HexDirection.SNeg);
        }
        
        private void AddWall(HexDirection hexDirection) {
            if (_info.CombinedDirections.Has(hexDirection)) return;
            var prefab = wallAssets.BasicWall;
            if (_info.Enabled && _info.AvailableDirections.Has(hexDirection)) {
                prefab = wallAssets.BasicWallWithDoor;
            }
            var wall = Instantiate(prefab, wallParent);
            wall.transform.localPosition = GetPositionForDirection(hexDirection);
            wall.transform.Rotate(0,GetRotationForDirection(hexDirection), 0);
        }

        private Vector3 GetPositionForDirection(HexDirection dir) {
            return dir switch {
                HexDirection.Q => new Vector3(size * (float)(HexUtils.Sqrt3)/2, 0, 0),
                HexDirection.QNeg => new Vector3(-size * (float)(HexUtils.Sqrt3)/2, 0, 0),
                HexDirection.R => new Vector3(size * (float)(HexUtils.Sqrt3)/4, 0, -0.75f*size),
                HexDirection.RNeg => new Vector3(-size * (float)(HexUtils.Sqrt3)/4, 0, 0.75f*size),
                HexDirection.S => new Vector3(size * (float)(HexUtils.Sqrt3)/4, 0, 0.75f*size),
                HexDirection.SNeg => new Vector3(-size * (float)(HexUtils.Sqrt3)/4, 0, -0.75f*size),
                _ => new Vector3()
            };
        }
        
        private float GetRotationForDirection(HexDirection dir) {
            return dir switch {
                HexDirection.Q => 90,
                HexDirection.QNeg => 90,
                HexDirection.R => -30,
                HexDirection.RNeg => -30,
                HexDirection.S => 30,
                HexDirection.SNeg => 30,
                _ => 0,
            };
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                //Log.Info("Trigger Detected!");
                var manager = DI.Get<PCGManager>();
                if (manager != null) {
                    manager.OnPlayerEntered(_info);
                }
            }
        }

        protected override void UpdateCellInfo(WorldCellInfo info) {
            base.UpdateCellInfo(info);
            
        }
    }
}
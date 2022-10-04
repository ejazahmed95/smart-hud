using RangerRPG.Grids;
using UnityEngine;

namespace FiaMaze.World {
    public class Door : MonoBehaviour {

        [SerializeField] private DoorLoc exitLocation;
        [SerializeField] private bool _isActive;
        [SerializeField] private bool _isLocked;
        
        public DoorLoc ExitLocation => exitLocation;
        
        public bool IsActive {
            get => _isActive;
            set {
                _isActive = value;
            }
        }
        
        public bool IsLocked {
            get => _isLocked;
            set {
                _isLocked = value;
            }
        }

        public void SetExitLocation(AxialPosition location, HexDirection dir) {
            exitLocation.location = location;
            exitLocation.direction = dir;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                
            }
        }
    }

    [System.Serializable]
    public struct DoorLoc {
        public AxialPosition location;
        public HexDirection direction;
    }
}
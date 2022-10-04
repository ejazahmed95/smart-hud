using FiaMaze.Types;
using FiaMaze.World;
using RangerRPG.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FiaMaze.Input {
    public class ActionMapHandler: SingletonBehaviour<ActionMapHandler> {
        [SerializeField] private ActionMapType currentActionMapType = ActionMapType.Game;

        private PlayerInput _playerInput;

        public override void Awake() {
            base.Awake();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start() {
            _playerInput.SwitchCurrentActionMap(currentActionMapType.String());
        }

        public void OnHUDOpen() {
            currentActionMapType = ActionMapType.HUD;
            _playerInput.SwitchCurrentActionMap(currentActionMapType.String());
            //Screen.lockCursor = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            HUDManager.Instance.UpdateHUD(true);
        }

        public void OnHUDClose() {
            currentActionMapType = ActionMapType.Game;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _playerInput.SwitchCurrentActionMap(currentActionMapType.String());
            HUDManager.Instance.UpdateHUD(false);
        }
    }

}
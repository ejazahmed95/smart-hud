using System;
using RangerRPG.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FiaMaze.World {
    public class HUDManager: SingletonBehaviour<HUDManager> {

        [SerializeField] private bool hudVisible = false;
        [SerializeField] private GameObject hudObject;

        private void Start() {
            hudObject.SetActive(hudVisible);
        }
        
        public void UpdateHUD(bool active) {
            hudVisible = active;
            hudObject.SetActive(hudVisible);
        }
    }
}
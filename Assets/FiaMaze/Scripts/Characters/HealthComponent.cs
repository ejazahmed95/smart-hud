using System;
using UnityEngine;
using UnityEngine.Events;

namespace FiaMaze.Characters {
    public class HealthComponent : MonoBehaviour {
        public int MaxHealth = 100;
        public int Health = 100;

        private void Start() {
            Health = MaxHealth;
        }
        
        public void Damage(int i) {
            Health -= i;
        }
        public float GetPercent() {
            if (Health < 0) return 0;
            return (float) Health / MaxHealth;
        }
    }
}
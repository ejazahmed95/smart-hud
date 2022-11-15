using FiaMaze.Characters;
using UnityEngine;

namespace FiaMaze.World {
    public class DamagePickup : SimplePickup {

        public int healthBonus = 10;
        
        public override void HandleTrigger(GameObject other) {
            base.HandleTrigger(other);
            var healthComponent = other.GetComponent<HealthComponent>();
            if (healthComponent) {
                healthComponent.Damage(healthBonus);
            }
        }
    }
}
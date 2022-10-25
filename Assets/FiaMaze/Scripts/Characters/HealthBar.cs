using UnityEngine;

namespace FiaMaze.Characters {
    public class HealthBar : MonoBehaviour {
        [SerializeField] private GameObject barObjectRef;
        
        public void SetPercent(float newPercent) {
            barObjectRef.transform.localScale = new Vector3(newPercent, 1);
        }
    }
}
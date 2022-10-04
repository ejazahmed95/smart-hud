using UnityEngine;
using TMPro;

namespace Test {
    public class UpdateTMP : MonoBehaviour {
        public TMP_Text textRef;
        public string stringVal;
        
        private void Start() {
            textRef.text = stringVal;
        }
    }
}
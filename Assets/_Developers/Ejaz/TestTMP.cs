using TMPro;
using UnityEngine;

namespace _Developers.Ejaz {
    public class TestTMP : MonoBehaviour {
        public TMP_Text textRef;
        public string stringVal;
        
        private void Start() {
            textRef.text = stringVal;
        }
    }
}
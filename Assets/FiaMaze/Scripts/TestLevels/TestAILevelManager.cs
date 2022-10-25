using System.Collections;
using RangerRPG.Core;
using TMPro;
using UnityEngine;

namespace FiaMaze.TestLevels {
    public class TestAILevelManager : SingletonBehaviour<TestAILevelManager> {
        public GameObject GameoverText;
        
        public void GameWon() {
            GameoverText.SetActive(true);
            StartCoroutine(CloseGame());
        }
        
        private IEnumerator CloseGame() {
            
            yield return new WaitForSeconds(4f);
            QuitGame();
        }
        
        private void QuitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
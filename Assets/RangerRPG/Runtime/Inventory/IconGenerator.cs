using System;
using UnityEditor;
using UnityEngine;

namespace RangerRPG.Inventory {
    public class IconGenerator : MonoBehaviour {

        public Camera camera;
        public string name = "Staff";
        
        private void Start() {
            TakeScreenshot($"{Application.dataPath}/{name}_icon.png");
        }

        void TakeScreenshot(string fullPath) {

            RenderTexture rt = new RenderTexture(256, 256, 24);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null;

            if (Application.isEditor) {
                DestroyImmediate(rt);
            } else {
                Destroy(rt);
            }

            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(fullPath, bytes);
            
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #endif
        }
    }
}
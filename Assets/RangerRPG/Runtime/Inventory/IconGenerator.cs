using System.Collections.Generic;
using RangerRPG.Core;
using UnityEditor;
using UnityEngine;

namespace RangerRPG.Inventory {
    public class IconGenerator : MonoBehaviour {

        public Camera cameraRef;

        public string iconFolderPath = "/";
        public List<Collider> assets = new();

        public float sizeRatio = 1.5f;
        
        private void Start() {
            CreateAssetIcons();
            //TakeScreenshot($"{Application.dataPath}{iconFolderPath}{assetName}_icon.png");
        }
        
        private void CreateAssetIcons() {
            foreach (var asset in assets) {
                asset.gameObject.SetActive(false);
            }
            
            foreach (var asset in assets) {
                asset.gameObject.SetActive(true);
                AlignAsset(asset);
                TakeScreenshot($"{Application.dataPath}{iconFolderPath}{asset.name}_icon.png");
                asset.gameObject.SetActive(false);
            }
        }
        
        private void AlignAsset(Collider asset) {
            var assetTransform = asset.transform;
            var pivotOffset = assetTransform.GetComponent<Renderer>().bounds.center;
            Debug.Log($"Pivot Offset for {asset.name} = {pivotOffset}");
            assetTransform.localPosition = new Vector3(0, 0, 0) - new Vector3(pivotOffset.x, pivotOffset.y, 0);

            
            
            var bounds = asset.bounds;
            var size = Mathf.Max(bounds.extents.x, bounds.extents.y);
            cameraRef.orthographicSize = (size * sizeRatio);
        }

        private void TakeScreenshot(string fullPath) {

            RenderTexture rt = new RenderTexture(256, 256, 24);
            cameraRef.targetTexture = rt;
            Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
            cameraRef.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            cameraRef.targetTexture = null;
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
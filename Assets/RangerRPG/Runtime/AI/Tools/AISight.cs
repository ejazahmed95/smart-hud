using System;
using UnityEngine;

namespace RangerRPG.AI.Tools {
    public class AISight : MonoBehaviour {

        [SerializeField] private float distance = 10;
        [SerializeField] private float angle = 30;
        [SerializeField] private float height = 1.0f;
        [SerializeField] private Color meshColor = Color.red;
        [SerializeField] private LayerMask occlusionLayers;

        //[field: SerializeField] public float Distance { get; private set; }

        private Mesh mesh;
        
        private void Start() {
            CreateWedgeMesh();
        }

        public bool IsInSight(Transform objectTransform) {
            Vector3 origin = transform.position;
            Vector3 dest = objectTransform.transform.position;
            Vector3 direction = dest - origin;

            // if (direction.y < 0 || direction.y > height) {
            //     return false;
            // }

            direction.y = 0;
            float deltaAngle = Vector3.Angle(direction, transform.forward);
            if (deltaAngle > angle) {
                return false;
            }

            origin.y += height / 2;
            dest.y = origin.y;
            if (Physics.Linecast(origin, dest, occlusionLayers)) {
                return false;
            }
            
            return true;
        }
        
        private Mesh CreateWedgeMesh() {
            Mesh mesh = new Mesh();

            int numTriangles = 8;
            int numVertices = numTriangles * 3;

            Vector3[] vertices = new Vector3[numVertices];
            int[] triangles = new int[numVertices];

            var bottomCenter = Vector3.zero;
            var bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
            var bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
            
            var topCenter = bottomCenter + Vector3.up*height;
            var topLeft = bottomLeft + Vector3.up * height;
            var topRight = bottomRight + Vector3.up * height;

            var vIndex = 0;
            CreateFaceVertices(bottomCenter, bottomLeft, topLeft, topCenter, ref vIndex, ref vertices); // Left
            CreateFaceVertices(bottomCenter, topCenter, topRight, bottomRight, ref vIndex, ref vertices); // Right
            CreateFaceVertices(bottomLeft, bottomRight, topRight, topLeft, ref vIndex, ref vertices); // Outer
            CreateTriangle(topCenter, topLeft, topRight, ref vIndex, ref vertices);
            CreateTriangle(bottomCenter, bottomRight, bottomLeft, ref vIndex, ref vertices);

            for (int i = 0; i < numVertices; i++) {
                triangles[i] = i;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            return mesh;
        }

        private void OnValidate() {
            mesh = CreateWedgeMesh();
        }

        private void OnDrawGizmos() {
            if (mesh == null) return;
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        private void CreateFaceVertices(Vector3 bLeft, Vector3 bRight, Vector3 tRight, Vector3 tLeft, ref int index, ref Vector3[] vertices) {
            CreateTriangle(bLeft, bRight, tRight, ref index, ref vertices);
            CreateTriangle(tRight, tLeft, bLeft, ref index, ref vertices);
        }
        
        private void CreateTriangle(Vector3 v1, Vector3 v2, Vector3 v3, ref int index, ref Vector3[] vertices) {
            vertices[index++] = v1;
            vertices[index++] = v2;
            vertices[index++] = v3;
        }
    }
}
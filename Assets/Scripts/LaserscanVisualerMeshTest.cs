using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LaserscanVisualerMeshTest : LaserScanVisualizer
    {
        public Material material;
        private GameObject LaserScanMesh;
        private Mesh mesh;
        private Vector3[] meshVerticies;
        private Color[] meshVertexColors;
        private int[] meshTriangles;
        private bool IsCreated = false;

        private void Create()
        {
            LaserScanMesh = new GameObject("LaserScanMesh");
            LaserScanMesh.transform.parent = null;

            LaserScanMesh.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = LaserScanMesh.AddComponent<MeshRenderer>();
            meshRenderer.material = material;

            mesh = LaserScanMesh.GetComponent<MeshFilter>().mesh;
            meshVerticies = new Vector3[directions.Length + 1];
            meshTriangles = new int[3 * (directions.Length - 1)];
            meshVertexColors = new Color[meshVerticies.Length];

            IsCreated = true;
        }

        protected override void Visualize()
        {
            if (!IsCreated)
                Create();

            LaserScanMesh.transform.SetPositionAndRotation(base_transform.position, base_transform.rotation);

            meshVerticies[0] = Vector3.zero;
            meshVertexColors[0] = Color.green;
            for (int i = 0; i < meshVerticies.Length - 1; i++)
            {
                meshVerticies[i + 1] = ranges[i] * directions[i] * 5;
                meshVertexColors[i + 1] = GetColor(ranges[i]);
            }
            for (int i = 0; i < meshTriangles.Length / 3; i++)
            {
                meshTriangles[3 * i] = 0;
                meshTriangles[3 * i + 1] = i + 2;
                meshTriangles[3 * i + 2] = i + 1;
            }

            mesh.vertices = meshVerticies;
            mesh.triangles = meshTriangles;
            mesh.colors = meshVertexColors;
        }

        protected override void DestroyObjects()
        {
            Destroy(LaserScanMesh);
            IsCreated = false;
        }
    }
}
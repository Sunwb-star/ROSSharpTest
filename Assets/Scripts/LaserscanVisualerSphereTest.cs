using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LaserscanVisualerSphereTest : LaserScanVisualizer
    {
        [Range(0.01f, 0.1f)]
        public float objectWidth;
        public Material material;

        private GameObject laserScanSpheres;
        private GameObject[] LaserScan;
        private bool IsCreated = false;

        private void Create(int numOfSpheres)
        {
            laserScanSpheres = new GameObject("laserScanSpheres");
            laserScanSpheres.transform.parent = null;

            LaserScan = new GameObject[numOfSpheres];

            for (int i = 0; i < numOfSpheres; i++)
            {
                LaserScan[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                DestroyImmediate(LaserScan[i].GetComponent<Collider>());
                LaserScan[i].name = "LaserScanSpheres";
                LaserScan[i].transform.parent = laserScanSpheres.transform;
                LaserScan[i].GetComponent<Renderer>().material = material;
            }
            IsCreated = true;
        }

        protected override void Visualize()
        {
            if (!IsCreated)
                Create(directions.Length);

            laserScanSpheres.transform.SetPositionAndRotation(base_transform.position, base_transform.rotation);

            for (int i = 0; i < directions.Length; i++)
            {
                LaserScan[i].SetActive(ranges[i] != 0);
                LaserScan[i].GetComponent<Renderer>().material.SetColor("_Color", GetColor(ranges[i]));
                LaserScan[i].transform.localScale = objectWidth * Vector3.one;
                LaserScan[i].transform.localPosition = ranges[i] * directions[i] * 5;
            }
        }

        protected override void DestroyObjects()
        {
            for (int i = 0; i < LaserScan.Length; i++)
                Destroy(LaserScan[i]);

            Destroy(laserScanSpheres);
            IsCreated = false;
        }
    }
}

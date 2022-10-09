using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LaserscanVisualerLinesTest : LaserScanVisualizer
    {
        [Range(0.001f, 0.01f)]
        public float objectWidth;
        public Material material;
        private GameObject laserScanLines;
        private GameObject[] LaserScan;
        private bool IsCreated = false;

        private void Create(int numOfLines)
        {
            laserScanLines = new GameObject("laserScanLines");
            laserScanLines.transform.parent = null;

            LaserScan = new GameObject[numOfLines];
            for (int i = 0; i < numOfLines; i++)
            {
                LaserScan[i] = new GameObject("LaserScanLines");
                LaserScan[i].transform.parent = laserScanLines.transform;
                LaserScan[i].AddComponent<LineRenderer>();
                LaserScan[i].GetComponent<LineRenderer>().material = material;
            }
            IsCreated = true;
        }

        protected override void Visualize()
        {
            if (!IsCreated)
                Create(directions.Length);

            laserScanLines.transform.SetPositionAndRotation(base_transform.position, base_transform.rotation);

            for (int i = 0; i < directions.Length; i++)
            {
                LaserScan[i].transform.localPosition = ranges[i] * directions[i] * 5;
                LineRenderer lr = LaserScan[i].GetComponent<LineRenderer>();
                lr.startColor = GetColor(ranges[i]);
                lr.endColor = GetColor(ranges[i]);
                lr.startWidth = objectWidth;
                lr.endWidth = objectWidth;
                lr.useWorldSpace = false;
                lr.SetPosition(0, new Vector3(0, 0, 0));
                lr.SetPosition(1, -LaserScan[i].transform.localPosition);
            }
        }

        protected override void DestroyObjects()
        {
            for (int i = 0; i < LaserScan.Length; i++)
                Destroy(LaserScan[i]);

            Destroy(laserScanLines);
            IsCreated = false;
        }
    }
}

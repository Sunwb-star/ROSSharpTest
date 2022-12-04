using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class LaserScanSubscriberYunJiSuan : UnitySubscriber<MessageTypes.Sensor.LaserScan>{
        public LaserScanWriter laserScanWriter;
        public Text distance;
        float data;
        private bool isMessageReceived;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.LaserScan laserScan)
        {
            data = laserScan.ranges[320];
            isMessageReceived = true;
            laserScanWriter.Write(laserScan);
            // Debug.Log("yishoudao");
        }

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }
        private void ProcessMessage ()
        {
            if(data == 0)
            {
                distance.text = "与障碍物距离过近";
            }
            distance.text = "正前方障碍物距离(m):" + data;
            distance.fontSize = 50;
        }
    }
}
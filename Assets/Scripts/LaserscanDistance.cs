using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class LaserscanDistance : UnitySubscriber<MessageTypes.Sensor.LaserScan>
    {

        public Text distance;
        float [] ranges;
        private bool isMessageReceived;
        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.LaserScan laserScanmessage)
        {
            Debug.Log("写入前已收到");
            ranges = laserScanmessage.ranges;
            isMessageReceived = true;
            Debug.Log("已收到");
        }

        private void ProcessMessage ()
        {
            distance.text = "正前方障碍物距离(m):" + ranges[0];
            distance.fontSize = 50;
        }
    }
}

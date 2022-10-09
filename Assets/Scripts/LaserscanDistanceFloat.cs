using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class LaserscanDistanceFloat : UnitySubscriber<MessageTypes.Std.Float32>
    {

        public Text distance;
        float data;
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

        protected override void ReceiveMessage(MessageTypes.Std.Float32 message)
        {
            data = message.data;
            isMessageReceived = true;
            Debug.Log("已收到");
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

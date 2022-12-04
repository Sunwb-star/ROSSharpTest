using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class JoyToTwistYunJiSuan : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        private JoyAxisReader[] JoyAxisReaders;
        private JoyButtonReader[] JoyButtonReaders;
        private MessageTypes.Geometry.Twist message;
        public double linerspeed = 0.7;
        public double angularconvert = -3.14;
        protected override void Start()
        {
            base.Start();
            InitializeGameObject();
            InitializeMessage();
        }

        private void Update()
        {
            UpdateMessage();
        }

        private void InitializeGameObject()
        {
            JoyAxisReaders = GetComponents<JoyAxisReader>();
            JoyButtonReaders = GetComponents<JoyButtonReader>();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist();
            message.linear = new MessageTypes.Geometry.Vector3();
            message.angular = new MessageTypes.Geometry.Vector3();
        }

        private void UpdateMessage()
        {
            message.linear.x = linerspeed * JoyAxisReaders[0].Read();
            message.linear.y = 0;
            message.linear.z = 0;
            message.angular.z = angularconvert * JoyAxisReaders[1].Read();
            message.angular.x = 0;
            message.angular.y = 0;
            Publish(message);
        }
    }
}
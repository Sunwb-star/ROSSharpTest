using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class JoyStickPublisher : UnityPublisher<MessageTypes.Sensor.Joy>
    {
        private JoyStickReaderHorizontal JoyStickHorizontal;
        private JoyStickReaderVertical JoyStickVertical;
        private JoyButtonReader[] JoyButtonReaders;

        public string FrameId = "Unity";

        private MessageTypes.Sensor.Joy message;

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
            JoyStickHorizontal = GetComponent<JoyStickReaderHorizontal>();
            JoyStickVertical = GetComponent<JoyStickReaderVertical>();
            JoyButtonReaders = GetComponents<JoyButtonReader>();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Sensor.Joy();
            message.header.frame_id = FrameId;
            message.axes = new float[2];
            message.buttons = new int[JoyButtonReaders.Length];
        }

        private void UpdateMessage()
        {
            message.header.Update();

            // for (int i = 0; i < JoyAxisReaders.Length; i++)
            //     message.axes[i] = JoyAxisReaders[i].Read();
            message.axes[0] = JoyStickVertical.Read();
            message.axes[1] = JoyStickHorizontal.Read();

            for (int i = 0; i < JoyButtonReaders.Length; i++)
                message.buttons[i] = (JoyButtonReaders[i].Read() ? 1 : 0);

            Publish(message);
        }
    }
}

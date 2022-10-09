using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class JoyStickReaderVertical : MonoBehaviour
    {
        public FixedJoystick joystick;

        // private void Update() {
        //     float result = Read();
        //     Debug.Log(result);
        // }
        public float Read()
        {
            return joystick.Vertical;
        }
    }
}


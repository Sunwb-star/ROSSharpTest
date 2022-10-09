using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class JoyStickReaderHorizontal : MonoBehaviour
    {
        public FixedJoystick joystick;

        // private void Update() {
        //     float result = Read();
        //     Debug.Log(result);
        // }
        public float Read()
        {
            return joystick.Horizontal;
        }
    }
}


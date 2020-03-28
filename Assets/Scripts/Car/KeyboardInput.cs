using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public class KeyboardInput : InputManager
    {
        protected override void ReadInput()
        {
            float throttle = Input.GetAxis("Vertical");
            fl_throttle = throttle;
            fr_throttle = throttle;
            rl_throttle = throttle;
            rr_throttle = throttle;

            float steer = Input.GetAxis("Horizontal");
            fl_steer = steer;
            fr_steer = steer;
            rl_steer = 0;
            rr_steer = 0;

            float brake = Input.GetKey(KeyCode.Space) ? 1f : 0f;
            fl_brake = brake;
            fr_brake = brake;
            rl_brake = brake;
            rr_brake = brake;
        }
    }
}
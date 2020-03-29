using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    /*
     * \brief Input controller using keyboard.
     */
    public class KeyboardInput : InputManager
    {
        protected override void ReadInput()
        {
            // UP / DOWN arrows control the throttle
            float throttle = Input.GetAxis("Vertical");
            fl_throttle = throttle;
            fr_throttle = throttle;
            rl_throttle = throttle;
            rr_throttle = throttle;

            // LEFT / RIGHT arrows control the steering
            float steer = Input.GetAxis("Horizontal");
            fl_steer = steer;
            fr_steer = steer;
            rl_steer = 0;
            rr_steer = 0;

            // SPACE button controls the braking (boolean)
            float brake = Input.GetKey(KeyCode.Space) ? 1f : 0f;
            fl_brake = brake;
            fr_brake = brake;
            rl_brake = brake;
            rr_brake = brake;
        }

        public override bool isReversing()
        {
            if (fl_steer < 0)
                return true;
            return false;
        }
    }
}
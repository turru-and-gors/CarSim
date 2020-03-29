using System;

namespace Car
{
    /*
     * \brief Input controller for the Logitech G29 Steering wheel and pedals.
     * Works only on Windows. Logitech Game Software must be installed.
     */
    public class LogitechInput : InputManager
    {
        private bool goesForward;

        protected override void OnStart()
        {
            goesForward = true;
            LogitechGSDK.LogiSteeringInitialize(false);
        }

        protected override void ReadInput()
        {
            if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
            {
                
                LogitechGSDK.DIJOYSTATE2ENGINES controlState;
                controlState = LogitechGSDK.LogiGetStateUnity(0);

                // Steering wheel value     -- range [-32768, 32767]
                //                             convert to range [-1, 1]
                float steer = (float)controlState.lX / (float)short.MaxValue;
                fl_steer = steer;
                fr_steer = steer;
                rl_steer = 0;
                rr_steer = 0;                

                // Brake                    -- range [-32768, 32767]    -> (full, none)
                //                             convert to range [0, 1]  -> (none, full)
                float brake = 0.5f - ((float)controlState.lRz / (float)short.MaxValue)/2f;
                fl_brake = brake;
                fr_brake = brake;
                rl_brake = brake;
                rr_brake = brake;

                // Forward / Backward       -- x button on wheel
                if (controlState.rgbButtons[0] == 128)
                    goesForward = !goesForward;

                // Throttle                 -- range [-32768, 32767]    -> (full, none)
                //                             convert to range [0, 1]  -> (none, full)
                float throttle = 0.5f - ((float)controlState.lY / (float)short.MaxValue) / 2f;
                if (!goesForward) throttle = -throttle;
                fl_throttle = throttle;
                fr_throttle = throttle;
                rl_throttle = throttle;
                rr_throttle = throttle;
            }
        }

        public override bool isReversing()
        {
            return !goesForward;
        }

        void OnApplicationQuit()
        {
            LogitechGSDK.LogiSteeringShutdown();
        }
    }
}
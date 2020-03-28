using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


namespace Car
{
    public abstract class InputManager : MonoBehaviour
    {
        protected float fl_throttle, fr_throttle, rl_throttle, rr_throttle;
        protected float fl_brake, fr_brake, rl_brake, rr_brake;
        protected float fl_steer, fr_steer, rl_steer, rr_steer;
        public Vehicle car;

        protected virtual void OnStart() { }
        protected virtual void OnUpdate() { }
        protected abstract void ReadInput();

        private void Start()
        {
            Assert.IsNotNull(car, "A vehicle must be defined");
            OnStart();
        }

        private void Update()
        {
            ReadInput();
            OnUpdate();
        }

        private void FixedUpdate()
        {
            car.Throttle(fl_throttle, fr_throttle, rl_throttle, rr_throttle);
            car.Brake(fl_brake, fr_brake, rl_brake, rr_brake);
            car.Steer(fl_steer, fr_steer, rl_steer, rr_steer);
        }
    }
}
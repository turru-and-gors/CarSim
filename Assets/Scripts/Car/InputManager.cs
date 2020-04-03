using UnityEngine;
using UnityEngine.Assertions;


namespace Car
{
    /*
     * \brief Abstract class for Input controller.
     * 
     * Implement this class for adding new input controllers.
     * ReadInput MUST be implemented.
     * OnStart will be called at the beginning of the program (optional).
     * OnUpdate will be called on each frame (optional).
     */
    public abstract class InputManager : MonoBehaviour
    {
        protected float fl_throttle, fr_throttle, rl_throttle, rr_throttle;
        protected float fl_brake, fr_brake, rl_brake, rr_brake;
        protected float fl_steer, fr_steer, rl_steer, rr_steer;
        protected bool reverseOn;
        public Vehicle car;     /*!< Reference to the vechicle's game object. */

        /* \brief Override this if you need initialization code. */
        protected virtual void OnStart() { }
        /* \brief Override this if you need to update on every frame. */
        protected virtual void OnUpdate() { }
        /* \brief Implement this for reading data from the new controller. */
        protected abstract void ReadInput();
        public abstract bool isReversing();

        // This function is automatically called when the simulation starts
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
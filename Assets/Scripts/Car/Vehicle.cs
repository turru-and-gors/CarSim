using UnityEngine;

namespace Car
{
    /*
     * \brief Vehicle controller. 
     * 
     * Place this component inside the car's game object. A RigidBody component
     * must be present here too.
     */
    [RequireComponent(typeof(Rigidbody))]
    public class Vehicle : MonoBehaviour
    {
        public Wheel frontLeft;         /*!< Reference to the Front-Left wheel game object. */
        public Wheel frontRight;        /*!< Reference to the Front-Right wheel game object. */
        public Wheel rearLeft;          /*!< Reference to the Rear-Left wheel game object. */
        public Wheel rearRight;         /*!< Reference to the Rear-Right wheel game object. */
        public Transform centerOfMass;  /*!< Transform representing the vehicle's Center of Mass (optional). */

        private Rigidbody rb;           // Rigid body component of the vehicle
        private Vector3 previousVelocity; // To compute the acceleration (Needs testing)

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (centerOfMass)
                rb.centerOfMass = centerOfMass.position;

            frontLeft.CarRigidBody    = rb;
            frontRight.CarRigidBody   = rb;
            rearLeft.CarRigidBody     = rb;
            rearRight.CarRigidBody    = rb;

            // Four wheel steer
            frontLeft.Steerable     = true;
            frontRight.Steerable    = true;
            rearLeft.Steerable      = true;
            rearRight.Steerable     = true;

            previousVelocity = Vector3.zero;
        }

        // TODO: test this value
        private void FixedUpdate()
        {
            previousVelocity = rb.velocity;
        }

        /*
         * \brief Set the torque values for each wheel, in range [-1, 1].
         * \param fl Front-Left torque.
         * \param fr Front-Right torque.
         * \param rl Rear-Left torque.
         * \param rr Rear-Right torque.
         */
        public void Throttle(float fl, float fr, float rl, float rr)
        {
            frontLeft.Torque    = fl;
            frontRight.Torque   = fr;
            rearLeft.Torque     = rl;
            rearRight.Torque    = rr;
        }

        /*
         * \brief Set the steering angle for each wheel in range [-1, 1].
         * \param fl Front-Left angle.
         * \param fr Front-Right angle.
         * \param rl Rear-Left angle.
         * \param rr Rear-Right angle.
         */
        public void Steer(float fl, float fr, float rl, float rr)
        {
            frontLeft.Steer     = fl;
            frontRight.Steer    = fr;
            rearLeft.Steer      = rl;
            rearRight.Steer     = rr;
        }

        /*
         * \brief Set the braking value for each wheel, in range [-1, 1].
         * \param fl Front-Left brake.
         * \param fr Front-Right brake.
         * \param rl Rear-Left brake.
         * \param rr Rear-Right brake.
         */
        public void Brake(float fl, float fr, float rl=0, float rr=0)
        {
            frontLeft.Brake     = fl;
            frontRight.Brake    = fr;
            rearLeft.Brake      = rl;
            rearRight.Brake     = rr;
        }

        /*
         * \brief Get the velocity of the car, in m/s.
         * \return Velocity of the car in 3D.
         */
        public Vector3 GetVelocity()
        {
            return rb.velocity;
        }

        public float GetSpeed()
        {
            return transform.InverseTransformVector(rb.velocity).z;
        }

        /*
         * \brief Get the acceleration of the car, in m/s^2.
         * \return Acceleration of the car in 3D.
         */
        public Vector3 GetAcceleration()
        {
            return (rb.velocity - previousVelocity) / Time.fixedDeltaTime;
        }

        /*
         * \brief Get the euler angles of the car, in degrees.
         * \return [Yaw, Pitch, Roll]
         */
        public Vector3 GetEulerAngles()
        {
            return rb.rotation.eulerAngles;
        }
    }
}
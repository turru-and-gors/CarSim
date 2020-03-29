using UnityEngine;

namespace Car
{
    /*
     * \brief Wheel controller.
     * Place this component on a game object that contains a WheelCollider.
     * The mesh must be a child of this game object.
     */
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour
    {
        private WheelCollider wCollider;
        private Transform mesh;
        private float torque = 0;
        private float steer = 0;
        private float brake = 0;

        // ********************************************************************
        // ********************************************************************
        public float MaxTorque { get; set; } = 4000;    /*!< Maximum torque the wheel's motor can provide, in Nm. */
        public float MaxSteer { get; set; } = 35;       /*!< Maximum angle the wheel can turn, in degrees. */
        public float MaxBrake { get; set; } = 2000;     /*!< Maximum brake torque for the wheel, in Nm. */
        public Rigidbody CarRigidBody { get; set; }     /*!< A reference to the vehicle's rigid body component. */
        public bool Steerable { get; set; } = false;    /*!< Can this wheel turn? */

        /*
         * \brief Get/Set the wheel's current torque value, in range [-1, 1].
         */
        public float Torque
        {
            get { return torque; }
            set
            {
                if (value > 1 || value < -1)
                    torque = value > 1 ? MaxTorque : -MaxTorque;
                else
                    torque = value * MaxTorque;
            }
        }

        /*
         * \brief Get/Set the wheel's steering angle, in range [-1, 1].
         */
        public float Steer
        {
            get { return steer; }
            set
            {
                if (value > 1 || value < -1)
                    steer = value > 1 ? MaxSteer : -MaxSteer;
                else
                    steer = value * MaxSteer;
            }
        }

        /*
         * \brief Get/Set the wheel's brake torque, in range [-1, 1].
         */
        public float Brake
        {
            get { return brake; }
            set
            {
                if (value < 0 || value > MaxBrake)
                    brake = value < 0 ? 0 : MaxBrake;
                else
                    brake = value * MaxBrake;
            }
        }

        // ********************************************************************
        // ********************************************************************
        private void Start()
        {
            wCollider = GetComponent<WheelCollider>();
            mesh = transform.GetChild(0);
        }


        private void FixedUpdate()
        {
            // Update brake
            wCollider.brakeTorque = brake * Time.fixedDeltaTime;

            // Update torque
            wCollider.motorTorque = torque * Time.fixedDeltaTime;

            // Update steer
            if (Steerable)
            {
                wCollider.steerAngle = steer;
                gameObject.transform.localEulerAngles = new Vector3(0f, steer, 0f);
            }

            // Animate the wheel
            if (CarRigidBody)
            {
                int sign = CarRigidBody.transform.InverseTransformDirection(CarRigidBody.velocity).z >= 0 ? 1 : -1;
                mesh.Rotate(CarRigidBody.velocity.magnitude * sign / wCollider.radius, 0f, 0f);
            }
            
        }


    }

}
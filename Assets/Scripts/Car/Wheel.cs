
using UnityEngine;

namespace Car
{

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
        public float MaxTorque { get; set; } = 4000;
        public float MaxSteer { get; set; } = 35;
        public float MaxBrake { get; set; } = 2000;
        public Rigidbody CarRigidBody { get; set; }
        public bool Steerable { get; set; } = false;

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
        /**
         * \brief Create a wheel controller.
         */
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
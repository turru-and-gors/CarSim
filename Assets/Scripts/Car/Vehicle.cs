using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class Vehicle : MonoBehaviour
    {
        public Wheel frontLeft, frontRight;
        public Wheel rearLeft, rearRight;
        public Transform centerOfMass;

        private Rigidbody rb;
        private Vector3 previousVelocity;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            if (centerOfMass)
                rb.centerOfMass = centerOfMass.position;

            frontLeft.CarRigidBody    = rb;
            frontRight.CarRigidBody   = rb;
            rearLeft.CarRigidBody     = rb;
            rearRight.CarRigidBody    = rb;

            // Two wheel steer
            frontLeft.Steerable     = true;
            frontRight.Steerable    = true;
            rearLeft.Steerable      = true;
            rearRight.Steerable     = true;

            previousVelocity = Vector3.zero;
        }

        private void FixedUpdate()
        {
            previousVelocity = rb.velocity;
        }

        public void Throttle(float fl, float fr, float rl, float rr)
        {
            frontLeft.Torque    = fl;
            frontRight.Torque   = fr;
            rearLeft.Torque     = rl;
            rearRight.Torque    = rr;
        }

        public void Steer(float fl, float fr, float rl, float rr)
        {
            frontLeft.Steer     = fl;
            frontRight.Steer    = fr;
            rearLeft.Steer      = rl;
            rearRight.Steer     = rr;
        }

        public void Brake(float fl, float fr, float rl=0, float rr=0)
        {
            frontLeft.Brake     = fl;
            frontRight.Brake    = fr;
            rearLeft.Brake      = rl;
            rearRight.Brake     = rr;
        }

        public Vector3 GetVelocity()
        {
            return rb.velocity;
        }

        public Vector3 GetAcceleration()
        {
            return (rb.velocity - previousVelocity) / Time.fixedDeltaTime;
        }

        public Vector3 GetEulerAngles()
        {
            return rb.rotation.eulerAngles;
        }
    }
}
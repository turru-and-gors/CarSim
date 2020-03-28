using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Car;

public class SimpleScene : MonoBehaviour
{
    public Vehicle car;
    public Speedometer speedometer;
    
    void Update()
    {
        Vector3 velocity = car.GetVelocity();
        speedometer.SetVelocity(velocity.z);
    }
}

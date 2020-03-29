using UnityEngine;
using Car;

/*
 * \brief Manager for SimpleScene.
 */
public class SimpleScene : MonoBehaviour
{
    public Vehicle car;
    public Speedometer speedometer;
    public InputManager inputManager;
    
    void Update()
    {
        float speed = car.GetSpeed();
        speedometer.SetVelocity( speed );
        speedometer.Reversing = inputManager.isReversing();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public float maxVelocity = 180.0f;
    public float minAngle = 225f;
    public float maxAngle = -45f;
    public Transform needle;
    public Text velocityText;

    private float angleRange;

    private void Start()
    {
        angleRange = maxAngle - minAngle;
        needle.localEulerAngles = new Vector3(0, 0, minAngle);
    }

    public void SetVelocity(float velocity)
    {
        float normalizedVelocity = velocity / maxVelocity;
        float angle = minAngle + normalizedVelocity * angleRange;

        needle.localEulerAngles = new Vector3(0, 0, angle);

        int velInteger = (int)velocity;
        velocityText.text = velInteger.ToString();
    }
}

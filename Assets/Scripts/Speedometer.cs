using UnityEngine;
using UnityEngine.UI;

/*
 * \brief Display the vehicle's velocity in km/h.
 */
public class Speedometer : MonoBehaviour
{
    public float maxVelocity = 180.0f;  /*!< Maximum velocity the vehicle can reach, in km/h */
    public float minAngle = 225f;       /*!< For the needle, angle of zero velocity in degrees. */
    public float maxAngle = -45f;       /*!< For the needle, angle of maximum velocity in degrees. */
    public Transform needle;            /*!< Transform of the needle's game object. */
    public Text velocityText;           /*!< Reference to the Text in the speedometer. */
    public GameObject reverseIndicator; /*!< Reference to the reverse indicator. */

    private float angleRange;

    private void Start()
    {
        angleRange = maxAngle - minAngle;
        needle.localEulerAngles = new Vector3(0, 0, minAngle);
    }

    /*
     * \brief Set the value of the vehicle's velocity in km/h.
     * 
     * This function will upgade both the needle and the text of the speedometer.
     */
    public void SetVelocity(float velocity)
    {
        if (velocity < 0) return;

        float normalizedVelocity = velocity * 3.6f / maxVelocity;
        float angle = minAngle + normalizedVelocity * angleRange;

        needle.localEulerAngles = new Vector3(0, 0, angle);

        int velInteger = (int)(velocity * 3.6f);
        velocityText.text = velInteger.ToString();
    }

    /*
     * \brief Show/Hide reverse indicator.
     */
    public bool Reversing {
        get
        {
            return reverseIndicator.activeSelf;
        }

        set
        {
            reverseIndicator.SetActive(value);
        }
    }
}

using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public float ForceMagnitude = 1f;
    [Range(0,1)]
    public float minForceMagnitudePercentage = 0.1f;
    public float maxHoldDuration = 2f;

    private Vector3 up;
    void Awake()
    {
        up = gameObject.transform.up;
        currentForceMagnitude = ForceMagnitude;
    }

    public float currentForceMagnitude;
    public float currentHoldDuration;
    private bool isActive = false;
    void Update()
    {
        if (isActive)
        {
            currentHoldDuration += Time.deltaTime;
        }
        else
        {
            currentHoldDuration -= Time.deltaTime;
        }

        currentHoldDuration = Mathf.Clamp(currentHoldDuration, 0, maxHoldDuration);

        currentForceMagnitude = CalculateForce(currentHoldDuration);
    }

    void OnTriggerStay(Collider other)
    {
        if (isActive)
        {
            if (other.attachedRigidbody)
            {
                other.attachedRigidbody.AddForce(up * currentForceMagnitude);
            }
        }
    }

    private float CalculateForce(float holdDuration)
    {
        float inverseForceToHoldDurtion = (holdDuration - maxHoldDuration) / (-maxHoldDuration);
        float force = inverseForceToHoldDurtion <= 0 ? minForceMagnitudePercentage : inverseForceToHoldDurtion;
        return force * ForceMagnitude;
    }

    public void Toggle()
    {
        isActive = !isActive;
    }
}
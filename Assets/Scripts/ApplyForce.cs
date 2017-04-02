using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public float maxForceMagnitude = 1f;
    public float minForceMagnitude = 0.1f;
    public float maxHoldDuration = 2f;

    private Vector3 up;
    void Awake()
    {
        up = gameObject.transform.up;
        currentForceMagnitude = maxForceMagnitude;
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
        float force = inverseForceToHoldDurtion <= 0 ? minForceMagnitude : inverseForceToHoldDurtion;
        return force * maxForceMagnitude;
    }

    public void Toggle()
    {
        isActive = !isActive;
    }
}
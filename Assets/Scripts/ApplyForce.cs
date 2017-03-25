using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public float forceMagnitude = 1f;

    private bool isActive = false;
    private Vector3 up;

    void Awake()
    {
        up = gameObject.transform.up;
    }

    void OnTriggerStay(Collider other)
    {
        if (isActive)
        {
            if (other.attachedRigidbody)
            {
                other.attachedRigidbody.AddForce(up * forceMagnitude);
            }
        }
    }

    public void Toggle()
    {
        isActive = !isActive;
    }
}
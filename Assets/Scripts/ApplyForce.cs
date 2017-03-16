using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    public float forceMagnitude = 1f;

    private bool isActive = false;

    void OnTriggerStay(Collider other)
    {
        if (isActive)
        {
            if (other.attachedRigidbody)
            {
                Debug.Log("Force applied.");
                other.attachedRigidbody.AddForce(gameObject.transform.up * forceMagnitude);
            }
        }
    }

    public void Toggle()
    {
        isActive = !isActive;
    }
}
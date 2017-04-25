using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public DataHolder dataHolder;
    private GravityInput gravityInput;

    private float timer;
    private float layerId;

    private void Awake()
    {
        layerId = LayerMask.NameToLayer("Disappear");
        gravityInput = FindObjectOfType<GravityInput>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == layerId)
        {
            timer += Time.deltaTime;
            if (timer >= dataHolder.disappearTimer)
            {
                Destroy();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerId)
        {
            if (dataHolder.OnEntering != null) dataHolder.OnEntering();
            ResetTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerId)
        {
            if (dataHolder.OnExiting != null) dataHolder.OnExiting();
        }
    }

    private void ResetTimer()
    {
        timer = 0;
    }

    public virtual void OnObjectReuse()
    {
        gravityInput.gravityBodies.Add(GetComponent<Rigidbody>());
        ResetTimer();
    }

    protected void Destroy()
    {
        if (dataHolder.OnTimerPassed != null)
        {
            dataHolder.OnTimerPassed();
        }
        if (dataHolder.isDisappearing)
        {
            gravityInput.gravityBodies.Remove(GetComponent<Rigidbody>());
            gameObject.SetActive(false);
        }
    }
}
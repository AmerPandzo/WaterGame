using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public LoopBehaviour loopBehaviour;

    private float timer;
    private float layerId;

    private void Awake()
    {
        layerId = LayerMask.NameToLayer("Disappear");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == layerId)
        {
            timer += Time.deltaTime;
            if (timer >= loopBehaviour.disappearTimer)
            {
                Destroy();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerId)
        {
            if (loopBehaviour.OnEntering != null) loopBehaviour.OnEntering();
            ResetTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerId)
        {
            if (loopBehaviour.OnExiting != null) loopBehaviour.OnExiting();
        }
    }

    private void ResetTimer()
    {
        timer = 0;
    }

    public virtual void OnObjectReuse()
    {
        ResetTimer();
    }

    protected void Destroy()
    {
        if (loopBehaviour.OnTimerPassed != null)
        {
            loopBehaviour.OnTimerPassed();
        }
        if (loopBehaviour.isDisappearing)
        {
            gameObject.SetActive(false);
        }
    }
}
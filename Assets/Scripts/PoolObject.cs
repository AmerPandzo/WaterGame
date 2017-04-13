using System;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private bool isDisappearing;
    private float disappearTimer;
    private float entertime;
    public event Action OnTimerPassed;
    public event Action OnEntering;
    public event Action OnExiting;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer != 5)
        if (OnEntering != null) OnEntering();
        entertime = Time.time;
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.layer != 5)
        if (OnExiting != null) OnExiting();
        if ((Time.time - entertime) > disappearTimer)
        {
            Destroy();
        }
    }

    public virtual void OnObjectReuse()
    {
        entertime = 0;
    }

    public void SetPoolObjectBehavoiur(float disappearTimer, bool isDisappearing)
    {
        this.disappearTimer = disappearTimer;
        this.isDisappearing = isDisappearing;
    }

    protected void Destroy()
    {
        if (OnTimerPassed != null) OnTimerPassed();
        if (isDisappearing) gameObject.SetActive(false);
    }
}
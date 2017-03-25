using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPointerDownAction;
    public UnityAction OnPointerUpAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction();
        //Debug.Log(gameObject.name + " Was Pressed.");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpAction();
        //Debug.Log(gameObject.name + " Was Released.");
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonPressRelease : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent OnPress;
    public UnityEvent OnRelease;
    
    private bool pointerDown;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        if (OnRelease != null)
            OnRelease.Invoke();
    }

    public void Update()
    {
        if (pointerDown)
        { 
            if (OnPress != null)
                OnPress.Invoke();
        }
    }
}

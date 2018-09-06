using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler {

    public float zoom;
    private bool pressing;

	public void OnPointerDown(PointerEventData data)
    {
        pressing = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        pressing = false;
    }
    public void OnPointerExit(PointerEventData data)
    {
        pressing = false;
    }

    private void LateUpdate()
    {
        if (pressing)
            InputHandler.Instance.Zoom = zoom;
    }
}

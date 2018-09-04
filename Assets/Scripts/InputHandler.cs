using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : Singleton
{
    public static InputHandler Instance { get; private set; }

    public bool Touched { get; private set; }
    public bool PreviousTouched { get; private set; }
    public bool Tapped { get { return Touched && !PreviousTouched; } }
    public bool TouchReleased { get { return !Touched && PreviousTouched; } }
    public float Zoom { get; private set; }

    private bool IsPointerOverUIObject(Vector2 pos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public Vector3 TouchPosition { get; private set; }
    public Vector3 PreviousTouchPosition { get; private set; }

    // Swipe
    private float swipeResistance = 50;
    private Vector2 swipeModifier = new Vector2(1.0f, 1.5f);
    public bool Swiped { get; private set; }
    public Vector2 SwipeDirection { get; private set; }

    public override void AssignInstance()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateZoom();
    }

    private void UpdateZoom()
    {
        if(Application.isMobilePlatform)
        {
            // TODO:
        } else
        {
            Zoom = -Input.GetAxis("Mouse ScrollWheel");
        }
    }

    public bool GetTouchPosition(int fingerId, out Vector3 touchPosition)
    {
        touchPosition = Static.VectorZero;

        if (Application.isMobilePlatform)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);

                if (touch.fingerId == fingerId)
                {
                    touchPosition = touch.position;
                    return true;
                }
            }
        }
        else
        {
            if (fingerId >= 0 && Input.GetMouseButton(0))
            {
                touchPosition = Input.mousePosition;
                return true;
            }
        }
        return false;
    }

    public int TouchInRadius(Vector2 position, float radius)
    {
        var sqrRadius = radius * radius;

        if (Application.isMobilePlatform)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began && Vector2.SqrMagnitude(position - touch.position) < sqrRadius)
                {
                    return touch.fingerId;
                }
            }
        }
        else
        {
            Vector2 touchPos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (Vector2.SqrMagnitude(position - touchPos) < sqrRadius)
                    return 0;
            }
        }
        return -1;
    }

    public int GetTouchId(int touchId)
    {
        if (Application.isMobilePlatform)
        {
            if (touchId < 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    var touch = Input.GetTouch(i);

                    if (touch.phase == TouchPhase.Began && !IsPointerOverUIObject(touch.position))
                        return touch.fingerId;
                }
                return -1;
            }
            return (Input.touchCount >= touchId && Input.GetTouch(touchId).phase != TouchPhase.Ended) ? touchId : -1;
        }
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject(Input.mousePosition))
            return 0;
        return -1;
    }
    
    private void CheckSwipe()
    {
        if (!Touched && PreviousTouched)
        {
            SwipeDirection = TouchPosition - PreviousTouchPosition;
            Swiped = (Mathf.Abs(SwipeDirection.x * swipeModifier.x) + Mathf.Abs(SwipeDirection.y * swipeModifier.y) >= swipeResistance);
            return;
        }
        Swiped = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    public Camera Camera { get; private set; }
    public float speed = 1, zoomSpeed = 1;
    public int minZoom = 5, maxZoom = 8;
    private bool overEdge;
    private Vector3 overEdgeDir;

	// Use this for initialization
	void Start () {
        Camera = GetComponent<Camera>();
	}

    public void HandleUpdate()
    {
        var time = Time.deltaTime;
        HandleZoom(time);
        HandleMove(time);
    }

    public void HandleFixedUpdate()
    {
        var time = Time.fixedDeltaTime;
    }

    private void HandleZoom(float time)
    {
        var size = Camera.orthographicSize;
        var targetSize = Mathf.Clamp(size 
            + InputHandler.Instance.Zoom * time * zoomSpeed, minZoom, maxZoom);
        Camera.orthographicSize = Mathf.Lerp(size, targetSize, time * zoomSpeed);
    }

    private void HandleMove(float time)
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var mouseViewPos = Camera.ScreenToViewportPoint(Input.mousePosition);
        var mouseDelta = GameManager.Instance.MouseDelta;
        /*
        if (mouseViewPos.x > 0.99f || mouseViewPos.x < 0.01f
            || mouseViewPos.y > 0.99f || mouseViewPos.y < 0.01f)
        {
            if (!overEdge)
            {
                overEdgeDir = mouseDelta.normalized;
                overEdge = true;
            } 
            else
            {
                if(mouseDelta.x != 0 && mouseDelta.y != 0)
                    overEdgeDir = mouseDelta.normalized;
            }
            if(overEdgeDir.x > 0)
                deltaX = Mathf.Max(deltaX, overEdgeDir.x);
            else
                deltaX = Mathf.Min(deltaX, overEdgeDir.x);
            if(overEdgeDir.y > 0)
                deltaY = Mathf.Max(deltaY, overEdgeDir.y);
            else
                deltaY = Mathf.Min(deltaY, overEdgeDir.y);
        }
        else
        {
            overEdge = false;
        }
        */
        transform.position += new Vector3(deltaX, deltaY) * speed * time;
        CheckBounds();
    }

    public void CheckBounds()
    {
        if (CameraBounds.Instance)
            transform.position = CameraBounds.Instance.GetPosInBounds
                (transform.position, Camera.ViewportToWorldPoint(Vector3.zero),
                Camera.ViewportToWorldPoint(Vector3.one));
    }

    public Vector3 PointerWorldPos
    {
        get
        {
            var pos = Camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }
}

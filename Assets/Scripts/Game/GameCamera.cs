using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    public Camera Camera { get; private set; }
    public float speed = 1, zoomSpeed = 1;
    public int minZoom = 5, maxZoom = 8;

	// Use this for initialization
	void Start () {
        Camera = GetComponent<Camera>();
	}

    public void HandleUpdate()
    {
        var time = Time.deltaTime;
        HandleMove(time);
        HandleZoom(time);
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
        transform.position +=
            new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * time;

        if (CameraBounds.Instance)
            transform.position = CameraBounds.Instance.GetPosInBounds
                (transform.position, Camera.ViewportToWorldPoint(Vector3.zero),
                Camera.ViewportToWorldPoint(Vector3.one));
    }
}

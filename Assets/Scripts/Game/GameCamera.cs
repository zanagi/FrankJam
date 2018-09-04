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

    public void HandleUpdate(float time)
    {
        HandleZoom(time);
        HandleMove(time);
    }

    private void HandleZoom(float time)
    {
        var size = Camera.orthographicSize;

        Camera.orthographicSize = Mathf.Clamp(size 
            + InputHandler.Instance.Zoom * Time.deltaTime * zoomSpeed, minZoom, maxZoom);
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

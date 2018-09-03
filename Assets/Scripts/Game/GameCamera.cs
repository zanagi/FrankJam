using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

    public Camera Camera { get; private set; }
    public float speed;

	// Use this for initialization
	void Start () {
        Camera = GetComponent<Camera>();
	}

    public void HandleUpdate(float time)
    {
        transform.position += 
            new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * time;

        if (CameraBounds.Instance)
            transform.position = CameraBounds.Instance.GetPosInBounds(transform.position, Vector3.zero);
    }
}

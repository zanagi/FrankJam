using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float time, shaking = 0.1f;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    void Update ()
    {
        if (!GameManager.Instance.IsIdle)
            return;
        time -= Time.deltaTime;
        transform.localPosition = startPos + 
            new Vector3(Random.Range(-shaking, shaking), Random.Range(-shaking, shaking));
        if (time <= 0)
            Explode();
	}
    

    private void Explode()
    {
        Destroy(gameObject);
    }
}

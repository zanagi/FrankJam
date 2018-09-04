using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public float visibility = 25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var frank = other.GetComponent<Frank>();

        if(frank)
        {
            Debug.Log("Frank in shop area");
        }
    }
}

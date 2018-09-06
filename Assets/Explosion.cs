using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var frank = collision.GetComponent<Frank>();

        if(frank)
        {
            GameManager.Instance.notificationManager.ShowNotification(
                "Killed Frank by a bomb explosion!");
            frank.Die();
        }
    }
}

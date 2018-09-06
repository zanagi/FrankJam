using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!GameManager.Instance.IsIdle)
            return;

        var frank = other.GetComponent<Frank>();
        if (frank)
        {
            GameManager.Instance.ShowGameOver(frank);
        }
    }
}

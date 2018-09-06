using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    public Text text;
    public NotificationId id;

    public void SetValues(string str, NotificationId id = NotificationId.None)
    {
        text.text = str.ToUpper();
        this.id = id;
    }
}

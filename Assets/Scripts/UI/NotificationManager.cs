using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NotificationId
{
    None, Bomb, Poison, Poison2, Money, Event, Hire
}

public class NotificationManager : MonoBehaviour {

    public Notification notificationPrefab;
    public Transform notificationOrigin, notificationTarget;
    public float animTime, minWaitTime, maxWaitTime;
    private List<Notification> notifications = new List<Notification>();
	private static string notificationSFX = "Notification";

	public void ShowNotification(string text, NotificationId id = NotificationId.None)
    {
        var count = notifications.Count;
        for(int i = 0; i < count; i++)
        {
            if (notifications[i].id == id && id != NotificationId.None)
                return;
        }
        var notification = notificationPrefab.Spawn(transform);
        notification.transform.localScale = Vector3.one;
        notification.transform.localPosition = notificationOrigin.localPosition;
        notification.SetValues(text, id);
        notifications.Add(notification);

        if(notifications.Count == 1)
        {
            StartCoroutine(AnimateNotification());
        }
    }

    private IEnumerator AnimateNotification()
    {
        if (notifications.Count == 0)
            yield break;

        var notification = notifications[0];

		// Show
        yield return AnimateTransform(notification.transform,
            notificationOrigin.transform.position, notificationTarget.transform.position);

		SFXManager.Instance.PlaySFX(notificationSFX);
		// Wait
		var t = 0.0f;
        while(t < maxWaitTime)
        {
            if (GameManager.Instance.IsIdle)
            {
                t += Time.deltaTime;
                if (notifications.Count > 1 && t >= minWaitTime)
                    break;
            }
            yield return null;
        }

        // Hide
        yield return AnimateTransform(notification.transform, 
            notificationTarget.transform.position,
            notificationOrigin.transform.position);
        
        // Remove notification
        notifications.RemoveAt(0);
        notification.Recycle();

        if(notifications.Count > 0)
            yield return AnimateNotification();
    }

    private IEnumerator AnimateTransform(Transform tr, Vector3 start, Vector3 end)
    {
        var t = 0.0f;
        while (t < animTime)
        {
            if (GameManager.Instance.IsIdle)
            {
                t += Time.deltaTime;
                tr.position = Vector3.Lerp(start, end, t / animTime);
            }
            yield return null;
        }
        tr.position = end;
    }
}

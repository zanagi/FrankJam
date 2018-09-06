using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAlarm : MonoBehaviour {

	private int frankCount;
	private string alarmSfx = "Alarm";

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var frank = collision.GetComponent<Frank>();

		if(frank)
		{
			if (frankCount == 0)
			{
				SFXManager.Instance.PlaySFX(alarmSfx);
			}
			frankCount++;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		var frank = collision.GetComponent<Frank>();

		if (frank)
		{
			frankCount--;
		}
	}
}

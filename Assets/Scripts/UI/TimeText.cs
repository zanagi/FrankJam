using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeText : MonoBehaviour {

    public string front;
	private Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
        SetText();
	}
	
	void Update ()
	{
        SetText();
    }

    private void SetText()
    {
        var totalSeconds = (int)GameManager.Instance.time;
        var totalHours = totalSeconds / 60 / 60;
        totalSeconds -= totalHours * 60 * 60;
        var totalMinutes = totalSeconds / 60;
        totalSeconds -= totalMinutes * 60;
        text.text = front + string.Format("{0}:{1}:{2}", totalHours.ToString("00"), totalMinutes.ToString("00"), totalSeconds.ToString("00"));
    }
}

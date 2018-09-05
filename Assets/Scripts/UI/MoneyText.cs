using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {

    public int deltaSpeed = 1;
    private Text text;
    private int targetNumber, currentNumber;
    
    private void Update()
    {
        int delta = 0;

        if (currentNumber < targetNumber)
            delta = Mathf.Min(deltaSpeed, targetNumber - currentNumber);
        else if (currentNumber > targetNumber)
            delta = -Mathf.Min(deltaSpeed, currentNumber - targetNumber);
        currentNumber += delta;
		SetText();
	}

    public void SetNumber(int number, bool skip = false)
    {
        if(!text)
            text = GetComponent<Text>();
        targetNumber = number;

        if(skip)
        {
            currentNumber = number;
			SetText();
        }
    }

	private void SetText()
	{
		text.text = string.Format("{0} MK", currentNumber);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrankText : MonoBehaviour {

	private Text text;

	// Use this for initialization
	void Start()
	{
		text = GetComponent<Text>();
	}

	void Update()
	{
		text.text = string.Format("{0}/{1}", GameManager.Instance.frankCount, GameManager.Instance.startFrankCount);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour {

	public static readonly string sfxName = "Click";

	private void Start()
	{
		var button = GetComponent<Button>();
		if (button)
			button.onClick.AddListener(() => PlaySound());
	}

	public void PlaySound()
	{
		SFXManager.Instance.PlaySFX(sfxName);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMActivator : MonoBehaviour {

    [SerializeField]
    private string bgmName;
    [SerializeField]
    private bool instant = false;

	// Use this for initialization
	void Start ()
    {
        BGMManager.Instance.SetBGM(bgmName, instant);
		Destroy(gameObject);
	}
}

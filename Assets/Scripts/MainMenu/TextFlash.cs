using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlash : MonoBehaviour {

    private MaskableGraphic graphics;

    [SerializeField]
    private float speed;
    private float opacity;
    private bool opacityRising;

	void Start ()
    {
        graphics = GetComponent<MaskableGraphic>();
	}
	
	private void Update ()
    {
		if(opacityRising)
        {
            opacity += speed * Time.deltaTime;
            if(opacity >= 1.0f)
            {
                opacity = 1.0f;
                opacityRising = false;
            }
        } else
        {
            opacity -= speed * Time.deltaTime;
            if (opacity <= 0.0f)
            {
                opacity = 0.0f;
                opacityRising = true;
            }
        }
        graphics.color = Static.SetAlpha(graphics.color, opacity);
	}
}

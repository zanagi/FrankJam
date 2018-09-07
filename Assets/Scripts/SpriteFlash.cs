using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlash : MonoBehaviour {

    public Color normalColor = Color.white, flashColor = Color.yellow;
    public Vector3 normalScale = Vector3.one, flashScale = 1.1f * Vector3.one;
    private SpriteRenderer spriteRenderer;
    private bool flashUp;
    private float currentTime;

    private const float flashTime = 0.5f;
    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentTime = Random.Range(0, flashTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameManager.Instance.IsIdle)
            return;

        currentTime += Time.deltaTime;
        var t = currentTime / flashTime;
        if (flashUp)
        {
            spriteRenderer.color = Color.Lerp(normalColor, flashColor, t);
            transform.localScale = Vector3.Lerp(normalScale, flashScale, t);

            if (currentTime > flashTime)
            {
                currentTime = 0;
                flashUp = false;
            }
        } else
        {
            spriteRenderer.color = Color.Lerp(flashColor, normalColor, t);
            transform.localScale = Vector3.Lerp(flashScale, normalScale, t);

            if (currentTime > flashTime)
            {
                currentTime = 0;
                flashUp = true;
            }
        }
    }
}

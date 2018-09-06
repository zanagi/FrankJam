using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float time, shaking = 0.1f, explosionTime = 1.5f, explosionScale = 2.0f;
    public GameObject explosion;
    private Vector3 startPos;
    private SpriteRenderer spriteRenderer;
	private float tickTime;
	private static string tickSfx = "BombTick";

    private void Start()
    {
        startPos = transform.localPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        explosion.transform.localScale = Vector3.zero;
        explosion.SetActive(false);
    }

    void Update ()
    {
        if (!GameManager.Instance.IsIdle || time <= 0)
            return;
        time -= Time.deltaTime;
		tickTime -= Time.deltaTime;
        transform.localPosition = startPos + 
            new Vector3(Random.Range(-shaking, shaking), Random.Range(-shaking, shaking));
		if (time <= 0)
		{
			Explode();
			return;
		}
		if(tickTime <= 0)
		{
			tickTime += 1.0f;
			SFXManager.Instance.PlaySFX(tickSfx);
		}
	}
    

    private void Explode()
    {
        spriteRenderer.enabled = false;
        StartCoroutine(AnimateExplosion());
    }

    private IEnumerator AnimateExplosion()
    {
        var t = 0.0f;
        explosion.SetActive(true);
        while(t < explosionTime)
        {
            t += Time.deltaTime;
            explosion.transform.localScale =
                Vector3.Lerp(Vector3.zero, Vector3.one * explosionScale, t / explosionTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}

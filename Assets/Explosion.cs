using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	private static string sfxName = "Explosion", deathSfxName = "DeathBomb";

	private void Start()
	{
		SFXManager.Instance.PlaySFX(sfxName);
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        var frank = collision.GetComponent<Frank>();

        if(frank)
        {
            GameManager.Instance.notificationManager.ShowNotification(
                "Killed Frank by a bomb explosion!");
			SFXManager.Instance.PlaySFX(deathSfxName);
			frank.Die();
        }

        var assassin = collision.GetComponent<Assassin>();
        if(assassin)
        {
            GameManager.Instance.notificationManager.ShowNotification(
                "Killed assassin by a bomb explosion!");
            assassin.Die();
        }
    }
}

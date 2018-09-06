using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

	public static SFXManager Instance { get; private set; }

	[SerializeField]
	private AudioSource[] sfxPrefabs;

	private void Awake()
	{
		if (Instance)
		{
			return;
		}
		Instance = this;
	}

	public void PlaySFX(AudioSource bgmPrefab, Vector3 position)
	{
		StartCoroutine(PlaySFXEnum(bgmPrefab, position));
	}
	
	public void PlaySFX(string name)
	{
		PlaySFX(name, Vector3.zero);
	}

	public void PlaySFX(string name, Vector3 position)
	{
		for (int i = 0; i < sfxPrefabs.Length; i++)
		{
			if (sfxPrefabs[i].name == name)
			{
				PlaySFX(sfxPrefabs[i], position);
				break;
			}
		}
	}


	private IEnumerator PlaySFXEnum(AudioSource sfxPrefab, Vector3 position)
	{
		var sfxInstance = sfxPrefab.Spawn();
		sfxInstance.transform.position = position;
		sfxInstance.Play();

		while (sfxInstance && sfxInstance.isPlaying)
			yield return null;

        if(sfxInstance)
		    sfxInstance.Recycle();
	}
}

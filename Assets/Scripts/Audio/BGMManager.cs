using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    public static BGMManager Instance { get; private set; }
    [SerializeField]
    private float bgmChangeTime = 3.0f;

    [SerializeField]
    private AudioSource[] bgmPrefabs;

    private AudioSource currentSource;

    private void Awake()
    {
        if (Instance)
        {
            return;
        }
        Instance = this;
    }

    public void SetBGM(AudioSource bgmPrefab, bool instant = false)
    {
        StartCoroutine(SetBGMEnum(bgmPrefab, instant));
    }

    public void SetBGM(string name, bool instant = false)
    {
        for (int i = 0; i < bgmPrefabs.Length; i++)
        {
            if (bgmPrefabs[i].name == name)
            {
                SetBGM(bgmPrefabs[i], instant);
                break;
            }
        }
    }
    
    private IEnumerator SetBGMEnum(AudioSource bgmPrefab, bool instant = false)
    {
		if (currentSource && currentSource.name == bgmPrefab.name)
			yield break;

        if(!instant)
            yield return StopCurrentBGM();

        if (currentSource)
            currentSource.Recycle();

        currentSource = bgmPrefab.Spawn(transform);
		currentSource.name = bgmPrefab.name;
        currentSource.Play();
        // Debug.Log("Start BGM: " + currentSource.name);

        if (!instant)
            yield return FadeInCurrentBGM();
    }

    private IEnumerator StopCurrentBGM()
    {
        if(currentSource)
        {
            while(currentSource.volume > 0.0f)
            {
                currentSource.volume -= Time.fixedDeltaTime / bgmChangeTime;
                yield return new WaitForFixedUpdate();
            }
            currentSource.volume = 0.0f;
        }
    }

    private IEnumerator FadeInCurrentBGM()
    {
        var targetVolume = currentSource.volume;
        currentSource.volume = 0.0f;

        while (currentSource.volume < targetVolume)
        {
            currentSource.volume = Mathf.Min(targetVolume, currentSource.volume + Time.fixedDeltaTime / bgmChangeTime);
            yield return new WaitForFixedUpdate();
        }
        currentSource.volume = targetVolume;
    }
}

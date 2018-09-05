using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Idle, Pause, Cutscene
}

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public GameCamera GameCamera { get; private set; }
    public GameState GameState { get; private set; }
    public MoneyText MoneyText { get; private set; }
    public bool IsIdle { get { return GameState == GameState.Idle; } }

    public int money = 2000;
	public Transform selectableWindowTransform;
	[HideInInspector]
	public float time;
	[HideInInspector]
	public int frankCount, startFrankCount;

	// Intro
	public Image introScreen;
	public float introWaitTime, introFadeTime;

	void Awake ()
    {
        if (Instance)
            return;
        Instance = this;
        GameCamera = GetComponentInChildren<GameCamera>();
        MoneyText = GetComponentInChildren<MoneyText>();
        MoneyText.SetNumber(money, true);
		frankCount = startFrankCount = FindObjectsOfType<Frank>().Length;

		// Intro
		GameState = GameState.Cutscene;
		introScreen.gameObject.SetActive(true);
		StartCoroutine(PlayIntro());
	}

	private IEnumerator PlayIntro()
	{
		var t = 0.0f;
		var frameTime = 0.02f;
		while (t <= introWaitTime)
		{
			if (Input.GetMouseButtonDown(0))
				break;
			t += frameTime;
			yield return new WaitForSecondsRealtime(frameTime);
		}

		t = 0.0f;
		while(t <= introFadeTime)
		{
			t += frameTime;
			introScreen.SetAlpha(1.0f - t / introFadeTime);
			yield return new WaitForSecondsRealtime(frameTime);
		}
		introScreen.gameObject.SetActive(false);
		GameState = GameState.Idle;
	}

    private void Update()
	{
		if (!IsIdle)
			return;

		time += Time.deltaTime;
		GameCamera.HandleUpdate();
    }

    private void FixedUpdate()
    {
        if (!IsIdle)
            return;

        GameCamera.HandleFixedUpdate();
    }

    public bool SpendMoney(int moneySpent)
    {
        if(money >= moneySpent)
        {
            money -= moneySpent;
            MoneyText.SetNumber(money);
            return true;
        }
        return false;
    }
}

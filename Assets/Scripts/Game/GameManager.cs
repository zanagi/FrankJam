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
    public NotificationManager notificationManager { get; private set; }
    public bool IsIdle { get { return GameState == GameState.Idle; } }
	public bool IsPaused { get { return GameState == GameState.Pause; } }
    public float FrankRatio { get { return 1.0f * frankCount / startFrankCount; } }

	public int money = 2000;
	public Transform selectableWindowTransform;
	[HideInInspector]
	public float time;
	[HideInInspector]
	public int frankCount, startFrankCount;

	// Intro
	public CanvasGroup introScreen;
	public Button introButton;
	public float introWaitTime, introFadeTime;
	private bool introStarted;

	// Pause & other screens
	public GameObject pauseScreen, gameOverScreen;

    // Spawning
    public Node[] spawnNodes;
    public Frank frankPrefab;

	void Awake ()
    {
        if (Instance)
            return;
        Instance = this;
        GameCamera = GetComponentInChildren<GameCamera>();
        MoneyText = GetComponentInChildren<MoneyText>();
        MoneyText.SetNumber(money, true);
        notificationManager = GetComponentInChildren<NotificationManager>();

		// Intro
		GameState = GameState.Cutscene;
		introScreen.gameObject.SetActive(true);
		pauseScreen.SetActive(false);

        // Spawn franks
        SpawnFranks();
    }

    private void SpawnFranks()
    {
        for(int i = 0; i < spawnNodes.Length; i++)
        {
            var frank = Instantiate(frankPrefab);
            frank.targetNode = spawnNodes[i];
        }
        frankCount = startFrankCount = spawnNodes.Length;
    }

	public void PlayIntro()
	{
		if (introStarted)
			return;
		introStarted = true;
		introButton.interactable = introButton.GetComponent<HoverImage>().enabled = false;
		StartCoroutine(PlayIntroCoroutine());
	}

	private IEnumerator PlayIntroCoroutine()
	{
		var frameTime = 0.02f;
		/*
		var t = 0.0f;
		while (t <= introWaitTime)
		{
			if (Input.GetMouseButtonDown(0))
				break;
			t += frameTime;
			yield return new WaitForSecondsRealtime(frameTime);
		}
		*/
		var t2 = 0.0f;
		while(t2 <= introFadeTime)
		{
			t2 += frameTime;
			introScreen.alpha = (1.0f - t2 / introFadeTime);
			yield return new WaitForSecondsRealtime(frameTime);
		}
		introScreen.gameObject.SetActive(false);
		GameState = GameState.Idle;
    }

	public void PauseGame()
	{
		GameState = GameState.Pause;
		pauseScreen.SetActive(true);
	}

	public void UnpauseGame()
	{
		GameState = GameState.Idle;
		pauseScreen.SetActive(false);
	}

	public void QuitGame()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

	public void RestartGame()
	{
		LoadingScreen.Instance.ReloadScene();
	}

    public void ShowGameOver()
    {
        GameState = GameState.Pause;
        gameOverScreen.SetActive(true);
    }

	private void Update()
	{
		if (!IsIdle)
			return;
		if (Input.GetKeyDown(KeyCode.Escape))
			PauseGame();

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

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
    public WeaponManager weaponManager { get; private set; }

	public float money = 2000, moneyGain = 50;
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
	public GameObject pauseScreen, gameOverScreen, winScreen;

    // Spawning
    public Node[] spawnNodes;
    public Frank frankPrefab;
    public GameObject deathIconPrefab;

    // Game over
    [Header("Game Over")]
    public float cameraTime, endTime;
    public Transform endTarget, endTarget2;

    // Bounty
    public int baseBounty, bountyIncrease;

	//
	public SelectableObject main;

	void Awake ()
    {
        if (Instance)
            return;
        Instance = this;
        GameCamera = GetComponentInChildren<GameCamera>();
        MoneyText = GetComponentInChildren<MoneyText>();
        MoneyText.SetNumber((int)money, true);
        notificationManager = GetComponentInChildren<NotificationManager>();
        weaponManager = GetComponentInChildren<WeaponManager>();

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
		var t2 = 0.0f;
		while(t2 <= introFadeTime)
		{
			t2 += frameTime;
			introScreen.alpha = (1.0f - t2 / introFadeTime);
			yield return new WaitForSecondsRealtime(frameTime);
		}
		introScreen.gameObject.SetActive(false);
		GameState = GameState.Idle;
        // ShowGameOver(FindObjectOfType<Frank>());
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

    public void Win()
    {
        GameState = GameState.Pause;
        winScreen.SetActive(true);
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

    public void ShowGameOver(Frank frank)
    {
        GameState = GameState.Pause;
        frank.finalSkip = true;
        StartCoroutine(AnimateGameOver(frank));
    }

    private IEnumerator AnimateGameOver(Frank frank)
    {
        var t = 0.0f;
        var frankStart = frank.transform.position;
        var cameraStart = GameCamera.transform.position;
        while(t < cameraTime)
        {
            t += Time.deltaTime;
            frank.transform.position = Vector3.Lerp(frankStart, endTarget.position, t / cameraTime);
            var camPos = Vector3.Lerp(cameraStart, endTarget.position, t / cameraTime);
            camPos.z = cameraStart.z;
            GameCamera.transform.position = camPos;
            GameCamera.CheckBounds();
            yield return null;
        }
        frank.transform.SetParent(endTarget);
        frank.finalSkip = false;
        t = 0.0f;
        var startBoat = endTarget.position;
        while(t < endTime)
        {
            t += Time.deltaTime;
            endTarget.transform.position = Vector3.Lerp(
                startBoat, endTarget2.position, t / endTime);
            yield return null;
        }
        gameOverScreen.SetActive(true);
    }

	private void Update()
	{
		if (!IsIdle)
			return;
		if (Input.GetKeyDown(KeyCode.Escape) && (!SelectableObject.selected || SelectableObject.selected == main))
        {
            PauseGame();
            return;
        }
        if (frankCount == 0)
        {
            Win();
            return;
        }
		time += Time.deltaTime;
        money += moneyGain * Time.deltaTime;
        MoneyText.SetNumber((int)money);
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
            MoneyText.SetNumber((int)money);
            return true;
        }
        return false;
    }

    public void OnFrankKilled()
    {
        frankCount -= 1;
        money += baseBounty;
        baseBounty += bountyIncrease;
        MoneyText.SetNumber((int)money);
    }
}

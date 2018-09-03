﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void Awake ()
    {
        if (Instance)
            return;
        Instance = this;
        GameCamera = GetComponentInChildren<GameCamera>();
        MoneyText = GetComponentInChildren<MoneyText>();
        MoneyText.SetNumber(money, true);
	}

    private void Update()
    {
        if (!IsIdle)
            return;

        Test();
        GameCamera.HandleUpdate(Time.deltaTime);
    }

    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            money -= 100;
            MoneyText.SetNumber(money);
        }
    }
}
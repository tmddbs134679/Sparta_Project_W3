using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

//public enum EUIState
//{
//    HOME,
//    GAME,
//    GAMEOVER
//}
public class UIManager : MonoBehaviour
{
    HomeUI homeUI;
    GameUI gameUI;
    GameOverUI gameOverUI;

    private EUISTATE currentState;

    private void Awake()
    {
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

        ChangeState(EUISTATE.HOME);
    }

    public void SetPlayGame()
    {
        ChangeState(EUISTATE.GAME);
    }

    public void SetGameOver()
    {
        ChangeState(EUISTATE.GAMEOVER);
    }

    public void ChangeWave(int waveIdx)
    {
        gameUI.UpdateWaveText(waveIdx);
    }

    public void ChangePlayerHP(float currentHP, float maxHP)
    {
        gameUI.UpdateHPSlider(currentHP / maxHP);
    }

    private void ChangeState(EUISTATE state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);

    }
}

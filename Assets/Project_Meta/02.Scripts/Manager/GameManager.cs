using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


namespace Meta
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public static event Action OnGameStart;
        public static event Action OnGameEnd;

        public int GamePoint;
        private int GameStartCount;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GameStartCount = 3;
            Time.timeScale = 1;
            HandleGameStart();
        }

        private void GameStart()
        {
            OnGameStart?.Invoke();
        }

        public void GameEnd()
        {
            Time.timeScale = 0;
            RankSort(GamePoint);
            UIManager.Instance.ShowUI(EUIState.GAMEOVER);

            OnGameEnd?.Invoke();
        }

        private void HandleGameStart()
        {
            StartCoroutine(GameStartRoutine());
        }

        private IEnumerator GameStartRoutine()
        {
            
            while (GameStartCount > -1)
            {
                GameUI gameUI = UIManager.Instance.GetUI<GameUI>(EUIState.HOME);

                gameUI.UpdateCountdown(GameStartCount);
                yield return new WaitForSeconds(1f);
                GameStartCount--;
            }

            yield return new WaitForSeconds(1f);

            
            GameStart();
        }

        public void AddScore(int point)
        {
            GameUI gameUI = UIManager.Instance.GetUI<GameUI>(EUIState.HOME);
            GamePoint += point;
            gameUI.UpdatePoint(GamePoint);
        }

        public void RankSort(int gamePoint)
        {
            int rank1 = PlayerPrefs.GetInt("Rank1", 0);
            int rank2 = PlayerPrefs.GetInt("Rank2", 0);
            int rank3 = PlayerPrefs.GetInt("Rank3", 0);

            if (gamePoint > rank1)
            {
                PlayerPrefs.SetInt("Rank3", rank2);
                PlayerPrefs.SetInt("Rank2", rank1);
                PlayerPrefs.SetInt("Rank1", gamePoint);
            }
            else if (gamePoint > rank2)
            {
                PlayerPrefs.SetInt("Rank3", rank2);
                PlayerPrefs.SetInt("Rank2", gamePoint);
            }
            else if (gamePoint > rank3)
            {
                PlayerPrefs.SetInt("Rank3", gamePoint);
            }

            PlayerPrefs.Save();
        }

    }
}

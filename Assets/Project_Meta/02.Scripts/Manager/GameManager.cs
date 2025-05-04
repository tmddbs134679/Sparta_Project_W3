using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
  
    }
}

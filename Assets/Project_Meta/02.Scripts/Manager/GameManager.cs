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

        public int GamePoint;
        public int GameStartCount = 3;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            HandleGameStart();
        }

        public void GameStart()
        {
            OnGameStart?.Invoke();
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
        public void GameEnd()
        {

        }
    }
}

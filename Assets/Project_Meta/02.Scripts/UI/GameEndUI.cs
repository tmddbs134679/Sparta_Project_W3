using Meta;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndUI : UIBase
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button endBtn;

    public override EUIState UIType => EUIState.GAMEOVER;

 
    void Start()
    {

        startBtn.onClick.AddListener(ReStartMiniGame);
        endBtn.onClick.AddListener(ReturnToLobby);

    }

    private void ReStartMiniGame()
    {
        SceneManager.LoadScene("MiniGame");
    }

    private void ReturnToLobby()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Meta");
    }

}

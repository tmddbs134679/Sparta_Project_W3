using Meta;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndUI : UIBase
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button endBtn;
    [SerializeField] private TextMeshProUGUI rank1;
    [SerializeField] private TextMeshProUGUI rank2;
    [SerializeField] private TextMeshProUGUI rank3;


    public override EUISTATE UIType => EUISTATE.GAMEOVER;

 
    void Start()
    {

        startBtn.onClick.AddListener(ReStartMiniGame);
        endBtn.onClick.AddListener(ReturnToLobby);

    }
    public override void Show()
    {
        UpdateRank();
        base.Show();
       
    }
    private void ReStartMiniGame()
    {   
        MainGameManager.Instance.LoadSceneAsync(EGAMESTATE.MINIGAME);
    }

    private void ReturnToLobby()
    {
        Time.timeScale = 1;
        MainGameManager.Instance.LoadSceneAsync(EGAMESTATE.LOBBY);
    }

    private void UpdateRank()
    {
        rank1.text = PlayerPrefs.GetInt("Rank1").ToString();
        rank2.text = PlayerPrefs.GetInt("Rank2").ToString();
        rank3.text = PlayerPrefs.GetInt("Rank3").ToString();
    }
}

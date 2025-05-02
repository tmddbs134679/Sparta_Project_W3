using Meta;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartUI : UIBase
{

    [SerializeField]private Button startBtn;
    [SerializeField] private Button exitBtn;
    public override EUIState UIType => EUIState.GAMESTART;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(OnClickStart);
        exitBtn.onClick.AddListener(OnClickExit);
    }


    private void OnClickStart()
    {
        SceneManager.LoadScene("MiniGame");
    }
    private void OnClickExit()
    {
        gameObject.SetActive(false);
    }

   
}

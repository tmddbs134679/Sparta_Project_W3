using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{

    [SerializeField]private Button startBtn;
    [SerializeField] private Button exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(OnClickStart);
        exitBtn.onClick.AddListener(OnClickExit);
    }


    private void OnClickStart()
    {
       
    }
    private void OnClickExit()
    {
        gameObject.SetActive(false);
    }


}

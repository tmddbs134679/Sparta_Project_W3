using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Meta
{
    public class GameUI : UIBase
    {
        [SerializeField] private TextMeshProUGUI pointText;
        [SerializeField] private TextMeshProUGUI countdownText;

        public override EUIState UIType => EUIState.HOME;



        public void UpdateCountdown(int countTxt)
        {
            countdownText.text = countTxt.ToString();

            if(countTxt == 0)
                countdownText.gameObject.SetActive(false);
        }



    }

}

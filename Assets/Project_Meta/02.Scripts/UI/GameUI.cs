using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Meta
{
    public class GameUI : UIBase
    {
        [SerializeField] private TextMeshProUGUI StageText;
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private TextMeshProUGUI pointText;

        public override EUIState UIType => EUIState.HOME;

        public void UpdateCountdown(int countTxt)
        {
            countdownText.text = countTxt.ToString();

            if(countTxt == 0)
                countdownText.gameObject.SetActive(false);
        }

        public void UpdatePoint(int point)
        {
           pointText.text = point.ToString();   
        }

    }

}

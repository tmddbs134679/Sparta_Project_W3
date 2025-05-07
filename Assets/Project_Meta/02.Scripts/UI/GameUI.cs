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

        public override EUISTATE UIType => EUISTATE.HOME;

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

        public void UpdateStage(int stage)
        {
            StageText.text = stage.ToString();
        }

    }

}

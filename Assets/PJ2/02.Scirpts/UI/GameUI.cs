using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Slider hpSlider;

    private void Start()
    {
        UpdateHPSlider(1);
    }


    public void UpdateHPSlider(float per)
    {
        hpSlider.value = per;
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    protected override EUISTATE GetUIState()
    {
        return EUISTATE.GAME;
    }
}

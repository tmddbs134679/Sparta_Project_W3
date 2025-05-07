using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager UIManager;

    public virtual void Init(UIManager uiManager)
    {
        this.UIManager = uiManager;
    }

    protected abstract EUISTATE GetUIState();

    public void SetActive(EUISTATE state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}

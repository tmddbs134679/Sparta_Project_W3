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

    protected abstract EUIState GetUIState();

    public void SetActive(EUIState state)
    {
        this.gameObject.SetActive(GetUIState() == state);
    }
}

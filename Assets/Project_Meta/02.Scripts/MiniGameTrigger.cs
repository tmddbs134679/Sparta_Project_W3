using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Meta.UIManager.Instance.ShowUI(EUIState.GAMESTART);
    }
}

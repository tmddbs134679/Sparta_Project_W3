using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    private bool canTrigger = false;

    private void Start()
    {
        StartCoroutine(EnableTrigger());
    }
    private IEnumerator EnableTrigger()
    {
        yield return new WaitForSeconds(0.5f);  // 0.5초 후부터 트리거 활성화
        canTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTrigger)
            return;

        Meta.UIManager.Instance.ShowUI(EUISTATE.GAMESTART);
    }
}

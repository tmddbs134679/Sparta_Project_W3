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
        yield return new WaitForSeconds(0.5f);  // 0.5�� �ĺ��� Ʈ���� Ȱ��ȭ
        canTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTrigger)
            return;

        Meta.UIManager.Instance.ShowUI(EUISTATE.GAMESTART);
    }
}

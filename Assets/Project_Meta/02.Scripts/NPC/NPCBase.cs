using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class NPCBase : MonoBehaviour
{
    public ENPCState _npcState;
    public string NPCID;
    // Start is called before the first frame update

    public void SetState(ENPCState state) 
    {
        _npcState = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DialogueController.Instance.StartDialogue(this);

        }
    }
}

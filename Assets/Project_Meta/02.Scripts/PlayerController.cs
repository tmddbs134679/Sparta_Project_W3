using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta
{
    public class PlayerController : MonoBehaviour
    {


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Monster"))
            {
                GameManager.instance.GameEnd();
            }
        }
    }

}

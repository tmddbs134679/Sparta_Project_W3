using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta
{
    public class UIManager : MonoBehaviour
    {
       public static UIManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }



    }

}

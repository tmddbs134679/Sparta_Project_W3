using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Meta
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public int GamePoint;

        private void Awake()
        {
            instance = this;
        }     

        public void GameStart()
        {

        }

        public void GameEnd()
        {

        }





    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPos = player.transform.position;
        targetPos.z = -10f;

        transform.position = targetPos;
 
        
    }
}
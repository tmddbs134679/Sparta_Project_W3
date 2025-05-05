using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFactory<T> where T : MonoBehaviour
{
    private GameObject prefab;
    private Transform parent;
    private Queue<T> pool = new Queue<T>();
    private int initCount;


    public PoolFactory(GameObject prefab, Transform parent, int initCount)
    {
        this.prefab = prefab;
        this.parent = parent;
        this.initCount = initCount;

        for(int i=0; i <initCount; i++)
        {
            CreateNewObject();

        }
    }

    private void CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Enqueue(obj.GetComponent<T>());
    }


    public T Get(Vector3 position)
    {
        if (pool.Count == 0)
        {
          
            for (int i = 0; i < initCount; i++)
            {
                CreateNewObject();
            }
        }

        T obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        obj.transform.position = position;

        return obj;
    }
    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }


}

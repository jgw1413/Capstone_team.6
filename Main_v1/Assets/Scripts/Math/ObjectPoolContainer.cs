using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolContainer : MonoBehaviour {
    private static ObjectPoolContainer instance;
    public static ObjectPoolContainer Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("ObjectPoolContainer");
                instance = obj.AddComponent<ObjectPoolContainer>();
            }
            return instance;
        }
    }

    Dictionary<string, List<GameObject>> objectPoolDic = new Dictionary<string, List<GameObject>>();

    public void CreateObjectPool (string poolingName, GameObject obj, int createCount)
    {
        List<GameObject> poolList = new List<GameObject>();
        for (int i = 0; i < createCount; i++)
        {
            GameObject clone = Instantiate(obj, this.transform);
            clone.name = poolingName;
            poolList.Add(clone);
        }
        objectPoolDic.Add(poolingName, poolList);
    }

    public GameObject Pop (string poolingName)
    {
        if (objectPoolDic[poolingName].Count == 1)
        {
            GameObject clone = Instantiate(objectPoolDic[poolingName][0], this.transform);
            clone.name = poolingName;
            objectPoolDic[poolingName].Add(clone);
            Debug.LogError("Need More Object Pool :" + poolingName);
        }

        GameObject returnObj = objectPoolDic[poolingName][0];
        objectPoolDic[poolingName].RemoveAt(0);
        return returnObj;
    }

    public void Return (GameObject obj)
    {
        if (objectPoolDic.ContainsKey(obj.name))
        {
            objectPoolDic[obj.name].Add(obj);
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }


}

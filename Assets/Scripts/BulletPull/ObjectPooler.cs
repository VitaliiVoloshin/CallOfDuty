using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler: MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    public bool shouldExpand = true;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++) {
            pooledObjects.Add(PoolObjectInstantiate());
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }
        if (shouldExpand) {
            pooledObjects.Add(PoolObjectInstantiate());
            return pooledObjects[pooledObjects.Count - 1];
        } else {
            return null;
        }
    }

    private GameObject PoolObjectInstantiate()
    {
        GameObject obj = Instantiate(objectToPool);
        obj.transform.parent = this.transform;
        obj.SetActive(false);
        return obj;
    }
}

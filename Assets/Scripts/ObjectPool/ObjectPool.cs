using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool 
{
    private GameObject Parent;
    public PoolableObject Prefab;
    private int Size;
    private List<PoolableObject> AvailableObjects;


    public ObjectPool(PoolableObject prefab, int Size)
    {
        this.Prefab = prefab;
        this.Size= Size;
        AvailableObjects = new List<PoolableObject>(Size);
    }

    public static ObjectPool CreateInstance(PoolableObject Prefab, int Size)
    {
        ObjectPool pool = new ObjectPool(Prefab, Size);

        pool.Parent = new GameObject(Prefab.name + "Pool");
        pool.CreateObjects();

        return pool;
    }

    public void CreateObjects()
    {
        for (int i = 0; i < Size; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        PoolableObject poolableObject = GameObject.Instantiate(Prefab, Vector3.zero, Quaternion.identity, Parent.transform);
        poolableObject.Parent = this;
        poolableObject.gameObject.SetActive(false); // PoolableObject handles re-adding the object to the AvailableObjects
    }

    public void ReturnObjectToPool(PoolableObject poolableObject)
    {
        AvailableObjects.Add(poolableObject);
    }
   

    public PoolableObject GetObject()
    {
        if (AvailableObjects.Count == 0)
        {
            CreateObject();   
        }
        PoolableObject instance = AvailableObjects[0];
        AvailableObjects.RemoveAt(0);

        instance.gameObject.SetActive(true);

        return instance;
    }
}

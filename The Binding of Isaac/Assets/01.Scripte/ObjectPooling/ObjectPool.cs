using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }
    public Transform parnets;
    public int ObjectNumber = 50;
    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<Tears> poolingObjectQueue = new Queue<Tears>();

    private void Awake()
    {
        Instance = this;

        Initialize(ObjectNumber);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Tears CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<Tears>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(parnets);
        return newObj;
    }

    public static Tears GetObject()
    {
        if (Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObject = Instance.CreateNewObject();
            newObject.gameObject.SetActive(true);
            return newObject;
        }
    }

    public static void ReturnObject(Tears obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
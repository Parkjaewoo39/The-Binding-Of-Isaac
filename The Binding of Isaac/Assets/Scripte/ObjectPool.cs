using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObjectPool Instance;
    public int ObjectNumber;
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
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Tears>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
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
            return null;
        }

    }

    public static void ReturnObject(Tears obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}

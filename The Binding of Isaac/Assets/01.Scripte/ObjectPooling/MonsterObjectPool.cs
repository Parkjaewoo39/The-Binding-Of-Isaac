using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    public static MonsterObjectPool Instance;
    public int ObjectNumber;
    [SerializeField]
    private GameObject poolingObjectPrefab;
    public Transform parnets;

    Queue<MobTear> poolingObjectQueue = new Queue<MobTear>();

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

    private MobTear CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab,parnets).GetComponent<MobTear>();
        newObj.gameObject.SetActive(false);
       
        return newObj;
    }

    public static MobTear GetObject()
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

    public static void ReturnObject(MobTear obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}

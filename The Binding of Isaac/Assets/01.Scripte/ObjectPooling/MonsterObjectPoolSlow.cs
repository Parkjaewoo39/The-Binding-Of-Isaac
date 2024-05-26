using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterObjectPoolSlow : MonoBehaviour
{
    // Start is called before the first frame update
    public static MonsterObjectPoolSlow Instance;
    public int ObjectNumber;
    [SerializeField]
    private GameObject poolingObjectPrefab;
    public Transform parnets;

    Queue<MobSlowTear> poolingObjectQueue = new Queue<MobSlowTear>();

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

    private MobSlowTear CreateNewObject()
    {
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<MobSlowTear>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(parnets);
        return newObj;
    }

    public static MobSlowTear GetObject()
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

    public static void ReturnObject(MobSlowTear obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}

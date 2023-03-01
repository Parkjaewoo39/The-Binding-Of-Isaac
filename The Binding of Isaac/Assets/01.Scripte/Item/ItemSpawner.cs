using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spanwable
    {
        public GameObject gameObject;
        public float weight;
    }
    public List<Spanwable> itmes = new List<Spanwable>();

    float totalWeight;

    void Awake() 
    {
        totalWeight = 0;
        foreach(var spawnable in itmes)
        {
            totalWeight += spawnable.weight;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        float  pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = itmes[0].weight;
        
        while(pick > cumulativeWeight && chosenIndex < itmes.Count-1)
        {
            chosenIndex ++;
            cumulativeWeight += itmes[chosenIndex].weight;
        }
        GameObject i = Instantiate(itmes[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

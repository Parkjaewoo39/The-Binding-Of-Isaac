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
    public List<Spanwable> items = new List<Spanwable>();

    float totalWeight;

    void Awake() 
    {
        totalWeight = 0;
        foreach(var spawnable in items)
        {
            totalWeight += spawnable.weight;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        float  pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].weight;
        
        while(pick > cumulativeWeight && chosenIndex < items.Count-1)
        {
            chosenIndex ++;
            cumulativeWeight += items[chosenIndex].weight;
        }
        if (this.gameObject.transform.parent.name == "GoldRoom" )
        {
            GameObject i = Instantiate(items[chosenIndex].gameObject, new Vector2(transform.position.x, transform.position.y + 0.2f), Quaternion.identity) as GameObject;
            i.transform.SetParent(transform);

            i.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        }
        else 
        {
            GameObject i = Instantiate(items[chosenIndex].gameObject, new Vector2(transform.position.x, transform.position.y + 0.2f), Quaternion.identity) as GameObject;
            i.transform.SetParent(transform);

            i.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
        }
        
        
      
    }

}

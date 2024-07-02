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
<<<<<<< HEAD
    }
    public List<Spanwable> itmes = new List<Spanwable>();
=======
        
    }
    public List<Spanwable> items = new List<Spanwable>();
>>>>>>> Develop

    float totalWeight;

    void Awake() 
    {
        totalWeight = 0;
<<<<<<< HEAD
        foreach(var spawnable in itmes)
=======
        foreach(var spawnable in items)
>>>>>>> Develop
        {
            totalWeight += spawnable.weight;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        float  pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = itmes[0].weight;
        
        while(pick > cumulativeWeight && chosenIndex < itmes.Count-1)
        {
            chosenIndex ++;
            cumulativeWeight += itmes[chosenIndex].weight;
        }
        GameObject i = Instantiate(itmes[chosenIndex].gameObject, new Vector3( transform.position.x,transform.position.y+10,0), Quaternion.identity) as GameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
=======
        
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

>>>>>>> Develop
}

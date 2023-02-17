using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{     
    [SerializeField]
    private List<Grid> gridMapList = new List<Grid>();
    
    private float xSize = 5;
    private float ySize = 5;

    // Start is called before the first frame update
    void Start()
    {
        var gridMap = Resources.Load("Tilemap", typeof(GameObject)) as GameObject;

        for (int i = 0; i < ySize; i++) 
        {
            for (int ii = 0; ii < xSize; ii ++) 
            {
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

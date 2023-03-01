using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDoor : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;
    public GameObject doorCollider;
    private GameObject player;

    private float widthOffset = 1.75f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch(doorType)
            {
                case DoorType.bottom:
                player.transform.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                break;
                case DoorType.left:
                player.transform.position = new Vector2(transform.position.x - widthOffset, transform.position.y );
                break;
                case DoorType.right:
                player.transform.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
                break;
                case DoorType.top:
                player.transform.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                break;

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

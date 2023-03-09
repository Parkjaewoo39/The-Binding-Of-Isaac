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
    private RectTransform rect;
    private float widthOffset = -2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch(doorType)
            {
                case DoorType.bottom:
                rect.position = new Vector2(transform.position.x, transform.position.y - widthOffset)*100;
                break;
                case DoorType.left:
                rect.position = new Vector2(transform.position.x - widthOffset, transform.position.y )*100;
                break;
                case DoorType.right:
                rect.position = new Vector2(transform.position.x + widthOffset, transform.position.y)*100;
                break;
                case DoorType.top:
                rect.position = new Vector2(transform.position.x, transform.position.y + widthOffset)*100;
                break;

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rect = gameObject.AddComponent<RectTransform>();
        switch(doorType)
            {
                case DoorType.bottom:
                rect.position = new Vector2(transform.position.x, transform.position.y - widthOffset);
                break;
                case DoorType.left:
                rect.position = new Vector2(transform.position.x - widthOffset, transform.position.y );
                break;
                case DoorType.right:
                rect.position = new Vector2(transform.position.x + widthOffset, transform.position.y);
                break;
                case DoorType.top:
                rect.position = new Vector2(transform.position.x, transform.position.y + widthOffset);
                break;

            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

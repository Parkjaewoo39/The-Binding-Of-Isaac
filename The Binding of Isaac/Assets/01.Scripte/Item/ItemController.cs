using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item
{
    public string name;
    public string description;
    public Sprite itemImage;
}
public class ItemController : MonoBehaviour
{
    public Item item;
    public float healthChange;
    public float moveSpeedChange;
    public float tearSpeedChange;
    public float tearSizeChange;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite =item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }       

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerManager.itemAmount ++;
            PlayerManager.HealIsaac(healthChange);
            PlayerManager.MoveSpeedChange(moveSpeedChange);
            PlayerManager.FireRateChange(tearSpeedChange);
            PlayerManager.TearSizeChange(tearSizeChange);
            PlayerManager.instance.UpdateCollectedItmes(this);

            Destroy(gameObject);
        }
    }
}

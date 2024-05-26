using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemField : MonoBehaviour
{
    public Item item;
    public SpriteRenderer itemSprite;
    public void SetItem(Item _item) 
    {
        item.itemType = _item.itemType;
        item.itemId = _item.itemId;
        item.itemEnergy = _item.itemEnergy;
        item.name = _item.name;
        item.itemImage = _item.itemImage;
        item.itemInventoryImage = _item.itemInventoryImage;
    }
    public Item GetItem() 
    {
        return item;
    }
    public void DestroyItem() 
    {
        Destroy(gameObject);   
    }
    
}

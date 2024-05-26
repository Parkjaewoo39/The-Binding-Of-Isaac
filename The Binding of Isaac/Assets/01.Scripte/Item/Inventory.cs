using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    PlayerController playerContorller;
    public List<Item> items = new List<Item>();
    public bool AddItem(Item _item)
    {
        items.Add(_item);
        switch (_item.itemType)
        {
            case ItemType.Passive:
                GameManager.Instance.UpdatePassiveItem(_item);
                break;
            case ItemType.Active:
                GameManager.Instance.UpdateActiveItem(_item);
                break;
        }
       
        
        
        return true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            ItemField itemField = other.GetComponent<ItemField>();

            if (AddItem(itemField.GetItem()))
            {
                itemField.DestroyItem();
            }

        }
    }
}

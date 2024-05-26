using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType 
{
    None,
    Passive,
    Active
}
[System.Serializable]
public class Item
{
    
    public ItemType itemType;
    public int itemId;
    public float itemEnergy;
    public float itemEnergyMax;
    public string name;  
    public Sprite itemImage;
    public Sprite itemInventoryImage;

    
    public Item(ItemType itemType, int itemId, float itemEnergy, float itemEnergyMax, string name, Sprite itemImage, Sprite itemInventoryImage) 
    {
        this.itemType = itemType;
        this.itemId = itemId;
        this.itemEnergy = itemEnergy;
        this.itemEnergyMax = itemEnergyMax;
        this.name = name;
        this.itemImage = itemImage;
        this.itemInventoryImage = itemInventoryImage;

    }
   
    
}


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item 
{
    protected string itemName = default;
   
    protected float damage = default;
    protected float speed = default; 

}


class WoodenSpoon : Item
{
    public  WoodenSpoon() 
    {

        this.itemName = "¿ìµå ½ºÇ¬";
        this.damage = 0f;
        this.speed= 5f;
    } 
        
}

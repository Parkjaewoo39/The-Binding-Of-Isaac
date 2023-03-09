using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public float heart;
    public float numOfHearts;
     private float fillValue;

    public GameObject heartContainer;
    public Image[] hearts;
    public Sprite fullheart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Update()
    {
        heart = PlayerManager.Health;
        numOfHearts = PlayerManager.MaxHealth;
        if(heart > numOfHearts)
        {
            heart = numOfHearts;
        }
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < heart)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;                
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        //  fillValue = (float)heart;
        // fillValue = fillValue /numOfHearts;
        // heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}

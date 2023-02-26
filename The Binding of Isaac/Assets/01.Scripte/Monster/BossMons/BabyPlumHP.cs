using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class BabyPlumHP : MonoBehaviour
{
    
    public Image hpbar;
   
    public float maxHp;
    public float currenthp;

    void Update()
    {
        BabyPlumHp();
    }
    // Start is called before the first frame update
    void Start()
    {
        hpbar = GetComponent<Image>();
        
        currenthp = BabyPlum1.babyPlumHp;
        maxHp = currenthp;
    }

    public void BabyPlumHp()
    {       
        hpbar.fillAmount = currenthp / maxHp;
    }
    
}
  
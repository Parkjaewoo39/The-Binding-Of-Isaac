using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class BabyPlumHP : MonoBehaviour
{

    public GameObject hpContainer;
    public GameObject uiParnet;
    public Image hpbar;
    public float fillValue;
    public float maxHp;
    public float currenthp;

    void Update()
    {
        if (0 < currenthp )
        {
            BabyPlumHp();
        }

    }
    // Start is called before the first frame update
    void Start()
    {

        currenthp = BabyPlum.babyPlumHp;
        maxHp = BabyPlum.babyPlumHp;
    }

    public void BabyPlumHp()
    {
        fillValue = (float)BabyPlum.babyPlumHp;
        fillValue = fillValue / maxHp;

        hpContainer.GetComponent<Image>().fillAmount = fillValue;
    }

    public void BossHpUiActiveTrue() 
    {
        uiParnet.SetActive(true);
    }

    public void BossHpUiAcitveFalse() 
    {
        uiParnet.SetActive(false);
    }
}

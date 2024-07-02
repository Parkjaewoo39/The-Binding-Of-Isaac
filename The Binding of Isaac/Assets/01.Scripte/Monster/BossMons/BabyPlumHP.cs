using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class BabyPlumHP : MonoBehaviour
{

    public GameObject hpContainer;
<<<<<<< HEAD
=======
    public GameObject uiParnet;
>>>>>>> Develop
    public Image hpbar;
    public float fillValue;
    public float maxHp;
    public float currenthp;

    void Update()
    {
<<<<<<< HEAD
        BabyPlumHp();
=======
        if (0 < currenthp )
        {
            BabyPlumHp();
        }

>>>>>>> Develop
    }
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        currenthp = BabyPlum1.babyPlumHp;
        maxHp = BabyPlum1.babyPlumHp;
=======

        currenthp = BabyPlum.babyPlumHp;
        maxHp = BabyPlum.babyPlumHp;
>>>>>>> Develop
    }

    public void BabyPlumHp()
    {
<<<<<<< HEAD
        fillValue = (float)BabyPlum1.babyPlumHp;
        fillValue = fillValue / maxHp;
        hpContainer.GetComponent<Image>().fillAmount = fillValue;
    }

=======
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
>>>>>>> Develop
}

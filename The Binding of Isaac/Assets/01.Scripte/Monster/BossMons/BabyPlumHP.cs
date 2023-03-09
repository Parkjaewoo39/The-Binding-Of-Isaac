using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;

public class BabyPlumHP : MonoBehaviour
{

    public GameObject hpContainer;
    public Image hpbar;
    public float fillValue;
    public float maxHp;
    public float currenthp;

    void Update()
    {
        BabyPlumHp();
    }
    // Start is called before the first frame update
    void Start()
    {
        currenthp = BabyPlum1.babyPlumHp;
        maxHp = BabyPlum1.babyPlumHp;
    }

    public void BabyPlumHp()
    {
        fillValue = (float)BabyPlum1.babyPlumHp;
        fillValue = fillValue / maxHp;
        hpContainer.GetComponent<Image>().fillAmount = fillValue;
    }

}

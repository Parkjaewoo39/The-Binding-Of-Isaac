using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public static int itemAmount = 0;

    private static int coinCount  = 0;
    private static int keyCount = 0;
    private static int bombCount = 0;

    private string coinNumber = default;
    private string keyNumber = default;
    private string bombNumber = default;   

    private static float health = 6;
    private static float maxHealth = 6;
    private static float moveSpeed = 0.5f;
    private static float fireRate = 0.5f;
    private static float tearSize = 30f;

    public static float Health { get => health; set => health = value; }

    public static float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float TearSize { get => tearSize; set => tearSize = value; }

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI bombText;
    public TextMeshProUGUI keyText;

    private bool bootCollected = false;
    private bool screwCollected = false;
    public List<string> collectedNames = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       coinText.text = "" + coinCount;
       bombText.text = "" + bombCount;
       keyText.text = "" + keyCount;
    }

    public static void DamageIsaac(float damage)
    {
        health -= damage;

        if (Health <= 0)
        {
            DieIsaac();
        }
    }

    public static void HealIsaac(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate)
    {
        FireRate += rate;
    }
    public static void TearSizeChange(float size)
    {
        tearSize += size;
    }

    public static void CoinChange(int coin)
    {
        coinCount += coin;
    }

    public static void BombChange(int bomb)
    {
        bombCount += bomb;
    } 

    public static void KeyChange(int key)
    {
        keyCount += key;
    } 

    public void UpdateCollectedItmes(ItemController item)
    {
        collectedNames.Add(item.item.name);

        foreach(string i in collectedNames)
        {
            switch(i)
            {
                case "Boot":
                bootCollected = true;
                break;
                case "Screw":
                screwCollected = true;
                break;                
            }
            if(bootCollected && screwCollected)
            {
                FireRateChange(0.25f);
            }
        }
    }

    private static void DieIsaac()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            Debug.Log(other.tag == "Boss");
            DamageIsaac(1);
        }
        if (other.tag == "BossTear")
        {
            DamageIsaac(1);
        }
        if (other.tag == "Enemy")
        {
            DamageIsaac(0.5f);
        }

        if(other.tag == "Coin")
        {
            CoinChange(1);
        }
        if(other.tag == "Bomb")
        {
            BombChange(1);
        }
        if(other.tag == "Key")
        {
            KeyChange(1);
        }
    }
}

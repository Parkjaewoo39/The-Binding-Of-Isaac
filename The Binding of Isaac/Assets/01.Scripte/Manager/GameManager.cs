using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LayerMask layerMask;
    public Item activeItem;
    public static float isaacHeartHp;
    public static float isaacHeartMaxHp;
    public static float isaacDamage;
    public static float isaacTearSpeed;
    public static float isaacRange;
    public static float isaacTime;
    public static float isaacMaxReload;
    public static float isaacReload;
    public static float isaacTearHigh;
    public static float isaacMoveSpeed;
    public static float isaacTearSize;
    public static float bombDamage;

    public static int necronomiconDamage;
    public static int activeEnergy;
    public static int activeEnergyMax;
    public static int coin;
    public static int bomb;
    public static int key;
    public string isaacItem;

    public bool isRoomMake = false;

    public List<string> collectedNames = new List<string>();
    public static int itemAmount = 0;

    public float IsaacHealthHp
    {
        get { return isaacHeartHp; }
        set { isaacHeartHp = value; }
    }

    public float IsaacHeartMaxHp
    {
        get { return isaacHeartMaxHp; }
        set { isaacHeartMaxHp = value; }
    }

    public float IsaacDamage
    {
        get { return isaacDamage; }
        set { isaacDamage = value; }
    }
    public float IsaacBombDamage
    {
        get { return bombDamage; }
        set { bombDamage = value; }
    }

    public float IsaacTearSpeed
    {
        get { return isaacTearSpeed; }
        set { isaacTearSpeed = value; }
    }
    public float IsaacTearSize
    {
        get { return isaacTearSize; }
        set { isaacTearSize = value; }
    }

    public float IsaacRange
    {
        get { return isaacRange; }
        set { isaacRange = value; }
    }

    public float IsaacTime
    {
        get { return isaacTime; }
        set { isaacTime = value; }
    }

    public float IsaacMaxReload
    {
        get { return isaacMaxReload; }
        set { isaacMaxReload = value; }
    }

    public float IsaacReload
    {
        get { return isaacReload; }
        set { isaacReload = value; }
    }

    public float IsaacTearHigh
    {
        get { return isaacTearHigh; }
        set { isaacTearHigh = value; }
    }

    public float IsaacMoveSpeed
    {
        get { return isaacMoveSpeed; }
        set { isaacMoveSpeed = value; }
    }
    public int PickUpCoin
    {
        get { return coin; }
        set { coin = value; }
    }
    public int PickUpKey
    {
        get { return key; }
        set { key = value; }
    }
    public int PickUpBomb
    {
        get { return bomb; }
        set { bomb = value; }
    }

    public bool RoomMake
    {
        get { return isRoomMake; }
        set { isRoomMake = value; }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        InitializedGame();
        collectedNames = new List<string>();

    }

    public void Start()
    {


    }

    private void Update()
    {

    }
    void InitializedGame()
    {

        isaacHeartHp = 3f;
        isaacHeartMaxHp = 3;
        isaacDamage = 3.5f;
        isaacTearSpeed = 8f;
        isaacRange = 1f;
        isaacTime = 1f;
        isaacMaxReload = 0.5f;
        isaacReload = 0f;
        isaacTearHigh = 1f;
        isaacMoveSpeed = 0.5f;
        isaacTearSize = 4f;
        bombDamage = 100;
        necronomiconDamage = 40;
        coin = 0;
        key = 1;
        bomb = 3;
    }
    
    public void IsaacNormalHit()
    {
        isaacHeartHp -= 0.5f;
        if (isaacHeartHp <= 0)
        {
            isaacHeartHp = 0f;
        }

        UiManager.instance.UpdateIsaacHeartUi();
    }
    public void IsaacBossHit()
    {
        isaacHeartHp -= 1f;
        if (isaacHeartHp < 0 || isaacHeartHp == 0)
        {
            isaacHeartHp = 0f;
        }
        UiManager.instance.UpdateIsaacHeartUi();
    }

    public void IsaacIncreaseHp(float current, float max)
    {
        isaacHeartHp += current;
        isaacHeartMaxHp += max;

        if ( isaacHeartMaxHp < isaacHeartHp)
        {
            isaacHeartHp = isaacHeartMaxHp;
        }
        if (10 < isaacHeartMaxHp)
        {
            isaacHeartMaxHp = 10f;
        }
        if (10 < isaacHeartMaxHp)
        {
            isaacHeartMaxHp = 10f;
        }
        UiManager.instance.UpdateIsaacHeartUi();
    }
    public static void FireRateChange(float rate)
    {
        isaacReload += rate;
    }
    public static void MoveSpeedChange(float rate)
    {
        isaacMoveSpeed += rate;
    }
    public void UpdatePassiveItem(Item item)
    {

        switch (item.itemId)
        {
            case 101:
                FireRateChange(-10f);
                break;
            case 102:
                MoveSpeedChange(5f);
                break;
        }


    }
    public void UpdateActiveItem(Item item)
    {
        activeItem = item;
        UiManager.instance.GetActive(item);
       
    }
    public void UseAcitve( )
    {
        if (activeItem != null && 0 < activeItem.itemEnergy )
        {
            if (activeItem.itemEnergy == activeItem.itemEnergyMax)
            {
                ApplyActiveItem(activeItem);
                activeItem.itemEnergy = 0;
                UiManager.instance.UpdateAcitvEnergyUi(activeItem);
            }
        }
        else { Debug.Log("이게 없네"); }
       
    }
    public void ApplyActiveItem(Item item) 
    {
        switch (item.itemId)
        {
            case 501:
                UseNecroneomicon();
                
                break;

        }
    }

    public void AddEnergy(Item item)
    {
        item.itemEnergy += 1;
        if (item.itemEnergyMax < item.itemEnergy)
        {
            item.itemEnergy = item.itemEnergyMax;
        }
    }
    public void UseNecroneomicon()
    {
        Vector2 roomCenter = new Vector2(transform.position.x, transform.position.y);
        Vector2 roomSize = new Vector2(10, 7);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(roomCenter, roomSize, 0f, layerMask);
        foreach (Collider2D enemyCollider in enemies)
        {
            IEnemy enemy = enemyCollider.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.Hit(necronomiconDamage);
            }
        }
    }
    public void Pause(bool pause)
    {
        if (pause == true)
        {

            Time.timeScale = 0;
            UiManager.instance.PauseUi(pause);
        }
        else
        {

            Time.timeScale = 1;
            UiManager.instance.PauseUi(pause);
        }
    }
    public static void CoinChange(int _Coin)
    {

        coin += _Coin;
    }

    public static void BombChange(int _Bomb)
    {
        bomb += _Bomb;
    }

    public static void KeyChange(int _Key)
    {
        key += _Key;
    }

    public static void NextScene() 
    {
        SceneManager.LoadScene("00.TitleScene");
    }
   
}

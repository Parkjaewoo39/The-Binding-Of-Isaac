using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    public int passiveItem;
    public int activeItem;
    public float isaacHeartHp;
    public float isaacHeartMaxHp;
    public float isaacDamage;
    public float isaacTearSpeed;
    public float isaacRange;
    public float isaacTime;
    public float isaacMaxReload;
    public float isaacReload;
    public float isaacTearHigh;
    public float isaacMoveSpeed;
    public float isaacTimeHigh;
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    PlayerData playerData = new PlayerData();
    //저장 경로 스트링
    string path;
    string fileName = "SaveLoad";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        //DontDestroyOnLoad(this.gameObject);
        path = Application.persistentDataPath + "/";
    }

    public List<Item> itemDB = new List<Item>();
    public GameObject itemPrefab;
    private void Start()
    {
        
      
    }

    public void SaveData() 
    {
        string playerDataJson = JsonUtility.ToJson(playerData);
        File.WriteAllText(path + fileName, playerDataJson);
    }
    public void LoadData() 
    {
        string playerDataJson = File.ReadAllText(path + fileName);
        playerData = JsonUtility.FromJson<PlayerData>(playerDataJson);
    }

}

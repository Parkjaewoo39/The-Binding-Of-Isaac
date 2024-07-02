using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using System.Reflection;


public class Room : MonoBehaviour
{

    RoomManager roomManager;
    Rigidbody2D roomRigid;

    public String adjacentRoomName;

    public GameObject[] basicDoorOpen = new GameObject[4];  //[0]:top [1]:bottom [2]:left [3]:right
    public GameObject[] bossDoorOpen = new GameObject[4];   //[0]:top [1]:bottom [2]:left [3]:right
    public GameObject[] goldDoorOpen = new GameObject[4];   //[0]:top [1]:bottom [2]:left [3]:right   

    public GameObject[] basicDoorClose = new GameObject[4];  //[0]:top [1]:bottom [2]:left [3]:right
    public GameObject[] bossDoorClose = new GameObject[4];   //[0]:top [1]:bottom [2]:left [3]:right
    public GameObject[] goldDoorClose = new GameObject[4];   //[0]:top [1]:bottom [2]:left [3]:right   


    [SerializeField] Transform[] mobSpawnPosition;
    [SerializeField] Transform bossSpwanPosition;
    [SerializeField] GameObject[] mobPrefabs;
    [SerializeField] GameObject bossPrefabs;


    public Vector2Int RoomIndex { get; set; }
    Vector2Int roomIndex;

    public int roomValue;
    public int roomDoorCount; //방문이 한개인 끝방
    public int mobCount;
    public int onlyOneCheck;
    public bool isbool = false;
    public bool isKeyGoldRoom = false;
    private void Awake()
    {

        roomManager = GameObject.FindObjectOfType<RoomManager>();
        if (roomManager == null)
        {
            Debug.LogError("RoomManager를 찾을 수 없습니다.");
        }

    }
    void Start()
    {
        roomRigid = GetComponent<Rigidbody2D>();

        roomIndex = RoomIndex;
        if (this.gameObject.name == "MobRoom")
        {
            MobSpawn(isbool);

        }
        else if (this.gameObject.name == "StartROom")
        {
            if (0 < mobCount)
            {
                roomManager.StartCoroutine(roomManager.RoomMake());
            }
        }
        onlyOneCheck = 1;

    }   //Start()


    public void MobSpawn(bool isbool)
    {
        int randomNumPosition = UnityEngine.Random.Range(0, mobSpawnPosition.Length);
        if (randomNumPosition != 0 && isbool == true && onlyOneCheck == 1)
        {
            for (int i = 0; i < randomNumPosition; i++)
            {
                int randomNumMobPrefab = UnityEngine.Random.Range(0, mobPrefabs.Length);
                Transform spawnTrans = mobSpawnPosition[i];
                GameObject mobSpawn = mobPrefabs[randomNumMobPrefab];
                if (mobSpawn != null && spawnTrans != null)
                {
                    GameObject mob = Instantiate(mobSpawn, spawnTrans.transform.position, Quaternion.identity);
                    mob.transform.SetParent(spawnTrans.transform);
                    mob.SetActive(true);

                    mobCount += 1;
                    onlyOneCheck -= 1;
                }
            }
        }
        else
        {
            mobCount = 0;
            onlyOneCheck = 0;
        }


    }   //MobSpawn()
    public void BossSpawn(bool isBossBool)
    {
        if (isBossBool == true && onlyOneCheck == 1)
        {
            Transform bossSpawn = bossSpwanPosition;
            GameObject boss = Instantiate(bossPrefabs, bossSpawn.transform.position, Quaternion.identity);
            boss.transform.SetParent(bossSpawn.transform);
            boss.SetActive(true);

            mobCount += 1;

        }

    }
    public void MobDie()
    {

        mobCount -= 1;

        if (mobCount == 0)
        {
            isbool = false;
            onlyOneCheck = 0;
            if (GameManager.Instance.activeItem != null)
            {
                GameManager.Instance.activeItem.itemEnergy += 1;
                UiManager.instance.UpdateAcitvEnergyUi(GameManager.Instance.activeItem);

            }
            else { Debug.Log("asdf"); }
            PlayerEnterRoom(this);
        }
       


    }


    public void OpenDoor(Vector2Int direction)
    {
        if (adjacentRoomName != null)
        {
            int index = DoorDirectionIndex(direction);

            if (adjacentRoomName == "BossRoom" || gameObject.name == "BossRoom")
            {
                bossDoorOpen[index].SetActive(true);
                bossDoorClose[index].SetActive(false);
                basicDoorOpen[index].SetActive(false);
            }
            else if (adjacentRoomName == "GoldRoom")
            {
                if (isKeyGoldRoom)
                {
                    goldDoorOpen[index].SetActive(true);
                    goldDoorClose[index].SetActive(false);
                    basicDoorOpen[index].SetActive(false);
                }
                else
                {
                    goldDoorOpen[index].SetActive(false);
                    goldDoorClose[index].SetActive(true);
                    basicDoorOpen[index].SetActive(false);
                }
            }
            else if (gameObject.name == "GoldRoom") 
            {
                goldDoorOpen[index].SetActive(true);
            }
            else
            {
                basicDoorOpen[index].SetActive(true);
                basicDoorClose[index].SetActive(false);
            }

        }
        else
        {

        }


    }
    public void CloseDoor(Vector2Int direction)
    {

        int index = DoorDirectionIndex(direction);
        if (adjacentRoomName != null)
        {

            if (adjacentRoomName == "BossRoom" || gameObject.name == "BossRoom")
            {

                bossDoorClose[index].SetActive(true);
                bossDoorOpen[index].SetActive(false);

            }
            else if (adjacentRoomName == "GoldRoom" || gameObject.name == "GoldRoom")
            {
                if (isKeyGoldRoom)
                {
                    goldDoorOpen[index].SetActive(true);
                    goldDoorClose[index].SetActive(false);
                    basicDoorOpen[index].SetActive(false);
                }
                else
                {

                    goldDoorOpen[index].SetActive(false);
                    goldDoorClose[index].SetActive(true);
                    basicDoorOpen[index].SetActive(false);
                }
            }

            else
            {
                basicDoorClose[index].SetActive(true);
                basicDoorOpen[index].SetActive(false);

            }

        }
        else
        {

        }

    }


    //Vector2Int를 index로 치환.
    public int DoorDirectionIndex(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
        {
            return 0;
        }
        else if (direction == Vector2Int.down)
        {
            return 1;
        }
        else if (direction == new Vector2Int(-1, 0))
        {
            return 2;
        }
        else if (direction == Vector2Int.right)
        {
            return 3;
        }
        else
        {

            return -1;
        }
    }

    public void PlayerEnterRoom(Room room)
    {
        if (room != null)
        {

            if (room.mobCount > 0)
            {
                //방에 모빌이 있으면 문을 닫음
                roomManager.CloseDoors(room.gameObject, room.RoomIndex.x, room.RoomIndex.y);
            }
            else
            {
                //방에 모빌이 없으면 문을 염
                roomManager.OpenDoors(room.gameObject, room.RoomIndex.x, room.RoomIndex.y);
            }
        }
        else Debug.Log("room 이 null");
    }
    public void ClearBossRoom(bool isbool)
    {
        if (this.gameObject.name == "BossRoom") 
        {
            GameObject bossRoom = GetComponent<Room>().gameObject;
            bossRoom.transform.GetChild(5).gameObject.SetActive(isbool);
            bossRoom.transform.GetChild(6).gameObject.SetActive(isbool);

        }

    }

}



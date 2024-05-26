using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    public GameObject roomStart;  //시작방
    public GameObject roomBoss;   //보스방
    public GameObject roomGold;   //황금방
    public GameObject roomMob;    //몹방    

    [SerializeField] GameObject player;     //Isaac 프리팹    


    public List<Room> rooms = new List<Room>();
    public GameObject roomParnet;  //room의 부모 GameObject

    public Rigidbody2D roomRigid;
    public PlayerController playerController;
    public BoxCollider2D boxCollider2D;

    [SerializeField] private int maxRooms = 15;
    [SerializeField] private int minRooms = 10;

    int roomWidth = 20;
    int roomHeight = 12;

    int gridSizeX = 10;
    int gridSizeY = 10;


    private Vector2 initialRoomPosition;
    private List<GameObject> roomObjects = new List<GameObject>();
    private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

    private int[,] roomGrid;

    public int roomCount;
    public int mobCount;

    public bool isGeneration = false;          //방 생성 완료 bool
    public bool isRoomInstanceCheck = false;    //코루틴 방 생성 bool
    public bool isRoomMoveCheck = false;        //방 사이 이동 bool
    public bool isMobCountCheck = false;        //몹이 있는지 확인 bool
    public bool isBossRoomClearCheck = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        roomRigid = roomParnet.GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();


        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        initialRoomPosition = roomParnet.transform.position;

        StartRoomGenerationFronRoom(initialRoomIndex);
        if (!isRoomInstanceCheck)
        {
            StartCoroutine(RoomMake());
        }


    }

    private void Update()
    {


    }
    //방생성 코루틴
    public IEnumerator RoomMake()
    {
        isRoomInstanceCheck = true;
        while (roomCount < maxRooms)
        {
            if (0 < roomQueue.Count && roomCount < maxRooms && !isGeneration)
            {
                Vector2Int roomIndex = roomQueue.Dequeue();
                int gridX = roomIndex.x;
                int gridY = roomIndex.y;

                TryGenerateRoom(new Vector2Int(gridX - 1, gridY));
                TryGenerateRoom(new Vector2Int(gridX + 1, gridY));
                TryGenerateRoom(new Vector2Int(gridX, gridY + 1));
                TryGenerateRoom(new Vector2Int(gridX, gridY - 1));

            }
            else if (roomCount < minRooms)
            {
                RegenerateRoom();
                GameManager.Instance.isRoomMake = false;

            }
            else if (!isGeneration)
            {
                Vector2Int bossRoomIndex = roomObjects[roomObjects.Count - 1].GetComponent<Room>().RoomIndex;
                BossRoom(bossRoomIndex);
                GoldRoom();
                UpdateAllRoomsDoors();
                GameManager.Instance.isRoomMake = true;
                isGeneration = true;
            }
            yield return null;
        }

        if (isRoomInstanceCheck)
        {
            Vector2Int bossRoomIndex = roomObjects[roomObjects.Count - 1].GetComponent<Room>().RoomIndex;
            BossRoom(bossRoomIndex);
            GoldRoom();
            UpdateAllRoomsDoors();
            GameManager.Instance.isRoomMake = true;

        }
        isRoomInstanceCheck = false;


    }
    //방생성 후 모든방의 문을 다시 콜해서 보스룸 황금방 Door 픽스
    private void UpdateAllRoomsDoors()
    {
        foreach (var roomObject in roomObjects)
        {
            Room roomScript = roomObject.GetComponent<Room>();
            Vector2Int roomIndex = roomScript.RoomIndex;
            OpenDoors(roomObject, roomIndex.x, roomIndex.y);
        }
    }

    private void StartRoomGenerationFronRoom(Vector2Int roomIndex)
    {
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x, y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomStart, GetPositionFrontGridIndex(roomIndex), Quaternion.identity);
        rooms.Add(initialRoom.GetComponent<Room>());
        initialRoom.transform.SetParent(roomParnet.transform);
        initialRoom.name = $"{roomStart.name}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }

    //황금방 생성
    private void GoldRoom()
    {
        //
        List<Room> room = new List<Room>();
        foreach (var roomObject in roomObjects)
        {
            Room roomScript = roomObject.GetComponent<Room>();
            if (CountAdjacentRooms(roomScript.RoomIndex) <= 1 && roomScript.name != "BossRoom" &&
                    roomScript.name != "StartRoom" && roomObject != roomObjects[roomObjects.Count - 2])
            {
                room.Add(roomScript);
            }
        }

        // room 리스트에서 무작위로 하나의 방을 선택하여 황금방으로 변경합니다.
        if (room.Count > 0)
        {
            Room randomRoom = room[Random.Range(0, room.Count)];
            Vector3 position = randomRoom.transform.position;
            Quaternion rotation = randomRoom.transform.rotation;

            Vector2Int roomIndex = randomRoom.RoomIndex;


            // 새로운 황금방 생성
            GameObject goldRoom = Instantiate(roomGold, position, rotation);
            goldRoom.transform.SetParent(roomParnet.transform);
            goldRoom.GetComponent<Room>().RoomIndex = roomIndex;
            goldRoom.gameObject.SetActive(true);
            goldRoom.name = $"GoldRoom";
            rooms.Add(goldRoom.GetComponent<Room>());
            roomObjects.Add(goldRoom);

            Destroy(randomRoom.gameObject);
            rooms.Remove(randomRoom);
            roomObjects.Remove(randomRoom.gameObject);

            // rooms 리스트와 roomObjects 리스트에 새로운 황금방 추가

            OpenDoors(goldRoom, randomRoom.RoomIndex.x, randomRoom.RoomIndex.y);

        }
    }
    //보스방 생성
    private void BossRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        GameObject newBossRoom = Instantiate(roomBoss, GetPositionFrontGridIndex(roomIndex), Quaternion.identity);

        newBossRoom.transform.SetParent(roomParnet.transform);
        newBossRoom.GetComponent<Room>().RoomIndex = roomIndex;
        newBossRoom.gameObject.SetActive(true);
        newBossRoom.name = $"BossRoom";
        rooms.Add(newBossRoom.GetComponent<Room>());
        roomObjects.Add(newBossRoom);

        Destroy(roomObjects[roomObjects.Count - 2]);
        rooms.Remove(roomObjects[roomObjects.Count - 2].GetComponent<Room>());
        roomObjects.Remove(roomObjects[roomObjects.Count - 2]);
        OpenDoors(newBossRoom, x, y);


    }

    //방생성 함수
    private bool TryGenerateRoom(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;

        if (0 <= x && x < gridSizeX && 0 <= y && y < gridSizeY)
        {
            if (maxRooms <= roomCount)
                return false;
            if (0.5f < Random.value && roomIndex != Vector2Int.zero)
                return false;
            if (1f < CountAdjacentRooms(roomIndex))
                return false;
            if (roomIndex == new Vector2Int(gridSizeX / 2, gridSizeY / 2))
            {
                return false;
            }

            roomQueue.Enqueue(roomIndex);
            roomGrid[x, y] = 1;
            roomCount++;


            var newRoom = Instantiate(roomMob, GetPositionFrontGridIndex(roomIndex), Quaternion.identity);
            newRoom.transform.SetParent(roomParnet.transform);
            newRoom.GetComponent<Room>().RoomIndex = roomIndex;
            newRoom.name = $"{roomMob.name}";
            roomObjects.Add(newRoom);


            rooms.Add(newRoom.GetComponent<Room>()); //룸 Index 등록   

            OpenDoors(newRoom, x, y);




            //countMax = roomCount;

            return true;
        }
        else
        {

            return false;
        }
    }

    //방 생성 중 잘 못 되었을때 초기화 후 다시 생성하는 함수
    public void RegenerateRoom()
    {
        roomObjects.ForEach(Destroy);
        roomObjects.Clear();
        roomGrid = new int[gridSizeX, gridSizeY];
        roomQueue.Clear();
        roomCount = 0;
        isGeneration = false;
        rooms.Clear();
        rooms = new List<Room>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
        StartRoomGenerationFronRoom(initialRoomIndex);
    }


    // GameObject 방의 붙은면을 찾아 해당 방의 Door를 여는 함수
    public void OpenDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();
        //newRoomScript.adjacentRoomName = "";
        //붙은 면
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        if (0 < x && roomGrid[x - 1, y] != 0 && leftRoomScript != null)
        {

            newRoomScript.adjacentRoomName = leftRoomScript.name;
            leftRoomScript.adjacentRoomName = newRoomScript.name;


            newRoomScript.OpenDoor(Vector2Int.left);
            leftRoomScript.OpenDoor(Vector2Int.right);


        }
        //오른쪽
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0 && rightRoomScript != null)
        {


            newRoomScript.adjacentRoomName = rightRoomScript.name;
            rightRoomScript.adjacentRoomName = newRoomScript.name;


            newRoomScript.OpenDoor(Vector2Int.right);
            rightRoomScript.OpenDoor(Vector2Int.left);

        }
        //위쪽
        if ((y < gridSizeY - 1 && roomGrid[x, y + 1] != 0 && topRoomScript != null))
        {


            topRoomScript.adjacentRoomName = newRoomScript.name;
            newRoomScript.adjacentRoomName = topRoomScript.name;


            newRoomScript.OpenDoor(Vector2Int.up);
            topRoomScript.OpenDoor(Vector2Int.down);

        }
        //아랫쪽
        if ((0 < y && roomGrid[x, y - 1] != 0 && bottomRoomScript != null))
        {
            if (0 == newRoomScript.mobCount)
            {

                newRoomScript.adjacentRoomName = bottomRoomScript.name;
                bottomRoomScript.adjacentRoomName = newRoomScript.name;

            }
            newRoomScript.OpenDoor(Vector2Int.down);
            bottomRoomScript.OpenDoor(Vector2Int.up);
        }

    }
    public void CloseDoors(GameObject room, int x, int y)
    {
        Room newRoomScript = room.GetComponent<Room>();

        //붙은 면
        Room leftRoomScript = GetRoomScriptAt(new Vector2Int(x - 1, y));
        Room rightRoomScript = GetRoomScriptAt(new Vector2Int(x + 1, y));
        Room topRoomScript = GetRoomScriptAt(new Vector2Int(x, y + 1));
        Room bottomRoomScript = GetRoomScriptAt(new Vector2Int(x, y - 1));

        if (0 < x && roomGrid[x - 1, y] != 0 && leftRoomScript != null)
        {

            newRoomScript.adjacentRoomName = leftRoomScript.name;
            leftRoomScript.adjacentRoomName = newRoomScript.name;
            newRoomScript.CloseDoor(Vector2Int.left);
            leftRoomScript.CloseDoor(Vector2Int.right);




        }
        //오른쪽
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0 && rightRoomScript != null)
        {
            newRoomScript.adjacentRoomName = rightRoomScript.name;
            rightRoomScript.adjacentRoomName = newRoomScript.name;
            newRoomScript.CloseDoor(Vector2Int.right);
            rightRoomScript.CloseDoor(Vector2Int.left);


        }
        //위쪽
        if ((y < gridSizeY - 1 && roomGrid[x, y + 1] != 0 && topRoomScript != null))
        {
            topRoomScript.adjacentRoomName = newRoomScript.name;
            newRoomScript.adjacentRoomName = topRoomScript.name;
            newRoomScript.CloseDoor(Vector2Int.up);
            topRoomScript.CloseDoor(Vector2Int.down);


        }
        //아랫쪽
        if ((0 < y && roomGrid[x, y - 1] != 0 && bottomRoomScript != null))
        {
            newRoomScript.adjacentRoomName = bottomRoomScript.name;
            bottomRoomScript.adjacentRoomName = newRoomScript.name;
            newRoomScript.CloseDoor(Vector2Int.down);
            bottomRoomScript.CloseDoor(Vector2Int.up);

        }

    }



    Room GetRoomScriptAt(Vector2 index)
    {
        GameObject roomObject = roomObjects.Find(r => r.GetComponent<Room>().RoomIndex == index);
        if (roomObject != null)
            return roomObject != null ? roomObject.GetComponent<Room>() : null;
        else
        {
            return null;
        }
    }

    private int CountAdjacentRooms(Vector2Int roomIndex)
    {
        int x = roomIndex.x;
        int y = roomIndex.y;
        int count = 0;

        if (0 < x && roomGrid[x - 1, y] != 0) count++;   //왼쪽 붙은 면
        if (x < gridSizeX - 1 && roomGrid[x + 1, y] != 0) count++;    //오른쪽 붙은 면
        if (0 < y && roomGrid[x, y - 1] != 0) count++;   //아랫쪽 붙dms 면
        if (y < gridSizeY - 1 && roomGrid[x, y + 1] != 0) count++;   //윗쪽 붙은 면

        return count;
    }

    //Gird 위치 잡아주는 함수
    private Vector3 GetPositionFrontGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX / 2),
        roomHeight * (gridY - gridSizeY / 2));
    }

    //기즈모 가시적으로 보이게 하는 함수
    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 position = GetPositionFrontGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }



    //Door Trigger시 이동 함수들
    public IEnumerator LeftDoorTouch()
    {
        isRoomMoveCheck = true;
        boxCollider2D.enabled = false;
        playerController.isaacSprite.color = new Color(1f, 1f, 1f, 0f);
        player.transform.GetChild(0).gameObject.SetActive(false); ;
        Vector3 rightDistance = roomParnet.transform.position + new Vector3(20f, 0f, 0f);
        while (roomParnet.transform.position != rightDistance)
        {
            roomParnet.transform.position = Vector3.MoveTowards(roomParnet.transform.position, rightDistance, Time.deltaTime * 75f);
            yield return null;
        }
        player.transform.position = new Vector3(7.1f, player.transform.position.y, player.transform.position.z);
        playerController.isaacSprite.color = new Color(1f, 1f, 1f, 1f);
        player.transform.GetChild(0).gameObject.SetActive(true); ;
        boxCollider2D.enabled = true;
        isRoomMoveCheck = false;
    }

    public IEnumerator RightDoorTouch()
    {
        isRoomMoveCheck = true;
        boxCollider2D.enabled = false;
        playerController.isaacSprite.color = new Color(1f, 1f, 1f, 0f);
        player.transform.GetChild(0).gameObject.SetActive(false); ;

        Vector3 rightDistance = roomParnet.transform.position + new Vector3(-20f, 0f, 0f);
        while (roomParnet.transform.position != rightDistance)
        {
            roomParnet.transform.position = Vector3.MoveTowards(roomParnet.transform.position, rightDistance, Time.deltaTime * 75f);
            yield return null;
        }
        player.transform.position = new Vector3(-7.1f, player.transform.position.y, player.transform.position.z);
        playerController.isaacSprite.color = new Color(1f, 1f, 1f, 1f);
        player.transform.GetChild(0).gameObject.SetActive(true); ;
        boxCollider2D.enabled = true;
        isRoomMoveCheck = false;
    }

    public IEnumerator UpDoorTouch()
    {
        isRoomMoveCheck = true;
        boxCollider2D.enabled = false;
        playerController.isaacSprite.color = new Color(1f, 1f, 1f, 0f);
        player.transform.GetChild(0).gameObject.SetActive(false); ;

        Vector3 rightDistance = roomParnet.transform.position + new Vector3(0f, -12f, 0f);
        while (roomParnet.transform.position != rightDistance)
        {
            roomParnet.transform.position = Vector3.MoveTowards(roomParnet.transform.position, rightDistance, Time.deltaTime * 50f);
            yield return null;
        }
        player.transform.position = new Vector3(player.transform.position.x, -3, player.transform.position.z);
        playerController.isaacSprite.color = new Color(1f, 1f, 1f, 1f);
        player.transform.GetChild(0).gameObject.SetActive(true);
        boxCollider2D.enabled = true;
        isRoomMoveCheck = false;
    }

    public IEnumerator DownDoorTouch()
    {
        isRoomMoveCheck = true;
        boxCollider2D.enabled = false;
        playerController.isaacSprite.color = new Color(1f, 1f, 1f, 0f);
        player.transform.GetChild(0).gameObject.SetActive(false); ;

        Vector3 rightDistance = roomParnet.transform.position + new Vector3(0f, 12f, 0f);
        while (roomParnet.transform.position != rightDistance)
        {
            roomParnet.transform.position = Vector3.MoveTowards(roomParnet.transform.position, rightDistance, Time.deltaTime * 50f);
            yield return null;
        }
        player.transform.position = new Vector3(player.transform.position.x, 3, player.transform.position.z);
        playerController.isaacSprite.color = new Color(1f, 1f, 1f, 1f);
        player.transform.GetChild(0).gameObject.SetActive(true); ;
        boxCollider2D.enabled = true;
        isRoomMoveCheck = false;
    }

    //스타트룸과 가장 먼방을 찾는거
    //

    public void PlayerEnterRoom(Room room)
    {
        if (room != null)
        {

            if (0 < room.mobCount)
            {

                CloseDoors(room.gameObject, room.RoomIndex.x, room.RoomIndex.y);
            }
            else
            {
                OpenDoors(room.gameObject, room.RoomIndex.x, room.RoomIndex.y);
            }
        }
        else Debug.Log("room 이 null");
    }


    //public void ClearBossRoom(bool isbool)
    //{

    //    roomBoss.transform.GetChild(5).gameObject.SetActive(isbool);
    //    roomBoss.transform.GetChild(6).gameObject.SetActive(isbool);
    //    roomBoss.transform.GetChild(7).gameObject.SetActive(isbool);
    //    Debug.Log("Child 5 활성화 상태: " + roomBoss.transform.GetChild(5).gameObject.activeSelf);
    //    Debug.Log("Child 6 활성화 상태: " + roomBoss.transform.GetChild(6).gameObject.activeSelf);
    //}
    //public void ClearBossRoom(bool isbool)
    //{
    //    roomBoss.transform.GetChild(5).gameObject.SetActive(isbool);
    //    roomBoss.transform.GetChild(6).gameObject.SetActive(isbool);
    //}

}

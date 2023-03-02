using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class IRoomInfo
{

    public string name;
    public int X;
    public int Y;
}
public class IRoomController : MonoBehaviour
{
    public static IRoomController instance;
    public GameObject tileGrid;
    string currentWorldName = "Basement";

    IRoomInfo currentLoadRoomData;

    Queue<IRoomInfo> loadRoomQueue = new Queue<IRoomInfo>();

    public List<IRoom> loadedRooms = new List<IRoom>();

    public GameObject roomPrefabs = default;

    bool isLoadingRoom = false;
    bool spawnedBoosRoom = false;
    bool updatedRooms = false;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        UpdateRoomQueue();
        // LoadRoom("Start", 0, 0);
        // LoadRoom("Empty", 1, 0);
        // LoadRoom("Empty", -1, 0);
        // LoadRoom("Empty", 0, 1);
        // LoadRoom("Empty", 0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        if (loadRoomQueue.Count == 0)
        {
            if(!spawnedBoosRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if(spawnedBoosRoom && !updatedRooms)
            {
                foreach(IRoom room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        Debug.Log(currentLoadRoomData);
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));

    }
    IEnumerator SpawnBossRoom()
    {
        spawnedBoosRoom =true;
        yield return new WaitForSeconds(0.5f);
       if(loadRoomQueue.Count == 0)
       {
        IRoom bossRoom = loadedRooms[loadedRooms.Count -1];
        IRoom tempRoom = new IRoom(bossRoom.X, bossRoom.Y);
        Destroy(bossRoom.gameObject);
        var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && 
        r.Y == tempRoom.Y);
        loadedRooms.Remove(roomToRemove);
        LoadRoom("End", tempRoom.X, tempRoom.Y);
       }
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }   
        IRoomInfo newRoomData = new IRoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
        
    }
    
    

    IEnumerator LoadRoomRoutine(IRoomInfo info)
    {
        string roomName = currentWorldName + info.name;
        GameObject loadRoom = Instantiate(roomPrefabs); 
        loadRoom.transform.SetParent(tileGrid.transform,false);
        IRoom loadRoomScript =loadRoom.GetComponent<IRoom>();
        //SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        yield return null;
        // while (loadRoom.isDone == false)
        // {
        //     yield return null;
        // }
    }

    public void RegisterRoom(IRoom room)
    {        //Debug.Log(currentLoadRoomData);
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y) )
        {
            room.transform.position = new Vector3
            (
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                0
            );
            
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " "
             + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;
            Debug.Log(room.transform.position);
            loadedRooms.Add(room);           
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public IRoom FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }
}
// Start is called before the first frame update


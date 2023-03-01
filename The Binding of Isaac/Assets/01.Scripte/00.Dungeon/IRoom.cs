using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRoom : MonoBehaviour
{

    public int Width;
    public int Height;
    public int X;
    public int Y;

    private bool updatedDoors = false;
    public IRoom(int x, int y)
    {
        X = x;
        Y = y;
    }

    public IDoor leftDoor;
    public IDoor rightDoor;
    public IDoor topDoor;
    public IDoor bottomDoor;

    public List<IDoor> doors = new List<IDoor>();
    // Start is called before the first frame update
    void Start()
    {
        if (IRoomController.instance == null)
        {
            Debug.Log(IRoomController.instance);
            return;
        }
        // if(IRoomController.instance != null)
        // {
        // }

        IDoor[] ds = GetComponentsInChildren<IDoor>();
        foreach (IDoor d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case IDoor.DoorType.right:
                    rightDoor = d;
                    break;
                case IDoor.DoorType.left:
                    leftDoor = d;
                    break;
                case IDoor.DoorType.top:
                    topDoor = d;
                    break;
                case IDoor.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }
        IRoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if(name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (IDoor door in doors)
        {
            switch (door.doorType)
            {
                case IDoor.DoorType.right:
                if(GetRight() == null)
                door.gameObject.SetActive(false);                    
                    break;
                case IDoor.DoorType.left:
                    if(GetLeft() == null)
                door.gameObject.SetActive(false);
                    break;
                case IDoor.DoorType.top:
                    if(GetTop() == null)
                door.gameObject.SetActive(false);
                    break;
                case IDoor.DoorType.bottom:
                   if(GetBottom() == null)
                door.gameObject.SetActive(false);
                    break;
            }
        }
    }


    public IRoom GetRight()
    {
        if(IRoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return IRoomController.instance.FindRoom((X + 1), Y);
        }
        return null;
    }
    public IRoom GetLeft()
    {
        if(IRoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return IRoomController.instance.FindRoom((X - 1), Y);
        }
        return null;
    }
    public IRoom GetTop()
    {
        if(IRoomController.instance.DoesRoomExist(X , Y + 1))
        {
            return IRoomController.instance.FindRoom((X ), Y + 1);
        }
        return null;
    }
    public IRoom GetBottom()
    {
        if(IRoomController.instance.DoesRoomExist(X , Y - 1))
        {
            return IRoomController.instance.FindRoom(X , Y - 1);
        }
        return null;
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }
    // Update is called once per frame
    
}

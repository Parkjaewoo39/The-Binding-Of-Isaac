using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    RoomManager roomManager;
    public SpriteRenderer spriteRenderer;
    [SerializeField] GameObject roomParnet ;
    public float moveSpeed = 5f;
    public bool isOpenDoorLeftRight = false;
    public bool isOpenDoorUpDown = false;
    public bool isOpenDoorUp = false;
    private Rigidbody2D rigid;
    public BoxCollider2D playerBoxCollider;

    void Start()
    {
        playerBoxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        GameObject roommmm = GameObject.Find("RoomManager");
        if(roommmm != null)
        {
            roomManager = roommmm.GetComponent<RoomManager>();
        }
    }
    
    void Update()
    {
       float moveInputLR = Input.GetAxis("Horizontal");
       float moveInputUD = Input.GetAxis("Vertical");

       Vector2 moveDirection = new Vector2(moveInputLR,moveInputUD);

       rigid.velocity = moveDirection * moveSpeed;
        
    }

    //상하좌우 Door충돌시 방 이동 코루틴 호출
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "RightDoor" || other.gameObject.name == "BossRightDoor" || other.gameObject.name == "GoldRightDoor")
        {  
            if(!roomManager.isRoomMoveCheck)
            {
                 StartCoroutine(roomManager.RightDoorTouch());
            } 
        }
        if(other.gameObject.name == "LeftDoor" || other.gameObject.name == "BossLeftDoor" || other.gameObject.name == "GoldLeftDoor")
        {
            if(!roomManager.isRoomMoveCheck)
            {
                StartCoroutine(roomManager.LeftDoorTouch());  
            }
        }
        if(other.gameObject.name == "TopDoor" || other.gameObject.name == "BossTopDoor" || other.gameObject.name == "GoldTopDoor")
        { 
            if(!roomManager.isRoomMoveCheck)
            {
                StartCoroutine(roomManager.UpDoorTouch());           
            }
        }
        if(other.gameObject.name == "BottomDoor" || other.gameObject.name == "BossBottomDoor" || other.gameObject.name == "GoldBottomDoor")
        {
            if(!roomManager.isRoomMoveCheck)
            {
                StartCoroutine(roomManager.DownDoorTouch());
               
            }
        }
    }    
}

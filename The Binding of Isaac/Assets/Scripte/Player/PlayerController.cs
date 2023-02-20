using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Singleton<PlayerController>
{
    public static PlayerController player;

    public static float isaacHeartHp;

    public static float isaacHeartMaxHp;

    public static float isaacDamage ;

    public static float isaacTearSpeed = 0.5f;

    public static float isaacRange ;

    public static float isaacmaxReload;        //�ִ� ����

    public static float isaacReload;      //���� ����

    private float isaacMoveSpeed = 0.5f;   //�⺻ �ӵ�

    public GameObject IsaacBody = default;
    public GameObject IsaacHead = default;

    private Rigidbody2D IsaacRigid;

    private Animator IsaacImage = default;
    private GameObject tearPoolObj;
    //private SpriteRenderer iRenderer;

    public bool isGetKeyCheck = false;

    Vector3 lookDirection;
    
    //!{ Start()
    void Start()
    {
        if (player == null)
        {
            player = this;
        }

        IsaacRigid = gameObject.GetComponent<Rigidbody2D>();
        IsaacImage = gameObject.GetComponent<Animator>();
        //iRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        isGetKeyCheck = false;
        tearPoolObj = transform.parent.gameObject.FindChildObj("TearPool");

        playerStat();
    }   //Start()
  


    //!{playerStat()
    private void playerStat()
    {
        isaacHeartHp = 6f;  //ü��

        isaacHeartMaxHp = 6f;   //�ִ�ü��
            
        isaacDamage = 3.5f; //������

        isaacTearSpeed = 1f;    //���� �ӵ�

        isaacRange = 1f;    //��Ÿ�

        isaacmaxReload = 1f;       //�ִ� ����

        isaacReload = 2f;      //���� ����

        isaacMoveSpeed = 0.5f;   //�̵� �ӵ�
    }    //!}playerStat()

    

    //!{Update()
    void Update()
    {
        //������ �̹���
        if (!Input.anyKey)
        {
            IsaacImage.SetBool("Stop", true);
        }

        //{wasd �̵�

        //WŰ �Է½� �̵� �� �ִϸ��̼�
        if (Input.GetKey(KeyCode.W))
        {
            IsaacImage.SetBool("Up", true);
            IsaacRigid.AddForce(Vector2.up * isaacMoveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            IsaacImage.SetBool("Up", false);
        }

        //SŰ �Է½� �̵� �� �ִϸ��̼�
        if (Input.GetKey(KeyCode.S))
        {
            IsaacImage.SetBool("Down", true);
            IsaacRigid.AddForce(Vector2.down * isaacMoveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            IsaacImage.SetBool("Down", false);
        }

        //DŰ �Է½� �̵� �� �ִϸ��̼�
        if (Input.GetKey(KeyCode.D))
        {
            IsaacImage.SetBool("Right", true);
            IsaacRigid.AddForce(Vector2.right * isaacMoveSpeed, ForceMode2D.Impulse);

            transform.localScale = new Vector2(2.2f, 2f);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            IsaacImage.SetBool("Right", false);
        }

        //AŰ �Է½� �̵� �� �ִϸ��̼�
        if (Input.GetKey(KeyCode.A))
        {
            IsaacImage.SetBool("Right", true);
            IsaacRigid.AddForce(Vector2.left * isaacMoveSpeed, ForceMode2D.Impulse);

            //iRenderer.flipX = true;
            transform.localScale = new Vector2(-2.2f, 2f);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            IsaacImage.SetBool("Right", false);
        }

        //}wasd �̵�

        //{ArrowŰ ����



        Vector2 tearPos = IsaacBody.transform.position;
              

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var tear = ObjectPool.GetObject();
            if (tear != null)
            {
                tear.transform.SetParent(tearPoolObj.transform);
                tear.transform.localScale = new Vector3(1f, 1f, 0f);

                tear.transform.position = tearPos;             
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var tear = ObjectPool.GetObject();
            if (tear != null)
            {
                tear.transform.SetParent(tearPoolObj.transform);
                tear.transform.localScale = new Vector3(1f, 1f, 0f);


                //var direction = new Vector2(-1.0f, 0f);
                tear.transform.position = tearPos; ;
                //tear.Shoot(direction);
            }
            else { };
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var tear = ObjectPool.GetObject();
            if (tear != null)
            {
                tear.transform.SetParent(tearPoolObj.transform);
                tear.transform.localScale = new Vector3(1f, 1f, 0f);

                // var direction = new Vector2(0f, 1.0f);
                tear.transform.position = tearPos;
                //tear.Shoot(direction);
            }
            else { };
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var tear = ObjectPool.GetObject();
            if (tear != null)
            {
                tear.transform.SetParent(tearPoolObj.transform);
                tear.transform.localScale = new Vector3(1f, 1f, 0f);

                // var direction = new Vector2(0f, -1.0f);
                tear.transform.position = tearPos;
                // tear.Shoot(direction);
            }
            else { };
        }

    }       //Update()
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Door")
        {
            FadeInOut.Instance.setFade(true, 1.35f);

            GameObject nextRoom = other.gameObject.transform.parent.GetComponent<Door>().nextRoom;
            Door nextDoor = other.gameObject.transform.parent.GetComponent<Door>().SideDoor;

            // ���� ������ �ľ� �� ĳ���� ��ġ ����
            Vector3 currPos = new Vector3(nextDoor.transform.position.x, 0.5f, nextDoor.transform.position.z) + (nextDoor.transform.localRotation * (Vector3.forward * 3));

            Player.Instance.transform.position = currPos;

            for (int i = 0; i < RoomController.Instance.loadedRooms.Count; i++)
            {
                if (nextRoom.GetComponent<Room>().parent_Position == RoomController.Instance.loadedRooms[i].parent_Position)
                {
                    RoomController.Instance.loadedRooms[i].childRooms.gameObject.SetActive(true);
                }
                else
                {
                    RoomController.Instance.loadedRooms[i].childRooms.gameObject.SetActive(false);
                }
            }

            FadeInOut.Instance.setFade(false, 0.15f);
        }
    }


}

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
    public static float isaacDamage;
    public static float isaacTearSpeed;
    public static float isaacRange;
    public static float isaacTime;
    public static float isaacMaxReload;        //??? ????                                               //
    public static float isaacReload;      //???? ????
    public static float isaacTearHigh;
    private static float isaacMoveSpeed = 0.5f;   //?? ???



    public GameObject Tear;
    public GameObject IsaacBody = default;
    public GameObject IsaacHead = default;

    private GameObject tearPoolObj;
    private Rigidbody2D IsaacRigid;
    private Animator IsaacImage = default;
    //private SpriteRenderer iRenderer;

    public bool isGetKeyCheck = false;

    Vector3 lookDirection;
    Vector2 direction;

    //!{playerStat()
    public void playerStat()
    {
        isaacHeartHp = 6f;  //???

        isaacHeartMaxHp = 6f;   //??????

        isaacDamage = 3.5f; //??????

        isaacTearSpeed = 5f;    //???? ???

        isaacRange = 1f;    //????

        // isaacMaxReload = 1f;       //??? ????

        // isaacReload = 2f;      //???? ????

        isaacTearHigh = 3f;

        isaacTime = 0.5f;


        isaacMoveSpeed = 10f;   //??? ???
    }    //!}playerStat()

    //!{ Start()
    void Start()
    {
        if (player == null)
        {
            player = this;
        }

        IsaacRigid = GetComponent<Rigidbody2D>();
        IsaacImage = gameObject.GetComponent<Animator>();
        //iRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        isGetKeyCheck = false;
        tearPoolObj = transform.parent.gameObject.FindChildObj("TearPool");

        playerStat();

        // PlayerPrefs.SetFloat("isaacTearSpeedVal", isaacTearSpeed);
    }   //Start()

    // //!{Shoot R&D
    // public void Shoot(Vector3 direction)
    // {
    //     Vector2 tearPos = IsaacBody.transform.position;

    //     var tear = ObjectPool.GetObject();
    //     if (tear != null)
    //     {
    //         tear.transform.SetParent(tearPoolObj.transform);
    //         tear.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    //         tear.transform.position = new Vector2(tearPos.x,tearPos.y+5f);
    //         //tear.transform.localRotation = Quaternion.Euler(0.0f, 0f, -90f);
    //         tear.transform.eulerAngles = direction * 1;
    //     }
    //     //this.direction = direction;        
    // }   //!}Shoot R&D

    Vector3 right = new Vector3(0, 0, -90f);
    Vector3 left = new Vector3(0, 0, 90f);
    Vector3 up = new Vector3(0, 0, 0f);
    Vector3 down = new Vector3(0, 0, -180f);
    void Shoot(Vector3 vec, float x, float y)
    {
        Vector2 tearPos = IsaacBody.transform.position;

        GameObject tear = Instantiate(Tear, transform.position, transform.rotation) as GameObject;
        tear.transform.Rotate(vec);
        tear.GetComponent<Rigidbody2D>().gravityScale = 0;
        tear.GetComponent<Rigidbody2D>().velocity =
         new Vector3(
             (x < 0) ? Mathf.Floor(x) * isaacTearSpeed : Mathf.Ceil(x) * isaacTearSpeed,

             (y < 0) ? Mathf.Floor(y) * isaacTearSpeed : Mathf.Ceil(y) * isaacTearSpeed,
             0
        );
    }

    //!{Update()
    void Update()
    {
        isaacMaxReload = PlayerManager.FireRate;
        isaacMoveSpeed = PlayerManager.MoveSpeed;


        //?????? ?????
        if (!Input.anyKey)
        {
            IsaacImage.SetBool("Stop", true);
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");
        IsaacRigid.velocity = new Vector3(
        horizontal * isaacMoveSpeed * 50, vertical * isaacMoveSpeed * 50, 0);

        if (Input.GetKey(KeyCode.UpArrow) && (shootHor != 0 || shootVer != 0) && Time.time > isaacReload + isaacMaxReload)
        {
            Shoot(up, shootHor, shootVer);
            isaacReload = Time.time;
        }
        if (Input.GetKey(KeyCode.DownArrow) && (shootHor != 0 || shootVer != 0) && Time.time > isaacReload + isaacMaxReload)
        {
            Shoot(down, shootHor, shootVer);
            isaacReload = Time.time;
        }
        if (Input.GetKey(KeyCode.RightArrow) && (shootHor != 0 || shootVer != 0) && Time.time > isaacReload + isaacMaxReload)
        {
            Shoot(right, shootHor, shootVer);
            isaacReload = Time.time;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && (shootHor != 0 || shootVer != 0) && Time.time > isaacReload + isaacMaxReload)
        {
            Shoot(left, shootHor, shootVer);
            isaacReload = Time.time;
        }



        //{wasd ???

        //W? ??��? ??? ?? ???????
        if (Input.GetKey(KeyCode.W))
        {
            IsaacImage.SetBool("Up", true);
            //IsaacRigid.AddForce(Vector2.up * isaacMoveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            IsaacImage.SetBool("Up", false);

        }

        //S? ??��? ??? ?? ???????
        if (Input.GetKey(KeyCode.S))
        {
            IsaacImage.SetBool("Down", true);
            //IsaacRigid.AddForce(Vector2.down * isaacMoveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            IsaacImage.SetBool("Down", false);
        }

        //D? ??��? ??? ?? ???????
        if (Input.GetKey(KeyCode.D))
        {
            IsaacImage.SetBool("Right", true);
            //IsaacRigid.AddForce(Vector2.right * isaacMoveSpeed, ForceMode2D.Impulse);

            transform.localScale = new Vector2(2.2f, 2f);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            IsaacImage.SetBool("Right", false);

        }

        //A? ??��? ??? ?? ???????
        if (Input.GetKey(KeyCode.A))
        {
            IsaacImage.SetBool("Right", true);
            // IsaacRigid.AddForce(Vector2.left * isaacMoveSpeed, ForceMode2D.Impulse);

            //Renderer.flipX = true;
             transform.localScale = new Vector2(-2.2f, 2f);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            IsaacImage.SetBool("Right", false);
        }

        //}wasd ???


        //{Arrow? ????
        // Vector2 tearPos = IsaacBody.transform.position;
        // if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     Shoot(right, shootHor, shootVer);
        // }

        // if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {

        //     Shoot(left, shootHor, shootVer);

        // }

        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {

        //     Shoot(up, shootHor, shootVer);

        // }
        // if (Input.GetKeyDown(KeyCode.DownArrow))
        // {

        //     Shoot(down, shootHor, shootVer);           

        // }


    }       //Update()
    


}

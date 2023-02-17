using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static float isaacDamage = 3.5f;
    public static float isaacTearSpeed = 0.1f;
    public static float isaacRange = 1f;
    public static float isaacmaxReload;        //최대 연사
    public static float isaacReload;      //현재 연사

    private float isaacMoveSpeed = 0.5f;   //기본 속도

    public GameObject IsaacBody = default;
    public GameObject IsaacHead = default;

    private Rigidbody2D IsaacRigid;

    private Animator IsaacImage = default;
    //private SpriteRenderer iRenderer;
    private GameObject tearPoolObj;
    public bool isGetKeyCheck = false;

    Vector3 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        IsaacRigid = gameObject.GetComponent<Rigidbody2D>();
        IsaacImage = gameObject.GetComponent<Animator>();
        //iRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        isGetKeyCheck = false;
        tearPoolObj = transform.parent.gameObject.FindChildObj("TearPool");
    }
         

    // Update is called once per frame
    void Update()
    {
        //정지시 이미지
        if (!Input.anyKey)
        {
            IsaacImage.SetBool("Stop", true);
        }

        //{wasd 이동

        //W키 입력시 이동 및 애니메이션
        if (Input.GetKey(KeyCode.W))
        {
            IsaacImage.SetBool("Up", true);
            IsaacRigid.AddForce(Vector2.up * isaacMoveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            IsaacImage.SetBool("Up", false);            
        }

        //S키 입력시 이동 및 애니메이션
        if (Input.GetKey(KeyCode.S))
        {
            IsaacImage.SetBool("Down", true);
            IsaacRigid.AddForce(Vector2.down * isaacMoveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            IsaacImage.SetBool("Down", false);
        }

        //D키 입력시 이동 및 애니메이션
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

        //A키 입력시 이동 및 애니메이션
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

        //}wasd 이동

        //{Arrow키 공격
        
        Vector2 tearPos = IsaacBody.transform.position;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
            var tear = ObjectPool.GetObject();
            if (tear != null)
            {
                
                tear.transform.SetParent(tearPoolObj.transform);
                tear.transform.localScale = new Vector3(1f, 1f, 0f);                

                var direction = new Vector2(1.0f, 0f);
                tear.transform.position = tearPos;
                tear.Shoot(direction);
            }
            else { };
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var tear = ObjectPool.GetObject();
            if (tear != null)
            {
                tear.transform.SetParent(tearPoolObj.transform);
                tear.transform.localScale = new Vector3(1f, 1f, 0f);


                var direction = new Vector2(-1.0f, 0f);
                tear.transform.position = tearPos;;
                tear.Shoot(direction);                
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

                var direction = new Vector2(0f, 1.0f);
                tear.transform.position = tearPos;
                tear.Shoot(direction);
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

                var direction = new Vector2(0f, -1.0f);
                tear.transform.position = tearPos;
                tear.Shoot(direction);
            }
            else { };
        }

    }


}

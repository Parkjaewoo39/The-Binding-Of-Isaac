using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speedMax = 0.05f;       

    public GameObject IsaacBody = default;
    public GameObject IsaacHead = default;

    private Rigidbody2D IsaacRigid;

    private Animator IsaacImage = default;
    private SpriteRenderer iRenderer;

    public bool isGetKeyCheck = false;

    Vector3 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        IsaacRigid = GetComponent<Rigidbody2D>();
        IsaacImage = GetComponent<Animator>();
        isGetKeyCheck = false;
        iRenderer = this.gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //정지시 이미지
        if (!Input.anyKey)
        {
            IsaacImage.SetBool("Stop", true);
        }

        //W키 입력시 이동 및 애니메이션
        if (Input.GetKey(KeyCode.W))
        {
            IsaacImage.SetBool("Up", true);
            IsaacRigid.AddForce(Vector2.up * speedMax, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            IsaacImage.SetBool("Up", false);
        }

        //W키 입력시 이동 및 애니메이션
        if (Input.GetKey(KeyCode.S))
        {
            IsaacImage.SetBool("Down", true);
            IsaacRigid.AddForce(Vector2.down * speedMax, ForceMode2D.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.S)) 
        {
            IsaacImage.SetBool("Down", false);
        }

        //D키 입력시 이동 및 애니메이션
        if (Input.GetKey(KeyCode.D))
        {
           IsaacImage.SetBool("Right", true);
            IsaacRigid.AddForce(Vector2.right * speedMax, ForceMode2D.Impulse);

            iRenderer.flipX = false;
        }
        else if (Input.GetKeyUp(KeyCode.D)) 
        {
            IsaacImage.SetBool("Right", false);
        }

        //A키 입력시 이동 및 애니메이션
        if (Input.GetKey(KeyCode.A))
        {
            IsaacImage.SetBool("Right", true);            
            IsaacRigid.AddForce(Vector2.left * speedMax, ForceMode2D.Impulse);

            iRenderer.flipX = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            IsaacImage.SetBool("Right", false);
        }




        //if (Input.GetKey(KeyCode.UpArrow)) 
        //{
        //    IsaacImage.SetBool("UpAT", true);   
        //}
        //else if (Input.GetKeyUp(KeyCode.UpArrow))
        //{
        //    IsaacImage.SetBool("UpAT", false);
        //}


        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    IsaacImage.SetBool("DownAT", true);
        //}
        //else if (Input.GetKeyUp(KeyCode.DownArrow))
        //{
        //    IsaacImage.SetBool("DownAT", false);
        //}


        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    IsaacImage.SetBool("LeftAT", true);
        //}
        //else if (Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    IsaacImage.SetBool("LeftAT", false);
        //}


        
        

    }


}

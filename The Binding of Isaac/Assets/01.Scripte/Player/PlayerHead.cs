using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
<<<<<<< HEAD
    private Animator IsaacImage;
    // Start is called before the first frame update
=======
    public Animator IsaacImage;
    private bool isAttackCheck = false;

>>>>>>> Develop
    void Start()
    {
        IsaacImage = GetComponent<Animator>();
    }

<<<<<<< HEAD
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IsaacImage.SetBool("RightAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            IsaacImage.SetBool("RightAT", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            IsaacImage.SetBool("LeftAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            IsaacImage.SetBool("LeftAT", false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {            
            IsaacImage.SetBool("UpAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            IsaacImage.SetBool("UpAT", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            IsaacImage.SetBool("DownAT", true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            IsaacImage.SetBool("DownAT", false);
        }
    }
   
}
=======

    void Update()
    {

        HandleMovementInput();
        HandleAttackInput();

    }
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.D))
        {
            IsaacImage.SetBool("Right", true);
        }
        else
        {
            IsaacImage.SetBool("Right", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            IsaacImage.SetBool("Left", true);
        }
        else
        {
            IsaacImage.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            IsaacImage.SetBool("Up", true);
        }
        else
        {
            IsaacImage.SetBool("Up", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            IsaacImage.SetBool("Down", true);
        }
        else
        {
            IsaacImage.SetBool("Down", false);
        }
    }

    void HandleAttackInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isAttackCheck = true;
            IsaacImage.SetBool("RightAt", true);
        }
        else
        {
            IsaacImage.SetBool("RightAt", false);
            isAttackCheck = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isAttackCheck = true;
            IsaacImage.SetBool("LeftAt", true);
        }
        else
        {
            IsaacImage.SetBool("LeftAt", false);
            isAttackCheck = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            isAttackCheck = true;
            IsaacImage.SetBool("UpAt", true);
        }
        else
        {
            IsaacImage.SetBool("UpAt", false);
            isAttackCheck = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            isAttackCheck = true;
            IsaacImage.SetBool("DownAt", true);
        }
        else
        {
            IsaacImage.SetBool("DownAt", false);
            isAttackCheck = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 트리거 충돌 시 플립 상태를 로그로 출력
        Debug.Log("Trigger entered with: " + other.gameObject.name);
        Debug.Log("Current flip state: " + GetComponent<SpriteRenderer>().flipX);
    }
}



>>>>>>> Develop

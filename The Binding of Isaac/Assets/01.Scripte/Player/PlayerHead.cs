using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    public Animator IsaacImage;
    private bool isAttackCheck = false;

    void Start()
    {
        IsaacImage = GetComponent<Animator>();
    }


    void Update()
    {



        if (Input.GetKey(KeyCode.RightArrow))
        {
            isAttackCheck = true;
            IsaacImage.SetBool("RightAt", true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            IsaacImage.SetBool("RightAt", false);
            isAttackCheck = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isAttackCheck = true;
            IsaacImage.SetBool("LeftAt", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            IsaacImage.SetBool("LeftAt", false);
            isAttackCheck = false;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isAttackCheck = true;
            IsaacImage.SetBool("UpAt", true);
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            IsaacImage.SetBool("UpAt", false);
            isAttackCheck = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isAttackCheck = true;
            IsaacImage.SetBool("DownAt", true);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            IsaacImage.SetBool("DownAt", false);
            isAttackCheck = false;
        }
        

        if (Input.GetKeyDown(KeyCode.D))
        {
            IsaacImage.SetBool("Right", true);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            IsaacImage.SetBool("Right", false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            IsaacImage.SetBool("Left", true);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            IsaacImage.SetBool("Left", false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            IsaacImage.SetBool("Up", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            IsaacImage.SetBool("Up", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            IsaacImage.SetBool("Down", true);
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            IsaacImage.SetBool("Down", false);
        }












    }
}



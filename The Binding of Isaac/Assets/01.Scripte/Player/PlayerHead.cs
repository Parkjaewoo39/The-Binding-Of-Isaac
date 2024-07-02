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
        // Ʈ���� �浹 �� �ø� ���¸� �α׷� ���
        Debug.Log("Trigger entered with: " + other.gameObject.name);
        Debug.Log("Current flip state: " + GetComponent<SpriteRenderer>().flipX);
    }
}




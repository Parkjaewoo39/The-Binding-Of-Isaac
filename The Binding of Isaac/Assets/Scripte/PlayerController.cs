using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speedX = 0.01f;
    private float speedY = 0.01f;
    public float maxSpeed = 2f ;

    public GameObject IsaacBody = default;
    public GameObject IsaacHead = default;

    private Rigidbody2D IsaacRigid;

    public Image IsaacImage = default;

    public bool isKeyUpCheck = false;

    Vector3 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        IsaacRigid = GetComponent<Rigidbody2D>();
        IsaacImage = GetComponent<Image>();
        isKeyUpCheck = false;
    }

    // Update is called once per frame
    void Update()
    {

        //IsaacRigid.AddForce(Vector2.zero, ForceMode2D.Impulse);


        if (Input.GetKey(KeyCode.W))
        {
            IsaacRigid.AddForce(Vector2.up * speedY, ForceMode2D.Impulse);

            if (IsaacRigid.velocity.y > maxSpeed)
            {
                IsaacRigid.velocity = new Vector2(maxSpeed, IsaacRigid.velocity.x);
            }
            else if (IsaacRigid.velocity.y < maxSpeed * (-1)) 
            {
                IsaacRigid.velocity = new Vector2(maxSpeed * (-1), IsaacRigid.velocity.x);
            }
            

        }
        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log($"{Input.GetKey(KeyCode.s)}");
            IsaacRigid.AddForce(Vector2.down * speedY, ForceMode2D.Impulse);

        }
        if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log($"{Input.GetKey(KeyCode.s)}");
            IsaacRigid.AddForce(Vector2.left * speedX, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log($"{Input.GetKey(KeyCode.s)}");
            IsaacRigid.AddForce(Vector2.right * speedX, ForceMode2D.Impulse);

        }

    }

    
}

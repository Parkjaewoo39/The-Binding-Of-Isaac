using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{

    private Rigidbody2D tearRigid;

    private Animator isaacTear = default;

    private Collider2D isaacCollider2D = default;

    private float tearSpeed = default;

    private float isaacNowTearSpeed = default;

    private bool isSomethingCheck = false;


    private void Awake()
    {
        GameObject.Find("Body").GetComponent<PlayerController>().playerStat();
    }
    void Start()
    {
        // PlayerController.isaacTearSpeed += 0f;

        isaacTear = GetComponent<Animator>();
        tearRigid = GetComponent<Rigidbody2D>();

        UpdateTearSpeed();
        isSomethingCheck = false;
    }

    void Update()
    {    
        if (!isSomethingCheck )
        {            
            tearRigid.velocity = transform.up * isaacNowTearSpeed * 20;
        }
    }

    //!{Shoot R&D
    
    public void DestroyTears()
    {
        ObjectPool.ReturnObject(this);
        CancelInvoke();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        isSomethingCheck= true;
        tearRigid.velocity = Vector3.zero;

        if (other.tag == "Wall")
        {
            tearRigid.velocity = Vector2.zero;
            isaacTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);
            
            // DestroyTears();
            //StartCoroutine("TearDestroy");
        }
        if (other.tag == "Enemy")
        {
            tearRigid.velocity = Vector2.zero;
            isaacTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);
            Debug.Log("??");
            // DestroyTears();
            //PlayerController.tearDamage
        }
        if (other.tag == "TearShadow")
        {
            isaacTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);

            DestroyTears();
        }

    }   

    public void UpdateTearSpeed()
    {
       
        isaacNowTearSpeed += PlayerController.isaacTearSpeed;
    }
    
}

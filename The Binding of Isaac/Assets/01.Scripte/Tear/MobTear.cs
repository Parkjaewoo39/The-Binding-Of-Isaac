using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTear : MonoBehaviour
{

    private Rigidbody2D mobTearRigid;

    private Animator mobTear = default;

    private Collider2D mobCollider2D = default;

    private float mobTearSpeed = 2.5f;

    private float mobFastTearSpeed = 5f;

    private bool isSomethingCheck = false;

       
    void Start()
    {
        // PlayerController.isaacTearSpeed += 0f;
        mobTear = GetComponent<Animator>();
        mobTearRigid = GetComponent<Rigidbody2D>();
        
        isSomethingCheck = false;
    }

    void Update()
    {
        if (!isSomethingCheck)
        {
            mobTearRigid.velocity = transform.up * mobTearSpeed * 20;            
        }
        
    }

    //!{Shoot R&D
    
    public void DestroyTears()
    {
        MonsterObjectPool.ReturnObject(this);
        CancelInvoke();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        isSomethingCheck= true;
        mobTearRigid.velocity = Vector3.zero;

        if (other.tag == "Wall")
        {
            mobTearRigid.velocity = Vector2.zero;
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);
            
            // DestroyTears();
            //StartCoroutine("TearDestroy");
        }
        if (other.tag == "Player")
        {
            mobTearRigid.velocity = Vector2.zero;
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);   
            // DestroyTears();
            //PlayerController.tearDamage
        }
        if (other.tag == "TearShadow")
        {
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);

            DestroyTears();
        }

    }   

    
    
}

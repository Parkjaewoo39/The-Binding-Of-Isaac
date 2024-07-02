using UnityEngine;
<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> Develop
using UnityEngine.UI;

public class MobTear : MonoBehaviour
{

    private Rigidbody2D mobTearRigid;

    private Animator mobTear = default;
<<<<<<< HEAD

    //private Collider2D mobCollider2D = default;
=======
    private CircleCollider2D circleCollider;
    
>>>>>>> Develop

    private float mobTearSpeed = 2.5f;

    private float mobFastTearSpeed = 5f;

    private bool isSomethingCheck = false;

       
    void Start()
    {
<<<<<<< HEAD
        // PlayerController.isaacTearSpeed += 0f;
=======
        circleCollider = GetComponent<CircleCollider2D>();
>>>>>>> Develop
        mobTear = GetComponent<Animator>();
        mobTearRigid = GetComponent<Rigidbody2D>();
        
        isSomethingCheck = false;        
    }

    void Update()
    {
        if (!isSomethingCheck)
        {
<<<<<<< HEAD
            mobTearRigid.velocity = transform.up * mobTearSpeed * 20;            
=======
            mobTearRigid.velocity = transform.up * mobTearSpeed ;
            StartCoroutine(DeathDelay());
>>>>>>> Develop
        }        
    }

    //!{Shoot R&D
    
    public void DestroyTears()
    {
        MonsterObjectPool.ReturnObject(this);
        CancelInvoke();
    }

<<<<<<< HEAD
    public void OnTriggerEnter2D(Collider2D other)
=======
    public void OnCollistionEnter2D(Collider2D other)
>>>>>>> Develop
    {
        isSomethingCheck= true;
        mobTearRigid.velocity = Vector3.zero;

<<<<<<< HEAD
        if (other.tag == "Wall")
        {
=======
        if (other.CompareTag("Wall") || other.CompareTag("Isaac") || other.CompareTag("Door"))
        {
            circleCollider.enabled = false;
>>>>>>> Develop
            mobTearRigid.velocity = Vector2.zero;
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);
            
<<<<<<< HEAD
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
=======
            
            //StartCoroutine("TearDestroy");
        }
        
>>>>>>> Develop
        if (other.tag == "TearShadow")
        {
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);

            DestroyTears();
        }
<<<<<<< HEAD
        if (other.tag == "Door") 
        {
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);

            DestroyTears();
        }

    }   

    
    
=======
        

    }
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3f);
        mobTear.SetBool("Something", false);
        Invoke("DestroyTears", 0.3f);
    }



>>>>>>> Develop
}

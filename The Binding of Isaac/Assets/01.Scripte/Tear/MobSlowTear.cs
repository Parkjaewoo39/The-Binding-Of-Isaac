using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSlowTear : MonoBehaviour
{

    private Rigidbody2D mobTearRigid;

    private Animator mobTear = default;

   // private Collider2D mobCollider2D = default;

<<<<<<< HEAD
    private float mobTearSpeed = 1.5f;

    private float mobFastTearSpeed = 5f;
=======
    private float mobTearSpeed = 0.7f;    
>>>>>>> Develop

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
        MonsterObjectPoolSlow.ReturnObject(this);
        CancelInvoke();
    }

<<<<<<< HEAD
    public void OnTriggerEnter2D(Collider2D other)
=======
    public void OnCollionEnter2D(Collider2D other)
>>>>>>> Develop
    {
        isSomethingCheck= true;
        mobTearRigid.velocity = Vector3.zero;

<<<<<<< HEAD
        if (other.tag == "Wall")
=======
        if (other.CompareTag("Wall") || other.CompareTag("Isaac") || other.CompareTag("Door"))
>>>>>>> Develop
        {
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
        if (other.tag == "TearShadow")
        {
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);

            DestroyTears();
        }

    }   

    
    
=======


            //StartCoroutine("TearDestroy");
        }

    }
    IEnumerator DeathDelay()
    {
       

        yield return new WaitForSeconds(3f);
        mobTear.SetBool("Something", false);
        Invoke("DestroyTears", 0.3f);
    }


>>>>>>> Develop
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSlowTear : MonoBehaviour
{

    private Rigidbody2D mobTearRigid;

    private Animator mobTear = default;

   // private Collider2D mobCollider2D = default;

    private float mobTearSpeed = 0.7f;    

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
            mobTearRigid.velocity = transform.up * mobTearSpeed ;
            StartCoroutine(DeathDelay());
        }
        
    }

    //!{Shoot R&D
    
    public void DestroyTears()
    {
        MonsterObjectPoolSlow.ReturnObject(this);
        CancelInvoke();
    }

    public void OnCollionEnter2D(Collider2D other)
    {
        isSomethingCheck= true;
        mobTearRigid.velocity = Vector3.zero;

        if (other.CompareTag("Wall") || other.CompareTag("Isaac") || other.CompareTag("Door"))
        {
            mobTearRigid.velocity = Vector2.zero;
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);


            //StartCoroutine("TearDestroy");
        }

    }
    IEnumerator DeathDelay()
    {
       

        yield return new WaitForSeconds(3f);
        mobTear.SetBool("Something", false);
        Invoke("DestroyTears", 0.3f);
    }


}

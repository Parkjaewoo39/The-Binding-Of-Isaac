using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MobTear : MonoBehaviour
{

    private Rigidbody2D mobTearRigid;

    private Animator mobTear = default;
    private CircleCollider2D circleCollider;
    

    private float mobTearSpeed = 2.5f;

    private float mobFastTearSpeed = 5f;

    private bool isSomethingCheck = false;

       
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
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
        MonsterObjectPool.ReturnObject(this);
        CancelInvoke();
    }

    public void OnCollistionEnter2D(Collider2D other)
    {
        isSomethingCheck= true;
        mobTearRigid.velocity = Vector3.zero;

        if (other.CompareTag("Wall") || other.CompareTag("Isaac") || other.CompareTag("Door"))
        {
            circleCollider.enabled = false;
            mobTearRigid.velocity = Vector2.zero;
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);
            
            
            //StartCoroutine("TearDestroy");
        }
        
        if (other.tag == "TearShadow")
        {
            mobTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);

            DestroyTears();
        }
        

    }
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(3f);
        mobTear.SetBool("Something", false);
        Invoke("DestroyTears", 0.3f);
    }



}


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tears : MonoBehaviour
{

    private Rigidbody2D tearRigid;

    private Animator isaacTear = default;

    private CircleCollider2D isaacCollider2D = default;
   
    

  

    private float isaacNowTearSpeed = default;

    private bool isSomethingCheck = false;
    

    private void Awake()
    {
        GameObject.Find("Isaac").GetComponent<PlayerController>();
    }   //Awake();
    
    void Start()
    {
        isaacTear = GetComponent<Animator>();
        tearRigid = GetComponent<Rigidbody2D>();
        isaacCollider2D = GetComponent<CircleCollider2D>();
        transform.localScale = new Vector2(GameManager.isaacTearSize, GameManager.isaacTearSize);

        UpdateTearSpeed();
        isSomethingCheck = false;
    }

    void Update()
    {
        tearRigid.velocity = transform.up * isaacNowTearSpeed * 1;
         StartCoroutine(DeathDelay());        
    }

    
    
    public void DestroyTears()
    {

        ObjectPool.ReturnObject(this);
        CancelInvoke();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        isSomethingCheck= true;
        tearRigid.velocity = Vector3.zero;

        if (other.tag == "Wall" || other.tag == "Enemy" || other.tag == "Boss")
        {
            isaacCollider2D.enabled = false;
            tearRigid.velocity = Vector2.zero;
            isaacTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);
            isSomethingCheck = false;


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
       
        isaacNowTearSpeed += GameManager.isaacTearSpeed;
    }

    IEnumerator DeathDelay() 
    {
        
        
        yield return new WaitForSeconds(GameManager.isaacRange);
        isaacTear.SetBool("Something", true);
        Invoke("DestroyTears", 0.3f);
    }
    
}

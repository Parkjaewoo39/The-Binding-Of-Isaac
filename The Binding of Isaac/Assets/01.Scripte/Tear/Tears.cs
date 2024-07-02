<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
=======

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
>>>>>>> Develop
using UnityEngine;

public class Tears : MonoBehaviour
{

    private Rigidbody2D tearRigid;

    private Animator isaacTear = default;

<<<<<<< HEAD
    private Collider2D isaacCollider2D = default;

    private float tearSpeed = default;
=======
    private CircleCollider2D isaacCollider2D = default;
   
    

  
>>>>>>> Develop

    private float isaacNowTearSpeed = default;

    private bool isSomethingCheck = false;
<<<<<<< HEAD


    private void Awake()
    {
        GameObject.Find("Body").GetComponent<PlayerController>().playerStat();
    }
    void Start()
    {
        // PlayerController.isaacTearSpeed += 0f;
       


        isaacTear = GetComponent<Animator>();
        tearRigid = GetComponent<Rigidbody2D>();
      
        transform.localScale = new Vector2(PlayerManager.TearSize, PlayerManager.TearSize);
=======
    

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
>>>>>>> Develop

        UpdateTearSpeed();
        isSomethingCheck = false;
    }

    void Update()
    {
<<<<<<< HEAD
        tearRigid.velocity = transform.up * isaacNowTearSpeed * 10;
         StartCoroutine(DeathDelay());
        // if (!isSomethingCheck )
        // {            
        // }
    }

    //!{Shoot R&D
    
    public void DestroyTears()
    {
=======
        tearRigid.velocity = transform.up * isaacNowTearSpeed * 1;
         StartCoroutine(DeathDelay());        
    }

    
    
    public void DestroyTears()
    {

>>>>>>> Develop
        ObjectPool.ReturnObject(this);
        CancelInvoke();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
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
        if (other.tag == "Boss")
        {
            tearRigid.velocity = Vector2.zero;
            isaacTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);
            Debug.Log("??");
            // DestroyTears();
            //PlayerController.tearDamage
=======
        
        isSomethingCheck= true;
        tearRigid.velocity = Vector3.zero;

        if (other.tag == "Wall" || other.tag == "Enemy" || other.tag == "Boss")
        {
            isaacCollider2D.enabled = false;
            tearRigid.velocity = Vector2.zero;
            isaacTear.SetBool("Something", true);
            Invoke("DestroyTears", 0.3f);
            isSomethingCheck = false;


>>>>>>> Develop
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
       
<<<<<<< HEAD
        isaacNowTearSpeed += PlayerController.isaacTearSpeed;
=======
        isaacNowTearSpeed += GameManager.isaacTearSpeed;
>>>>>>> Develop
    }

    IEnumerator DeathDelay() 
    {
<<<<<<< HEAD
        yield return new WaitForSeconds(PlayerController.isaacTime);
        yield return new WaitForSeconds(PlayerController.isaacRange);
=======
        
        
        yield return new WaitForSeconds(GameManager.isaacRange);
>>>>>>> Develop
        isaacTear.SetBool("Something", true);
        Invoke("DestroyTears", 0.3f);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AtFly : MonoBehaviour
{
    private Animator AtFlyAni = default;
    private Rigidbody2D AtFlyRigid = default;

    public Transform target;

    private float AtFlyHp = 3f;
    public static float AtFlySpeed = 10f;

    private bool isTargetCheck = false;
    Vector2 vec;

    void Start()
    {
        gameObject.SetActive(true);
        AtFlyAni = gameObject.GetComponentMust<Animator>();
        AtFlyRigid = gameObject.GetComponentMust<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
        
        StartCoroutine("mobMove");
    }

    //FixedUpdate()
    void FixedUpdate()
    {
        AtFlyRigid.velocity = new Vector2(AtFlySpeed, AtFlySpeed);

        //
        MoveCharacter(vec);
    }

    //
    private void MoveCharacter(Vector2 direction)
    {
        AtFlyRigid.MovePosition((Vector2)transform.position + (direction * AtFlySpeed * Time.deltaTime));
    }
    void Update()
    {
       // noAtFlyRigid.transform.LookAt(target);
        FollowTarget();

        //
        Vector3 direction = target.position - transform.position;        
        direction.Normalize();
        vec = direction;
    }


    
    IEnumerator mobMove()
    {
       // noAtFlySpeed = Random.Range(-1, 5);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine("mobMove");
    }

    public void startMove()
    {
        StartCoroutine("mobMove");
    }

    // ´«¹° ¸ÂÀ»¶§
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tear")
        {
            Hit();
            //Debug.Log($"{noAtFlyHp}");
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTargetCheck = false;
        }
    }



    //!{Hit()
    public void Hit()
    {
        AtFlyHp -= PlayerController.isaacDamage;

        if (0 < AtFlyHp)
        {

        }
        if (AtFlyHp <= 0)
        {
            Die();
        }
    }   //Hit()


    //!{Die()
    public void Die()
    {
        AtFlyAni.SetBool("Die", true);
        Invoke("DestroyMob", 0.3f);


        
    }   //Did()

    //private void Distroy()
    //{
    //    Object.Destroy(this);

    //}   //Distroy()

    public void DestroyMob()
    {        
        gameObject.SetActive(false);
    }

    public void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > 1 && isTargetCheck)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, AtFlySpeed);
        }
    }



}

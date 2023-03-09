using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NoAtFly : MonoBehaviour
{
    private Animator noAtFlyAni = default;
    private Rigidbody2D noAtFlyRigid = default;

    public Transform target;

    private float noAtFlyHp = 3f;
    public static float noAtFlySpeed = 1f;

    private bool isTargetCheck = false;
    Vector2 vec;

    void Start()
    {
        gameObject.SetActive(true);
        noAtFlyAni = gameObject.GetComponentMust<Animator>();
        noAtFlyRigid = gameObject.GetComponentMust<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
        
        StartCoroutine("mobMove");
    }

    //FixedUpdate()
    void FixedUpdate()
    {
        noAtFlyRigid.velocity = new Vector2(noAtFlySpeed, noAtFlySpeed);

        //
        MoveCharacter(vec);
    }

    //
    private void MoveCharacter(Vector2 direction)
    {
        noAtFlyRigid.MovePosition((Vector2)transform.position + (direction * noAtFlySpeed * Time.deltaTime));
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
        noAtFlySpeed = Random.Range(-1, 5);
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
            Debug.Log($"{noAtFlyHp}");
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
        noAtFlyHp -= PlayerController.isaacDamage;

        if (0 < noAtFlyHp)
        {

        }
        if (noAtFlyHp <= 0)
        {
            Die();
        }
    }   //Hit()


    //!{Die()
    public void Die()
    {
        noAtFlyAni.SetBool("Die", true);
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
            transform.position = Vector2.MoveTowards(transform.position, target.position, noAtFlySpeed);
        }
    }



}

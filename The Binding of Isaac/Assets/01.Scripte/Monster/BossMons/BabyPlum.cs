using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BabyPlum : MonoBehaviour
{
    private Animator babyPlumAni = default;
    private Rigidbody2D babyPlumRigid = default;

    public Transform target;

    private float babyPlumHp = 35f;
    public static float babyPlumSpeed = 10f;

    private bool isTargetCheck = false;
    Vector2 vec;

    void Start()
    {
        gameObject.SetActive(true);
        babyPlumAni = gameObject.GetComponentMust<Animator>();
        babyPlumRigid = gameObject.GetComponentMust<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
        
        StartCoroutine("mobMove");
    }

    //FixedUpdate()
    void FixedUpdate()
    {
        babyPlumRigid.velocity = new Vector2(babyPlumSpeed, babyPlumSpeed);

        //
        MoveCharacter(vec);
    }

    //
    private void MoveCharacter(Vector2 direction)
    {
        babyPlumRigid.MovePosition((Vector2)transform.position + (direction * babyPlumSpeed * Time.deltaTime));
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
        //Debug.Log(PlayerController.isaacDamage);
        babyPlumHp -= PlayerController.isaacDamage;
       // Debug.Log(babyPlumHp);

        if (0 < babyPlumHp)
        {

        }
        if (babyPlumHp <= 0)
        {
            Die();
        }
    }   //Hit()


    //!{Die()
    public void Die()
    {
        babyPlumAni.SetBool("Die", true);
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
            transform.position = Vector2.MoveTowards(transform.position, target.position, babyPlumSpeed);
        }
    }

    



}

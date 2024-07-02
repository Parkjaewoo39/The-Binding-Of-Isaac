using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NoAtFly : MonoBehaviour, IEnemy
{
    Room room;
    public PickUp pickUp;
    private Animator noAtFlyAni = default;
    private Rigidbody2D noAtFlyRigid = default;

    public Transform target;

    private float noAtFlyHp = 3f;
    public static float noAtFlySpeed = 1f;

    private bool isTargetCheck = false;
    Vector2 vec;

   
    void Start()
    {
        pickUp = FindObjectOfType<PickUp>();
        gameObject.SetActive(true);
        noAtFlyAni = gameObject.GetComponent<Animator>();
        noAtFlyRigid = gameObject.GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
        room = GetComponentInParent<Room>();
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
        noAtFlySpeed = Random.Range(-0.5f, 1);
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
            Hit(GameManager.Instance.IsaacDamage);
            
        }
        if (other.tag == "UseBomb") 
        {
            Hit(GameManager.Instance.IsaacBombDamage);
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
    public void Hit(float _float)
    {
        noAtFlyHp -= _float;

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
        PickUpDrop();
        if (room != null)
        {
            room.MobDie();
        }
        else { Debug.Log("asdf"); }

        
        

    }   

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

    private void PickUpDrop()
    {
        Vector3 position = transform.position;
        GameObject mob = this.gameObject;
        int index = Random.Range(0, 6);
        switch (index)
        {
            case 0:
                pickUp.KeyDrop(mob, position);
                break;
            case 1:
                pickUp.CoinDrop(mob, position);
                break;
            case 2:
                pickUp.BombDrop(mob, position);
                break;
            case 3:
                pickUp.HeartOneDrop(mob, position);
                break;
            case 4:
                pickUp.HeartHalfDrop(mob, position);
                break;
            default:
                break;

        }
    }


}

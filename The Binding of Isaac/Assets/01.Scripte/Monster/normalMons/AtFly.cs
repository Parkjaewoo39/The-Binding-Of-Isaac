using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

<<<<<<< HEAD
public class AtFly : MonoBehaviour
{
=======
public class AtFly : MonoBehaviour, IEnemy
{
    Room room;
    public PickUp pickUp;
>>>>>>> Develop
    private Animator AtFlyAni = default;
    private Rigidbody2D AtFlyRigid = default;

    public Transform target;

    private float AtFlyHp = 3f;
<<<<<<< HEAD
    public static float AtFlySpeed = 10f;
=======
    public static float AtFlySpeed = 1f;
>>>>>>> Develop

    private bool isTargetCheck = false;
    Vector2 vec;

    void Start()
    {
<<<<<<< HEAD
        gameObject.SetActive(true);
        AtFlyAni = gameObject.GetComponentMust<Animator>();
        AtFlyRigid = gameObject.GetComponentMust<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
        
=======
        
        pickUp = FindObjectOfType<PickUp>();
        gameObject.SetActive(true);
        AtFlyAni = gameObject.GetComponent<Animator>();
        AtFlyRigid = gameObject.GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().transform;
        room = GetComponentInParent<Room>();
>>>>>>> Develop
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
<<<<<<< HEAD
            Hit();
            //Debug.Log($"{noAtFlyHp}");
        }
=======
            Hit(GameManager.Instance.IsaacDamage);
            //Debug.Log($"{noAtFlyHp}");
        }
        if (other.tag == "UseBomb")
        {
            Hit(GameManager.Instance.IsaacBombDamage);
        }
>>>>>>> Develop
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTargetCheck = false;
        }
    }



    //!{Hit()
<<<<<<< HEAD
    public void Hit()
    {
        AtFlyHp -= PlayerController.isaacDamage;
=======
    public void Hit(float _float)
    {
        AtFlyHp -= _float ;
>>>>>>> Develop

        if (0 < AtFlyHp)
        {

        }
        if (AtFlyHp <= 0)
<<<<<<< HEAD
        {
=======
        {            
>>>>>>> Develop
            Die();
        }
    }   //Hit()


    //!{Die()
    public void Die()
    {
        AtFlyAni.SetBool("Die", true);
<<<<<<< HEAD
        Invoke("DestroyMob", 0.3f);


=======

        Invoke("DestroyMob", 0.3f);
        PickUpDrop();
        if (room != null) 
        {
            room.MobDie();
        }
>>>>>>> Develop
        
    }   //Did()

    //private void Distroy()
<<<<<<< HEAD
    //{
    //    Object.Destroy(this);

    //}   //Distroy()
=======
   
>>>>>>> Develop

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

<<<<<<< HEAD
=======
    private void PickUpDrop() 
    {
        Vector3 position = transform.position;
        GameObject mob = this.gameObject;
        int index = Random.Range(0, 6);
        switch (index) 
        {
            case 0: 
                pickUp.KeyDrop(mob,position);
                break;
            case 1:
                pickUp.CoinDrop(mob, position);
                break;
            case 2:
                pickUp.BombDrop(mob,position); 
                break;
            case 3:
                pickUp.HeartOneDrop(mob,position);
                break;
            case 4:
                pickUp.HeartHalfDrop(mob, position);
                break;
            default:
                break;
              
        }
    }

>>>>>>> Develop


}

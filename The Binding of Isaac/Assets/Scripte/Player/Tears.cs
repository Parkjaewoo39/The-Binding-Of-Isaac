using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{

    private Rigidbody2D tearRigid;

    private Animator IsaacTear = default;

    private Vector2 direction;

    float tearDamage = PlayerController.isaacDamage;

    float tearReload = PlayerController.isaacReload;

    float tearRate = PlayerController.isaacmaxReload;

    float tearSpeed = PlayerController.isaacTearSpeed;

    void Start()
    {
        IsaacTear = gameObject.GetComponent<Animator>();
        tearRigid = GetComponent<Rigidbody2D>();
       
       // tearRigid.AddForce(transform.forward * tearSpeed);

        tearDamage = PlayerController.isaacDamage;
        var tearS = PlayerController.isaacTearSpeed;

        tearReload = 0f;
        //tearRigid.velocity =transform.forward * tearS;
    }

    void Update()
    {
        Vector3 dircetion = Vector3.up;
        
        transform.Translate(direction);
        //tearRigid.velocity = transform.forward * PlayerController.isaacTearSpeed;

        if (0 < tearReload)
        {
            tearReload -= Time.deltaTime;
        }
    }

    public void Shoot(Vector2 direction)
    {
        //tearRigid.velocity = PlayerController.isaacTearSpeed * direction;
        //tearRigid.AddForce(transform.forward * tearSpeed);
        this.direction = direction;
        //Invoke("DestroyTears", 1.5f);
    }

    public void DestroyTears()
    {
        ObjectPool.ReturnObject(this);
        CancelInvoke();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            IsaacTear.SetBool("Wall", true);
        }
        if (other.tag == "Enemy")
        {           
            tearRigid.velocity = Vector2.zero;            
            IsaacTear.SetBool("Enemy", true);
            DestroyTears();


            //PlayerController.tearDamage
        }
       
    }

    public void Attack()
    {

    }


}

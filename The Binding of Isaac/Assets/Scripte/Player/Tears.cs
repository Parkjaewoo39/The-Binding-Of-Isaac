using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{

    private Rigidbody2D tearRigid;
    private Animator IsaacTear = default;

    private Vector2 direction;
    void Start()
    {
        IsaacTear = gameObject.GetComponent<Animator>();
        tearRigid = GetComponent<Rigidbody2D>();
        var tearS = PlayerController.isaacTearSpeed;
        //tearRigid.velocity =transform.forward * tearS;
    }

    void Update()
    {
        transform.Translate(direction);
        tearRigid.velocity = transform.forward * PlayerController.isaacTearSpeed;
    }

    public void Shoot(Vector2 direction)
    {
        //tearRigid.velocity = PlayerController.isaacTearSpeed * direction;
        this.direction = direction;
        Invoke("DestroyTears", 1.5f);
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
            
            //PlayerController.tearDamage
        }
    }

    public void Attack() 
    {
        float tearDamage = PlayerController.isaacDamage;        
    }


}

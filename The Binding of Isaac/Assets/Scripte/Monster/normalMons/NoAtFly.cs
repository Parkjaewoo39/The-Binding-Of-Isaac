using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAtFly : MonoBehaviour
{
    private Animator noAtFlyAni = default;
    private Rigidbody2D noAtFlyRigid = default;
    private RaycastHit2D rayHitIsaac = default;

    private float noAtFlyHp = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        noAtFlyAni = gameObject.GetComponentMust<Animator>();
        noAtFlyRigid = gameObject.GetComponentMust<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        //noAtFlyRigid.velocity = 
    }


    
    //!{Hit()
    public void Hit() 
    {
        Debug.Log(PlayerController.isaacDamage);
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
        CancelInvoke();
    }   //Did()

    private void Distroy() 
    {
        Object.Destroy(this);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tear")
        {
            Hit();
        }
    }
}

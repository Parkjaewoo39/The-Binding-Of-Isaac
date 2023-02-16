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
        IsaacTear = GetComponent<Animator>();
        tearRigid = GetComponent<Rigidbody2D>();
        var tearS = PlayerController.isaacTearSpeed;
        tearRigid.velocity =transform.forward * tearS;
    }

    void Update()
    {
        transform.Translate(direction);
    }

    public void Shoot(Vector2 direction)
    {
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
            IsaacTear.SetTrigger("Check");
        }
        if (other.tag == "Emeny")
        {
            //PlayerController.tearDamage
        }
    }
}

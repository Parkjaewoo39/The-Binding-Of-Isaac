using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{
    private Vector2 direction;
    private Animator IsaacTear = default;
    public void Shoot(Vector2 direction)
    {
        this.direction = direction;
        Invoke("DestoryTears", 1.5f);
    }

    public void DestroyTears()
    {
        ObjectPool.ReturnObject(this);
        CancelInvoke();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall"||other.tag == "Emeny")
        {
            IsaacTear.SetTrigger("normalTear");
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        IsaacTear = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

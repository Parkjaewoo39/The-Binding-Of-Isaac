using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tears : MonoBehaviour
{

    private Rigidbody2D tearRigid;

    private Animator IsaacTear = default;

    private Vector2 direction;

    private float tearSpeed = default;

    private float isaacNowTearSpeed = default;

    private bool isSomethingCheck = false;

    private void Awake()
    {
        GameObject.Find("Body").GetComponent<PlayerController>().playerStat();
    }
    void Start()
    {
        // PlayerController.isaacTearSpeed += 0f;

        IsaacTear = GetComponent<Animator>();
        tearRigid = GetComponent<Rigidbody2D>();

        UpdateTearSpeed();
        isSomethingCheck = false;
    }

    void Update()
    {
        //Debug.Log($"{tearSpeed}");
        //tearRigid.velocity = new Vector2(50 , 0 * tearSpeed) ;
        //tearRigid.velocity = transform.up * tearSpeed;
        //Debug.Log($"{isaacNowTearSpeed}");
        if (!isSomethingCheck)
        {
            tearRigid.velocity = transform.up * isaacNowTearSpeed * 20;
        }


        //Vector2 dircetion = Vector2.up;

        //transform.Translate(direction);

        //tearRigid.velocity = transform.forward * PlayerController.isaacTearSpeed;
    }



    public void DestroyTears()
    {
        ObjectPool.ReturnObject(this);
        CancelInvoke();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        isSomethingCheck= true;
        tearRigid.velocity = Vector3.zero;

        if (other.tag == "Wall")
        {
            tearRigid.velocity = Vector2.zero;
            IsaacTear.SetBool("Something", true);
            // DestroyTears();
            //StartCoroutine("TearDestroy");
        }
        if (other.tag == "Enemy")
        {
            tearRigid.velocity = Vector2.zero;
            IsaacTear.SetBool("Something", true);
            // DestroyTears();
            //PlayerController.tearDamage
        }
        if (other.tag == "TearShadow")
        {
            IsaacTear.SetBool("Something", true);
            DestroyTears();
        }

    }

    public void UpdateTearSpeed()
    {
        // isaacNowTearSpeed = PlayerPrefs.GetFloat("isaacTearSpeedVal");
        isaacNowTearSpeed += PlayerController.isaacTearSpeed;
    }
    //IEnumerator TearDestroyAnimation(string name, float ratio, bool play)
    //{
    //    var ani = animation["normalTear"];
    //}
}

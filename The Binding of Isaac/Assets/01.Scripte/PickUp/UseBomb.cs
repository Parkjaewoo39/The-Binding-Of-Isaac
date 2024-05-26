using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UseBomb : MonoBehaviour
{

    private Animator bombAni;
    
    Rigidbody2D bombRigid;
    BoxCollider2D bombBoxCollider;


    // Start is called before the first frame update
    void Start()
    {
        bombAni = gameObject.GetComponent<Animator>();
        bombRigid = gameObject.GetComponent<Rigidbody2D>();
        bombBoxCollider = gameObject.GetComponent<BoxCollider2D>();

        Exp();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Exp()
    {

        bombAni.SetBool("isExploresion", true);
        StartCoroutine(Delay());
        
    }
    IEnumerator Delay() 
    {
        yield return new WaitForSeconds(1.45f);
        bombBoxCollider.size = new Vector2(1f, 1f);
        bombBoxCollider.isTrigger = true;
        yield return new WaitForSeconds(0.1f);
        bombBoxCollider.enabled = false;
        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }
    


}

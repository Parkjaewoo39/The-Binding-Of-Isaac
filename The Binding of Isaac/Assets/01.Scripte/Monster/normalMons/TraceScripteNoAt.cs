using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TraceScripteNoAt : MonoBehaviour
{
    private Rigidbody2D colliRigid;
    private int moveVal;
    void Start()
    {
       
        
    }  
        
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           // transform.parent.GetComponent<NoAtFly>().
            transform.parent.GetComponent<NoAtFly>().startMove();
        }
    }
}

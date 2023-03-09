using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UseBomb : MonoBehaviour
{
    public static UseBomb instance;
    private Animator bombAni ;
    public GameObject obj;
    public GameObject useBombObj = default;

    public Rigidbody2D bombRigid;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bombAni = gameObject.GetComponent<Animator>();
        bombRigid = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine("Exp");
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject explor =Instantiate(useBombObj,transform.position, transform.rotation);
        
        explor.transform.localScale = new Vector3(1f,1f,1f);
        
        

        Collider2D[] clos = Physics2D.OverlapCircleAll(explor.transform.position, 2.0f);
        for(int i = 0; i < clos.Length; i++)
        {
            if(clos[i].gameObject.tag == "Enemy" )
            {
                
            }
            if((clos[i].gameObject.tag == "Boss"))
            {
                //BabyPlum1.GetComponent<BabyPlum1>().HitExp(100);
            }
            if(clos[i].gameObject.tag == "Player")
            {
                
                PlayerManager.DamageIsaac(1);
            }
            if(clos[i].gameObject.tag == "Obstacle" || clos[i].gameObject.tag == "Hidden")
            {

            }
        }
    }
     IEnumerator Exp()
    {
        yield return new WaitForSeconds(3f);
         bombAni.SetBool("Exp", true);         
         Destroy(gameObject);
    }
    
    
}

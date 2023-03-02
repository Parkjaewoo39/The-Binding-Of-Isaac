using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngien.UI;

public class UseBomb : MonoBehaviour
{
    public static UseBomb instance;
    private Animator bombAni =default;
    public GameObject obj;
    public GameObject useBombObj = default;
    
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
        bombAni = gameObject.GetComponenet<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject explor =Instantiate(useBombObj);
        explor.transform.position = transform.position;
        Destroy(explor ,3f);
        bombAni.SetBool("Exp", true);
        

        Collider2D[] clos = Physics2D.OverlapAreaAll(explor.transform.position, explor.transform.localScale*2);
        for(int i = 0; i < clos.Length; i++)
        {
            if(clos[i].gameObject.tag == "Enemy" || clos[i].gameObject.tag == "Boss")
            {
                BabyPlum1.HitExp(100);
            }
            if(clos[i].gameObject.tag == "Player")
            {
                
                PlayerManager.DieIsaac(1);
            }
            if(clos[i].gameObject.tag == "Obstacle" || clos[i].gameObject.tag == "Hidden")
            {

            }
        }
    }
}

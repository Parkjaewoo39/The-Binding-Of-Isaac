using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Animator coinAni = default;
    // Start is called before the first frame update
    void Start()
    {
        coinAni = gameObject.GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CoinPick();
            coinAni.SetBool("PickUp",true);
            DistroyPick();
        }
    }

    public void DistroyPick()
    {               
        Destroy(gameObject);
    }

     public void CoinPick()
    {
        PlayerManager.CoinChange(1);
    }
}

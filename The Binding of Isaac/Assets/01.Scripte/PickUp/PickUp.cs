using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject keyPrefab;
    public GameObject bombPrefab;
    public GameObject heartOne;
    public GameObject heartHalf;

    public Animator coinAni = default;
    private void Start()
    {
        
        coinAni = gameObject.GetComponent<Animator>();
    }
    public void HeartOneDrop(GameObject parent, Vector3 position)
    {
        GameObject coinObject = Instantiate(heartOne, position, Quaternion.identity);
        coinObject.transform.SetParent(parent.transform.parent);
        coinObject.name = heartOne.name;
    }
    public void HeartHalfDrop(GameObject parent, Vector3 position)
    {
        GameObject coinObject = Instantiate(heartHalf, position, Quaternion.identity);
        coinObject.transform.SetParent(parent.transform.parent);
        coinObject.name = heartHalf.name;
    }

    public void CoinDrop(GameObject parent,Vector3 position)
    {
        GameObject coinObject = Instantiate(coinPrefab, position, Quaternion.identity);
        coinObject.transform.SetParent(parent.transform.parent);
        coinObject.name = coinPrefab.name;
    }

    public void KeyDrop(GameObject parent,Vector3 position)
    {
       GameObject keyObject = Instantiate(keyPrefab, position, Quaternion.identity);
        keyObject.transform.SetParent(parent.transform.parent);
        keyObject.name = keyPrefab.name;
    }

    public void BombDrop(GameObject parnet,Vector3 position)
    {
        GameObject bombObject = Instantiate(bombPrefab, position, Quaternion.identity);
        bombObject.transform.SetParent(parnet.transform.parent);
        bombObject.name = bombPrefab.name;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            if (gameObject.name == "HeartOne") 
            {
                HeartOnePick();
                Destroy(gameObject);
            }
            if (gameObject.name == "HeartHalf")
            {
                HeartHalfPick();
                Destroy(gameObject);
            }
            if (gameObject.name == "Coin") 
            {
                CoinPick();
                coinAni.SetBool("PickUp", true);
                DistroyPick();
            }
            if (gameObject.name == "Key")
            {
                KeyPick();
                Destroy(gameObject);
            }
            if (gameObject.name == "Bomb")
            {
                BombPick();
                Destroy(gameObject);
            }
        }
        

    }

    public void DistroyPick()
    {
        coinAni.SetBool("PickUp", false);
        Destroy(gameObject);
    }

    public void HeartOnePick()
    {
        GameManager.Instance.IsaacIncreaseHp(1, 0);
    } public void HeartHalfPick()
    {
        GameManager.Instance.IsaacIncreaseHp(0.5f, 0);

    }
    public void CoinPick()
    {
        GameManager.CoinChange(1);
    }
    public void KeyPick()
    {
        GameManager.KeyChange(1);
    }
    public void BombPick()
    {
        GameManager.BombChange(1);
    }

}

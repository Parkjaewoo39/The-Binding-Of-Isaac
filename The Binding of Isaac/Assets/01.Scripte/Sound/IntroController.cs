using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public GameObject mainMenu = default;
   
    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {
        StartCoroutine(SoundStop());
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.anyKeyDown) 
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
            
        }
        
    }

    private IEnumerator SoundStop()
    {
        //yield return new WaitForSeconds(4.6f);
        gameObject.SetActive(true);
       
        yield return new WaitForSeconds(21f);
        
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void Reset()
    {
        StartCoroutine(SoundStop());
       
        this.gameObject.GetComponent<Animator>().enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSoundController : MonoBehaviour
{
    public GameObject mainMenu = default;
    AudioSource introSound = default;
    // Start is called before the first frame update
    private void Awake()
    {
       
    }
    void Start()
    {
        
        introSound = gameObject.GetComponent<AudioSource>();
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
        introSound.Play();
        yield return new WaitForSeconds(21f);
        introSound.Stop();
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void Reset()
    {
        StartCoroutine(SoundStop());
        introSound.Stop();
        this.gameObject.GetComponent<Animator>().enabled = true;
    }
}

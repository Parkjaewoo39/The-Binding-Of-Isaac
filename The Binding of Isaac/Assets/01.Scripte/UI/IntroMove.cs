using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMove : MonoBehaviour
{
    
    public GameObject introMove = default;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SetFalse());
        if (Input.anyKeyDown) 
        {
            //LoadSceneMode.Single
        }
    }

    private IEnumerator SetFalse() 
    {
        yield return new WaitForSeconds(4.6f);
        gameObject.SetActive(false);
    }
}

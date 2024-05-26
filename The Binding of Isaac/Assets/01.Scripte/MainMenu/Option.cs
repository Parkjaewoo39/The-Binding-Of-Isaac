using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle[] PlayCheck;
    public float speed;
    public Vector2 direction;
    bool keyDelay;
    public Sprite[] volume;
    public Image sfxImage;
    public Image MusicImage;
    // Start is called before the first frame update
    void Start()
    {
        keyDelay = false;
        MusicImage = PlayCheck[0].transform.GetChild(2).GetComponent<Image>();
        
        sfxImage = PlayCheck[1].transform.GetChild(2).GetComponent<Image>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RectTransform>().localPosition.x > 0)
        {
            GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            direction = Vector2.zero;
        }
        InputKey();
        transform.Translate(direction * Time.deltaTime * speed);
    }
    void InputKey()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (!keyDelay)
            {
                keyDelay = true;
                if (PlayCheck[0].interactable)
                {
                    PlayCheck[0].interactable = false;
                    PlayCheck[1].interactable = true;
                }
                else
                {
                    PlayCheck[0].interactable = true;
                    PlayCheck[1].interactable = false;
                }
                StartCoroutine(Delaykey());
            }

        }
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.parent.GetComponent<MainMenuController>().isOption = false;
            transform.parent.GetComponent<MainMenuController>().isOptionChange = true;
        }


    }
    IEnumerator Delaykey()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        keyDelay = false;
        Debug.Log(keyDelay);
    }
    public void Reset()
    {
        keyDelay = false;
        MusicImage = PlayCheck[0].transform.GetChild(2).GetComponent<Image>();

        sfxImage = PlayCheck[1].transform.GetChild(2).GetComponent<Image>();
    }
}

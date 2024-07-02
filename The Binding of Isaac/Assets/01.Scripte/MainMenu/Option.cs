using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle[] PlayCheck;
    public float speed;
    public Vector2 direction;
    bool keyDelay;
    public Sprite[] volumeSprite;
    public Image sfxImage;
    public Image musicImage;
    // Start is called before the first frame update
    void Start()
    {
       
        keyDelay = false;
        musicImage = PlayCheck[0].transform.GetChild(2).GetComponent<Image>();
        musicImage.sprite = volumeSprite[3];
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
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
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
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            AdjustVolume(-0.05f);
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            AdjustVolume(0.05f);

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
       
    }
    public void Reset()
    {
        keyDelay = false;
        musicImage = PlayCheck[0].transform.GetChild(2).GetComponent<Image>();

        sfxImage = PlayCheck[1].transform.GetChild(2).GetComponent<Image>();
    }

    public void AdjustVolume(float change)
    {
        if (PlayCheck[0].interactable)
        {
            SoundManager.instance.BgmVolumeController(change);
            BgmVolumeSprite();


        }
        else if (PlayCheck[1].interactable)
        {
           // SoundManager.instance.EffectVolumeController(change);
            
        }
    }
    public void BgmVolumeSprite( ) 
    {
        float volume = SoundManager.instance.bgmSource.volume;
        int index = Mathf.RoundToInt(volume / 0.05f);
        musicImage.sprite = volumeSprite[index];     

    }
}

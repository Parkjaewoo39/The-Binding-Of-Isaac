using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChoiceMenu : MonoBehaviour
{
    public Toggle[] PlayCheck;
    public float speed;
    public Vector2 direction;
    bool keyDelay;
    // Start is called before the first frame update
    void Start()
    {
        keyDelay = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RectTransform>().localPosition.y > 0 || GetComponent<RectTransform>().localPosition.x < 0)
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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (PlayCheck[0].interactable)
            {
                transform.parent.GetComponent<MainMenuController>().isNewRun = true;
            }
            else
            {
                transform.parent.GetComponent<MainMenuController>().isOption = true;
                transform.parent.GetComponent<MainMenuController>().isOptionChange = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))

        {
            transform.parent.GetComponent<MainMenuController>().isEscPress = true;
            //transform.
            //.parent.GetComponent<TitleMoving>().
        }
    }
    IEnumerator Delaykey()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        keyDelay = false;
        Debug.Log(keyDelay);
    }

    IEnumerator ActiveReset(Transform tran)
    {
        yield return new WaitForSeconds(0.4f);
        tran.gameObject.SetActive(false);
    }
}

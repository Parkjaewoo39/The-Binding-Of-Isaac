using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public bool isAniPress;
    public bool isEscPress;
    public bool isEscPressChange;
    public bool isNewRun;
    public bool isNewRunChange;
    public bool isOption;
    public bool isOptionChange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isAniPress)
        {

        }
        else
        {
            PressAny();
        }
        if (isEscPress)
        {
            PressEsc();
        }
        else
        {

        }
        if (isNewRun)
        {

        }
        if (isOption)
        {
            OptionOpen();
        }
        else
        {
            OptionClose();
        }
    }
    void PressAny()
    {
        if (Input.anyKeyDown)
        {
            transform.GetChild(0).GetComponent<TitleMoving>().isActve = true;
            transform.GetChild(1).GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(0, -1080f, 0), new Quaternion(0, 0, 0, 0));
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).GetComponent<ChoiceMenu>().direction = Vector2.up;
            transform.GetChild(1).GetComponent<ChoiceMenu>().speed = 4000f;
            isAniPress = true;
            StartCoroutine(ActiveReset(transform.GetChild(0)));
        }
    }

    void PressEsc()
    {
        if (isEscPressChange)
        {
            transform.parent.GetChild(0).GetComponent<TitleMoving>().isPassive = true;
            transform.parent.GetChild(1).GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(0, 1080f, 0), new Quaternion(0, 0, 0, 0));
            transform.parent.GetChild(1).gameObject.SetActive(true);
            transform.parent.GetChild(1).GetComponent<ChoiceMenu>().direction = Vector2.down;
            transform.parent.GetChild(1).GetComponent<ChoiceMenu>().speed = 4000f;
            isEscPressChange = false;
            StartCoroutine(ActiveReset(transform.GetChild(0)));

        }
    }
    void NewRunOpen()
    {

    }
    void NewRunClose()
    {

    }
    void OptionOpen()
    {
        if (isOptionChange)
        {

            transform.GetChild(2).GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(-1920f, 0, 0), new Quaternion(0, 0, 0, 0));
            transform.GetChild(2).gameObject.SetActive(true);
            transform.GetChild(2).GetComponent<Option>().direction = Vector2.right;
            transform.GetChild(2).GetComponent<Option>().speed = 4000f;
            transform.GetChild(1).GetComponent<ChoiceMenu>().direction = Vector2.right;
            transform.GetChild(1).GetComponent<ChoiceMenu>().speed = 4000f;
            StartCoroutine(ActiveReset(transform.GetChild(1)));
            isOptionChange = false;
        }
        isOption = false;
    }

    void OptionClose()
    {
        if (isOptionChange)
        {
            transform.GetChild(1).GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(+1920f, 0, 0), new Quaternion(0, 0, 0, 0));
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).GetComponent<Option>().direction = Vector2.left;
            transform.GetChild(2).GetComponent<Option>().speed = 4000f;
            transform.GetChild(1).GetComponent<ChoiceMenu>().direction = Vector2.left;
            transform.GetChild(1).GetComponent<ChoiceMenu>().speed = 4000f;
            StartCoroutine(ActiveReset(transform.GetChild(2)));
            isOptionChange = false;
        }
        isOption = true;




    }
    IEnumerator ActiveReset(Transform tran)
    {
        yield return new WaitForSeconds(0.4f);
        tran.gameObject.SetActive(false);
    }
}

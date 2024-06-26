using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewRun : MonoBehaviour
{
    public Toggle[] PlayCheck;
    public float speed;
    public Vector2 direction;
    bool keyDelay;
    // Start is called before the first frame update
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
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!keyDelay)
            {
                keyDelay = true;
                
                StartCoroutine(Delaykey());
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("01.BaseMent");
        }

        if (Input.GetKeyDown(KeyCode.Escape))

        {
            transform.parent.GetComponent<MainMenuController>().isEscPress = true;

        }
    }
    IEnumerator Delaykey()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        keyDelay = false;
        
    }

    IEnumerator ActiveReset(Transform tran)
    {
        yield return new WaitForSeconds(0.4f);
        tran.gameObject.SetActive(false);
    }


}

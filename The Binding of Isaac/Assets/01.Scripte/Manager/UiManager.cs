using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager gameManager;

    public GameObject hpUi;
    public Image activeItemUi;
    public Image activeEnergyUi;
    public Image activeEnergyBackGroundUi;
    public Sprite[] activeEnergryBar;
    public GameObject pauseUi;

    public Toggle[] toglles;
    public static UiManager instance;
    public TMP_Text coinText;
    public TMP_Text bombText;
    public TMP_Text keyText;

    public Image[] isaacHpUi;

    public Image[] filledHeart;
    public Image[] blankHeart;
    public Image halfHeart;


    private float hp;
    private float hpMax;

    public int coinCount;
    public int keyCount;
    public int bombCount;

    public float charge;
    public float chargeMax;

    public float arrowMoveAmount = 0.14f;
    public bool isPauseCheck = false;


    void Awake()
    {
        gameManager = GameManager.Instance;
        if (instance == null)
        {
            instance = this;
        }

        else instance = this;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();


    }
    void Start()
    {
        isPauseCheck = false;

        StartCoroutine(DelayInstance());

        activeItemUi.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0);
        activeEnergyUi.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0);
        activeEnergyBackGroundUi.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0);

    }

    void Update()
    {

        coinText.text = "" + GameManager.coin.ToString("D2");
        bombText.text = GameManager.bomb.ToString("D2");
        keyText.text = "" + GameManager.key.ToString("D2");
        if (playerController.isPause == true)
        {

            InputKey();
        }

    }
    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < toglles.Length; i++)
            {
                if (toglles[i].interactable)
                {
                    toglles[i].interactable = false;
                    toglles[(i + 2) % toglles.Length].interactable = true; // 업 화살표일 때, 다음 토글은 현재 인덱스에서 2칸 뒤의 토글
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i < toglles.Length; i++)
            {
                if (toglles[i].interactable)
                {
                    toglles[i].interactable = false;
                    toglles[(i + 1) % toglles.Length].interactable = true; // 다운 화살표일 때, 다음 토글은 현재 인덱스에서 1칸 뒤의 토글
                    break;
                }
            }
        }

        if (toglles[1].interactable && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)))
        {
            GameManager.Instance.Pause(playerController.isPause);
        }
        if (toglles[2].interactable && Input.GetKeyDown(KeyCode.Space))
        {

            SceneManager.LoadScene("00.TitleScene");
            SceneOneInitialized.InitializeScene1();

        }


    }


    private IEnumerator DelayInstance()
    {
        gameManager = GameManager.Instance;
        if (GameManager.Instance == null)
        {
            gameManager = GameManager.Instance;
        }
        yield return null;
        hp = GameManager.isaacHeartHp;
        hpMax = GameManager.isaacHeartMaxHp;
        coinCount = GameManager.coin;
        keyCount = GameManager.key;
        bombCount = GameManager.bomb;


        UpdateIsaacHeartUi();

    }
    public void UpdateIsaacHeartUi()
    {

        // hpMax만큼 반복하여 이미지 설정

        float hp = GameManager.Instance.IsaacHealthHp;
        float hpMax = GameManager.Instance.IsaacHeartMaxHp;

        // hpMax와 hp 값이 유효한지 확인
        if (hpMax > 0)
        {

            // hpMax만큼 반복하여 이미지 설정
            for (int index = 0; index < hpMax; index++)
            {
                // hp가 정수인 경
                if (index < Mathf.Floor(hp))
                {
                    filledHeart[index].gameObject.SetActive(true);
                    blankHeart[index].gameObject.SetActive(true);
                    filledHeart[index].fillAmount = 1f; // 전체 채움
                }
                else if (index == Mathf.Floor(hp) && hp % 1 != 0) // hp가 반칸인 경우
                {
                    filledHeart[index].gameObject.SetActive(true);
                    blankHeart[index].gameObject.SetActive(true);
                    filledHeart[index].fillAmount = 0.5f; // 반칸 채움
                }
                else
                {
                    filledHeart[index].gameObject.SetActive(false); // 비활성화
                    blankHeart[index].gameObject.SetActive(true);
                }

            }
        }


    }



    public void PauseUi(bool pause)
    {
        pauseUi.SetActive(pause);
        isPauseCheck = pause;
    }

    public void GetActive(Item item)
    {
        activeItemUi.sprite = item.itemImage;
        charge = item.itemEnergy;
        chargeMax = charge;

        activeEnergyBackGroundUi.sprite = activeEnergryBar[(int)(charge - 1)];
        activeEnergyUi.fillAmount = charge / chargeMax;

        activeItemUi.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        activeEnergyUi.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        activeEnergyBackGroundUi.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
    public void UpdateAcitvEnergyUi(Item item)
    {
        charge = item.itemEnergy;
        chargeMax = item.itemEnergyMax;
        if (0 < charge && 0 < chargeMax)
        {
            activeEnergyUi.fillAmount = charge / chargeMax;
        }
        else 
        {
            activeEnergyUi.fillAmount = 0;
        }
    }




}

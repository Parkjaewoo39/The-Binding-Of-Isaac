using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOneInitialized : MonoBehaviour
{
    // 씬1의 초기화를 담당하는 함수
    public static void InitializeScene1()
    {
       
        InitializeIntroSound();
        InitializeTitleMoving();
        InitializeChoiceMenu();
        InitializeOption();
        GameManager.Instance.Pause(false);
    }

    // 인트로 사운드 초기화 함수
    private static void InitializeIntroSound()
    {
        IntroSoundController introSoundController = FindObjectOfType<IntroSoundController>();
        if (introSoundController != null)
        {
            introSoundController.Reset(); 
        }
    }

    // 타이틀 무빙 초기화 함수
    private static void InitializeTitleMoving()
    {
        TitleMoving titleMoving = FindObjectOfType<TitleMoving>();
        if (titleMoving != null)
        {
            titleMoving.Reset(); 
        }
    }

    // 초이스 메뉴 초기화 함수
    private static void InitializeChoiceMenu()
    {
        ChoiceMenu choiceMenu = FindObjectOfType<ChoiceMenu>();
        if (choiceMenu != null)
        {
            choiceMenu.Reset();
        }
    }

    // 옵션 초기화 함수
    private static void InitializeOption()
    {
        Option option = FindObjectOfType<Option>();
        if (option != null)
        {
            option.Reset(); 
        }
    }
}
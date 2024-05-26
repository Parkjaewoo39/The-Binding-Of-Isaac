using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneOneInitialized : MonoBehaviour
{
    // ��1�� �ʱ�ȭ�� ����ϴ� �Լ�
    public static void InitializeScene1()
    {
       
        InitializeIntroSound();
        InitializeTitleMoving();
        InitializeChoiceMenu();
        InitializeOption();
        GameManager.Instance.Pause(false);
    }

    // ��Ʈ�� ���� �ʱ�ȭ �Լ�
    private static void InitializeIntroSound()
    {
        IntroSoundController introSoundController = FindObjectOfType<IntroSoundController>();
        if (introSoundController != null)
        {
            introSoundController.Reset(); 
        }
    }

    // Ÿ��Ʋ ���� �ʱ�ȭ �Լ�
    private static void InitializeTitleMoving()
    {
        TitleMoving titleMoving = FindObjectOfType<TitleMoving>();
        if (titleMoving != null)
        {
            titleMoving.Reset(); 
        }
    }

    // ���̽� �޴� �ʱ�ȭ �Լ�
    private static void InitializeChoiceMenu()
    {
        ChoiceMenu choiceMenu = FindObjectOfType<ChoiceMenu>();
        if (choiceMenu != null)
        {
            choiceMenu.Reset();
        }
    }

    // �ɼ� �ʱ�ȭ �Լ�
    private static void InitializeOption()
    {
        Option option = FindObjectOfType<Option>();
        if (option != null)
        {
            option.Reset(); 
        }
    }
}
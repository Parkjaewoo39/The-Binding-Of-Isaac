using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;
    public bool isFullScreen = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
       // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            ScreenChange(isFullScreen);
            isFullScreen = !isFullScreen;
        }
    }
    public void ScreenChange(bool isbool) 
    {
        if (isFullScreen) 
        {
            Screen.SetResolution(1980, 1080, FullScreenMode.FullScreenWindow);
        }
        else 
        {
            Screen.SetResolution(1980, 1080, FullScreenMode.Windowed);
        }
    }
}

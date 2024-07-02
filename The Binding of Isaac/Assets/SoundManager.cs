using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float bgmVolume;
    [SerializeField] private float effectVolume;
    public AudioSource bgmSource;
    public AudioClip[] bgmList;
    public AudioSource effectSource;
    public AudioClip[] effectList;
    public bool isTitle = false;

    private Dictionary<string, float> bgmMaxVolumes = new Dictionary<string, float>();
    private Dictionary<string, float> effectMaxVolumes = new Dictionary<string, float>();
    public enum SoundType 
    {
        BGM,
        EFFECT,
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            foreach (var clip in bgmList)
            {
                bgmMaxVolumes[clip.name] = 1.0f;
            }
            foreach (var clip in effectList)
            {
                effectMaxVolumes[clip.name] = 1.0f; 
            }
        }
        else 
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    private  void Update()
    {
        
        if (Input.anyKeyDown && isTitle ) 
        {
            ChangeTitleMusic();
        }
    }

    private void OnSceneLoaded(Scene name, LoadSceneMode loadName) 
    {
        for (int i = 0; i < bgmList.Length; i++) 
        {
            if (name.name == bgmList[i].name) 
            {                
                BgmPlay(bgmList[i]);
            }
        }
    }
    public void BgmPlay(AudioClip audioClip) 
    {
        switch (audioClip.name) 
        {
            case "00.TitleScene":
                bgmSource.clip = audioClip;
                bgmSource.loop = true;
                bgmSource.volume = 0.5f;
                bgmSource.Play();
                isTitle = true;
                break;
            case "01.BaseMent":
                bgmSource.clip = audioClip;
                bgmSource.loop = true;
                bgmSource.volume = 0.05f;
                bgmSource.Play();
                break;

        }
    }

    public void ChangeTitleMusic() 
    {
        isTitle = false;
        AudioClip otherSound = null;
        foreach (AudioClip clip in bgmList) 
        {
            if (clip.name == "TitleScreen") 
            {
                otherSound = clip;
                break;
            }
        }
        if (otherSound != null) 
        {
            bgmSource.clip = otherSound;
            bgmSource.loop = true;
            bgmSource.volume = 0.15f;
            bgmSource.Play();
        }
        
    }
    public void BgmVolumeController(float _volume)
    {
        bgmSource.volume += _volume;
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSourse;
    public Slider musicSlider, sfxSlider;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Theme");
        musicSlider.onValueChanged.AddListener(delegate { MusicVolumeChangeCheck(); });
        sfxSlider.onValueChanged.AddListener(delegate { SFXVolumeChangeCheck(); });
    }
    public void PlayMusic(string name)
    {

        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not found: "+name);
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s != null)
        {
            Debug.Log("sfx Not found");
        }
        else
        {
            sfxSourse.PlayOneShot(s.clip);
        }
    }
    public void MusicVolumeChangeCheck()
    {
        Debug.Log(musicSlider.value);
        musicSource.volume = musicSlider.value;
    }
    public void SFXVolumeChangeCheck()
    {
        Debug.Log(sfxSlider.value);
        sfxSlider.value = sfxSlider.value;
    }
}

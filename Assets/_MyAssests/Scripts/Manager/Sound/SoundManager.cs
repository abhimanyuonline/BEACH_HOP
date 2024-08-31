using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSourse;
    public Slider musicSlider, sfxSlider;

    public Button musicButton;
    public Sprite unmuteVolumeSprite;
    public Sprite muteVolumeSprite;


    private void Awake()
    {
       
    }
    private void Start()
    {
        PlayMusic("Theme");
        musicSlider.onValueChanged.AddListener(delegate { MusicVolumeChangeCheck(); });
        sfxSlider.onValueChanged.AddListener(delegate { SFXVolumeChangeCheck(); });


        var currentVolume = PlayerPrefs.GetInt("Volume");
        if (currentVolume >= 0)
        {
            musicButton.GetComponent<Image>().sprite = unmuteVolumeSprite;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = muteVolumeSprite;
        }
        musicSource.GetComponent<AudioSource>().volume = currentVolume;
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
    public void UpdateMuteUnmuteSettting()
    {
        var currentVolume = PlayerPrefs.GetInt("Volume");
        Debug.Log("Current Volume"+currentVolume);
        if (currentVolume >= 0)
        {
            musicButton.GetComponent<Image>().sprite = muteVolumeSprite;
            PlayerPrefs.SetInt("Volume", -1);
            
        }
        else
        {
            PlayerPrefs.SetInt("Volume", 1);
            musicButton.GetComponent<Image>().sprite = unmuteVolumeSprite;
        }
        musicSource.GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("Volume");

    }
}

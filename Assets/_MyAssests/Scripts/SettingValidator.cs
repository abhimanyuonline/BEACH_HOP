using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingValidator : MonoBehaviour
{
    [SerializeField] private Sprite[] soundToogleImages;
    [SerializeField] private Button soundToogleButton;

    [SerializeField] private Sprite[] charToogleImages;
    [SerializeField] private Button[] chardToogleButton;  
    [SerializeField] private Image selectedCharImage;
    [SerializeField] private SoundManager soundManager;

    int selectedCharIndex;
    int currentVolume;



    void Start(){
        foreach (var item in chardToogleButton)
        {
            item.onClick.AddListener(delegate { UpdateCharSelection(); });
        }
        soundToogleButton.onClick.AddListener (delegate { UpdateMuteUnmuteSettting(); });
    }
    private void OnEnable()
    {
        selectedCharIndex = PlayerPrefs.GetInt("CharIndex");
        selectedCharImage.GetComponent<Image>().sprite = charToogleImages[selectedCharIndex];

        currentVolume = PlayerPrefs.GetInt("Volume");
        if (currentVolume >= 0)
        {
            soundToogleButton.GetComponent<Image>().sprite = soundToogleImages[0];
        }
        else
        {
            soundToogleButton.GetComponent<Image>().sprite = soundToogleImages[1];
        }
        Debug.Log(selectedCharIndex+"Enabled"+ currentVolume);

    }
    void UpdateCharSelection(){
        
        if (selectedCharIndex == 0)
        {
            selectedCharIndex++;
        }
        else
        {
            selectedCharIndex = 0;
        }
        selectedCharImage.GetComponent<Image>().sprite = charToogleImages[selectedCharIndex];
        
    }

    public void UpdateMuteUnmuteSettting()
    {

        if (currentVolume >= 0)
        {
            currentVolume = -1;
            soundToogleButton.GetComponent<Image>().sprite = soundToogleImages[1];
        }
        else
        {
            
            currentVolume = 1;
            soundToogleButton.GetComponent<Image>().sprite = soundToogleImages[0];
        }
        soundManager.musicSource.GetComponent<AudioSource>().volume = currentVolume;

    }
    public void SaveData()
    {
        Debug.Log(selectedCharIndex + "Save" + currentVolume);
        PlayerPrefs.SetInt("CharIndex", selectedCharIndex);
        PlayerPrefs.SetInt("Volume", currentVolume);
    }
}

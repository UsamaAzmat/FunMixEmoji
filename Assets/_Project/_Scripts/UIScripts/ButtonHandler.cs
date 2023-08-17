using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Sprite toggleSprite;
    private Sprite originalSprite;
    public GameObject toggleGameObject;
    private bool isEnabled = false;

    private Image buttonImage;
    public buttonType ButtonType;


    public Toggle soundBtn, vibrateBtn, musicBtn;

    private void OnEnable()
    {
        if (ButtonType == buttonType.sound)
        {
            buttonImage.sprite = PlayerPrefsManager.SoundPref == 1 ? toggleSprite : originalSprite;
        }
        if (ButtonType == buttonType.music)
        {
            buttonImage.sprite = PlayerPrefsManager.MusicPref == 1 ? toggleSprite : originalSprite;
        }
        if (ButtonType == buttonType.vibrate)
        {
            buttonImage.sprite = PlayerPrefsManager.VibratePref == 1 ? toggleSprite : originalSprite;
        }
    }



    //public void enableSound(bool status)
    //{
    //    if (status)
    //    {
    //        PlayerPrefsManager.SoundPref = 0;
    //    }
    //    else
    //    {
    //        PlayerPrefsManager.SoundPref = 1;
    //    }
    //    SoundsManager.instSound?.SoundCheck();
    //}


    //public void enableMusic(bool status)
    //{
    //    if (status)
    //    {
    //        PlayerPrefsManager.MusicPref = 0;
    //    }
    //    else
    //    {
    //        PlayerPrefsManager.MusicPref = 1;
    //    }
    //}

    //public void enableVibrate(bool status)
    //{
    //    if (status)
    //    {
    //        PlayerPrefsManager.VibratePref = 0;
    //        SoundsManager.instSound.HepticPlay(MoreMountains.NiceVibrations.HapticTypes.HeavyImpact);
    //    }
    //    else
    //    {
    //        PlayerPrefsManager.VibratePref = 1;
    //    }
    //}

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        originalSprite = buttonImage.sprite;

        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Toggle the state
        isEnabled = !isEnabled;

        // Update the button's sprite
        buttonImage.sprite = isEnabled ? toggleSprite : originalSprite;

        // Set the active state of the toggle game object
        toggleGameObject.SetActive(isEnabled);

        switch (ButtonType)
        {
            case buttonType.sound:
                PlayerPrefsManager.SoundPref = isEnabled ? 1 : 0;
                SoundsManager.instSound?.SoundCheck();
                break;
            case buttonType.music:
                PlayerPrefsManager.MusicPref = isEnabled ? 1 : 0;
                break;
            case buttonType.vibrate:
                PlayerPrefsManager.VibratePref = isEnabled ? 1 : 0;
                break;
        }

        //switch (ButtonType)
        //{
        //    case buttonType.sound:
        //        PlayerPrefsManager.SoundPref = 1;
        //        break;
        //    case buttonType.music:
        //        PlayerPrefsManager.MusicPref = 1;
        //        break;
        //    case buttonType.vibrate:
        //        PlayerPrefsManager.VibratePref = 1;
        //        break;

        //}


    }
}

using UnityEngine;
using MoreMountains.NiceVibrations;
public class SoundsManager : MonoBehaviour
{
    #region singleton
    public static SoundsManager instSound;
    private void Awake()
    {
        if (instSound == null)
        {
            instSound = this;
        }
    }
    #endregion

    [Space]
    public AudioSource bgAudio;
    public AudioSource loopedSoundSource;
    [Space]


    #region AudioClips
    public AudioClip /*MainMenuSound,*//* ModeSelectionSound, LevelSelectionSound,*/ GamePlaySound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip mergeSound;
    [SerializeField] private AudioClip shuffleSound;
    [SerializeField] private AudioClip emojiPopSound;
    [SerializeField] private AudioClip mergeCompleteSound;
    [SerializeField] private AudioClip sketchingSound;
    [SerializeField] private AudioClip paintDoneSound;

    #endregion

    private void OnEnable()
    {
        GamePlaySoundPlay();
    }



    public void GamePlaySoundPlay()
    {
        bgAudio.clip = GamePlaySound;
        SoundCheck();

    }
    public void SoundCheck()
    {
        if (PlayerPrefsManager.SoundPref == 0)
        {
            bgAudio.Play();

        }
        else if (PlayerPrefsManager.SoundPref == 1)
        {
            bgAudio.Stop();

        }
    }



    public void PlayTheSound(soundType currentSound, bool loop = false)
    {
        if (PlayerPrefsManager.MusicPref == 1) return;

        AudioClip soundClip = null;
        float temVolume = 1;
        switch (currentSound)
        {
            case soundType.click:
                soundClip = clickSound;
                break;
            case soundType.mergeClick:
                soundClip = mergeSound;
                break;
            case soundType.shuffling:
                soundClip = shuffleSound;
                break;
            case soundType.emojiPop:
                soundClip = emojiPopSound;
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                break;
            case soundType.mergeDone:
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                soundClip = mergeCompleteSound;
                break;
            case soundType.sketching:
                soundClip = sketchingSound;
                break;
            case soundType.paintDone:
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
                soundClip = paintDoneSound;
                break;

        }
        if (soundClip != null)
        {
            if (loop)
            {
                loopedSoundSource.clip = soundClip;
                if (!loopedSoundSource.isPlaying) loopedSoundSource.Play();
            }
            else
            {
                //loopedSoundSource.loop = false;
                //loopedSoundSource.clip = soundClip;
                //loopedSoundSource.volume = temVolume;
                //loopedSoundSource.PlayOneShot(soundClip);
                // Play the sound once
                AudioSource.PlayClipAtPoint(soundClip, transform.position, temVolume);
            }
        }
    }

    public void StopLoopedSound()
    {
        if (loopedSoundSource != null)
        {
            loopedSoundSource.Stop();
            // Destroy(loopedSoundSource);
            // loopedSoundSource = null;
        }
    }

    bool setVolumeDown;
    public void changeSoundVolume()
    {
        setVolumeDown = !setVolumeDown;
        switch (setVolumeDown)
        {
            case true:
                bgAudio.volume = 0.05f;
                break;
            case false:
                bgAudio.volume = 0.15f;
                break;
        }
    }

    public void HepticPlay(HapticTypes hpType)
    {
        MMVibrationManager.Haptic(hpType);
    }


}

using UnityEngine;
using UnityEngine.UI;
using System;

public class SkinInfo : MonoBehaviour
{
    //public bool isRewarded;
    public int id;
    public GameObject adImg, tickImg;
    public int str;
    PencilHandler pen;
    public Sprite initialSp, finalSp;
    private void Awake()
    {
        pen = FindObjectOfType<PencilHandler>();
        pen.updateGlowPosition();
        //tickImg.SetActive(false);
        //if (id == 0)
        //{
        //    adImg.SetActive(false);
        //    tickImg.SetActive(true);
        //}
    }



    private void OnEnable()
    {
        str = PlayerPrefs.GetInt("SkinLock" + id);

        // if ((id == 2 && PlayerPrefs.GetInt("SkinLock" + id) == 0) || (id == 3 && PlayerPrefs.GetInt("SkinLock" + id) == 0))
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(UpdateSkin);
        if (str == 0)
        {
            if (id == 0) return;
            PlayerPrefs.SetInt("SkinLock" + id, 1);
            adImg.SetActive(true);
        }
    }
    void UpdateSkin()
    {
        if (PlayerPrefs.GetInt("SkinLock" + id) == 1)
        {
            UIManager._currentSkin = this;

            //PlayerPrefsManager.currentRewardCount = 3;
            PlayerPrefs.SetString("RewardType", "ShopItem");
            RemoteValues.Instance.Show_MAX_RewardedVideo();
            return;
        }
        PlayerPrefsManager.PlayerSkin = transform.GetSiblingIndex();
        SoundsManager.instSound?.PlayTheSound(soundType.mergeClick);
        pen.updateGlowPosition();
    }

    public void UpdateByReward()
    {
        PlayerPrefs.SetInt("SkinLock" + id, 2);
        if (adImg) adImg.SetActive(false);
        PlayerPrefsManager.PlayerSkin = transform.GetSiblingIndex();
        SoundsManager.instSound?.PlayTheSound(soundType.mergeClick);
        pen.updateGlowPosition();
        UIManager._currentSkin = null;
        EventsManager.UppdateClicked();
    }
}

using UnityEngine;
public class OnRewardedComplete : MonoBehaviour
{
    public static OnRewardedComplete instance;
    string RewardType;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void GiveReward()
    {
        RewardType = PlayerPrefs.GetString("RewardType");
        RemoteValues.Instance.LogCustomEvent(RewardType + "_Rewarded");
        if (RewardType == "ShopItem")
        {
            UIManager._currentSkin.UpdateByReward();
            PlayerPrefs.SetString("RewardType", "none");
        }

        if (RewardType == "ColorClick")
        {
            EventsManager.ColorChange(DrawManager.ColortoShow);
            PlayerPrefs.SetString("RewardType", "none");
        }

        if (RewardType == "SkipDrawLevel")
        {
            DrawManager.Instance.NextLevelDraw(true);
            PlayerPrefs.SetString("RewardType", "none");
        }
        if (RewardType == "SkipMatchLevel")
        {
            LevelManager.Instance.OnSkipMatchAd();
            PlayerPrefs.SetString("RewardType", "none");
        }

    }
}


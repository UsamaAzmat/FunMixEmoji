using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
//using Unity.RemoteConfig;
using UnityEngine.Networking;

public class RemoteValues : MonoBehaviour
{
    public static RemoteValues Instance;
    public MaxMediationController MAXManager;



    bool isShowingBanner;
    bool isShowingInter;
    bool isShowingRewarded;



    public bool isShowingAppOpen;
    public int minFrequency;
    public int maxFrequency;
    public bool isShowingAdmob;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        FetchRemoteConfiguration();
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        GameAnalytics.Initialize();
    }


    public void FetchRemoteConfiguration()
    {
        if (gameObject.activeSelf)
        {
            // ConfigManager.FetchCompleted += ApplyRemoteSettings;
            // ConfigManager.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), new appAttributes());
            Debug.Log("Fetched Settings");
        }
    }







    public void Show_MAX_RewardedVideo()
    {
        //if (!isShowingRewarded)
        //{
        //    return;
        //}
        UIManager.ResetCounterCLicks();
        MAXManager.ShowRewardedAd();
    }

    public void Show_MAX_Interstital()
    {
        //if (!isShowingInter)
        //{
        //    return;
        //}
        LogCustomEvent("Interstitial Showed");
        UIManager.ResetCounterCLicks();
        MAXManager.ShowInterstitial();
    }

    public void showBanner()
    {
        //if (!isShowingBanner)
        //{
        //    return;
        //}
        isShowingAdmob = false;
        MAXManager.ShowBanner();
    }

    public void HideBanner()
    {


        MAXManager.HideBanner();


    }











    #region ANALYTICS EVENTS

    public void LogResourceEvent(bool isAdd, float Ammount, string ItemType)
    {
        if (isAdd)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Coins", Ammount, ItemType, "");
        }
        else
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Coins", Ammount, ItemType, "");
        }
    }

    public void LogTaskStart(string taskName)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, taskName);
    }

    public void LogTaskFailed(string taskName, float taskScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, taskName, taskScore.ToString());
    }

    public void LogTaskSuccessful(string taskName, float taskScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, taskName, taskScore.ToString());
    }


    #region FUTX_Events


    public void LogOpenFirst_FTUX()
    {
        GameAnalytics.NewDesignEvent("Open_First_FTUX");
    }

    public void LogCustomFTUXEvent(string eventString)
    {
        GameAnalytics.NewDesignEvent(eventString);
    }

    public void LogLevelStart_FTUX(string levelName, int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "FTUX_" + levelName + levelNumber.ToString());
    }

    public void LogLevelCompleteFailed_FTUX(string levelName, int levelNumber, float levelScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "FTUX_" + levelName + levelNumber.ToString(), (int)levelScore);
    }

    public void LogLevelCompleteSuccessful_FTUX(string levelName, int levelNumber, float levelScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "FTUX_" + levelName + levelNumber.ToString(), (int)levelScore);
    }

    #endregion

    #region Design_Events

    public void LogOpenFirst()
    {
        GameAnalytics.NewDesignEvent("Open_First");
    }

    public void LogGameStart()
    {
        GameAnalytics.NewDesignEvent("Game_Start");
    }

    public void LogPlayerProfile()
    {
        GameAnalytics.NewDesignEvent("Player_Profile");
    }

    public void LogPlayButton()
    {
        GameAnalytics.NewDesignEvent("Play_Button");
    }

    public void LogCharacterSelected()
    {
        GameAnalytics.NewDesignEvent("Character_Selected");
    }

    public void LogVehicle_Selected()
    {
        GameAnalytics.NewDesignEvent("Vehicle_Selected");
    }

    public void LogWeapon_Selected()
    {
        GameAnalytics.NewDesignEvent("Weapon_Selected");
    }

    public void LogGameCompleteHome()
    {
        GameAnalytics.NewDesignEvent("Game_Complete_Home");
    }

    public void LogGameCompleteRestart()
    {
        GameAnalytics.NewDesignEvent("Game_Complete_Restart");
    }

    public void LogGameCompleteNext()
    {
        GameAnalytics.NewDesignEvent("Game_Complete_Next");
    }

    public void LogGameFailedHome()
    {
        GameAnalytics.NewDesignEvent("Game_Failed_Home");
    }

    public void LogGameFailedRestart()
    {
        GameAnalytics.NewDesignEvent("Game_Failed_Restart");
    }

    public void LogGamePauseHome()
    {
        GameAnalytics.NewDesignEvent("Game_Pause_Home");
    }

    public void LogGamePauseRestart()
    {
        GameAnalytics.NewDesignEvent("Game_Pause_Restart");
    }

    public void LogGamePauseResume()
    {
        GameAnalytics.NewDesignEvent("Game_Pause_Resume");
    }

    public void LogSelectedLevel(int selectedLevelNumber)
    {
        GameAnalytics.NewDesignEvent("Selected_Level_" + selectedLevelNumber);
    }

    public void LogSelectedMode(int selectedModeNumber)
    {
        GameAnalytics.NewDesignEvent("Selected_Mode_" + selectedModeNumber);
    }

    public void LogModeNSelectedLevel(int selectedModeNumber, int selectedLevelNumber)
    {
        GameAnalytics.NewDesignEvent("Mode" + selectedModeNumber + "_Selected_Level_" + selectedLevelNumber);
    }
    // Mode1_Env1_Selected_Level_1

    public void LogSelectedEnviornment(int selectedEnvNumber)
    {
        GameAnalytics.NewDesignEvent("Selected_Env_" + selectedEnvNumber);
    }

    public void LogSelectedEnviornmentMode(int selectedEnvNumber, int selectedModeNumber)
    {
        GameAnalytics.NewDesignEvent("Selected_Env" + selectedEnvNumber + "_Mode" + selectedModeNumber);
    }

    public void LogSelectedEnviornmentModeLevel(int selectedModeNumber, int selectedEnvNumber, int selectedLevelNumber)
    {
        GameAnalytics.NewDesignEvent("Mode" + selectedModeNumber + "_Env" + selectedEnvNumber + "_Selected_Level_" + selectedLevelNumber);
    }

    #endregion

    #region Progression_Event


    /************************ Progression Start Events************************/
    // Start_Level_1 
    public void LogLevelStart(int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Start_Level_" + levelNumber.ToString());
    }
    // Mode1_Start_Level_1,
    public void LogLevelStart(int modeNumber, int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Mode" + modeNumber + "_Start_Level_" + levelNumber.ToString());
    }
    //Mode1_Env1_Start_Level_1
    public void LogLevelStart(int modeNumber, int envNumber, int levelNumber)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Mode" + modeNumber + "_Env" + envNumber + "_Start_Level_" + levelNumber.ToString());
    }

    /************************ Progression Failed Events************************/

    //Failed_Level_1
    public void LogLevelCompleteFailed(int levelNumber, float levelScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Failed_Level_" + levelNumber.ToString(), (int)levelScore);

    }
    //Mode1_Failed_Level_1
    public void LogLevelCompleteFailed(int modeNumber, int levelNumber, float levelScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Mode" + modeNumber + "_Failed_Level_" + levelNumber.ToString(), (int)levelScore);

    }
    //Mode1_Env1_Failed_Level_1
    public void LogLevelCompleteFailed(int modeNumber, int envNumber, int levelNumber, float levelScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Mode" + modeNumber + "_Env" + envNumber + "_Failed_Level_" + levelNumber.ToString() + levelNumber.ToString(), (int)levelScore);
    }

    /************************ Progression Complete Events************************/

    //Complete_Level_1
    public void LogLevelCompleteSuccessful(int levelNumber, float levelScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Complete_Level_" + levelNumber.ToString(), (int)levelScore);
    }
    //Mode1_Complete_Level_1
    public void LogLevelCompleteSuccessful(int modeNumber, int levelNumber, float levelScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Mode" + modeNumber + "_Complete_Level_" + levelNumber.ToString(), (int)levelScore);
    }
    //Mode1_Env1_Complete_Level_1
    public void LogLevelCompleteSuccessful(int modeNumber, int envNumber, int levelNumber, float levelScore)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Mode" + modeNumber + "_Env" + envNumber + "_Complete_Level_" + levelNumber.ToString() + levelNumber.ToString(), (int)levelScore);
    }

    #endregion

    /// <summary>
    /// The string can be written only with a-zA-Z0-9 characters, no space, no special characters.
    /// </summary>
    public void LogCustomEvent(string eventName)
    {
        GameAnalytics.NewDesignEvent(eventName);

    }




    public void LogShopPurchaseEvent(int price, string itemID, string itemName)
    {
        GameAnalytics.NewBusinessEvent("Virtual Currency", price, "Shop Purchase", itemID, itemName);
    }

    public void LogIAPPurchaseEvent(int price, string itemID, string itemName)
    {
        GameAnalytics.NewBusinessEvent("USD", price, "IAP Purchase", itemID, itemName);
    }

    public void LogRewardEvent(float rewardAmount, string rewardType)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "VirtualCurrency", rewardAmount, rewardType, "");
    }
    #endregion

    #region RemoteConfig


    public struct userAttributes { }
    public struct appAttributes { }

    //void ApplyRemoteSettings(ConfigResponse configResponse)
    //{
    //    switch (configResponse.requestOrigin)
    //    {
    //        case ConfigOrigin.Default:
    //            isShowingBanner = true;
    //            isShowingInter = true;
    //            isShowingRewarded = true;


    //            isShowingAppOpen = true;
    //            minFrequency = 1;
    //            maxFrequency = 1;


    //            MAXManager.enabled = true;



    //            break;

    //        case ConfigOrigin.Cached:



    //            break;

    //        case ConfigOrigin.Remote:



    //            GetServerPlacementInfoResponse();

    //            break;
    //    }

    //}

    public void GetServerPlacementInfoResponse()
    {




        //isShowingBanner = ConfigManager.appConfig.GetBool("isShowingBanner", true);
        //isShowingBanner = ConfigManager.appConfig.GetBool("isShowingMediumBanner", true);
        //isShowingInter = ConfigManager.appConfig.GetBool("isShowingInter", true);
        //isShowingRewarded = ConfigManager.appConfig.GetBool("isShowingRewarded", true);

        //isShowingAppOpen = ConfigManager.appConfig.GetBool("isShowingAppOpen", true);
        //minFrequency = ConfigManager.appConfig.GetInt("minFrequency", 1);
        //maxFrequency = ConfigManager.appConfig.GetInt("maxFrequency", 1);


        MAXManager.enabled = true;






    }
    #endregion


}




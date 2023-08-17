using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetChecker : MonoBehaviour
{
    public GameObject internetCheckerPanel;

    // Update is called once per frame
    void Update()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            internetCheckerPanel.SetActive(false);
        }
        else
        {
            internetCheckerPanel.SetActive(true);
        }
    }

    public void ExitGame()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            OpenWifiSettings();
        }
        else
        {
            Application.Quit();
        }
    }

    private void OpenWifiSettings()
    {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", "android.settings.WIFI_SETTINGS");

        activity.Call("startActivity", intentObject);
    }
}

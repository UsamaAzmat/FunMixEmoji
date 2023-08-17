using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TutorialHandler : MonoBehaviour
{
    public List<GameObject> tutorialObj = new List<GameObject>();
    public int indexer;
    public float waitTime = 0.2f;
    public static TutorialHandler Instance;
    public UnityEvent[] onShow;
    internal static bool isTutorial;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        isTutorial = true;
        showNextTutorial();
    }

    public void showNextTutorial(float waitTime = 0.2f)
    {
        if (PlayerPrefsManager.tutorialPrefs > 0) return;

        // 
        //  RemoteValues.Instance.LogCustomEvent("Profile_start");
        if (isTutorial)
        {
            Invoke("showTutorial", waitTime);
            isTutorial = false;
        }
    }

    private void showTutorial()
    {
        if (indexer >= tutorialObj.Count)
        {
            //RemoteValues.Instance.LogCustomEvent("Profile_End");
            PlayerPrefsManager.tutorialPrefs += 2;
            return;
        }
        foreach (var item in tutorialObj)
        {
            item.SetActive(false);
        }
        tutorialObj[indexer].SetActive(true);
        if (onShow[indexer] != null) onShow[indexer].Invoke();
        //MainMenu.Instance?.CallBackEvents();
        indexer++;
        isTutorial = true;

    }


    public void HideAllTutorials()
    {
        foreach (var item in tutorialObj)
        {
            item.SetActive(false);
        }
        //MainMenu.Instance?.CallBackEvents();
    }

}

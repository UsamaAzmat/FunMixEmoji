using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using System;

public class DrawManager : MonoBehaviour
{
    #region singleton
    public static DrawManager Instance;
    private void Awake()
    {
        //if (Instance == null)
        Instance = this;
    }
    #endregion

    [ShowInInspector] public static Color ColortoShow;

    [ReadOnly] public GameObject currentLevel;
    public List<GameObject> Levels = new List<GameObject>();
    public static HashSet<int> displayedColors = new HashSet<int>();

    [Space]
    public TextMeshProUGUI levelText;
    public GameObject tapToStart, winScreen, genericView, drawGamePlay, holdText, dragToPaint;


    private void OnEnable()
    {
        EventsManager.OnDrawWin += DrawWin;
        EventsManager.onMouseDown += MouseEnter;
    }


    private void OnDisable()
    {
        EventsManager.OnDrawWin -= DrawWin;
        EventsManager.onMouseDown -= MouseEnter;
    }

    private void MouseEnter()
    {
        holdText.SetActive(false);
    }

    // this will enable the controls everytime we click back btn
    public void EnableDrawControls()
    {
        tapToStart.SetActive(false);
        drawGamePlay.SetActive(true);
        GameObject obj = Instantiate(Levels[PlayerPrefsManager.LevelNumberDraw], this.transform);
        levelText.text = "LEVEL " + (PlayerPrefsManager.LevelNumberDraw + 1).ToString();
        currentLevel = obj;
        UIManager.Instance.LowerBtnHandler.SetActive(false);
        EventsManager.UppdateClicked();
        RemoteValues.Instance.LogCustomEvent("Mode_2_Level_" + PlayerPrefsManager.LevelNumberDraw.ToString() + "_start");
    }

    // destroyer method of current level
    public void DisableLevel()
    {
        SoundsManager.instSound.StopLoopedSound();
        displayedColors.Clear();
        UIManager.Instance.LowerBtnHandler.SetActive(true);
        drawGamePlay.SetActive(false);
        if (currentLevel == null) return;
        Destroy(currentLevel.gameObject);
        currentLevel = null;
        tapToStart.SetActive(true);
        UIManager.Instance.UpdateClicks();

    }

    public void NextLevelDraw(bool isNext)
    {
        SoundsManager.instSound.PlayTheSound(soundType.click);
        SoundsManager.instSound.HepticPlay(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
        DisableLevel();
        winScreen.SetActive(false);
        UIManager.Instance.DrawWinPanel.SetActive(false);
        if (isNext)
        {
            if (PlayerPrefsManager.LevelNumberDraw >= Levels.Count - 1)
                PlayerPrefsManager.LevelNumberDraw = 0;
            else
                PlayerPrefsManager.LevelNumberDraw += 1;
        }

        // UIManager.Instance.changeUIEmoji(true);
        UIManager.Instance.DrawItemDisplay();
        // EventsManager.onDrawClicked();
        if (TutorialHandler.Instance)
            TutorialHandler.Instance.showNextTutorial(0.5f);

        /////// Interstitial
    }



    public void skipLevel()
    {
        PlayerPrefs.SetString("RewardType", "SkipDrawLevel");
        RemoteValues.Instance.Show_MAX_RewardedVideo();
    }


    private void DrawWin()
    {
        genericView.transform.position = new Vector3(0, 0, -15f);
        StartCoroutine(LevelCompleted());
    }
    IEnumerator LevelCompleted()
    {
        yield return new WaitForSeconds(0.5f);
        RemoteValues.Instance.Show_MAX_Interstital();
        winScreen.SetActive(true);
        RemoteValues.Instance.LogCustomEvent("Mode_2_Level_" + PlayerPrefsManager.LevelNumberDraw.ToString() + "_end");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;
using TMPro;
public class UIManager : MonoBehaviour
{
    #region singleton
    public static UIManager Instance;
    private void Awake()
    {
        //if (Instance == null)
        Instance = this;
        if (PlayerPrefsManager.privacyOpener == 0)
        {
            privacyPanel.SetActive(true);
        }
    }
    #endregion

    public static int ClicksCounter;
    public static int totalCounts;



    public static bool isDrawing = false;
    public static bool isMatch = false;
    public GameObject MergeLargeBtn, DrawLargeBtn, MatchLargeBtn;
    public GameObject LowerBtnHandler;
    public GameObject questionEmoji;
    public GameObject DrawWinPanel, DrawGamePlay;
    public GameObject[] levelEmoji, levelSumEmoji;
    public bool isMerger;
    public ParticleManager particleManager;
    internal static SkinInfo _currentSkin;

    private void Start()
    {

        if (isMerger) MergeItemsDisplay();
        else
            DrawItemDisplay();


        totalCounts = UnityEngine.Random.Range(9, 14);
    }

    private void OnEnable()
    {
        EventsManager.OnDrawWin += DrawWin;
    }
    private void OnDisable()
    {
        EventsManager.OnDrawWin -= DrawWin;
    }



    public void MergeItemsDisplay()
    {
        EventsManager.onMergeClicked();
        MergeLargeBtn.SetActive(true);
        DrawLargeBtn.SetActive(false);
        MatchLargeBtn.SetActive(false);
        isDrawing = isMatch = false;
    }
    public void DrawItemDisplay()
    {
        changeUIEmoji(true);
        EventsManager.onDrawClicked();
        MergeLargeBtn.SetActive(false);
        DrawLargeBtn.SetActive(true);
        MatchLargeBtn.SetActive(false);
        isDrawing = true;
    }

    public void MatchItemDisplay()
    {
        LowerBtnHandler.SetActive(true);
        isMatch = true;
        isDrawing = false;
        EventsManager.onMatchClicked();
        MergeLargeBtn.SetActive(false);
        DrawLargeBtn.SetActive(false);
        MatchLargeBtn.SetActive(true);
    }

    public void PlayOnBoardEnd()
    {
        MatchLargeBtn.GetComponent<DOTweenAnimation>().DORestartAllById("DotweenDone");
    }


    public void changeUIEmoji(bool isSecond)
    {
        foreach (var item in levelEmoji)
        {
            item.SetActive(false);
        }
        if (isSecond)
        {
            foreach (var item in levelSumEmoji)
            {
                item.SetActive(false);
            }
            questionEmoji.SetActive(true);
            levelSumEmoji[PlayerPrefsManager.LevelNumberDraw].SetActive(true);
            return;
        }
        questionEmoji.SetActive(false);
        levelEmoji[PlayerPrefsManager.LevelNumberDraw].SetActive(true);
    }



    public void PlayParticles()
    {
        particleManager.PlayRandomParticle();
    }


    private void DrawWin()
    {
        DrawWinPanel.SetActive(true);
        DrawGamePlay.SetActive(false);
    }


    public void privacyDisable()
    {
        PlayerPrefsManager.privacyOpener += 2;
        privacyPanel.SetActive(false);
    }

    public void UpdateClicks()
    {
        ClicksCounter += 1;
        if (ClicksCounter == totalCounts)
        {
            StartCoroutine(UpdateCountdown());
            //RemoteValues.Instance.Show_MAX_Interstital();
        }
    }


    public static void ResetCounterCLicks()
    {
        ClicksCounter = 0;
        totalCounts = UnityEngine.Random.Range(10, 14);
    }
    public GameObject loadAdingPanel, privacyPanel;
    public TextMeshProUGUI loadingText;

    private IEnumerator UpdateCountdown()
    {
        loadAdingPanel.SetActive(true);
        int countdownValue = 4;

        while (countdownValue > 0)
        {
            //foreach (var item in countdownText)
            //{
            //    item.SetActive(false);
            //}
            //countdownText[countdownValue - 1].SetActive(true);
            loadingText.text = countdownValue.ToString();
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }

        loadAdingPanel.SetActive(false);

        ResetCounterCLicks();
        RemoteValues.Instance.Show_MAX_Interstital();
        StopCoroutine("UpdateCountdown");
    }

    #region Sketching Inputs
    public void MouseEnter()
    {
        EventsManager.MouseClicked();
    }
    public void MouseUp()
    {
        EventsManager.MouseExit();
    }
    #endregion

}

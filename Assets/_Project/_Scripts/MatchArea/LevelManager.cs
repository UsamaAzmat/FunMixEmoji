using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{

    [TitleGroup("Puzzle Controls")]


    public static LevelManager Instance;
    public TextMeshProUGUI LevelNumberDisplay;
    [Space]
    [FoldoutGroup("LevelsList")]
    public List<GameObject> Levels = new List<GameObject>();

    [Space]
    [FoldoutGroup("Script References")]
    [ReadOnly] public MatchObject _MatchObject;

    [FoldoutGroup("Script References")]
    [ReadOnly] public ConnecterManager _Connector;

    [FoldoutGroup("Script References")]
    [ReadOnly] public LevelScrip _CurrentLevel;


    [Space]
    [FoldoutGroup("Level Complete Items")]
    public GameObject WinPopperPrefab;
    [FoldoutGroup("Level Complete Items")]
    public GameObject WinPanel;
    [FoldoutGroup("Level Complete Items")]
    public GameObject LosePanel;


    private void Awake()
    {
        Instance = this;
    }


    public void setUpLevel()
    {
        RemoveLevel();
        int currentLevel = PlayerPrefsManager.LevelNumberMatch;
        GameObject obj = Instantiate(Levels[PlayerPrefsManager.LevelNumberMatch], this.transform) as GameObject;
        obj.SetActive(false);
        _CurrentLevel = obj.GetComponent<LevelScrip>();
        _MatchObject = obj.GetComponent<MatchObject>();
        _Connector = obj.GetComponent<ConnecterManager>();
        _CurrentLevel.gameObject.SetActive(true);

        if (LevelNumberDisplay != null)
        {
            LevelNumberDisplay.text = ("LEVEL  " + (currentLevel + 1)).ToString();
        }
        RemoteValues.Instance.LogCustomEvent("Mode_3_Level_" + PlayerPrefsManager.LevelNumberMatch.ToString() + "_start");
        if (PlayerPrefsManager.LevelNumberMatch == 1) UIManager.Instance.PlayOnBoardEnd();

    }


    public void RemoveLevel()
    {
        if (_CurrentLevel == null) return;
        Destroy(_CurrentLevel.gameObject);
        _CurrentLevel = null;
        _Connector = null;
        _MatchObject = null;
    }


    public void OnSkipMatchAd()
    {
        // Rewarded Add Here
        if (PlayerPrefsManager.LevelNumberMatch >= Levels.Count - 1) PlayerPrefsManager.LevelNumberMatch = 0;
        else
            PlayerPrefsManager.LevelNumberMatch += 1;

        RemoveLevel();
        LosePanel.SetActive(false);
        WinPopperPrefab.SetActive(false);
        WinPanel.SetActive(false);
        UIManager.Instance.MatchItemDisplay();
    }
    public void SkipLevel_Puzzle()
    {
        PlayerPrefs.SetString("RewardType", "SkipMatchLevel");
        RemoteValues.Instance.Show_MAX_RewardedVideo();
    }
    public void LevelRestart_Puzzle(bool isWin)
    {
        PlayerPrefsManager.AdonMatch++;
        if (PlayerPrefsManager.AdonMatch % 2 == 0)
            RemoteValues.Instance.Show_MAX_Interstital();
        if (isWin)
        {
            TutorialHandler.Instance.showNextTutorial(0.1f);
            if (PlayerPrefsManager.LevelNumberMatch >= Levels.Count - 1)
            {
                PlayerPrefsManager.LevelNumberMatch = 0;
            }
            else
            {
                PlayerPrefsManager.LevelNumberMatch += 1;
            }
        }
        RemoveLevel();
        LosePanel.SetActive(false);
        WinPopperPrefab.SetActive(false);
        WinPanel.SetActive(false);
        UIManager.Instance.MatchItemDisplay();

    }
}

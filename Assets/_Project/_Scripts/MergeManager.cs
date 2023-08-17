using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using MagneticScrollView;
using System;

public class MergeManager : MonoBehaviour
{

    #region singleton
    public static MergeManager Instance;
    private void Awake()
    {
        //if (Instance == null)
        Instance = this;
        thisSp = GetComponent<SpriteRenderer>();
    }
    #endregion


    [FoldoutGroup("Emoji Selection")]
    [ReadOnly] public EmojiInfo leftEmoji, rightEmoji;
    public MagneticScrollRect leftScroll, rightScroll;
    SpriteRenderer thisSp;


    [Space]
    public GameObject combinePanel;
    public Image leftImg, rightImg;
    public Button MergeButton, favBtn;
    public SpriteRenderer finalImage;

    [Space]
    public GameObject[] MergeItems, DrawItems, MatchItems;
    Vector2Int vectorAddress;

    private void Start()
    {
        PlayerPrefsManager.mrgeCheck = PlayerPrefsManager.tutorialPrefs;
        if (PlayerPrefsManager.tutorialPrefs != 0)
            Invoke("PlayRandomization", 0.1f);
    }
    private void OnEnable()
    {
        MagneticScrollRect.onScrollingEvent += Scrolling;
        EventsManager.onDrawDisplay += DisplayDrawControls;
        EventsManager.onMergeDisplay += DisplayMergeControls;
        EventsManager.onMatchDisplay += DisplayMatchControls;
    }
    bool favClicked;


    private void OnDisable()
    {
        MagneticScrollRect.onScrollingEvent -= Scrolling;
        EventsManager.onDrawDisplay -= DisplayDrawControls;
        EventsManager.onMergeDisplay -= DisplayMergeControls;
        EventsManager.onMatchDisplay -= DisplayMatchControls;
    }

    private void Scrolling()
    {
        favClicked = false;
        finalImage.gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = true;
        MergeButton.interactable = false;
        favBtn.gameObject.SetActive(false);
    }

    internal void EnableButtons()
    {
        MergeButton.interactable = true;
    }



    public void MergeElements()
    {
        if (PlayerPrefsManager.mrgeCheck % 2 != 0)
        {
            // Debug.LogError("Usama");
            RemoteValues.Instance.Show_MAX_Interstital();
        }
        PlayerPrefsManager.mrgeCheck += 1;

        RemoteValues.Instance.LogCustomEvent("Mode_1_Merge_" + PlayerPrefsManager.mrgeCheck.ToString() + "Clicked");


        MergeButton.interactable = false;
        leftImg.sprite = leftEmoji.GetComponent<Image>().sprite;
        rightImg.sprite = rightEmoji.GetComponent<Image>().sprite;
        combinePanel.SetActive(true);
        string address = "Merger/" + (leftEmoji.ID + 1).ToString() + "/" + (rightEmoji.ID + 1).ToString();
        Sprite tempSp = Resources.Load<Sprite>(address);
        finalImage.sprite = tempSp;

        //Debug.LogError(leftEmoji.ID + " and " + rightEmoji.ID);
        vectorAddress = new Vector2Int(leftEmoji.ID, rightEmoji.ID);
        //PlayerPrefsManager.favCount += 1;
    }

    public void MergeCompleted()
    {
        favBtn.gameObject.SetActive(true);
        SoundsManager.instSound?.PlayTheSound(soundType.mergeDone);
        combinePanel.SetActive(false);
        finalImage.gameObject.SetActive(true);
        thisSp.enabled = false;
    }


    public void favrtClicked()
    {
        if (favClicked) return;
        favClicked = true;
        PlayerPrefsManager.favCount += 1;
        string cc = vectorAddress.x.ToString() + "," + vectorAddress.y;
        PlayerPrefs.SetString(PlayerPrefsManager.favCount.ToString(), cc);
    }


    void PlayRandomization()
    {
        if (leftScroll.gameObject.activeInHierarchy) leftScroll.StartAutoScroll();
        if (rightScroll.gameObject.activeInHierarchy) rightScroll.StartAutoScroll();
    }



    ////// Display the Merge And Draw Controls
    ///


    public void DisplayMergeControls()
    {
        leftScroll.ResetScroll();
        rightScroll.ResetScroll();
        UIManager.isDrawing = false;
        UIManager.isMatch = false;
        SoundsManager.instSound.PlayTheSound(soundType.click);
        foreach (var item in MergeItems)
        {
            item.SetActive(true);
        }
        foreach (var item in MatchItems)
        {
            item.SetActive(false);
        }
        thisSp.enabled = true;
        foreach (var item in DrawItems)
        {
            item.SetActive(false);
        }
        SoundsManager.instSound.StopLoopedSound();
        LevelManager.Instance.RemoveLevel();
    }

    public void DisplayDrawControls()
    {
        UIManager.isDrawing = true;
        UIManager.isMatch = false;
        LevelManager.Instance.RemoveLevel();
        SoundsManager.instSound.StopLoopedSound();
        SoundsManager.instSound.PlayTheSound(soundType.click);
        favBtn.gameObject.SetActive(false);
        finalImage.gameObject.SetActive(false);
        foreach (var item in MergeItems)
        {
            item.SetActive(false);
        }
        foreach (var item in MatchItems)
        {
            item.SetActive(false);
        }
        thisSp.enabled = false;
        foreach (var item in DrawItems)
        {
            item.SetActive(true);
        }
    }




    public void DisplayMatchControls()
    {
        SoundsManager.instSound.PlayTheSound(soundType.click);
        LevelManager.Instance.setUpLevel();
        SoundsManager.instSound.StopLoopedSound();
        favBtn.gameObject.SetActive(false);
        finalImage.gameObject.SetActive(false);
        foreach (var item in MergeItems)
        {
            item.SetActive(false);
        }
        foreach (var item in DrawItems)
        {
            item.SetActive(false);
        }
        thisSp.enabled = false;
        foreach (var item in MatchItems)
        {
            item.SetActive(true);
        }
    }

}

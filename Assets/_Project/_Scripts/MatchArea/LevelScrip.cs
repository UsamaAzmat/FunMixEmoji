using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

public class LevelScrip : MonoBehaviour
{


    [Space(30)]
    [PreviewField(100, Sirenix.OdinInspector.ObjectFieldAlignment.Left)]
    public List<GameObject> MatchableObjectsLeft;

    [Space(30)]
    [PreviewField(100, Sirenix.OdinInspector.ObjectFieldAlignment.Left)]
    public List<GameObject> MatchableObjectsRight;


    [HideInInspector] public bool DisableMousePos;

    Vector3 minScale;
    Vector3 maxScale;
    float speed = 2f;
    float duration = 5f;
    GameObject PopperRefObj;




    [HideInInspector] public List<Vector3> LeftIconPositions, RightIconPositions;
    private void Awake()
    {
        int indexCounter = 1;
        for (int i = 0; i < MatchableObjectsLeft.Count; i++)
        {
            MatchableObjectsLeft[i].transform.name = "Object" + indexCounter;
            MatchableObjectsRight[i].transform.name = "Object" + indexCounter++;
        }
    }


    private void Start()
    {
        DisableMousePos = false;
        minScale = new Vector3(0.0f, 0.0f, 1.0f);
        maxScale = new Vector3(0.6f, 0.6f, 1.0f);
        speed = 8f;
        duration = 8f;
    }

    public void AllObjectsMatched(bool win)
    {
        DisableMousePos = true;
        PlayerPrefsManager.tutorialPrefs += 1;

        if (win)
        {

            //PopperRefObj = Instantiate(LevelManager.Instance.WinPopperPrefab, transform);
            //  PopperRefObj.transform.position = new Vector3(0f, 0f, 1f);
            LevelManager.Instance.WinPopperPrefab.SetActive(true);
            StartCoroutine(ShowPanel(true));
            GiveRewardForLevel();
        }
        else
        {
            StartCoroutine(ShowPanel(false));
        }
    }

    public void GiveRewardForLevel()
    {


    }

    public IEnumerator ShowPanel(bool isWin)
    {
        UIManager.Instance.LowerBtnHandler.SetActive(false);

        RemoteValues.Instance.LogCustomEvent("Mode_3_Level_" + PlayerPrefsManager.LevelNumberMatch.ToString() + "_end");
        if (isWin)
        {
            yield return new WaitForSeconds(2f);
            LevelManager.Instance.WinPanel.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            LevelManager.Instance.LosePanel.SetActive(true);
        }
    }
    [Button]
    void setScaleToZero(float values)
    {
        foreach (var item in MatchableObjectsLeft)
        {
            item.transform.localScale = Vector3.zero;
        }
        foreach (var item in MatchableObjectsRight)
        {
            item.transform.localScale = Vector3.zero;
        }
    }


    #region Get Data for Emojis

    public GameObject leftData, rightData;
    [Button()]

    public void getLeftData()
    {
        SpriteRenderer[] sps = leftData.GetComponentsInChildren<SpriteRenderer>();
        MatchableObjectsLeft.Clear();
        for (int i = 0; i < sps.Length; i++)
        {
            MatchableObjectsLeft.Add(sps[i].gameObject);
        }
    }

    [Button()]
    public void getRightData()
    {
        SpriteRenderer[] sps = rightData.GetComponentsInChildren<SpriteRenderer>();
        MatchableObjectsRight.Clear();
        for (int i = 0; i < sps.Length; i++)
        {
            MatchableObjectsRight.Add(sps[i].gameObject);
        }
    }

    public string spriteFolder = "Sprites"; // Folder path within Resources folder
    public string spriteSuffix = "b"; // Suffix to match

    public List<Sprite> filteredSpritesLeft = new List<Sprite>();
    public List<Sprite> filteredSpritesRight = new List<Sprite>();


    [Button]
    private void LoadSpritesWithSuffix()
    {
        Sprite[] allSprites = Resources.LoadAll<Sprite>(spriteFolder);
        filteredSpritesRight.Clear();
        filteredSpritesLeft.Clear();
        foreach (Sprite sprite in allSprites)
        {
            if (sprite.name.EndsWith("a"))
            {
                filteredSpritesLeft.Add(sprite);
            }
            if (sprite.name.EndsWith("b"))
            {
                filteredSpritesRight.Add(sprite);
            }
        }
    }


    [Button]
    void assignSprites()
    {
        for (int i = 0; i < MatchableObjectsLeft.Count; i++)
        {
            MatchableObjectsLeft[i].GetComponent<SpriteRenderer>().sprite = filteredSpritesLeft[i];
        }
        for (int i = 0; i < MatchableObjectsRight.Count; i++)
        {
            MatchableObjectsRight[i].GetComponent<SpriteRenderer>().sprite = filteredSpritesRight[i];
        }
    }
    #endregion

}

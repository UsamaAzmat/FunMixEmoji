using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LevelViewHandler : MonoBehaviour
{
    public List<GameObject> levelPrefab = new List<GameObject>();
    public Sprite unlockSp, lockSp;
    private void OnEnable()
    {
        ScrollRect scrollRect = GetComponentInChildren<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 0f;

        foreach (var item in levelPrefab)
        {
            item.GetComponent<Image>().sprite = lockSp;
            item.SetActive(false);
        }
        int levelNumber;
        if (UIManager.isMatch) levelNumber = 25;
        else levelNumber = 10;
        for (int i = 0; i < levelNumber; i++)
        {
            levelPrefab[i].SetActive(true);
            if (UIManager.isDrawing)
            {
                if (i < PlayerPrefsManager.LevelNumberDraw)
                {
                    levelPrefab[i].GetComponent<Image>().sprite = unlockSp;
                }
            }
            if (UIManager.isMatch)
            {
                if (i < PlayerPrefsManager.LevelNumberMatch)
                {
                    levelPrefab[i].GetComponent<Image>().sprite = unlockSp;
                }
            }

        }



        //if (UIManager.isDrawing)
        //{
        //}

        //if (UIManager.isMatch)
        //{
        //    for (int i = 0; i < levelPrefab.Count; i++)
        //    {
        //        if (i < PlayerPrefsManager.LevelNumberMatch)
        //        {
        //            levelPrefab[i].SetActive(true);
        //            levelPrefab[i].GetComponent<Image>().sprite = unlockSp;
        //        }
        //    }
        //}
    }

    public GameObject content;

    [Button]
    void getdata()
    {
        levelPrefab.Clear();
        TextMeshProUGUI[] tms = content.GetComponentsInChildren<TextMeshProUGUI>(true);
        for (int i = 0; i < tms.Length; i++)
        {
            tms[i].text = (i + 1).ToString();
            levelPrefab.Add(tms[i].transform.parent.gameObject);
        }
    }
}

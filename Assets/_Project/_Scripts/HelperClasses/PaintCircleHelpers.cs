using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class PaintCircleHelpers : MonoBehaviour
{

    public List<DotsInfo> paintCircles = new List<DotsInfo>();
    DOTweenAnimation dotween;
    [Button]
    void getData()
    {
        paintCircles.Clear();
        DotsInfo[] inf = GetComponentsInChildren<DotsInfo>(true);
        for (int i = 0; i < inf.Length; i++)
        {
            paintCircles.Add(inf[i]);
        }
    }
    [Button]
    void addCollider()
    {
        for (int i = 0; i < paintCircles.Count; i++)
        {
            paintCircles[i].GetComponent<SpriteRenderer>().color = Color.white;
            Color spriteColor = paintCircles[i].GetComponent<SpriteRenderer>().color;
            spriteColor.a = 0;
            paintCircles[i].GetComponent<SpriteRenderer>().color = spriteColor;
            paintCircles[i].gameObject.SetActive(true);
        }
    }

    public void Check()
    {
        Debug.LogError("Usama");
    }
    public void Check1()
    {
        Debug.LogError("Usama1");
    }

}

using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using PaintIn3D;
using System.Collections;
using DG.Tweening;

public class PaintController : MonoBehaviour
{
    [ShowInInspector] [ReadOnly] public static int paintCounter;
    public static bool isControlEnable;
    //public static Color selectedColor;
    public GameObject choosePaint, pencilColor;
    public List<GameObject> paintAreas = new List<GameObject>();
    public List<GameObject> paintAnim = new List<GameObject>();
    public DrawManager drawManager;


    [ReadOnly] public P3dChangeCounterFill counterFill;
    private void OnEnable()
    {
        paintCounter = 0;
        EventsManager.UpdateColor += ColorSelected;
    }
    private void OnDisable()
    {
        EventsManager.UpdateColor -= ColorSelected;
    }


    void Start()
    {

        drawManager = transform.root.GetComponent<DrawManager>();
        UIManager.Instance.changeUIEmoji(false);
        choosePaint.SetActive(true);
    }


    private void ColorSelected(Color obj)
    {
        // selectedColor = obj;
        drawManager.dragToPaint.SetActive(true);
        choosePaint.SetActive(false);
        pencilColor.SetActive(true);
        pencilColor.transform.GetChild(2).GetComponent<SpriteRenderer>().color = obj;
        paintAnim[paintCounter].SetActive(true);
        EventsManager.UppdateClicked();
        counterFill = paintAreas[paintCounter].GetComponentInChildren<P3dChangeCounterFill>();
        counterFill.Counters.Add(paintAreas[paintCounter].GetComponentInChildren<P3dChangeCounter>());
        //colorNib.color = obj;
    }

    private void Update()
    {
        if (counterFill)
        {
            if (counterFill.cachedImage.fillAmount <= 0.035f)
            {
                UpdateColorCodes();
            }
        }
    }


    public void UpdateColorCodes()
    {
        SoundsManager.instSound.StopLoopedSound();
        SoundsManager.instSound.PlayTheSound(soundType.paintDone);
        isControlEnable = false;
        counterFill.cachedImage.fillAmount = 1;
        counterFill = null;
        paintAnim[paintCounter].transform.parent.gameObject.SetActive(false);
        pencilColor.SetActive(false);
        paintCounter++;
        UIManager.Instance.PlayParticles();
        if (paintCounter >= paintAreas.Count)
        {
            // Level WIn
            EventsManager.DrawLevelWIn();
        }
        else
        {
            choosePaint.SetActive(true);
            paintAreas[paintCounter].SetActive(true);
        }

    }

    [Button("Change Color of Do tween animation")]
    void getData()
    {
        //for (int i = 0; i < paintAnim.Count; i++)
        //{
        //    paintAnim[i].GetComponent<DOTweenAnimation>().endValueColor = "938E8E";
        //}
        //5E5858
        Color color = new Color(
       (float)System.Convert.ToInt32("5E", 16) / 255f,
       (float)System.Convert.ToInt32("58", 16) / 255f,
       (float)System.Convert.ToInt32("58", 16) / 255f
   );

        // Assuming 'paintAnim' is an array or list of GameObjects
        for (int i = 0; i < paintAnim.Count; i++)
        {
            DOTweenAnimation dotweenAnim = paintAnim[i].GetComponent<DOTweenAnimation>();
            if (dotweenAnim != null)
            {
                dotweenAnim.endValueColor = color;
            }
            else
            {
                Debug.LogWarning("DOTweenAnimation component not found on GameObject " + paintAnim[i].name);
            }
        }

    }

}


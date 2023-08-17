using DG.Tweening;
using UnityEngine;

public class DotsInfo : MonoBehaviour
{
    SpriteRenderer sp;
    DOTweenAnimation dotween;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        dotween = GetComponent<DOTweenAnimation>();
    }
    private void OnEnable()
    {
        //EventsManager.UpdateColor += ChangeColor;
    }
    private void OnDisable()
    {
        // EventsManager.UpdateColor -= ChangeColor;
    }

    private void ChangeColor(Color obj)
    {
        GetComponent<SpriteRenderer>().color = obj;
    }



    public void PaintThisObject()
    {
        Destroy(GetComponent<CircleCollider2D>());
        Color spColor = sp.color;
        spColor.a = 1;
        sp.color = spColor;
        dotween.DORestart();
    }
}

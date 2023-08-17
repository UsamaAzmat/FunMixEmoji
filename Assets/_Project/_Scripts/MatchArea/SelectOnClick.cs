using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SelectOnClick : MonoBehaviour
{
    [HideInInspector] public bool startLine = false;
    [HideInInspector] public bool objectInRight = true;
    [HideInInspector] public Vector3 connectingPoint;
    GameObject outline;
    public UnityEvent onShowTutorial;
    Color tmpColor;
    [HideInInspector] public Vector3 minScale;
    Vector3 maxScale;
    float speed = 1f;
    float duration = 5f;

    private void Start()
    {
        startLine = false;
        maxScale = new Vector3(0.6f, 0.6f, 0.6f);
        speed = 10f;
        duration = 1f;
    }

    private void OnMouseDown()
    {
        if (!LevelManager.Instance._CurrentLevel.DisableMousePos)
        {
            startLine = true;
            onShowTutorial.Invoke();
            //   Vibration.Vibrate(10);
            SoundsManager.instSound.PlayTheSound(soundType.click);
            SoundsManager.instSound.HepticPlay(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            EnableOutline();
            LevelManager.Instance._MatchObject.AddToObjList(this.gameObject);
        }
    }

    private void OnMouseUp()
    {
        startLine = false;
    }

    public void EnableOutline()
    {
        outline = this.transform.parent.transform.gameObject;
        tmpColor = outline.GetComponent<SpriteRenderer>().color;
        tmpColor.a = 1f;
        outline.GetComponent<SpriteRenderer>().color = tmpColor;
        StartCoroutine(RepeatLerp(minScale, maxScale, duration));

    }

    public void DisableOutline()
    {
        outline = this.transform.parent.transform.gameObject;
        tmpColor = outline.GetComponent<SpriteRenderer>().color;
        tmpColor.a = 0f;
        outline.GetComponent<SpriteRenderer>().color = tmpColor;
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        outline = this.transform.parent.transform.gameObject;
        float i = 0.0f, rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            outline.transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
        i = 0.0f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            outline.transform.localScale = Vector3.Lerp(b, a, i);
            yield return null;
        }
    }
}

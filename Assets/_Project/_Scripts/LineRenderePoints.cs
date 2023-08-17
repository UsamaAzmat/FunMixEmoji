using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRenderePoints : MonoBehaviour
{
    public List<GameObject> DrawPaths = new List<GameObject>();
    public List<PathEmoji> EmojiDrawPath = new List<PathEmoji>();



    [ReadOnly] public bool isMoving = false;
    [ReadOnly] public int currentPointIndex = 0;
    [ReadOnly] public int imageCounter = 0;
    private bool moveToFirst;
    Vector3 startPos;

    private void OnEnable()
    {

        startPos = transform.position;
        moveToFirst = true;
        cameraMove.target = EmojiDrawPath[0].targetView;
        EventsManager.onMouseDown += MouseEnter;
        EventsManager.onMouseUp += MouseUp;
    }
    private void OnDisable()
    {
        ResetThePathDrawer();
        EventsManager.onMouseDown -= MouseEnter;
        EventsManager.onMouseUp -= MouseUp;
    }


    private void MouseUp()
    {
        SoundsManager.instSound.StopLoopedSound();
        isMoving = false;
    }

    private void MouseEnter()
    {
        isMoving = true;
    }



    void ResetThePathDrawer()
    {
        foreach (var item in EmojiDrawPath)
        {
            item.ResetItems();
        }
        isMoving = false;
        imageCounter = 0;
        currentPointIndex = 0;
    }

    void Update()
    {
        if (!UIManager.isDrawing) return;

        if (isMoving)
        {
            MoveAlongPath();
        }
        else if (moveToFirst)
        {
            MoveToFirstElement();
        }


    }

    void MoveAlongPath()
    {
        if (imageCounter == EmojiDrawPath.Count)
        {
            isMoving = false;
            return;
        }
        if (currentPointIndex == EmojiDrawPath[imageCounter].EmojiDottedPath.Count)
        {
            imageCounter += 1;
            currentPointIndex = 0;
            SoundsManager.instSound.PlayTheSound(soundType.paintDone);
            UIManager.Instance.PlayParticles();
            SoundsManager.instSound.StopLoopedSound();
            if (imageCounter <= EmojiDrawPath.Count) StartCoroutine(EnableFinalImage(imageCounter - 1));
            if (imageCounter >= EmojiDrawPath.Count)
            {
                isMoving = false;
                EventsManager.DrawCompleted();
                moveToFirst = true;
                return;
            }
            if (imageCounter < EmojiDrawPath.Count)
            {
                moveToFirst = true;
                //transform.position = EmojiDrawPath[imageCounter].EmojiDottedPath[0].transform.position;
                cameraMove.target = EmojiDrawPath[imageCounter].targetView;
                DrawPaths[imageCounter].SetActive(true);
            }
            isMoving = false;
            return;
        }
        moveToFirst = false;
        Vector3 targetPosition = EmojiDrawPath[imageCounter].EmojiDottedPath[currentPointIndex].transform.position;
        EmojiDrawPath[imageCounter].lineRenderer.positionCount = currentPointIndex + 1;
        EmojiDrawPath[imageCounter].lineRenderer.SetPosition(currentPointIndex, EmojiDrawPath[imageCounter].EmojiDottedPath[currentPointIndex].transform.position);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, EmojiDrawPath[imageCounter].speed * Time.deltaTime);

        SoundsManager.instSound.PlayTheSound(soundType.sketching, true);

        if (transform.position == targetPosition)
        {
            EmojiDrawPath[imageCounter].EmojiDottedPath[currentPointIndex].gameObject.SetActive(false);
            SoundsManager.instSound.HepticPlay(MoreMountains.NiceVibrations.HapticTypes.LightImpact);
            currentPointIndex++;
        }
    }


    public IEnumerator EnableFinalImage(int id)
    {
        yield return new WaitForSeconds(0.2f);
        EmojiDrawPath[id].finalRender.SetActive(true);
        EmojiDrawPath[id].lineRenderer.gameObject.SetActive(false);
        EmojiDrawPath[id].EmojiDottedPath[0].transform.parent.gameObject.SetActive(false);

    }

    void MoveToFirstElement()
    {
        Vector3 currentPath = EmojiDrawPath[imageCounter].EmojiDottedPath[0].transform.position;
        if (imageCounter >= EmojiDrawPath.Count)
        {
            moveToFirst = false;
            currentPath = startPos;
        }
        Vector3 targetPosition = currentPath;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            moveToFirst = false;
        }
    }


    [Button]
    void getData()
    {
        EmojiDrawPath.Clear();
        while (EmojiDrawPath.Count < DrawPaths.Count)
        {
            EmojiDrawPath.Add(new PathEmoji());
        }
        for (int i = 0; i < DrawPaths.Count; i++)
        {
            EmojiDrawPath[i].EmojiDottedPath.Clear();
            SpriteRenderer[] sps = DrawPaths[i].GetComponentsInChildren<SpriteRenderer>();
            for (int j = 0; j < sps.Length; j++)
            {
                EmojiDrawPath[i].EmojiDottedPath.Add(sps[j].gameObject);
            }
            EmojiDrawPath[i].lineRenderer = DrawPaths[i].GetComponentInChildren<LineRenderer>(true);
            EmojiDrawPath[i].targetView = DrawPaths[i].transform.GetComponentInChildren<camHelper>(true).transform;
            EmojiDrawPath[i].finalRender = DrawPaths[i].transform.GetComponentInChildren<finalRender>(true).gameObject;
            EmojiDrawPath[i].finalRender.SetActive(false);
            DrawPaths[i].SetActive(false);
        }

        DrawPaths[0].SetActive(true);

    }
    [Button]
    void changeColorTo1()
    {
        foreach (var item in EmojiDrawPath)
        {
            for (int i = 0; i < item.EmojiDottedPath.Count; i++)
            {
                item.EmojiDottedPath[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}

[System.Serializable]
public class PathEmoji
{
    public List<GameObject> EmojiDottedPath = new List<GameObject>();
    public LineRenderer lineRenderer;
    public Transform targetView;
    public float speed = 5f;
    public GameObject finalRender;
    public void ResetItems()
    {
        foreach (var item in EmojiDottedPath)
        {
            item.SetActive(true);
        }
        lineRenderer.positionCount = 0;
    }
}

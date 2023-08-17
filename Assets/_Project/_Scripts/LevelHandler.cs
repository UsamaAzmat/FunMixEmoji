using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject drawHandler, paintHandler;
    public DrawManager drawManager;

    private void Start()
    {
        drawManager = transform.root.GetComponent<DrawManager>();
    }
    private void OnEnable()
    {
        EventsManager.onDrawCompleted += EnablePainting;
    }
    private void OnDisable()
    {
        EventsManager.onDrawCompleted -= EnablePainting;
    }

    private void EnablePainting()
    {
        drawHandler.SetActive(false);
        paintHandler.SetActive(true);
        cameraMove.target = drawManager.genericView.transform;
    }

    [Button]
    public void ENablePaintAnims()
    {
        PaintController paint = paintHandler.GetComponent<PaintController>();
        //for (int i = 0; i < paint.paintAnim.Count; i++)
        //{
        //    paint.paintAnim[i].SetActive(true);
        //}
        paint.pencilColor.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}

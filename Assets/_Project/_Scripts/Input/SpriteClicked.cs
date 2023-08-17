using System.Collections;
using UnityEngine;

public class SpriteClicked : MonoBehaviour
{
    Camera cam;
    public Vector3 offset;
    public AudioSource audios;
    public GameObject colorPoint;
    bool moveToFirst;
    float stationaryTimer = 0.0f;


    public PaintController paintController;
    private void Awake()
    {
        cam = Camera.main;
        PaintController.isControlEnable = true;
        paintController = transform.parent.GetComponent<PaintController>();
    }

    private void OnEnable()
    {
        EventsManager.UppdateClicked();
    }




    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if (!audios.isPlaying) audios.Play();
                MouseEnter();
                stationaryTimer = 0.0f; // Reset the timer when touch moves

            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                stationaryTimer += Time.deltaTime;

                if (stationaryTimer >= 1.0f && audios.isPlaying)
                {
                    audios.Stop();
                    stationaryTimer = 0;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                MouseUp();
                audios.Stop();
                stationaryTimer = 0.0f; // Reset the timer when touch is released
            }
        }


#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            MouseEnter();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
#endif
    }

    public void MouseEnter()
    {
        if (!colorPoint.activeSelf) colorPoint.SetActive(true);
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = mousePosition + offset;
        PaintController.isControlEnable = true;
        paintController.paintAnim[PaintController.paintCounter].SetActive(false);
        paintController.drawManager.dragToPaint.SetActive(false);
    }

    public void MouseUp()
    {
        PaintController.isControlEnable = false;
        colorPoint.SetActive(false);
    }






}

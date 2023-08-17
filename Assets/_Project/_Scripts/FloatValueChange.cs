using UnityEngine;
using PaintIn3D;

public class FloatValueChange : MonoBehaviour
{
    public float minValue = 0.3f;
    public float maxValue = 0.4f;
    public float changeSpeed = 0.1f;

    public P3dPaintSphere spherePaint;

    private float currentValue;
    private bool increasing = true;

    private void Start()
    {
        currentValue = minValue;
    }

    private void Update()
    {

        //if (SpriteClicked.isControlEnable)
        //    spherePaint.Radius = currentValue;
        //else
        //    spherePaint.Radius = minValue;
        if (!PaintController.isControlEnable)
        {
            spherePaint.Radius = minValue;
            return;
        }
        if (increasing)
        {
            currentValue += changeSpeed * Time.deltaTime;
            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
                increasing = false;
            }
        }
        else
        {
            currentValue = minValue; // Set to minValue instantly when decreasing
            increasing = true;
        }
        //else
        //{
        //    currentValue -= changeSpeed * Time.deltaTime;
        //    if (currentValue <= minValue)
        //    {
        //        currentValue = minValue;
        //        increasing = true;
        //    }
        //}

        // Use the currentValue for whatever you need
        spherePaint.Radius = currentValue;

    }
}

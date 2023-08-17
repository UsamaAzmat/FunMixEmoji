using System;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    internal static event Action onUpdateCoins;
    internal static event Action onMergeDisplay, onDrawDisplay, onMatchDisplay;
    internal static event Action onMouseDown, onMouseUp;
    internal static event Action onDrawCompleted;
    internal static event Action setUpMatchGame;
    internal static event Action shopItem;
    internal static event Action<Color> UpdateColor;



    public static event Action OnDrawWin, onMatchWIn, onMergeWin;

    public static void onMergeClicked()
    {
        onMergeDisplay?.Invoke();
    }
    public static void onDrawClicked()
    {
        onDrawDisplay?.Invoke();
    }

    public static void onMatchClicked()
    {
        onMatchDisplay?.Invoke();
    }



    public static void MouseClicked()
    {
        onMouseDown?.Invoke();
    }

    public static void MouseExit()
    {
        onMouseUp?.Invoke();
    }
    public static void UppdateClicked()
    {
        onUpdateCoins?.Invoke();
    }
    public static void DrawCompleted()
    {
        onDrawCompleted?.Invoke();
    }
    public static void settingUpMatch()
    {
        setUpMatchGame?.Invoke();
    }

    public static void ColorChange(Color clr)
    {
        UpdateColor?.Invoke(clr);
    }


    public static void DrawLevelWIn()
    {
        OnDrawWin?.Invoke();
    }


    public static void MatchLevelWin()
    {
        onMatchWIn?.Invoke();
    }
    public static void MergeLevelWin()
    {
        onMergeWin?.Invoke();
    }



}

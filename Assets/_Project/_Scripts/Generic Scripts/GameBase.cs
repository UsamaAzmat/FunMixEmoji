using UnityEngine;


public class GameBase : MonoBehaviour
{
    public static bool isControlPaused;
}



public enum ItemSide
{
    left,
    right
}

public enum economyType
{
    coins,
    pencil,
    pencilNib,
    mergeEmojiMultiple,
    pencilNibDraw,
    pencilNib1,
}



public enum PanelShowBehaviour
{
    HIDE_PREVIOUS,
    KEEP_PREVIOUS
}
public enum PanelShowOrHide
{
    Show,
    Hide
}
public enum soundType
{
    click,
    win,
    lose,
    mergeClick,
    shuffling,
    emojiPop,
    mergeDone,
    sketching,
    drawing,
    paintDone,
    click2,
}

public enum buttonType
{
    sound,
    music,
    vibrate
}
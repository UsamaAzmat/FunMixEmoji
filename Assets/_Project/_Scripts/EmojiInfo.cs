using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class EmojiInfo : MonoBehaviour
{
    [ReadOnly] public int ID;
    [EnumPaging] public ItemSide EmojiSide;


    public int favID;

    public Image finalImage;
    public void getFavSprite(int idLeft, int idRight)
    {
        string address = "Merger/" + (idLeft + 1).ToString() + "/" + (idRight + 1).ToString();
        Sprite tempSp = Resources.Load<Sprite>(address);
        if (finalImage) finalImage.sprite = tempSp;
    }

}

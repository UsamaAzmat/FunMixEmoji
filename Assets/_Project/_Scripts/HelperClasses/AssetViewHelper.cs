using Sirenix.OdinInspector;
using UnityEngine;

public class AssetViewHelper : MonoBehaviour
{

    //[PreviewField(100, Sirenix.OdinInspector.ObjectFieldAlignment.Left)]
    public SpriteRenderer sprite;

    public string address;
    public int folderName;
    [ReadOnly]
    public int currentIndex = 1;

    [Button(ButtonSizes.Gigantic)]
    void ShowNextElement()
    {
        if (currentIndex >= 10)
        {
            currentIndex = 1;
        }
        else
            currentIndex++;
        string addrss = address + "/" + folderName.ToString() + "/" + currentIndex.ToString();
        Sprite sp = Resources.Load<Sprite>(addrss);
        sprite.sprite = sp;

    }

    [Button(ButtonSizes.Gigantic)]
    void ShowPreviousElement()
    {
        if (currentIndex <= 1)
        {
            currentIndex = 10;
        }
        else
            currentIndex--;
        string addrss = address + "/" + folderName.ToString() + "/" + currentIndex.ToString();
        Sprite sp = Resources.Load<Sprite>(addrss);
        sprite.sprite = sp;

    }
}

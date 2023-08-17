using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class EmojiAssignHandler : MonoBehaviour
{
    public List<Image> emojiList = new List<Image>();
    public string emojiPath;
    [Button(ButtonSizes.Large)]

    public void getEmojiImages()
    {
        emojiList.Clear();
        EmojiInfo[] img = transform.GetComponentsInChildren<EmojiInfo>(true);
        for (int i = 0; i < img.Length; i++)
        {
            emojiList.Add(img[i].GetComponent<Image>());
        }
    }
    [Button(ButtonSizes.Large)]
    void LoadEmojiSprites()
    {
        if (emojiList.Count <= 0) return;



        for (int i = 0; i < emojiList.Count; i++)
        {
            string address = emojiPath + "/" + (i + 1).ToString();
            emojiList[i].sprite = Resources.Load<Sprite>(address);
        }
    }

    [Button(ButtonSizes.Large)]
    void renameEmojis()
    {
        for (int i = 0; i < emojiList.Count; i++)
        {
            emojiList[i].name = "Emoji" + (i + 1).ToString();
        }
    }
}

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System;

public class CoinsUpdater : MonoBehaviour
{
    [EnumPaging] public economyType _economyType;
    [SerializeField] private TextMeshProUGUI CoinsText;
    [SerializeField] private bool isDisable;
    bool isFirstClick;
    private void OnEnable()
    {
        EventsManager.onUpdateCoins += UpdateCoins;
        EventsManager.UpdateColor += UpdateColors;
    }
    private void OnDisable()
    {
        EventsManager.onUpdateCoins -= UpdateCoins;
        EventsManager.UpdateColor -= UpdateColors;
    }

    private void UpdateColors(Color obj)
    {
        if (_economyType == economyType.pencilNib)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = obj;
            if (isDisable) this.enabled = false;
        }
    }

    private void UpdateCoins()
    {
        if (!CoinsText) CoinsText = GetComponentInChildren<TextMeshProUGUI>(true);
        switch (_economyType)
        {
            case economyType.coins:
                CoinsText.text = PlayerPrefsManager.CoinsCount.ToString();
                break;
            case economyType.pencil:
                updateAvatar("Pencils/" + PlayerPrefsManager.PlayerSkin.ToString());
                break;
            case economyType.mergeEmojiMultiple:
                if (isFirstClick) transform.GetChild(0).gameObject.SetActive(false);
                break;

            case economyType.pencilNib1:
                updateAvatar("Pencils/Nibs/" + PlayerPrefsManager.PlayerSkin.ToString());
                break;
            case economyType.pencilNibDraw:
                updateAvatar("Pencils/Nibs/" + PlayerPrefsManager.PlayerSkin.ToString());
                break;

        }
    }
    void updateAvatar(string storedPath)
    {
        Sprite sp = Resources.Load<Sprite>(storedPath);
        if (GetComponent<Image>())
        {
            if (sp != null)
            {
                Image sprite = GetComponent<Image>();
                sprite.sprite = sp;
            }
        }
        if (GetComponent<SpriteRenderer>())
        {
            if (sp != null)
            {
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.sprite = sp;
            }
        }
    }
}

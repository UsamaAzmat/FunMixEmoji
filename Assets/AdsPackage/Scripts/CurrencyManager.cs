using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{

    public static CurrencyManager Instance;
    public Text CurrencyText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        CurrencyText.text = "" + PlayerPrefs.GetInt("TotalEarning");
    }
    public void UpdateCurrencyAmount(int Amount)
    {
        PlayerPrefs.SetInt("TotalEarning", PlayerPrefs.GetInt("TotalEarning") + Amount);
        CurrencyText.text = "" + PlayerPrefs.GetInt("TotalEarning");
    }
}

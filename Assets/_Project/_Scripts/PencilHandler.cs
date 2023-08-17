using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class PencilHandler : MonoBehaviour
{
    public List<SkinInfo> penSkins = new List<SkinInfo>();
    // Start is called before the first frame update


    public void updateGlowPosition()
    {
        foreach (var item in penSkins)
        {
            item.tickImg.SetActive(false);
            item.GetComponent<Image>().sprite = item.initialSp;
        }
        penSkins[PlayerPrefsManager.PlayerSkin].tickImg.SetActive(true);
        penSkins[PlayerPrefsManager.PlayerSkin].GetComponent<Image>().sprite = penSkins[PlayerPrefsManager.PlayerSkin].finalSp;
    }

    [Button]
    void getData()
    {
        penSkins.Clear();
        SkinInfo[] sk = GetComponentsInChildren<SkinInfo>();
        for (int i = 0; i < sk.Length; i++)
        {
            penSkins.Add(sk[i]);
        }
    }
}

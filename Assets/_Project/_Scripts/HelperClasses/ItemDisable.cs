using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisable : MonoBehaviour
{
    public float waitTime = 3;

    private void OnEnable()
    {
        //StartCoroutine(DisableThisItem());

    }
    IEnumerator DisableThisItem()
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }
}

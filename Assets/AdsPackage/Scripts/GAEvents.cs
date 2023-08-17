using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameAnalytics.Initialize();
    }

    public void EventTest()
    {
        GameAnalytics.NewDesignEvent("GA Is Working Properly");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

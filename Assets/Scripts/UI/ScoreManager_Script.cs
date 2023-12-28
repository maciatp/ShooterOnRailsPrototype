using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager_Script : MonoBehaviour
{
    int currentHits = 0;
    UIHits uiHIts;


    public int GetCurrentHits
    {
        get { return currentHits; }
    }
    public UIHits UIHits 
    { 
        get { return uiHIts; } 
        set { uiHIts = value; } 
    } 

    

    public void AddHits(int numOfHitsToAdd)
    {
        currentHits += numOfHitsToAdd;
        uiHIts.UpdateUIHits();
        if (numOfHitsToAdd > 1)
        {
            GameObject.Find("UIHitFloatingText").gameObject.GetComponent<UIHitCombo_Script>().ActivateUIHitText(numOfHitsToAdd, this.gameObject.transform.position);
        }
    }
}

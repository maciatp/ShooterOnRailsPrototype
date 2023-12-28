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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHits(int numOfHitsToAdd)
    {
        currentHits += numOfHitsToAdd;
        uiHIts.UpdateUIHits();
        if (numOfHitsToAdd > 1)
        {
            GameObject.Find("UIHitText").gameObject.GetComponent<UIHitCombo_Script>().ActivateUIHitText(numOfHitsToAdd, this.gameObject.transform.position);
        }
    }
}

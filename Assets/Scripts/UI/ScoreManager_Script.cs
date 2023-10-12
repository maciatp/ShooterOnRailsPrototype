using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager_Script : MonoBehaviour
{
    public int actualHits = 0;




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
        actualHits += numOfHitsToAdd;
        if (numOfHitsToAdd > 1)
        {
            GameObject.Find("UIHitText").gameObject.GetComponent<UIHitCombo_Script>().ActivateUIHitText(numOfHitsToAdd, this.gameObject.transform.position);
        }
    }
}

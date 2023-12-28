using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHits : MonoBehaviour
{

    TMPro.TextMeshProUGUI playerHits_Text;
    ScoreManager_Script scoreManager_Script_;

    private void Start()
    {
        playerHits_Text = GetComponent<TMPro.TextMeshProUGUI>();
        scoreManager_Script_ = GameObject.Find("ScoreManager").GetComponent<ScoreManager_Script>();
        scoreManager_Script_.UIHits = this;
        playerHits_Text.text = (scoreManager_Script_.GetCurrentHits.ToString("000"));
    }
    public void UpdateUIHits()
    {
        playerHits_Text.text = (scoreManager_Script_.GetCurrentHits.ToString("000"));
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILivesText_Script : MonoBehaviour
{
    public TMPro.TextMeshProUGUI livesText;

    private void Awake()
    {
        livesText = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }
   

    public void UpdateLivesText()
    {
        livesText.text = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerHealth_Script>().currentLives.ToString("x 00");
    }
}

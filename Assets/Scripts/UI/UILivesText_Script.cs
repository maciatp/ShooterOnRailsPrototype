using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILivesText_Script : MonoBehaviour
{
    public TMPro.TextMeshProUGUI livesText;

    public void UpdateLivesText() // TODO : Separar por addlife substract life
    {
        livesText.text = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerHealth_Script>().CurrentLives.ToString("x 00");
    }
}

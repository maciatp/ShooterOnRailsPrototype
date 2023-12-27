using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealth_Script : MonoBehaviour
{
    bool isReady = false;
    bool mustBeVisible = false;
    [Header("Boss UI Parts")]
    [SerializeField] List<Image> bossIndicators = new List<Image>();
    [SerializeField] Image currentDamageImage;
    [SerializeField] TMPro.TextMeshProUGUI bossText;
    [Space]
    [Header("Boss UI Parameters")]
    [SerializeField] float timeToBeActive = 1f;
    [SerializeField] float fillingSpeed = 1.2f;
    [SerializeField] Color orange = new Color(1, 0.5927993f, 0, 1); //ORANGE
    [SerializeField] Color fullHealthColor;
    [SerializeField] float lerpMultiplier = 2;

    
    BossHealth_Script bossHealth_Script_;

    public BossHealth_Script BossHealthScript_
    { 
        get { return bossHealth_Script_; } 
        set { bossHealth_Script_ = value; }
    }



    private void Awake()
    {
        bossText = transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        bossText.color = new Color(0, 0, 0, 0);
        currentDamageImage = transform.GetChild(2).GetComponent<Image>();
        foreach (Image bossIndicator_GO in bossIndicators)
        {
           Image _bossIndicator = bossIndicator_GO.gameObject.GetComponent<Image>();
            _bossIndicator.fillAmount = 0;
        }
        bossIndicators[1].color = fullHealthColor;
    }


    // Update is called once per frame
    void Update()
    {
        if (mustBeVisible == true)
        {
            if(isReady != true)
            {
                if (bossIndicators[0].fillAmount < 1)
                {
                    FillUIBossHealth();
                }
                else
                {
                    //BossText Appearing
                    bossText.color = Color.Lerp(bossText.color, Color.black, Time.unscaledDeltaTime * lerpMultiplier * 2);
                    if(bossText.color == Color.black)
                    {
                        isReady = true;
                    }
                }
            }
            

            //UI HEALTH
            if ((currentDamageImage.fillAmount < 0.3) && (bossIndicators[1].color != fullHealthColor))
            {
                bossIndicators[1].color = Color.Lerp(bossIndicators[1].color, fullHealthColor, Time.unscaledDeltaTime * lerpMultiplier);
            }
            else if ((currentDamageImage.fillAmount >= 0.3 && currentDamageImage.fillAmount < 0.6) && bossIndicators[1].color != orange)
            {
                bossIndicators[1].color = Color.Lerp(bossIndicators[1].color, orange, Time.unscaledDeltaTime * lerpMultiplier); //ORANGE
            }
            else if ((currentDamageImage.fillAmount >= 0.6) && (bossIndicators[1].color != Color.red))
            {
                bossIndicators[1].color = Color.Lerp(bossIndicators[1].color, Color.red, Time.unscaledDeltaTime * lerpMultiplier) ;
            }

        }
    }

    public void EnableBossUIHealth()
    {

        StartCoroutine(EnableBossUIHealthCoroutine());   


    }

    IEnumerator EnableBossUIHealthCoroutine()
    {
        yield return new WaitForSecondsRealtime(timeToBeActive);

        mustBeVisible = true;
    }


    public void FillUIBossHealth()
    {
        foreach (Image bossIndicator_GO in bossIndicators)
        {
            if (bossIndicator_GO.fillAmount < 1)
            {
                bossIndicator_GO.fillAmount += Time.unscaledDeltaTime * fillingSpeed;
            }
        }
    }

    public void UpdateCurrentDamage(float damageToIncrease)
    {
        if(currentDamageImage.fillAmount < 1)
        {
            currentDamageImage.fillAmount += (damageToIncrease) * 0.01f;

           

        }
    }
    //FALTARÍA UNA PARA RECUPERAR VIDA (EN CASO DE).
}

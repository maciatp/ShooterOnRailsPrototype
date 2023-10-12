using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealth_Script : MonoBehaviour
{
    public List<Image> bossIndicators = new List<Image>();
    public Image currentDamageImage;
    public TMPro.TextMeshProUGUI bossText;

    public float timeToBeActive = 1f;
    public float fillingSpeed = 1.2f;
    public bool mustBeVisible = false;
    public bool isReady = false;
    public Color orange = new Color(1, 0.5927993f, 0, 1); //ORANGE
    public Color fullHealthColor;
    public float lerpMultiplier = 2;

    public string coroutineName;
    public BossHealth_Script bossHealth_Script_;



    private void Awake()
    {
        bossText = this.transform.GetChild(3).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        bossText.color = new Color(0, 0, 0, 0);

        currentDamageImage = this.transform.GetChild(2).GetComponent<Image>();
        foreach (Image bossIndicator_GO in bossIndicators)
        {
           Image _bossIndicator = bossIndicator_GO.gameObject.GetComponent<Image>();
            _bossIndicator.fillAmount = 0;
        }

        bossIndicators[1].color = fullHealthColor;

      
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    //public void EnableBossUIHealth()
    //{

        IEnumerator EnableBossUIHealthCoroutine()
        {
            yield return new WaitForSecondsRealtime(timeToBeActive);

            mustBeVisible = true;
        }

        
   // }


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

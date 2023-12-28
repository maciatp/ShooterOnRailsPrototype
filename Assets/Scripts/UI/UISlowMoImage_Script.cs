using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlowMoImage_Script : MonoBehaviour
{
    [Header("Public References")]
    public TimeManager_Script timeManager_Script_;
    public Image slowMoImage;
    public Vector2 originalPosition;
    public Vector2 offset;

    public Color red;
    public Color yellow;
    public Color blue;

    private void Awake()
    {
        timeManager_Script_ = GameObject.Find("Player").gameObject.GetComponent<TimeManager_Script>();
        slowMoImage = this.gameObject.GetComponent<Image>();
        originalPosition = this.transform.localPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slowMoImage.fillAmount = timeManager_Script_.CurrentSlowMoPoints / 100;

        if((slowMoImage.fillAmount >= 0.5)&& slowMoImage.color != blue)
        {
            slowMoImage.color = blue;
        }
        else if((slowMoImage.fillAmount < 0.5 && slowMoImage.fillAmount > 0.25) && slowMoImage.color != yellow)
        {
            slowMoImage.color = yellow;
        }
        else if((slowMoImage.fillAmount <= 0.25) && slowMoImage.color != red)
        {
            slowMoImage.color = red;
        }



        if (timeManager_Script_.IsSlowMoActivated)
        {
           //WORLD TO VIEWPORT transforma una posición del mundo a un rango de (0,0) a (1,1) 
            Vector2 pos = Camera.main.WorldToViewportPoint(GameObject.Find("Player").transform.position);
           //VIEWPORT TO SCREEN POINT transforma de (0,0) (1,1) a píxeles
            slowMoImage.transform.position = Camera.main.ViewportToScreenPoint(pos + offset);
            
        }
        else
        {
            slowMoImage.transform.localPosition = originalPosition;
        }
    }
}

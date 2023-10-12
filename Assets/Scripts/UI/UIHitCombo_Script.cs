using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIHitCombo_Script : MonoBehaviour
{
    private RectTransform rect_father;
    private RectTransform rect_hits;
    public TMPro.TextMeshProUGUI hitsText;

    public bool isVisible = false;
    public float timeVisibleLeft;
    public float timeVisibleTimeSpan = 3;
    public float lerpMultiplier = 3;
    public Vector2 offset;

    public Color white;
    public Color yellow;
    public Color orange;
    public Color red;
    public Color faded;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rect_father = this.gameObject.GetComponent<RectTransform>();
        hitsText = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        rect_hits = hitsText.gameObject.GetComponent<RectTransform>();
        hitsText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rect_father.anchoredPosition.x + rect_father.rect.width / 2 - rect_hits.rect.width / 2);
        //Debug.Log(rect_father.anchoredPosition.x + rect_father.rect.width / 2 - rect_hits.rect.width / 2);
        //Debug.Log(Camera.main.WorldToScreenPoint(hitsText.transform.position));
        

        if (hitsText.enabled == true)
        {
            Vector2 targetOffset = Camera.main.WorldToViewportPoint(offset);

            hitsText.gameObject.transform.position = new Vector2(hitsText.gameObject.transform.position.x, Mathf.Lerp(hitsText.transform.position.y, Screen.height/2 + targetOffset.y, Time.deltaTime * lerpMultiplier));

            timeVisibleLeft -= Time.unscaledDeltaTime;
            if(timeVisibleLeft <= 0)
            {
                if (hitsText.color.a <= faded.a + 0.1f)
                {
                    hitsText.color = Color.Lerp(hitsText.color, faded, Time.unscaledDeltaTime * lerpMultiplier);


                }
                else
                {
                    hitsText.enabled = false;
                    timeVisibleLeft = timeVisibleTimeSpan;

                }

            }
        }
        
    }

    public void ActivateUIHitText(int hits, Vector3 enemyPos)
    {
            timeVisibleLeft = timeVisibleTimeSpan;
        //Sólo se entra la primera vez, así sigo actualizando los colores y número de hits sin volver a moverlo
        if(hitsText.enabled == false)
        {
            hitsText.enabled = true;
            //Debug.Log(enemyPos.x  + " ENEMY POS ORIGINAL");
            Vector2 viewportPos = Camera.main.WorldToViewportPoint(enemyPos);
            viewportPos.x = Mathf.Clamp(viewportPos.x, 0, 1);
           
            //Debug.Log(viewportPos.x + " viewportPos X");
            
            Vector2 pos = Camera.main.ViewportToScreenPoint(viewportPos);
            pos.x = rect_father.anchoredPosition.x - rect_father.rect.width / 2 + rect_hits.rect.width / 2 + pos.x;
           // Debug.Log(pos.x + "SCREEN POINT BEFORE CLAMP");
            pos.x = Mathf.Clamp(pos.x, rect_father.anchoredPosition.x - rect_father.rect.width / 2 + rect_hits.rect.width / 2, rect_father.anchoredPosition.x + rect_father.rect.width / 2 - rect_hits.rect.width / 2);  // this.gameObject.GetComponent<RectTransform>().anchoredPosition.x - this.gameObject.GetComponent<RectTransform>().rect.width/2 + hitsText.gameObject.GetComponent<RectTransform>().rect.width/2, this.gameObject.GetComponent<RectTransform>().anchoredPosition.x + this.gameObject.GetComponent<RectTransform>().rect.width - hitsText.gameObject.GetComponent<RectTransform>().rect.width/2); //this.gameObject.GetComponent<RectTransform>().anchoredPosition.x + 
            pos.y = Mathf.Clamp(pos.y, rect_father.anchoredPosition.y - rect_father.rect.height / 2 + rect_hits.rect.height / 2, rect_father.anchoredPosition.y + rect_father.rect.height / 2 - rect_hits.rect.height / 2);
           
            rect_hits.anchoredPosition = pos;
           
            //Debug.Log(rect_hits.anchoredPosition.x + " LOCALPOSITION AFTER CLAMP");
        }

        hitsText.text = hits.ToString("HIT + 0");

        if ((hits < 3) && (hitsText.color != white))
        {
            hitsText.color = white;
        }
        else if(((hits >= 3)&&(hits < 5)) && (hitsText.color != yellow))
        {
            hitsText.color = yellow;
        }
        else if(((hits >= 5) && (hits < 8)) && (hitsText.color != orange))
        {
            hitsText.color = orange;
        }
        else if((hits >= 8 ) && (hitsText.color != Color.red))
        {
            hitsText.color = Color.red;
        }

    }

    //INVOCAR A LA ALTURA DEL ENEMIGO DERRIBADO, como eL CIRCULO DE SLOW MO.  HACER QUE SE MUEVA HACIA EL CENTRO UNA VEZ INVOCADO, dure un tiempo determinado, y luego desaparezca

}

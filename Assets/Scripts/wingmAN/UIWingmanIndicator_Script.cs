using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIWingmanIndicator_Script : MonoBehaviour
{
    GameObject wingman;
    Animator uIWingmanIndicator_Animator;
    GameObject player_GO;

    [SerializeField] Vector2 offset;

    private void Awake()
    {
        if(this.name.Contains("Falco"))
        {
            wingman = GameObject.Find("Wingman_Falco").gameObject.transform.GetChild(0).gameObject;
        }
        if (this.name.Contains("Slippy"))
        {
            wingman = GameObject.Find("Wingman_Slippy").gameObject.transform.GetChild(0).gameObject;
        }
        if (this.name.Contains("Peppy"))
        {
            wingman = GameObject.Find("Wingman_Peppy").gameObject.transform.GetChild(0).gameObject;
        }

        
        uIWingmanIndicator_Animator = GetComponent<Animator>();
        player_GO = GameObject.FindGameObjectWithTag("Player").gameObject;

       
    }

    

    // Update is called once per frame
    void Update()
    {
        //WORLD TO VIEWPORT transforma una posición del mundo a un rango de (0,0) a (1,1) 
        Vector2 pos = Camera.main.WorldToViewportPoint(wingman.transform.position);
       
        //Si distanceZ... es > 0, es que el wingman está por delante del player
        float distanceZtoPlayer = wingman.transform.position.z - player_GO.transform.position.z;
       
        if ((((pos.x > 0) && (pos.x < 1)) && ((pos.y > 0) && (pos.y < 1)) && (distanceZtoPlayer > 0)) && (!uIWingmanIndicator_Animator.enabled)) //RECORDATORIO: ATENCIÓN A !uiWingman, no te pases el negativo por alto (!delante)
        {
            //Debug.Log("la nave está en pantalla");
            uIWingmanIndicator_Animator.enabled = true;
        }
        else if ((((pos.x <= 0) || (pos.x >= 1)) || ((pos.y <= 0) || (pos.y >= 1)) || (distanceZtoPlayer < 0)) && (uIWingmanIndicator_Animator.enabled))
        {
            //Debug.Log("la nave ha salido de pantalla");
            uIWingmanIndicator_Animator.enabled = false;
            gameObject.GetComponent<Image>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
        }
        if ((((pos.x > 0) && (pos.x < 1)) && ((pos.y > 0) && (pos.y < 1)) && (distanceZtoPlayer > 0)) && uIWingmanIndicator_Animator.enabled)
        {
            //Debug.Log("se está actualizando EL INDICADOR");
            uIWingmanIndicator_Animator.gameObject.transform.position = Camera.main.ViewportToScreenPoint(pos + offset);
        }

    }

    
}

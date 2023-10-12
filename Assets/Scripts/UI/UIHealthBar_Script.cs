using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar_Script : MonoBehaviour
{
    [Header("Bools")]
    public bool isBarExtraSize = false;
    public bool healthbarMustGrow = false;
    public bool mustDeltaBarMove = false;
    public bool healthIncreasing = false;
    public bool healthDecreasing = false;
    //public bool mustDeltaBarMoveRight_Decreasing = false;

    [Space]
    [Header("Public references")]
    public Transform childHealthBar;
    public Transform deltaBar;
    public PlayerHealth_Script playerHealth_Script_;

    public Image healthBarImage;
    public Image deltaBarImage;

    [Space]
    [Header("Parameters")]
    public Vector3 barNormalSize = new Vector3(100 , 1 );
    public Vector3 barExtraSize = new Vector3(150, 1);
    public float secondsWaitingToGrow = 2;
    public float barGrowVelocity = 2;


    public float changingColorSpeed = 2;
    public float floatLerpDeltaBar = 0;
    public float lerpVelocity = 1;
    public float waitForScalingChildHealthBar = 0.5f;
    public Color lowHealthColor;
    public Color lowHealthColor2;
    [Space]
    public float healthbarX;
    public float deltaBarX;
    //public float deltaChange;

    private void Awake()
    {
        childHealthBar = transform.Find("ChildHealthBar");
        healthBarImage = childHealthBar.GetComponent<Image>();
        deltaBar = transform.Find("DeltaBar");
        deltaBarImage = deltaBar.gameObject.GetComponent<Image>();
        playerHealth_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth_Script>();
        barExtraSize = new Vector2(playerHealth_Script_.maxPlayerHealth, 1);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        deltaBar.localScale = new Vector2(childHealthBar.localScale.x, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbarX = childHealthBar.localScale.x;
        
        deltaBarX = deltaBar.localScale.x;
        
        if((childHealthBar.localScale.x > 75) && (healthBarImage.color != Color.cyan))
        {
            healthBarImage.color = Color.Lerp(healthBarImage.color, Color.cyan, Time.unscaledDeltaTime*3);

        }
       
        else if ((childHealthBar.localScale.x > 60) && (childHealthBar.localScale.x < 75) && (healthBarImage.color != Color.green))
        {
            healthBarImage.color = Color.Lerp(healthBarImage.color, Color.green, Time.unscaledDeltaTime*3);

        }
        else if ((childHealthBar.localScale.x > 35) && (childHealthBar.localScale.x < 60) && (healthBarImage.color != Color.yellow))
        {
            healthBarImage.color = Color.Lerp(healthBarImage.color, Color.yellow, Time.unscaledDeltaTime*3);

        }
       else if ((childHealthBar.localScale.x > 0) && (childHealthBar.localScale.x < 35) && (healthBarImage.color != lowHealthColor))
        {
            healthBarImage.color = Color.Lerp(healthBarImage.color, lowHealthColor, Time.unscaledDeltaTime*3);
            //healthBarImage.color = lowHealthColor;

        }
        /*else if ((childHealthBar.localScale.x > 0) && (childHealthBar.localScale.x < 35) && (healthBarImage.color != lowHealthColor2))
        {
            //healthBarImage.color = Color.Lerp(healthBarImage.color, lowHealthColor2, Time.unscaledDeltaTime);
            healthBarImage.color = lowHealthColor2;

        }*/

       /* if ((childHealthBar.localScale.x > 0) && (childHealthBar.localScale.x < 35) && ((Mathf.Round(Time.unscaledTime) % Time.unscaledTime == 0)))
        {
            Debug.Log("entro en el bucle");
            if(healthBarImage.color == Color.red)
            {
                healthBarImage.color = Color.white;
            }
            else if (healthBarImage.color == Color.white)
            {
                healthBarImage.color = Color.red;
            }
            }
            */
        


        if(( healthIncreasing || healthDecreasing) == true)   /*(deltaBar.localScale.x > 0.1f)*/ 
        {
            //floatLerpDeltaBar += Time.unscaledDeltaTime *  lerpVelocity;
            //MIRAR ESCALA
            if ((healthDecreasing == true && healthIncreasing == false) && (((childHealthBar.localScale.x < barNormalSize.x) && (isBarExtraSize == false)) || ((childHealthBar.localScale.x < barExtraSize.x) && (isBarExtraSize == true))))
            {
                //ScaleDeltaBarWhenDecreasing
                deltaBar.localScale = new Vector3(Mathf.Lerp(deltaBar.localScale.x, playerHealth_Script_.actualPlayerHealth, Time.unscaledDeltaTime * lerpVelocity), 1, 0);
                
            }
            else if((healthIncreasing == true ) && (((childHealthBar.localScale.x >= 0) ))) //else if((healthDecreasing == false && healthIncreasing == true ) && (((childHealthBar.localScale.x >= 0) )))
            {
                //ScaleHEALTHBARWhenIncreasing
                healthDecreasing = false;
                childHealthBar.localScale = new Vector3(Mathf.Lerp(childHealthBar.localScale.x, deltaBar.localScale.x , Time.unscaledDeltaTime * lerpVelocity), 1, 0);
                
                
            }



           
        }
         if((deltaBar.localScale.x < childHealthBar.localScale.x +0.1f) ) //RESTANDO VIDA        /* && (deltaBar.localScale.x <= childHealthBar.localScale.x))*/
        {
            //floatLerpDeltaBar = 0;
            
            deltaBar.localScale = new Vector3(childHealthBar.localScale.x, 1);
            if (healthIncreasing == true)
            {
                healthIncreasing = false;
            }
            if (healthDecreasing == true)
            {
                healthDecreasing = false;
            }
            
        }
        if((childHealthBar.localScale.x > deltaBar.localScale.x - 0.1f)) //AÑADIENDO VIDA
        {

            //floatLerpDeltaBar = 0;
           
            childHealthBar.localScale = new Vector3(deltaBar.localScale.x, 1);
            if (healthIncreasing == true)
            {
                healthIncreasing = false;
            }

            if (healthDecreasing == true)
            {
                healthDecreasing = false;
            }


        }

         if(healthbarMustGrow == true)
        {

            this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(Mathf.Lerp(this.gameObject.transform.GetChild(0).transform.localScale.x, barExtraSize.x +4, Time.unscaledDeltaTime * barGrowVelocity), 1,1);
            this.gameObject.transform.GetChild(1).transform.localScale = new Vector3(Mathf.Lerp(this.gameObject.transform.GetChild(1).transform.localScale.x, barExtraSize.x +2 , Time.unscaledDeltaTime * barGrowVelocity),1,1);
            this.gameObject.transform.GetChild(2).transform.localScale = new Vector3(Mathf.Lerp(this.gameObject.transform.GetChild(2).transform.localScale.x, playerHealth_Script_.actualPlayerHealth + 2, Time.unscaledDeltaTime * barGrowVelocity),1,1);
            this.gameObject.transform.GetChild(3).transform.localScale = new Vector3(Mathf.Lerp(this.gameObject.transform.GetChild(3).transform.localScale.x, playerHealth_Script_.actualPlayerHealth +2, Time.unscaledDeltaTime * barGrowVelocity),1,1);
            //Debug.Log("Estoy creciendo");
            if(this.gameObject.transform.GetChild(1).transform.localScale.x >= barExtraSize.x)
            {
                healthbarMustGrow = false;
              //  Debug.Log("He crecido");
            }

        }

    }

    public IEnumerator ActivateExtraHealthUI()
    {
        yield return new WaitForSecondsRealtime(secondsWaitingToGrow);

        healthbarMustGrow = true;
        isBarExtraSize = true;
        //this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(147,1,0);
        //this.gameObject.transform.GetChild(1).transform.localScale = barExtraSize;
        //this.gameObject.transform.GetChild(2).transform.localScale = new Vector3(playerHealth_Script_.actualPlayerHealth, 1, 0);
        //this.gameObject.transform.GetChild(3).transform.localScale = new Vector3(playerHealth_Script_.actualPlayerHealth,1,0);
    }
    public void DeactivateExtraHealthUI()
    {
        isBarExtraSize = false;
        this.gameObject.transform.GetChild(0).transform.localScale = new Vector3(102, 1, 0);
        this.gameObject.transform.GetChild(1).transform.localScale = barNormalSize;
        this.gameObject.transform.GetChild(2).transform.localScale = barNormalSize;
        this.gameObject.transform.GetChild(3).transform.localScale = barNormalSize;
    }


    public IEnumerator IncreaseBarSize(float healthPointsRestored)
    {
         if (((childHealthBar.localScale.x < barNormalSize.x) && (isBarExtraSize == false)) || ((childHealthBar.localScale.x < barExtraSize.x) && (isBarExtraSize == true)))
            {
                healthDecreasing = false;
                // para saber si mover o no childhealth o si esperar o no. Cuando positivo espera, cuando negativo, no.
                //deltaBar.localScale += new Vector3(healthPointsRestored * ((100/ childHealthBar.localScale.x)), 0); // todo el rollo para que mantenga la escal aunque childbar sea pequeña
                if (((childHealthBar.localScale.x + healthPointsRestored < barNormalSize.x) && (isBarExtraSize == false)) || ((childHealthBar.localScale.x + healthPointsRestored < barExtraSize.x) && (isBarExtraSize == true)))
                {
                    deltaBar.localScale = new Vector3(playerHealth_Script_.actualPlayerHealth, 1);
                }
                else if (((childHealthBar.localScale.x + healthPointsRestored > barNormalSize.x) && (isBarExtraSize == false)))
                {
                    deltaBar.localScale = new Vector3(barNormalSize.x, 1);
                }
                else if (((childHealthBar.localScale.x + healthPointsRestored > barExtraSize.x) && (isBarExtraSize == true)))
                {
                    deltaBar.localScale = new Vector3(barExtraSize.x, 1);
                }

                //deltaBar.localScale = new Vector3(playerHealth_Script_.actualPlayerHealth, 0);
                deltaBarImage.color = Color.green;

                yield return new WaitForSecondsRealtime(waitForScalingChildHealthBar);
                healthIncreasing = true;

                //deltaChange = healthPointsRestored;
                //mustDeltaBarMove = true;


            }
        
        /*
        else if( isBarExtraSize == true)
        {
            if (childHealthBar.localScale.x < barExtraSize.x)
            {
                healthDecreasing = false;
                // para saber si mover o no childhealth o si esperar o no. Cuando positivo espera, cuando negativo, no.
                //deltaBar.localScale += new Vector3(healthPointsRestored * ((100/ childHealthBar.localScale.x)), 0); // todo el rollo para que mantenga la escal aunque childbar sea pequeña
                if (childHealthBar.localScale.x + healthPointsRestored < barExtraSize.x)
                {
                    deltaBar.localScale = new Vector3(playerHealth_Script_.actualPlayerHealth, 1);
                }
                else if (childHealthBar.localScale.x + healthPointsRestored > barExtraSize.x)
                {
                    deltaBar.localScale = new Vector3(barExtraSize.x, 1);
                }

                //deltaBar.localScale = new Vector3(playerHealth_Script_.actualPlayerHealth, 0);
                deltaBarImage.color = Color.green;

                yield return new WaitForSecondsRealtime(waitForScalingChildHealthBar);
                healthIncreasing = true;

                //deltaChange = healthPointsRestored;
                //mustDeltaBarMove = true;


            }
        }*/

        if ((childHealthBar.localScale.x > barNormalSize.x) && (isBarExtraSize == false))
        {

            childHealthBar.localScale = barNormalSize;
            deltaBar.localScale = barNormalSize;
            //Debug.Log("he petado barra salud");
        }
        else if ((childHealthBar.localScale.x > barExtraSize.x) && (isBarExtraSize == true))
        {

            childHealthBar.localScale = barExtraSize;
            deltaBar.localScale = barExtraSize;
            //Debug.Log("he petado barra salud");
        }
        yield return null;
    }

    public IEnumerator DecreaseBarSize(float healthPointsDepleted) //EL VALOR QUE LE ENVÍAN YA ES NEGATIVO 
    {
        if((childHealthBar.localScale.x +( healthPointsDepleted)) >= 0)
        {
            //if(childHealthBar.localScale.x - deltaBar.localScale.x > 0)
            {

                //childHealthBar.localScale += new Vector3(healthPointsDepleted,0 ); // En realidad se restan, pero viene negativo
                //deltaBar.localScale -= new Vector3((healthPointsDepleted * ((100 / childHealthBar.localScale.x) )), 0); //en realidad se suman

                healthIncreasing = false;
                deltaBar.localScale = new Vector3(playerHealth_Script_.actualPlayerHealth + (-healthPointsDepleted), 1);
                deltaBarImage.color = Color.red;
               

                childHealthBar.localScale = new Vector3(playerHealth_Script_.actualPlayerHealth, 1); // En realidad se restan, pero viene negativo

                

                yield return new WaitForSecondsRealtime(waitForScalingChildHealthBar);
                // deltaChange = healthPointsDepleted;
                healthDecreasing = true;

            }
           // else
            {
                //deltaBar.localScale += new Vector3(healthPointsDepleted, 0);
            }
        }
        else if((childHealthBar.localScale.x +( healthPointsDepleted)) < 0)
        {
            childHealthBar.localScale = new Vector3(0,1);
            deltaBar.localScale = new Vector3(0, 1);
        }

        yield return null;
    }
}

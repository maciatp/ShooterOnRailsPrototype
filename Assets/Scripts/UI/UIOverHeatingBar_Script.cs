using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverHeatingBar_Script : MonoBehaviour
{
    bool isBarExtraSize = false;

    [SerializeField] Transform childOverHeatingBar;
    Vector3 overHeatingBarBarNormalSize = new Vector3(100, 1);
    Image overHeatingBarImage;
    PlayerMovement_Script playerMovement_Script_;

    private void Awake()
    {
        overHeatingBarBarNormalSize = new Vector2(childOverHeatingBar.localScale.x, 1);      //TODO: Pasar del awake al start; debe de estar en playermovement script, en el awake  
        
    }

    private void Start()
    {        
        overHeatingBarImage = childOverHeatingBar.GetComponent<Image>();
        overHeatingBarImage.color = Color.white;
        playerMovement_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement_Script>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if((childOverHeatingBar.localScale.x > overHeatingBarBarNormalSize.x/2) && (overHeatingBarImage.color == Color.white))
        {
            PaintYellow();
        }

        if ((childOverHeatingBar.localScale.x >= overHeatingBarBarNormalSize.x) && (overHeatingBarImage.color == Color.yellow))
        {
            PaintRed();
        }

        if ((childOverHeatingBar.localScale.x < overHeatingBarBarNormalSize.x/2)&&((overHeatingBarImage.color == Color.red)|| (overHeatingBarImage.color == Color.yellow)))
        {
            PaintWhite();
        }

        if ((childOverHeatingBar.localScale.x > 85) && (Mathf.Round(childOverHeatingBar.localScale.x) % 3 == 0) && (overHeatingBarImage.color == Color.yellow))
        {
            PaintWhite();
        }
        if(playerMovement_Script_.CanUseOverHeating == false)
        {
            PaintRed();
        }
    }

    private void PaintWhite()
    {
        overHeatingBarImage.color = Color.white;
    }

    public void PaintRed()
    {
        overHeatingBarImage.color = Color.red;
    }

    private void PaintYellow()
    {
        overHeatingBarImage.color = Color.yellow;
       
    }


    //SIN USAR, por ahora
    public void IncreaseBarSize(float overHeatingAddedPoints)
    {
        if (childOverHeatingBar.localScale.x < overHeatingBarBarNormalSize.x)
        {
            childOverHeatingBar.localScale += new Vector3(overHeatingAddedPoints, 0f);
        }
        if ((childOverHeatingBar.localScale.x > overHeatingBarBarNormalSize.x) && (isBarExtraSize == false))
        {
            childOverHeatingBar.localScale = overHeatingBarBarNormalSize;
        }

    }

    public void DecreaseBarSize(float overHeatingDepletedPoints)
    {
        if (childOverHeatingBar.localScale.x > 0)
        {
            childOverHeatingBar.localScale -= new Vector3(overHeatingDepletedPoints, 0f);
        }
      

    }
}


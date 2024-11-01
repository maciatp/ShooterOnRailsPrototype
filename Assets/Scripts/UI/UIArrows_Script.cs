﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIArrows_Script : MonoBehaviour
{
    [Header("UI Arrows")]
    [SerializeField] GameObject arrow_Right;
    [SerializeField] GameObject arrow_Left;
    [SerializeField] GameObject arrow_Up;
    [SerializeField] GameObject arrow_Down;
    [SerializeField] GameObject UIChooseWayArrows;
    [Tooltip("Zona de detección del player")]
    [SerializeField] Vector2 arrowThreshold = new Vector2(25,12.2f); // Zona de detección 
    
    private bool isArrowRightEnabled = false;
    
    private bool isArrowLeftEnabled = false;
    
    private bool isArrowUpEnabled = false;
    
    private bool isArrowDownEnabled = false;



    PlayerMovement_Script playerMovement_Script_;

    private void Awake()
    {

        playerMovement_Script_ = GameObject.Find("Player").gameObject.GetComponent<PlayerMovement_Script>();
       

        

        //arrowsAnimator = arrow_Right.gameObject.GetComponent<Animator>();

        

    }

    // Start is called before the first frame update
    void Start()
    {
        //arrowsAnimator.StopPlayback();
        arrow_Right.SetActive(false);
        arrow_Left.SetActive(false);
        arrow_Up.SetActive (false);
        arrow_Down.SetActive(false);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(UIChooseWayArrows.gameObject.activeSelf == false)
        {
            //RIGHT ARROW

            if ((playerMovement_Script_.transform.localPosition.x >= arrowThreshold.x) && isArrowRightEnabled == false)
            {
                EnableRightArrow();
            }
            else if ((playerMovement_Script_.transform.localPosition.x < arrowThreshold.x) && isArrowRightEnabled == true)
            {
                DisableRightArrow();
            }

            //LEFT ARROW
            if ((playerMovement_Script_.transform.localPosition.x <= -arrowThreshold.x) && isArrowLeftEnabled == false)
            {
                EnableLeftArrow();
            }
            else if ((playerMovement_Script_.transform.localPosition.x > -arrowThreshold.x) && isArrowLeftEnabled == true)
            {
                DisableLeftArrow();
            }


            //UP ARROW
            if ((playerMovement_Script_.transform.localPosition.y >= arrowThreshold.y) && isArrowUpEnabled == false)
            {
                EnableUpArrow();
            }
            else if ((playerMovement_Script_.transform.localPosition.y < arrowThreshold.y) && isArrowUpEnabled == true)
            {
                DisableUpArrow();
            }

            //DOWN ARROW
            if ((playerMovement_Script_.transform.localPosition.y <= -arrowThreshold.y) && isArrowDownEnabled == false)
            {
                EnableDownArrow();
            }
            else if ((playerMovement_Script_.transform.localPosition.y > -arrowThreshold.y) && isArrowDownEnabled == true)
            {
                DisableDownArrow();
            }
        }
        




    }


    //RIGHT ARROW
    private void EnableRightArrow()
    {
        isArrowRightEnabled = true;
        arrow_Right.SetActive(true);
    }

    public void DisableRightArrow()
    {
        isArrowRightEnabled = false;
        arrow_Right.SetActive(false);
    }
    //LEFT ARROW
    private void EnableLeftArrow()
    {
        isArrowLeftEnabled = true;
        arrow_Left.SetActive(true);
       
    }
    public void DisableLeftArrow()
    {
        isArrowLeftEnabled = false;
        arrow_Left.SetActive(false);
       
    }

    //UP ARROW
    private void EnableUpArrow()
    {
        isArrowUpEnabled = true;
        arrow_Up.SetActive(true);

    }
    private void DisableUpArrow()
    {
        isArrowUpEnabled = false;
        arrow_Up.SetActive(false);

    }
    //DOWN ARROW
    private void EnableDownArrow()
    {
        isArrowDownEnabled = true;
        arrow_Down.SetActive(true);

    }

    private void DisableDownArrow()
    {
        isArrowDownEnabled = false;
        arrow_Down.SetActive(false);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class TimeManager_Script : MonoBehaviour
{
    PlayerControls controls;

    [Header("Bools")]
    public bool infiniteSlowMo = false;
    public bool isSlowMotionActivated = false;
    public bool isMovingSlowMotion = false;
    public bool isSlowMoAble = false;
    public bool activateSlowMoManually = false;
    [Space]
    [Header("Parameters")]
    public static float player = 1;
    public static float global = 1;
    
    public float slowMotionFactor = 0.05f;
    public float returnToNormalTimeDuration = 2f;
    public float slowMoDepletionRate = 10;

    public float timescale_;



    public float actualSlowMoPoints = 0;
    public float totalSlowmoPoints = 100;
    public float slowMoMinimumToActivate = 0.5f;

    [Header("Public References")]
    public PlayerMovement_Script playerMovement_Script_;
    public UISlowMoBar_Script uISlowMoBar_Script_;
    public CinemachineImpulseManager cinemachineManager_;

    
   

    private void Awake()
    {
        //playerMovement_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement_Script>();

        playerMovement_Script_ = this.GetComponent<PlayerMovement_Script>();


        uISlowMoBar_Script_ = GameObject.Find("UISlowMoBar").GetComponent<UISlowMoBar_Script>();

        //Para que los camera shake en slow mo sean en unscaled time (mirar abajo también)
        //cinemachineManager_ = GameObject.Find("Camera").GetComponent<CinemachineImpulseManager>();

        //NEW INPUT SYSTEM

        //controls = playerMovement_Script_.controls;

        controls = new PlayerControls();

        controls.Gameplay.TimeControl.performed += ctx =>
        {

            //DEBERÍA FUNCIONAR PERO NO FUNCIONA: INVESTIGAR
            if ((isSlowMotionActivated == false) && (isMovingSlowMotion == false) && (isSlowMoAble == true))
            {
                DoSlowMotion();
            }
            else if (isSlowMotionActivated == true)
            {
                DeactivateSlowMotion();
            }


        };

        
    }

   

    // Update is called once per frame
    void Update()
    {
        if((activateSlowMoManually == true) && (isSlowMotionActivated == false))
        {
            DoSlowMotion();
        }

        if(infiniteSlowMo == true)
        {
            AddSlowMoPoints(10);
        }
        //Time.fixedDeltaTime = Time.timeScale * 0.02f;

        //DEBERÍA COMENTAR TODOS LOS INPUT OLD SYSTEM
        //if ((Input.GetButtonDown("SlowMotion")))
        //{
        //    if((isSlowMotionActivated == false) && (isMovingSlowMotion == false) && (isSlowMoAble == true))
        //    {
        //        DoSlowMotion();
        //    }
        //    else if(isSlowMotionActivated == true)
        //    {
        //        DeactivateSlowMotion();
        //    }
           
        //}

        timescale_ = Time.timeScale;



        
        if ((isMovingSlowMotion == true) && (isSlowMotionActivated == false))
        {
            Time.timeScale += (1f / returnToNormalTimeDuration) * Time.unscaledDeltaTime;
            Time.timeScale  = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            if (Time.timeScale == 1)
            {
                isMovingSlowMotion = false;
            }
        }
        if(actualSlowMoPoints > 0)
        {
            isSlowMoAble = true;
        }
        else { isSlowMoAble = false; }

        if((isSlowMotionActivated == true) && (actualSlowMoPoints > 0))
        {
            DecreaseSlowMoPoints();
        }

        if((actualSlowMoPoints <= 0) && (isSlowMotionActivated == true))
        {
            DeactivateSlowMotion();
            actualSlowMoPoints = 0;
        }
    }



    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    public void AddSlowMoPoints(float slowMoPointsWillIncrease)
    {
       
        actualSlowMoPoints += slowMoPointsWillIncrease;
        uISlowMoBar_Script_.IncreaseBarSize(slowMoPointsWillIncrease);
        if (actualSlowMoPoints >= totalSlowmoPoints)
        {
            actualSlowMoPoints = totalSlowmoPoints;
        }

    }
    public void DecreaseSlowMoPoints()
    {
        actualSlowMoPoints -= Time.unscaledDeltaTime * slowMoDepletionRate;
        uISlowMoBar_Script_.DecreaseBarSize(Time.unscaledDeltaTime * slowMoDepletionRate);
    }


    public void DoSlowMotion()
    {
        isSlowMotionActivated = true;
        isMovingSlowMotion = true;
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        //Para que los camera shake en slow mo sean en unscaled time
        //CinemachineImpulseManager.Instance.IgnoreTimeScale = true;
        //cinemachineManager_.IgnoreTimeScale = true;

        float origFov =  40 ;
        float endFov =  55 ;
        float origChrom =  0 ;
        float endChrom = 1;
        float origDistortion =  0 ;
        float endDistorton =  -30 ;

        Debug.Log("Hago Slowmo");
        //float zoom = 0;

        DOVirtual.Float(origChrom, endChrom, .2f, playerMovement_Script_.Chromatic);
        DOVirtual.Float(origFov, endFov, 0.2f, playerMovement_Script_.FieldOfView);
        DOVirtual.Float(origDistortion, endDistorton, .2f, playerMovement_Script_.DistortionAmount);
       

        //DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        //SetCameraZoom(zoom, .4f);




    }
    public void DeactivateSlowMotion()
    {

        isSlowMotionActivated = false;
        Debug.Log("DESACTIVO Slowmo");


        
       
       

        float origFov = 55;
        float endFov = 40;
        float origChrom = 1;
        float endChrom = 0;
        float origDistortion = -30;
        float endDistorton = 0;

        
        //float zoom = -7;

        DOVirtual.Float(origChrom, endChrom, .5f, playerMovement_Script_.Chromatic);
        DOVirtual.Float(origFov, endFov, .5f, playerMovement_Script_.FieldOfView);
        DOVirtual.Float(origDistortion, endDistorton, .5f, playerMovement_Script_.DistortionAmount);


    }



}



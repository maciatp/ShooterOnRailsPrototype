using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class TimeManager_Script : MonoBehaviour
{
    PlayerControls controls;

    static float player = 1;
    static float global = 1;
    float timescale_;

    [Header("Bools")]
    [SerializeField] bool infiniteSlowMo = false;
    [SerializeField] bool activateSlowMoManually = false;
    bool isSlowMotionActivated = false;
    bool isMovingSlowMotion = false;
    bool isSlowMoAble = false;

    [Space]
    [Header("Parameters")]
    [SerializeField] float slowMotionFactor = 0.05f;
    [SerializeField] float returnToNormalTimeDuration = 2f;
    [SerializeField] float slowMoDepletionRate = 10;




    [SerializeField] float currentSlowMoPoints = 0;
    [SerializeField] float totalSlowmoPoints = 100;
    [SerializeField] float slowMoMinimumToActivate = 0.5f;

    //EXTERNAL REFERENCES
    UISlowMoBar_Script uISlowMoBar_Script_;
    PlayerMovement_Script playerMovement_Script_;
    //CinemachineImpulseManager cinemachineManager_;

    public bool IsSlowMoActivated
    {
        get { return isSlowMotionActivated; }
        set { isSlowMotionActivated = value; }
    }
    public bool IsMovingSlowMo
    {
        get { return isMovingSlowMotion; }
        set { isMovingSlowMotion = value; }
    }
    public float CurrentSlowMoPoints
    {
        get { return currentSlowMoPoints; }
        set { currentSlowMoPoints = value; }
    }

    private void Awake()
    {
        

        playerMovement_Script_ = GetComponent<PlayerMovement_Script>();


        uISlowMoBar_Script_ = GameObject.Find("UISlowMoBar").GetComponent<UISlowMoBar_Script>();

        //Para que los camera shake en slow mo sean en unscaled time (mirar abajo también)
        //cinemachineManager_ = GameObject.Find("Camera").GetComponent<CinemachineImpulseManager>();

        //NEW INPUT SYSTEM
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
        if(currentSlowMoPoints > 0)
        {
            isSlowMoAble = true;
        }
        else { isSlowMoAble = false; }

        if((isSlowMotionActivated == true) && (currentSlowMoPoints > 0))
        {
            DecreaseSlowMoPoints();
        }

        if((currentSlowMoPoints <= 0) && (isSlowMotionActivated == true))
        {
            DeactivateSlowMotion();
            currentSlowMoPoints = 0;
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
       
        currentSlowMoPoints += slowMoPointsWillIncrease;
        uISlowMoBar_Script_.IncreaseBarSize(slowMoPointsWillIncrease);
        if (currentSlowMoPoints >= totalSlowmoPoints)
        {
            currentSlowMoPoints = totalSlowmoPoints;
        }

    }
    public void DecreaseSlowMoPoints()
    {
        currentSlowMoPoints -= Time.unscaledDeltaTime * slowMoDepletionRate;
        uISlowMoBar_Script_.DecreaseBarSize(Time.unscaledDeltaTime * slowMoDepletionRate);
    }


    public void DoSlowMotion()
    {
        isSlowMotionActivated = true;
        isMovingSlowMotion = true;
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        //Para que los camera shake en slow mo sean en unscaled time
        CinemachineImpulseManager.Instance.IgnoreTimeScale = true;
        //cinemachineManager_.IgnoreTimeScale = true;

        float origFov =  40 ;
        float endFov =  55 ;
        float origChrom =  0 ;
        float endChrom = 1;
        float origDistortion =  0 ;
        float endDistorton =  -30 ;

        Debug.Log("Hago Slowmo");
        //float zoom = 0;

        DOVirtual.Float(origChrom, endChrom, .5f, playerMovement_Script_.Chromatic);
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



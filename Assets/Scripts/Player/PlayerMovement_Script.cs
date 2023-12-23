using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement_Script : MonoBehaviour
{
    private float originalChasingDistance;

    public PlayerControls controls;
    public Joystick touchJoystick;

    [Header("Bools")]

    public bool isInvertedY = false;
    public bool isConteoBarrelRollLeftActive = false ;
    public bool isConteoBarrelRollRightActive = false;
    public float conteoBarrelRollLeft = 0;
    public float conteoBarrelRollRight = 0;
    public bool canCounterEnemyLasers = false;
    public bool isTilting = false;
    public bool isTiltingLeft = false;
    public bool isTiltingRight = false;
   
    public bool isTiltButtonPressed = false;
    public bool isBoosting = false;
    public bool isBoostingGamePad = false;
    public bool isBraking = false;
    public bool isBrakingGamePad = false;
    public bool canUseOverHeating = true;
    public bool isLooping = false;
    public bool isTankModeActivated = false;

    public bool isBossMode = false;
    public bool isAllRangeMode = false;

    [Space]
    public AudioClip boost_Sound;
    public AudioClip brake_Sound;
    public AudioSource playerAudioSource;

    [Space]

    private float angleToTilt = 0;


    [Header("Settings")]
    public bool usesJoystick = true;

    [Space]

    [Header("Parameters")]
    public Vector2 moveInput;
    public Vector2 moveDirection;
    public float xSpeed = 10f;
    public float ySpeed = 10f;
    public float xyLookSpeed = 8500f;
    //public float minimumAngleToMove = 15f;
 
    //public float yLookSpeed = 8500;
    public float chasingDistance = 2f; //afecta al grado de giro cuando la nave vira (cuanto más lejos esté el objetivo de la persecución, más pequeño será el ángulo de giro)
    public float forwardSpeed = 12f; //velocidad frontal lineal que tiene GameplayPlane
    public float originalXYLookSpeed;
    public float originalYSpeed;
    public float originalXSpeed;
    public float lerpTimeToHorizontalLean = 0.1f; //lo que tarda en girar el modelo sobre la Z cuando vira (tiene que ser 0,1 para que sea smooth)
    public float leanLimit = 60f; //lo máximo que va a alabear
    public float quickspinTimeSpan = 0.5f;
    public float timeTiltingToComplete = 0.15f; //tiempo que va a tardar el tilt hasta los 90 grados
    public float velocityMultiplierWhenTiltingAndTurning = 1.40f;
    public float boostingSpeedMultiplier = 2;
   
    public float actualPlayerOverHeating = 0;
    public float totalOverHeating = 100f;
    public int overHeatingDecreasingRate = 20;
    public int overHeatingIncreasingRate = 40;
    
    
    public float cameraFOVSpeed = 10;
    public float cameraTransformZ;
    public float conteoJoystickUp = 0;
    public float joystickUpTimeSpan = 0.12f;
    public float joystickVerticalMinimumToLooping = 0.45f;
    public float counterConteo = 0;
    public float counterDuration = 1f;





    [Space]

    [Header("Public References")]
    public Transform aimTarget;
    public CinemachineDollyCart dolly;
    public Transform cameraParent;
    public Animator arwing_Animator;
    public Animator player_Animator;
    public GameObject playerInScene;
    private UIOverHeatingBar_Script uIOverHeatingBar_Script_;
    private Transform childOverHeatingBar;
    private Rigidbody player_RB;
    public CinemachineVirtualCamera cineMachineVirtualCamera_script_;
    public GameObject mirilla_Lejos;
    public ChargedLaserSphere_Script chargedLaserSphere_Script_;
    private Transform playerModel; //el segundo nivel del G.O. player
    public GameObject playerArwing;
    public TrailRenderer leftTrail;
    public TrailRenderer rightTrail;
    [SerializeField]
    private bool isTrailsActive = true;


    [Space]

    [Header("Particles")]
    public ParticleSystem trail;
    public ParticleSystem circle;
    public ParticleSystem barrel;
    public ParticleSystem stars;
    

    private void Awake()
    {
        originalXYLookSpeed = xyLookSpeed;
        originalYSpeed = ySpeed;
        originalXSpeed = xSpeed;
        cineMachineVirtualCamera_script_ = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        playerInScene = GameObject.Find("Player");
        player_RB = playerInScene.GetComponent<Rigidbody>();
        uIOverHeatingBar_Script_ = GameObject.Find("UIOverHeatingBar").GetComponent<UIOverHeatingBar_Script>();
        childOverHeatingBar = uIOverHeatingBar_Script_.transform.Find("ChildOverHeatingBar");
        playerAudioSource = this.transform.GetChild(5).GetChild(1).GetComponent<AudioSource>();
        playerArwing = playerInScene.transform.GetChild(0).GetChild(0).gameObject;
        mirilla_Lejos = playerInScene.transform.GetChild(2).gameObject;
        originalChasingDistance = chasingDistance;


        //TOUCH CONTROLS
        //if ((Application.platform == RuntimePlatform.IPhonePlayer) || (Application.platform == RuntimePlatform.Android))
        //{
        //    touchJoystick.enabled = true;
        //}
        //else
        //{
        //    touchJoystick.enabled = false;
        //}

        //NEW INPUT SYSTEM
        // controls = this.GetComponent<PlayerShooting_Script>().controls;

        controls = new PlayerControls();
        //MOVERSE
        controls.Gameplay.Move.performed += ctx => moveInput.x = ctx.ReadValue<Vector2>().x;

        


        if (isInvertedY == false)
        {

            //GAMEPAD &&    //KEYBOARD
            controls.Gameplay.Move.performed += ctx =>
            {
                moveInput.y = ctx.ReadValue<Vector2>().y;
               

            };

         
            
        }
       else if (isInvertedY == true) //PARA INVERTIR EJE Y
        {
            //GAMEPAD && KEYBOARD
            controls.Gameplay.Move.performed += ctx =>
            {
                moveInput.y = -ctx.ReadValue<Vector2>().y;

            };
        
        }

       //CANCELED 
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;


        //TANK MODE
        controls.Gameplay.TankMode.performed += ctx =>
        {
            Debug.Log("Tank Mode ACTIVATED");
            isTankModeActivated = true;
            isTilting = false;
            isTiltingLeft = false;
            isTiltingRight = false;
            xyLookSpeed = 0;
            playerInScene.transform.localRotation = new Quaternion(0, 0, 0, 0);
        };

        controls.Gameplay.TankMode.canceled += ctx =>
        {
            Debug.Log("Tank Mode DEACTIVATED");
            isTankModeActivated = false;
            xyLookSpeed = originalXYLookSpeed;
            ySpeed = originalYSpeed;
            chasingDistance = originalChasingDistance;
        };



        //TILTING LEFT

        controls.Gameplay.TiltLeft.performed += ctx =>
        {
            
           if(isTankModeActivated == false)
            {
                playerModel.transform.DOLocalRotate(new Vector3(0, 0, 90), timeTiltingToComplete);

                ActivateTrails();

                isTilting = true;
                isTiltingLeft = true;
                isTiltingRight = false;
               // chasingDistance /= 2; //cuando tiltea la nave para que rote más hacia los lados.
                //xyLookSpeed *= 1.2f;
                //ySpeed /= 1.3f;

                if ((conteoBarrelRollLeft < quickspinTimeSpan) && (isConteoBarrelRollLeftActive == true) && (canCounterEnemyLasers == false))
                {
                    //int dir = Input.GetButtonDown("TriggerL") ? -1 : 1;

                    QuickSpinLeft();
                    isConteoBarrelRollLeftActive = false;

                    //isBarrelRolling = true;
                    conteoBarrelRollLeft = 0;


                }

                isConteoBarrelRollLeftActive = true;
            }

        };

        controls.Gameplay.TiltLeft.canceled += ctx =>
        {
            
            if (isTiltingLeft == true)
            {
                
                isTilting = false;
                DeactivateTrails();


                isTiltingLeft = false;
                //isConteoBarrelRollLeftActive = true;

                chasingDistance = originalChasingDistance; //ChasingDistance original. La cambio cuando tiltea la nave para que rote más hacia los lados.
                xyLookSpeed = originalXYLookSpeed;
                ySpeed = originalYSpeed;
            }





        };



        //TILTING RIGHT
        controls.Gameplay.TiltRight.performed += ctx =>
        {
           
            if(isTankModeActivated == false)
            {

                playerModel.transform.DOLocalRotate(new Vector3(0, 0, -90), timeTiltingToComplete);

                ActivateTrails();

                isTilting = true;
                isTiltingRight = true;
                isTiltingLeft = false;
                //chasingDistance /= 2; //ChasingDistance original. La cambio cuando tiltea la nave para que rote más hacia los lados.
                //xyLookSpeed *= 1.5f;
                //ySpeed /= 1.3f;


                if ((conteoBarrelRollRight < quickspinTimeSpan) && (isConteoBarrelRollRightActive == true) && (canCounterEnemyLasers == false)) //COMPROBACIÓN PARA BARREL ROLL
                {
                    //int dir = Input.GetButtonDown("TriggerL") ? -1 : 1;
                    //QuickSpin(dir);

                    QuickSpinRight();
                    // arwing_Animator.SetBool("isBarrelRolling_RIGHT_Anim", true);
                    //arwing_Animator.Play("Barrel_Right");
                    isConteoBarrelRollRightActive = false;
                    conteoBarrelRollRight = 0;
                }

                isConteoBarrelRollRightActive = true;
            }
        };

        controls.Gameplay.TiltRight.canceled += ctx =>
        {
            
            if(isTiltingRight == true)
            {
                isTilting = false;
               DeactivateTrails();
                isTiltingRight = false;
                xyLookSpeed = originalXYLookSpeed;
                ySpeed = originalYSpeed;
                chasingDistance = originalChasingDistance; //ChasingDistance original. La cambio cuando tiltea la nave para que rote más hacia los lados.
                //isConteoBarrelRollLeftActive = true;
            }




        };


        //BOOST

        controls.Gameplay.Boost.started += ctx =>
        {

            if ((isLooping == false) && ((conteoJoystickUp < joystickUpTimeSpan) && (conteoJoystickUp >= 0)) && ((actualPlayerOverHeating + (totalOverHeating / 5) < totalOverHeating)) && (canUseOverHeating == true))
            {
                Looping(true);
            }
            
           else if ((canUseOverHeating == true) && (isLooping == false))
            {
                ActivateBoost();
            }



        };

        controls.Gameplay.Boost.canceled += ctx =>
        {
            if((isLooping == false) && (isBoosting == true))
            {
                DeactivateBoost();
            }
        };

        //BRAKE

        controls.Gameplay.Brake.started += ctx =>
        {
            if (isBoosting == true)
            {
                isBoosting = false;
                Boost(false);
            }

            ActivateBrake();

        };

        controls.Gameplay.Brake.canceled += ctx =>
        {
            if(isBraking == true)
            {
                DeactivateBrake();
            }

        };


        



    }


    void Start()
    {
        playerModel = transform.GetChild(0);
        actualPlayerOverHeating = 0;
        SetSpeed(forwardSpeed); //HACE QUE LA VELOCIDAD SEA SIEMPRE 24 al empezar
        arwing_Animator = playerArwing.GetComponent<Animator>();
        player_Animator = playerInScene.GetComponent<Animator>();
        childOverHeatingBar.localScale = new Vector3(actualPlayerOverHeating,1);
        if(isTrailsActive == true)
        {
            DeactivateTrails();
        }
    }

    

    void Update()
    {
        
        if(canCounterEnemyLasers == true)
        {
            counterConteo += Time.deltaTime;
            if(counterConteo >= counterDuration)
            {
                canCounterEnemyLasers = false;
                counterConteo = 0;
               // Debug.Log("He desactivado barrelrolling");
            }
        }

       

        //TOUCH CONTROLS

        //Debug.Log(Application.platform);
        //if ((Application.platform == RuntimePlatform.IPhonePlayer) || (Application.platform == RuntimePlatform.Android))
        //{
        //    //Debug.Log(Application.platform);
        //    moveInput.x = touchJoystick.Horizontal;
        //    if (isInvertedY == true)
        //    {
        //        moveInput.y = -touchJoystick.Vertical;
        //    }
        //    else
        //    {

        //        moveInput.y = touchJoystick.Vertical;
        //    }
        //}



        ////AQUÍ VA TODO LO DE LOCALMOVE
        if (isLooping == false)
        {

            if ((moveInput.x != 0) || (moveInput.y != 0))
            {
                
                moveDirection = new Vector2((Mathf.Lerp(moveDirection.x, moveInput.x, Time.unscaledDeltaTime)), (Mathf.Lerp(moveDirection.y, moveInput.y, Time.unscaledDeltaTime))); //0.055
                                                                                                                                                    
            }
            else if ((Mathf.Abs(moveInput.x) <= 0.1f) && ((Mathf.Abs(moveInput.y) <= 0.1f)))
            {
                moveDirection = new Vector2((Mathf.Lerp(moveDirection.x, moveInput.x, Time.unscaledDeltaTime * 2.5f)), (Mathf.Lerp(moveDirection.y, moveInput.y, Time.unscaledDeltaTime * 2.5f))); //0.085 0.035
            }


            ////RotationLook(moveInput.x, moveInput.y, xyLookSpeed, yLookSpeed);
            RotationLook(moveDirection.x, moveDirection.y, xyLookSpeed); //*2

            //MOVIMIENTO NORMAL

            LocalMove(moveDirection.x, moveDirection.y, xSpeed, ySpeed, isTilting); //*2

            if(isTilting == false)
            {

            HorizontalLean(playerModel, moveInput.x, leanLimit, lerpTimeToHorizontalLean); //está sólo en tilting=false para que no se enderece cuando está tilteando
            }

           
            //else if (isTilting == true)
            //{


            //    //MOVIMIENTO LATERAL RÁPIDO CUANDO TILTING
            //    if (((isTiltingLeft == true) && (moveInput.x < 0)) || ((isTiltingRight == true) && (moveInput.x > 0)))
            //    {
            //        LocalMove(moveDirection.x, moveDirection.y, xSpeed * velocityMultiplierWhenTiltingAndTurning, ySpeed);
            //        //Debug.Log("Me muevo a más velocidad lateral");

            //        //RotationLook(moveDirection.x, moveDirection.y, xyLookSpeed, yLookSpeed);
            //    }
            //    else if (((isTiltingLeft == true) && (moveInput.x > 0)) || ((isTiltingRight == true) && (moveInput.x < 0)))
            //    {
            //        LocalMove(moveDirection.x, moveDirection.y, xSpeed, ySpeed);
            //        //Debug.Log("Me muevo a LA velocidad lateral");

            //        //RotationLook(moveDirection.x, moveDirection.y, xyLookSpeed , yLookSpeed);
            //    }
            //    else if ((moveInput.y > 0) || (moveInput.y < 0))
            //    {
            //        LocalMove(0, moveDirection.y, xSpeed, ySpeed);

            //        //RotationLook(moveDirection.x, moveDirection.y, xyLookSpeed, yLookSpeed);
            //    }
                
            //}
            //else if (isTankModeActivated == true)
            //{
            //    ySpeed = originalYSpeed / 3;
            //    xSpeed = originalXSpeed / 3;


            //}


        }

            /*
             *     if (((h > 0) && ( playerInScene.transform.localRotation.y >= -minimumAngleToMove)) || ((h < 0) && (playerInScene.transform.localRotation.y <= minimumAngleToMove))) //MINIMUMANGLE ES 0
                 {
                     LocalMove(h, 0, xSpeed, ySpeed);
                 }

                 if (((v < 0) && (playerInScene.transform.localRotation.x >= -minimumAngleToMove)) || ((v > 0) && (playerInScene.transform.localRotation.x <= minimumAngleToMove)))
                 {
                     LocalMove(0, v, xSpeed, ySpeed);
                 }

             * 
             * 
             * */


            //STATE MACHINE PARA LOS ALERONES
            if ((moveInput.x != 0) && ((moveInput.x >= 0.125f) || (moveInput.x <= -0.125f)))
        {
            arwing_Animator.SetBool("isMovingHoriz_Anim", true);
            arwing_Animator.SetFloat("tilting_Anim", moveInput.x);
        }
        if((moveInput.y != 0) && ((moveInput.y >= 0.125f) || (moveInput.y <= -0.125f)))
        {
            arwing_Animator.SetBool("isMovingVert_Anim", true);
            arwing_Animator.SetFloat("pitching_Anim", moveInput.y);
        }

        if(moveInput.x == 0)
        {
            arwing_Animator.SetFloat("tilting_Anim", 0);
            arwing_Animator.SetBool("isMovingHoriz_Anim", false);

        }
        if(moveInput.y == 0)
        {
            arwing_Animator.SetFloat("pitching_Anim", 0);
            arwing_Animator.SetBool("isMovingVert_Anim", false);
        }

        //LOOPING
        if( moveInput.y >= joystickVerticalMinimumToLooping)
        {
            
            conteoJoystickUp -= Time.unscaledDeltaTime;

        }
        if(moveInput.y < joystickVerticalMinimumToLooping)
        {
            conteoJoystickUp = joystickUpTimeSpan;
        }
        //contador para controlar el joystick movido (activar al mover el joystick en -Y, y si se pulsa boost a la vez, se hace looping
        

        
        

        if((isLooping == true) || (isBoosting==true)||(isBraking==true)||(isBoostingGamePad==true)||(isBrakingGamePad==true))
        {

            //STAMINA (OVERHEATING)
           
            if ((actualPlayerOverHeating <= totalOverHeating) && (canUseOverHeating==true))
            {
                actualPlayerOverHeating += Time.deltaTime * overHeatingIncreasingRate;
                childOverHeatingBar.localScale = new Vector3(actualPlayerOverHeating,1);
               
            }

            
            if((actualPlayerOverHeating >= totalOverHeating) || ((actualPlayerOverHeating < totalOverHeating) && (actualPlayerOverHeating > totalOverHeating / 2) && (canUseOverHeating == false))) //SIN STAMINA NO HAY PARTY!! AKA BOOST/BRAKE
            {
                arwing_Animator.SetBool("isBraking_Anim", false);
                arwing_Animator.SetBool("isBoosting_Anim", false);
                

                canUseOverHeating = false;
                if(isBoosting==true)
                {
                    isBoosting = false;
                    Boost(false);
                }
                if (isBoostingGamePad == true)
                {
                    isBoostingGamePad = false;
                    Boost(false);
                    
                }
                if (isBraking == true)
                {
                    isBraking = false;
                    
                    Brake(false);
                }
                if (isBrakingGamePad == true)
                {
                    isBrakingGamePad = false;
                    Brake(false);

                }
            }
        }
        //STAMINA (OVERHEATING)
        if((actualPlayerOverHeating > 0) && (((isBoosting==false)&& (isBraking==false))||((isBoostingGamePad==false)&&(isBrakingGamePad==false))))
        {
            actualPlayerOverHeating -= Time.deltaTime * overHeatingDecreasingRate;
            childOverHeatingBar.localScale = new Vector3(actualPlayerOverHeating, 1);

            if (actualPlayerOverHeating < 0)
            {
                actualPlayerOverHeating = 0;
                childOverHeatingBar.localScale = new Vector3(0, 1);
            }
        }
        if(( actualPlayerOverHeating < (totalOverHeating/2))&&(canUseOverHeating == false))
        {
            canUseOverHeating = true;
        }

  

           if(isConteoBarrelRollLeftActive == true)
             {
                 conteoBarrelRollLeft += Time.deltaTime;
             }
           if (isConteoBarrelRollRightActive == true)
           {
              conteoBarrelRollRight += Time.deltaTime;
           }

        if ((conteoBarrelRollLeft > quickspinTimeSpan) && (isConteoBarrelRollLeftActive == true))
        {
            conteoBarrelRollLeft = 0;
            isConteoBarrelRollLeftActive = false;
            
        }
        if ((conteoBarrelRollRight > quickspinTimeSpan) && (isConteoBarrelRollRightActive == true))
        {
            conteoBarrelRollRight = 0;
            isConteoBarrelRollRightActive = false;

        }

    }

    private void FixedUpdate()
    {
        //PARA QUE LOS TRAILS DE LAS ALAS SÓLO EMITAN CUANDO TE MUEVES (NO BORRAR!)
        if (((Mathf.Abs(moveInput.magnitude) > 0.015f)|| isBoosting == true) && (isTrailsActive == false))
        {
            ActivateTrails();
            
        }
        else if ((  ( (Mathf.Abs(moveDirection.magnitude) <= 0.10f) && (Mathf.Abs(moveInput.magnitude) <= 0.1f)) && isBoosting == false)  && (isTilting == false) &&(isTrailsActive == true))
        {
            DeactivateTrails();
            
        }
    }

    private void DeactivateBrake()
    {
        Brake(false);
        isBraking = false;
        arwing_Animator.SetBool("isBraking_Anim", false);
    }

    private void ActivateBrake()
    {
        if (canUseOverHeating == true)
        {
            Brake(true);
            isBraking = true;
            arwing_Animator.SetBool("isBraking_Anim", true);
        }

    }

    private void DeactivateBoost()
    {
        Boost(false);
        isBoosting = false;
        arwing_Animator.SetBool("isBoosting_Anim", false);
    }

    private void ActivateBoost()
    {
        isBraking = false;
        Brake(false);
        Boost(true);
        isBoosting = true;
        arwing_Animator.SetBool("isBoosting_Anim", true);
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    void Looping(bool state)
    {
        Debug.Log("looping state es " + state);
        float origFov = state ? 40 : 60;
        float endFov = state ? 60 : 40;
        float speed = state ? 0 : forwardSpeed;

        if (state == true)
        {
            aimTarget.parent.position = Vector3.zero;
            player_Animator.SetBool("isLooping_Anim", true);
            isLooping = true;
            //Gameplay Plane = 0
            //dolly.m_Speed = 0;

            DOVirtual.Float(dolly.m_Speed, speed, 0, SetSpeed);

            //actualPlayerOverHeating += totalOverHeating / 2;

            //cambia FOV según el estado true/false
            DOVirtual.Float(origFov, endFov, .5f, FieldOfView);


            //make player not kinematic?

            //FX
            cameraParent.GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();
            trail.Play();
            circle.Play();
            playerAudioSource.clip = boost_Sound;
            playerAudioSource.Play();

            //play looping

            //player_Animator.Play("PlayerLooping_Anim");
            //arwing_Animator.Play("Boost");

            Debug.Log("Looping");

        }


        
       
        
        else //if (state == false)
        {
            isLooping = false;
            player_Animator.SetBool("isLooping_Anim", false);
            dolly.m_Speed = forwardSpeed;
            
            playerArwing.transform.localPosition = new Vector3(0, 0, 0);
            playerInScene.transform.localPosition = new Vector3(playerInScene.transform.localPosition.x, playerInScene.transform.localPosition.y, 0);
            
            //playerInScene.transform.localRotation = new Quaternion(0, 0, 0,0);
            mirilla_Lejos.transform.localPosition = new Vector3(0, -0.09999996f, 27.375f); //PROBAR SI COMENTANDO ESTO FUNCIONA IGUAL, y borrar
            mirilla_Lejos.transform.localRotation = new Quaternion(0, 0, 0, 0);
            DOVirtual.Float(origFov, endFov, .5f, FieldOfView);
            Debug.Log("Ha terminado la animacion");
            trail.Stop();
            trail.GetComponent<TrailRenderer>().emitting = false;
            playerAudioSource.Stop();
            if ((GameObject.FindGameObjectWithTag("ChargedLaserSphere")) == true)
            {
                chargedLaserSphere_Script_ = GameObject.FindGameObjectWithTag("ChargedLaserSphere").GetComponent<ChargedLaserSphere_Script>();
                chargedLaserSphere_Script_.transform.localPosition = chargedLaserSphere_Script_.initialPosition;
            }


        }
        

        trail.GetComponent<TrailRenderer>().emitting = state;

        float starsVel = state ? -1 : -35;
        float zoom = state ? -4 : 0;
        
        var pvel = stars.velocityOverLifetime;


        pvel.z = starsVel;


        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);
    }

    void DeactivateLooping()
    {
        Looping(false);
    }

   void DeactivateBarrelRolling()
    {
        if(canCounterEnemyLasers == true)
        {
            canCounterEnemyLasers = false;
        }
        
    }
    
    public void LocalMove(float x, float y, float xSpeed, float ySpeed, bool isTilting)  //MOVIMIENTO DEL JUGADOR EN LA PANTALLA
    {

        if((isTilting == false) || (((x < 0) && isTiltingRight) || ((x > 0) && isTiltingLeft)))
        {
            transform.localPosition += new Vector3(x * xSpeed, y * ySpeed, 0) * Time.unscaledDeltaTime;
        }
        else if(((x > 0) && isTiltingRight) || (x < 0) && isTiltingLeft)
        {
            transform.localPosition += new Vector3(x * xSpeed *velocityMultiplierWhenTiltingAndTurning, y * ySpeed, 0) * Time.unscaledDeltaTime;
        }
      
        //Debug.Log(new Vector3(x * xSpeed, y * ySpeed, 0) * Time.unscaledDeltaTime);
        //player_RB.MovePosition(new Vector3(x * xSpeed * 10, y * ySpeed * 10, 0) * Time.unscaledDeltaTime);
        //player_RB.AddForce(new Vector2(x * xSpeed, y * ySpeed));


        //player_RB.MovePosition(player_RB.position + (new Vector3(x * xSpeed, y * ySpeed,0) * Time.unscaledDeltaTime));
        ClampPosition();
    }

    void ClampPosition() //LÍMITES DE LA PANTALLA
    {
       Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
       pos.z = Mathf.Clamp(pos.z,10,10);
       

        //Debug.Log(pos.x + "pos.x" + "." + pos.y + "pos.y");
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float h, float v, float speed) //CONTROL DE PERSECUCIÓN
    {
        aimTarget.parent.position =  Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, chasingDistance);
           
        //ENCARGADO DEL CABECEO Y DE ROTAR LA NAVE HACIA LA POSICION DE APUNTADO (JOYSTICK H,V,1)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * xyLookSpeed * Time.unscaledDeltaTime); //AQUÍ ESTÁ LA GUISA DE LA PERSECUCIÓN: se rota a todo el player, no sólo al modelo
    }

   


    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime) //ALABEO EN LOS VIRAJES
    {
       Vector3 targetEulerAngles = target.localEulerAngles;
       target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z , -axis * leanLimit, lerpTime * Time.unscaledDeltaTime) );

       // Vector3 targetEulerAngles = new Vector3(target.localEulerAngles.x, target.localEulerAngles.y,  cameraTransformZ);
        //target.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -axis * leanLimit, lerpTime));


        //PROBAR SI  USANDO EL MISMO SISTEMA SE PUEDE HACER EL TILT DE LA NAVE

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }

    public void QuickSpinLeft() //BARREL ROLL
    {
        //if (!DOTween.IsTweening(playerModel))
        {
            canCounterEnemyLasers = true;
            Debug.Log("He activado barrelrolling");
            playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, (360 - playerModel.localEulerAngles.z)), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            barrel.Play();
            
            
        }
       
    }
    public void QuickSpinRight() //BARREL ROLL
    {
        //if (!DOTween.IsTweening(playerModel))
        {
            canCounterEnemyLasers = true;
            playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, ( -playerModel.localEulerAngles.z - (90))), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            barrel.Play();
            //Debug.Log(playerModel.localEulerAngles.z);

        }

    }


    //AJUSTES DE CÁMARA

    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }

    public void DistortionAmount(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>().intensity.value = x;
    }

    public void FieldOfView(float fov)
    {
        cameraParent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
    }

    public void Chromatic(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = x;
    }


    void Boost(bool state)
    {

        if (state)
        {
            cameraParent.GetComponentInChildren<CinemachineImpulseSource>().GenerateImpulse();
            trail.Play();
            circle.Play();
            playerAudioSource.clip = boost_Sound;
            playerAudioSource.Play();
            chasingDistance *= 1.5f;
            
            
        }
        else 
        {
            trail.Stop();
            circle.Stop();
            playerAudioSource.Stop();
            chasingDistance = originalChasingDistance;
            
        }
        trail.GetComponent<TrailRenderer>().emitting = state;

        float origFov = state ? 40 : 55;
        float endFov = state ? 55 : 40;
        float origChrom = state ? 0 : 1;
        float endChrom = state ? 1 : 0;
        float origDistortion = state ? 0 : -30;
        float endDistorton = state ? -30 : 0;
        float starsVel = state ? -60 : -35;
        float speed = state ? forwardSpeed * boostingSpeedMultiplier : forwardSpeed;
        float zoom = state ? -7 : 0;

        var emission = stars.emission;
        emission.rateOverDistance = state ? 6 : 1;

        var trails = stars.trails;
        trails.enabled = state ? true : false;

        DOVirtual.Float(origChrom, endChrom, .5f, Chromatic);
        DOVirtual.Float(origFov, endFov, .5f, FieldOfView);
        DOVirtual.Float(origDistortion, endDistorton, .5f, DistortionAmount);
        var pvel = stars.velocityOverLifetime;
        pvel.z = starsVel;

        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);

        
    }

    void Brake(bool state)
    {
        if(state)
        {
            playerAudioSource.clip = brake_Sound;
            playerAudioSource.Play();
        }
        else
        {
            playerAudioSource.Stop();
        }

        float speed = state ? forwardSpeed / 3 : forwardSpeed;
        float zoom = state ? 2.5f : 0;
        float origFov = state ? 40 : 30;
        float endFov = state ? 30 : 40;
        float starsVel = state ? -2.5f : -35f;
        var pvel = stars.velocityOverLifetime;
        pvel.z = starsVel;
        

        DOVirtual.Float(origFov, endFov, .5f, FieldOfView);
        DOVirtual.Float(dolly.m_Speed, speed, .15f, SetSpeed);
        SetCameraZoom(zoom, .4f);

       
    }

    public void ActivateTrails()
    {
        isTrailsActive = true;
        leftTrail.emitting = true;
        rightTrail.emitting = true;
    }
    public void DeactivateTrails()
    {
        if(isTilting == false)
        {

        isTrailsActive = false;
        leftTrail.emitting = false;
        rightTrail.emitting = false;
        }
    }
}

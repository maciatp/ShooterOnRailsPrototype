using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShooting_Script : MonoBehaviour
{

    public PlayerControls controls;


    public bool isLaserCharging = false;

    public bool isLaserCharged = false;

    public bool isLaserChargedAndButtonUp = false;

    public bool isChargedLaserInstanced = false;

    public bool isShootButtonPressed = false;

    public bool isBombShot = false;
    




   [Header("Public References")]
    public Transform singleLaser_Spawn;
    public Transform twinLasersSpawnLocation;
    //public Transform rightLaserSpawn;
    public Transform chargedLaser_Spawn;
    public Transform greenSingleLaserMuzzle;
    public Transform greenDoubleLaserMuzzle;
    public Transform blueDoubleLaserMuzzle;


   
    ChargedLaserSphere_Script chargedLaser_Script_;
    public PlayerDisplay_Script playerDisplay_Script_;
    public Bomb_Script bomb_Script_;
    public Mirillas_Script mirillas_Script_;
    public Mirilla_Externa_Script mirilla_Externa_Script_INSCENE;
    //public InputMaster controls;
   

    [Space]
    [Header("Audio")]
    public AudioSource playerShootingAudioSource;
    public AudioClip singleLaserFired_Sound;
    public AudioClip twinLaserFired_Sound;
    public AudioClip hyperLaserFired_Sound;


    [Space]
    [Header("Instantiables")]
    public GameObject laserBeam_GO;
    public GameObject twinLasers_GO;
    public GameObject hyperLasers_GO;
    public GameObject chargedLaser_GO;
    public GameObject chargedLaserInShip;
    public GameObject chargingLaserFX;
    public GameObject chargingLaserFX_INSCENE;


    
    

    [Space]
    public GameObject bomb_GO;
    public GameObject bombINSCENE;
    


    [Header("Parameters")]
    public float conteoChargingLaser = 0;
    public float chargeLaserTimeSpan = 1f;
    
    public float conteoUseBeforeDeactivateChargedLaser = 0; //conteo que hace para cuando sueltas disparo con el láser cargado
    public float UseBeforeDeactivateChargedLaserTimeSpan = 2.5f; //tiempo que tienes para volver a pulsar disparo antes de que se desactive disp cargado


    

    public float laserRafagaIntervalBetweenShots = 0.1f;
    

    public int laserUpgradesCaught = 0;
    public int actualBombs = 3;

    float conteoBomb = 0;
    public float ablingExplodeBombTimeSpan = 0.1f;

    public float ConteoBomb
    {
        get { return conteoBomb; }
        set { conteoBomb = value;}
    }
   
    //public ParticleSystem LaserLeft;
    //public ParticleSystem LaserRight;

    void Awake()
    {
       
        
        CheckLaserPowerUpSound();

        controls = new PlayerControls();
        


        //BOTÓN DE DISPARO PULSADO
        controls.Gameplay.Fire.started += context =>
        {
            FireButtonPressed();

        };

        //BOTÓN DE DISPARO SOLTADO
        controls.Gameplay.Fire.canceled += context =>
        {
            FireButtonReleased();
        };

        controls.Gameplay.FireBomb.performed += ctx =>
        {
            if ((isBombShot == false) && (actualBombs >= 1))
            {
                FireBomb();
            }

            else if ((isBombShot == true) && (conteoBomb > ablingExplodeBombTimeSpan))
            {

                    ExplodeBomb();

            }

            
        };


    }

    

    // Start is called before the first frame update
    void Start()
    {
        playerDisplay_Script_ = GameObject.Find("UIBombs").GetComponent<PlayerDisplay_Script>();
        
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLaserCharging == true)
        {
            conteoChargingLaser += Time.unscaledDeltaTime;
        }

        if((conteoChargingLaser >= chargeLaserTimeSpan)&&(isLaserCharged == false))
        {
            isLaserCharging = false;
            isLaserCharged = true;
            InstantiateChargedLaser();
            DeactivateConteoChargeLaser();
        }

        if (isLaserChargedAndButtonUp == true)
        {
            conteoUseBeforeDeactivateChargedLaser += Time.deltaTime;
        }

        if(conteoUseBeforeDeactivateChargedLaser > UseBeforeDeactivateChargedLaserTimeSpan)
        {
           DeactivateChargedLaser();
            conteoUseBeforeDeactivateChargedLaser = 0;
        }


        if (isBombShot == true)
        {
            conteoBomb += Time.deltaTime;
        }



    }

    private void BeginShootProcess()
    {
        isShootButtonPressed = true;
        BeginChargeLaser();
        StartCoroutine("Fire");
    }

    public void FireBomb()
	{
		bombINSCENE = (GameObject)Instantiate (bomb_GO, chargedLaser_Spawn.position, chargedLaser_Spawn.rotation) as GameObject;
		bomb_Script_ = bombINSCENE.GetComponent<Bomb_Script> ();
		DecreaseOneBomb ();

		isBombShot = true;
	}

	public void ExplodeBomb()
    {
        bomb_Script_.Explode();
        isBombShot = false;        
        //Debug.Log("Llamo a explode desde playershooting");

    }

    void DeactivateConteoChargeLaser()
    {
        isLaserCharging = false;
        conteoChargingLaser = 0;
        

    }

   
   public IEnumerator Fire()
    {
        if (laserUpgradesCaught == 0)
        {
               for (int lasersShot = 0; lasersShot < 3; lasersShot += 1)// DESCOMENTAR CUANDO HAYAN TERMINADO PRUEBAS DE DISPARO
                {
               

                 if (isShootButtonPressed == true)
                    {
                    GameObject laserBeam = (GameObject)Instantiate(laserBeam_GO, singleLaser_Spawn.position, singleLaser_Spawn.rotation);
                    greenSingleLaserMuzzle.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    laserBeam.name = "LaserBeam";// SÓLO FUNCIONA SI SE LLAMA EXACTAMENTE IGUAL (CON CLONE INCLUIDO), por eso lo renombro
                    playerShootingAudioSource.Play();

                    }
                    else
                    {
                        // playerShootingAudioSource.Stop();
                    }
                 yield return new WaitForSecondsRealtime(laserRafagaIntervalBetweenShots);// DESCOMENTAR CUANDO HAYAN TERMINADO PRUEBAS DE DISPARO
            }




        }
        if (laserUpgradesCaught == 1)
        {
            for (int lasersShot = 0; lasersShot < 3; lasersShot += 1)
            {
                if (isShootButtonPressed == true)
                {
                   GameObject twinLasers = (GameObject)Instantiate(twinLasers_GO, twinLasersSpawnLocation.position, twinLasersSpawnLocation.rotation);
                    twinLasers.name = "TwinLasers";
                    greenDoubleLaserMuzzle.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    greenDoubleLaserMuzzle.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    //Instantiate(laserBeam_GO, rightLaserSpawn.position, rightLaserSpawn.rotation);

                    playerShootingAudioSource.Play();
                }
                yield return new WaitForSecondsRealtime(laserRafagaIntervalBetweenShots);
            }
        }
        if (laserUpgradesCaught == 2)
        {
            for (int lasersShot = 0; lasersShot < 3; lasersShot += 1)
            {
                if (isShootButtonPressed == true)
                {
                    GameObject hyperLasers = (GameObject)Instantiate(hyperLasers_GO, twinLasersSpawnLocation.position, twinLasersSpawnLocation.rotation);
                    hyperLasers.name = "HyperLasers";
                    blueDoubleLaserMuzzle.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                    blueDoubleLaserMuzzle.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    //Instantiate(laserAdvancedBeam_GO, rightLaserSpawn.position, rightLaserSpawn.rotation);

                    playerShootingAudioSource.Play();
                }
                yield return new WaitForSecondsRealtime(laserRafagaIntervalBetweenShots);
            }
        }
    }

   


    void BeginChargeLaser()
    {
        isLaserCharging = true;
        chargingLaserFX_INSCENE = (GameObject)Instantiate(chargingLaserFX, chargedLaser_Spawn.position, chargedLaser_Spawn.rotation, this.transform);
    }

    void InstantiateChargedLaser()
    { 

        
       chargedLaserInShip = (GameObject)Instantiate(chargedLaser_GO, chargedLaser_Spawn.position, chargedLaser_Spawn.rotation, chargedLaser_Spawn) as GameObject;
        chargedLaser_Script_ = chargedLaserInShip.GetComponent<ChargedLaserSphere_Script>();
        
        isChargedLaserInstanced = true;
    }
    void BeginChargedLaserConteoDeactivation()
    {
        isLaserChargedAndButtonUp = true;
    }
    
    void DeactivateChargedLaser()
    {
        chargedLaser_Script_.Deactivate();


        isLaserCharged = false;
        isChargedLaserInstanced = false;
        isLaserChargedAndButtonUp = false;
        mirillas_Script_.ReturnToDefaultFar();
        if (mirilla_Externa_Script_INSCENE != null)
        {
            mirilla_Externa_Script_INSCENE.DestroyMirilla_Externa();
        }


    }

    void ShootChargedLaser()
    {
        
        chargedLaser_Script_.ShootChargedLaser();
        isLaserChargedAndButtonUp = false;
        isLaserCharged = false;
        conteoUseBeforeDeactivateChargedLaser = 0;

        mirillas_Script_.ReturnToDefaultFar();
        if (mirilla_Externa_Script_INSCENE != null)
        {
            mirilla_Externa_Script_INSCENE.DestroyMirilla_Externa();
        }


        Destroy(chargingLaserFX_INSCENE.gameObject);
    }

    public void FireButtonReleased()
    {
        if (isLaserCharged == false)
        {
            isShootButtonPressed = false;
            DeactivateConteoChargeLaser();
            Destroy(chargingLaserFX_INSCENE.gameObject);
        }
        else
        {
            isShootButtonPressed = false;
            BeginChargedLaserConteoDeactivation();
            Destroy(chargingLaserFX_INSCENE.gameObject);
        }
    }

    public void FireButtonPressed()
    {
        if (isLaserCharged == false)
        {
            isShootButtonPressed = true;
            BeginChargeLaser();
            StartCoroutine("Fire");
        }
        if (isLaserCharged == true)
        {
            if ((isLaserChargedAndButtonUp == true) && (chargedLaser_Script_.isSphereShot == false)) //DISPARO
            {

                ShootChargedLaser();
                BeginChargeLaser(); //creo que aquí está el por qué se instancian varios Fx al disparar. comentar para probar

            }
        }
    }


    public void AddOneLaserPowerUp()
    {
        CheckLaserPowerUpSound();

        if (laserUpgradesCaught < 2)
        {
            laserUpgradesCaught += 1;

        }
        else
        {
            laserUpgradesCaught = 2;
        }
    }

    private void CheckLaserPowerUpSound()
    {
        if ((laserUpgradesCaught == 0) && (playerShootingAudioSource.clip != singleLaserFired_Sound))
        {
            playerShootingAudioSource.clip = singleLaserFired_Sound;
        }
        else if ((laserUpgradesCaught == 1) && (playerShootingAudioSource.clip != twinLaserFired_Sound))
        {
            playerShootingAudioSource.clip = twinLaserFired_Sound;
        }
        else if ((laserUpgradesCaught == 2) && (playerShootingAudioSource.clip != hyperLaserFired_Sound))
        {
            playerShootingAudioSource.clip = hyperLaserFired_Sound;
        }
    }

    public void AddOneBomb()
    {
        actualBombs += 1;
        //playerDisplay_Script_.SetUIBombs(actualBombs);
        playerDisplay_Script_.AddUIBomb(actualBombs, +1); //+1 porque se añaden bombas
        
    }
    public void DecreaseOneBomb()
    {
        actualBombs -= 1;
        //playerDisplay_Script_.SetUIBombs(actualBombs);
        playerDisplay_Script_.AddUIBomb(actualBombs, -1); //-1 porque se quitan bombas
    }

}

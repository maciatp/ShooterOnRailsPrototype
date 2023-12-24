using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealth_Script : MonoBehaviour
{
    [Header("Bools")]
    public bool totalHealthAtStart = false;
    public bool isPlayerInvincible = false;
    public bool isExtraHealthActivated = false;
    [Space]
    [Header("Player Health")]
    public float actualPlayerHealth;
    public float totalPlayerHealth;
    public float maxPlayerHealth;
    public int currentLives = 3;
    public float conteoCoolDownDamage = 0;
    public float coolDownDamageTimeSpan = 1;
    public int actualGoldRings = 0;
    [Space]
    [Header("Parameters")]
    public int movedWhenDamagedDistance = 7;
    [Space]
    [Header("Public References")]
    public GameObject lightningFX;
    public GameObject smokeFX;
    public ParticleSystem smokeParticleSystem;
    public ParticleSystem lightningParticleSystem;
    
    //public Transform cameraParent;

    [Header("Public Script References")]
    
    public UIHealthBar_Script uIHealthBar_Script_;
    public UIGoldRings_Script uIGoldRings_Script_;
    public UILivesText_Script uILivesText_Script_;
   
    public Rigidbody rB_playerInScene;
    public PlayerMovement_Script playerMovement_Script_;
    public UIDamage_Script uIDamage_Script_;
    public CinemachineImpulseSource cinemachineImpulse_;
    [Space]
    [Header("Damage Animation")]
    public Animator playerAnimator;
    public Animation damageAnimation;


    [Space]
    public EnemyHealth_Script enemyHealth_Script_;
    [Space]
    [Header("Audio References")]
    public AudioSource playerHealthBarAudioSource;
    public AudioSource playerHitAudioSource;

    public AudioClip healthAlertLight;
    public AudioClip healthAlertModerate;
    public AudioClip healthAlertExtreme;
    public AudioClip arwingHit_Sound;
    public AudioClip arwingObstacleHit_Sound;
    public AudioClip laserCountered_Sound;

    private void Awake()
    {
        uIDamage_Script_ = GameObject.Find("UIDamagePanel").GetComponent<UIDamage_Script>();
        playerMovement_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement_Script>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").gameObject.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        rB_playerInScene = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Rigidbody>();
        uIHealthBar_Script_ = GameObject.Find("UIHealthBar").GetComponent<UIHealthBar_Script>();
        uIGoldRings_Script_ = GameObject.Find("UIGoldRings").GetComponent<UIGoldRings_Script>();
        uILivesText_Script_ = GameObject.Find("UILives").GetComponent<UILivesText_Script>();
        
        cinemachineImpulse_ = this.GetComponent<CinemachineImpulseSource>();
        //cameraParent = GameObject.Find("CameraHolder").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        smokeFX.SetActive(false);
        lightningFX.SetActive(false);
        
        if(totalHealthAtStart == true)
        {
             actualPlayerHealth = totalPlayerHealth;
        }
        CheckHealth();

        uIHealthBar_Script_.childHealthBar.localScale = new Vector3(actualPlayerHealth , uIHealthBar_Script_.transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (actualPlayerHealth > totalPlayerHealth) //BORRAR ESTO CUANDO HAYA TERMINADO CON PRUEBAS DE SALUD UI
        {
            actualPlayerHealth = totalPlayerHealth;
        }*/

        if(isPlayerInvincible == true)
        {
            conteoCoolDownDamage += Time.deltaTime;
            if (conteoCoolDownDamage > coolDownDamageTimeSpan)
            {
                isPlayerInvincible = false;
                conteoCoolDownDamage = 0;
            }
        }
       


    }

    private void CheckHealth()
    {
        if (actualPlayerHealth <= 10)
        {
            //Ésto sirve para setear la duración del SistPartíc. a 3. NO PUEDES CAMBIAR DURATION DIRECTAMENTE
            smokeParticleSystem.Stop();
            var main_Smoke = smokeParticleSystem.main;
            main_Smoke.duration = 0.5f;
            smokeParticleSystem.Play();

            smokeFX.SetActive(true);

            lightningParticleSystem.Stop();
            var main_Lightning = lightningParticleSystem.main;
            main_Lightning.duration = 0.5f;
            lightningParticleSystem.Play();

            lightningFX.SetActive(true);

            playerHealthBarAudioSource.clip = healthAlertExtreme;
            playerHealthBarAudioSource.Play();
        }
        if ((actualPlayerHealth <= 20) && (actualPlayerHealth > 10))
        {

            //Ésto sirve para setear la duración del SistPartíc. a 3. NO PUEDES CAMBIAR DURATION DIRECTAMENTE
            smokeParticleSystem.Stop();
            var main_Smoke = smokeParticleSystem.main;
            main_Smoke.duration = 1;
            smokeParticleSystem.Play();

            smokeFX.SetActive(true);

            lightningParticleSystem.Stop();
            var main_Lightning = lightningParticleSystem.main;
            main_Lightning.duration = 1;
            lightningParticleSystem.Play();

            lightningFX.SetActive(true);

            playerHealthBarAudioSource.clip = healthAlertModerate;
            playerHealthBarAudioSource.Play();
        }

        if ((actualPlayerHealth <= 30) && (actualPlayerHealth > 20))
        {
            //Ésto sirve para setear la duración del SistPartíc. a 3. NO PUEDES CAMBIAR DURATION DIRECTAMENTE
            smokeParticleSystem.Stop();
            var main_Smoke = smokeParticleSystem.main;
            main_Smoke.duration = 2;
            smokeParticleSystem.Play();

            smokeFX.SetActive(true);

            lightningParticleSystem.Stop();
            var main_Lightning = lightningParticleSystem.main;
            main_Lightning.duration = 2;
            lightningParticleSystem.Play();

            lightningFX.SetActive(true);

            playerHealthBarAudioSource.clip = healthAlertLight;
            playerHealthBarAudioSource.Play();

        }
        if (actualPlayerHealth > 30)
        {
            smokeFX.SetActive(false);
            lightningFX.SetActive(false);
            playerHealthBarAudioSource.Stop();

        }
    }

    public void AddGoldRing()
    {
        actualGoldRings += 1;
        uIGoldRings_Script_.CheckGoldRings(actualGoldRings);
        if((actualGoldRings >= 3) && (isExtraHealthActivated == false))
        {
            ActivateExtraHealth();
            actualGoldRings = 0;
        }
        else if ((actualGoldRings >= 3) && (isExtraHealthActivated == true))
        {
                //AÑADIR UNA VIDA
                actualGoldRings = 0;
                AddExtraLife();
        }
    }

    public void AddExtraLife()
    {
        currentLives++;
        uILivesText_Script_.UpdateLivesText();
    }

    public void ActivateExtraHealth()
    {
        isExtraHealthActivated = true;
        actualPlayerHealth *= maxPlayerHealth/100;
        totalPlayerHealth = maxPlayerHealth;
        uIHealthBar_Script_.StartCoroutine("ActivateExtraHealthUI"); //TODO: crear método que inicie la corrutina
    }

    public void IncreaseHealth(float healthwillIncrease)
    {
        if (actualPlayerHealth < totalPlayerHealth)
        {
            actualPlayerHealth += healthwillIncrease;

            uIHealthBar_Script_.StartCoroutine("IncreaseBarSize", (healthwillIncrease)); //SE LO PASO A LA BARRA DE SALUD PARA QUE CREZCA //TODO: crear método que inicie la corrutina

            if (actualPlayerHealth > totalPlayerHealth)
            {
                actualPlayerHealth = totalPlayerHealth;
            }
            CheckHealth();
        }
    }

    public void DecreaseHealth(float healthWillDecrease)
    {
        if(actualPlayerHealth > 0)
        {
            actualPlayerHealth -= healthWillDecrease;

           
           cinemachineImpulse_.GenerateImpulse(); //CAMERA SHAKE!!

            uIHealthBar_Script_.StartCoroutine("DecreaseBarSize", (-healthWillDecrease)); // SE LO PASO A LA BARRA PARA QUE DECREZCA  //TODO: crear método que inicie la corrutina
            ActivateCoolDownDamage();

           
            int rand = Random.Range(movedWhenDamagedDistance, -movedWhenDamagedDistance);
            if (rand < 0)
            {
                playerMovement_Script_.LocalMove(-movedWhenDamagedDistance, movedWhenDamagedDistance, 10, 10, false); //PARA MOVER AL PLAYER CUANDO SUFRE DAÑO
                
            }
            else if (rand >= 0)
            {
                playerMovement_Script_.LocalMove(movedWhenDamagedDistance, movedWhenDamagedDistance, 10, 10, false); //PARA MOVER AL PLAYER CUANDO SUFRE DAÑO
            }
           


            playerAnimator.Play("ReceiveDamage");
            uIDamage_Script_.PlayDamageUI();
        }
        
       else if (actualPlayerHealth <= 0)
       {
            actualPlayerHealth = 0;
            //DIE
          
       }
       CheckHealth();
    }

    public void ActivateCoolDownDamage()
    {
        isPlayerInvincible = true;
    }


    private void OnCollisionEnter(Collision collision)  //TODAS LAS COLISIONES QUE QUITEN DAÑO, AQUÍ
    {
        if(isPlayerInvincible != true)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemyHealth_Script_ = collision.gameObject.GetComponent<EnemyHealth_Script>();
                if( enemyHealth_Script_.isShotDown == false)
                {
                    DecreaseHealth(12);
                    playerHitAudioSource.clip = arwingObstacleHit_Sound;
                    playerHitAudioSource.Play();
                }
                                             
                enemyHealth_Script_.ExplodeShipModel(); // TODO: Pasar gestión de destrucción a nave enemiga. (cuando colisione con player)
                
            }
            if ((collision.gameObject.tag == "EnemyLaserBeam"))
            {
                EnemyBeam_Script enemyBeam_ = collision.gameObject.GetComponent<EnemyBeam_Script>();

                if(playerMovement_Script_.canCounterEnemyLasers == false)
                {
                    DecreaseHealth(enemyBeam_.damagePointsToPlayer); //el daño ya se aplica en EnemyBeam_Script
                    playerHitAudioSource.clip = arwingHit_Sound;
                    playerHitAudioSource.Play();
                    Destroy(enemyBeam_.gameObject); //TODO: Pasar gestión de destrucción a láser enemigo
                }
                else
                {

                    // SONIDO CUANDO REBOTA
                    playerHitAudioSource.clip = laserCountered_Sound;
                    playerHitAudioSource.Play();

                    enemyBeam_.gameObject.GetComponent<Rigidbody>().velocity = enemyBeam_.gameObject.transform.forward * -enemyBeam_.enemyBeam_Speed; //para que rebote el láser
                    enemyBeam_.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                }
            }
            

            if ((collision.gameObject.layer == 14)|| (collision.gameObject.tag == "Scenario"))
            {

                DecreaseHealth(10);

                playerHitAudioSource.clip = arwingHit_Sound;
                playerHitAudioSource.Play();

            }

            if (collision.gameObject.tag == "Obstacle")
            {
                DecreaseHealth(10);
               
                playerHitAudioSource.clip = arwingObstacleHit_Sound;
                playerHitAudioSource.Play();
            }

          
        }

    }
}

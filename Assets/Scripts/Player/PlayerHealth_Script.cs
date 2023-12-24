using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerHealth_Script : MonoBehaviour
{
    [Header("Bools")]
    [SerializeField] bool totalHealthAtStart = false;
    bool isPlayerInvincible = false;
    bool isExtraHealthActivated = false;
    [Space]
    [Header("Player Health")]
    [SerializeField] float currentPlayerHealth;
    [SerializeField] float totalPlayerHealth;
    [SerializeField] float maxPlayerHealth;
    [SerializeField] int currentLives = 3;
    float conteoCoolDownDamage = 0;
    float coolDownDamageTimeSpan = 1;
    [Space]
    [Header("Parameters")]
    [SerializeField] int currentGoldRings = 0;
    [SerializeField] int movedWhenDamagedDistance = 7;
    [Space]
    [Header("Public References")]
    [SerializeField] GameObject lightningFX;
    [SerializeField] GameObject smokeFX;
    [SerializeField] ParticleSystem smokeParticleSystem;
    [SerializeField] ParticleSystem lightningParticleSystem;


    
    UIHealthBar_Script uIHealthBar_Script_;
    UIGoldRings_Script uIGoldRings_Script_;
    UILivesText_Script uILivesText_Script_;
    PlayerMovement_Script playerMovement_Script_;
    UIDamage_Script uIDamage_Script_;
    Animator playerAnimator;
    


    
    [Space]
    [Header("Audio References")]
    [SerializeField] AudioSource playerHealthBarAudioSource;
    [SerializeField] AudioSource playerHitAudioSource;

    [SerializeField] AudioClip healthAlertLight;
    [SerializeField] AudioClip healthAlertModerate;
    [SerializeField] AudioClip healthAlertExtreme;
    [SerializeField] AudioClip arwingHit_Sound;
    [SerializeField] AudioClip arwingObstacleHit_Sound;
    [SerializeField] AudioClip laserCountered_Sound;

    public float CurrentHealth
    {
        get { return currentPlayerHealth; }
        set { currentPlayerHealth = value; }
    }
    public float MaxHealth
    {
        get { return maxPlayerHealth; }
        set { maxPlayerHealth = value; }
    }
    public int CurrentLives
    {
        get { return currentLives; }
        set { currentLives = value; }
    }


    private void Awake()
    {
        playerMovement_Script_ = gameObject.GetComponent<PlayerMovement_Script>();
        playerAnimator = gameObject.GetComponent<Animator>();
              
        uIDamage_Script_ = GameObject.Find("UIDamagePanel").GetComponent<UIDamage_Script>();
        uIHealthBar_Script_ = GameObject.Find("UIHealthBar").GetComponent<UIHealthBar_Script>();
        uIGoldRings_Script_ = GameObject.Find("UIGoldRings").GetComponent<UIGoldRings_Script>();
        uILivesText_Script_ = GameObject.Find("UILives").GetComponent<UILivesText_Script>();
       
    }
    // Start is called before the first frame update
    void Start()
    {
        smokeFX.SetActive(false);
        lightningFX.SetActive(false);
        
        if(totalHealthAtStart == true)
        {
             currentPlayerHealth = totalPlayerHealth;
        }
        CheckHealth();

        uIHealthBar_Script_.childHealthBar.localScale = new Vector3(currentPlayerHealth , uIHealthBar_Script_.transform.localScale.y);
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
        if (currentPlayerHealth <= 10)
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
        if ((currentPlayerHealth <= 20) && (currentPlayerHealth > 10))
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

        if ((currentPlayerHealth <= 30) && (currentPlayerHealth > 20))
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
        if (currentPlayerHealth > 30)
        {
            smokeFX.SetActive(false);
            lightningFX.SetActive(false);
            playerHealthBarAudioSource.Stop();

        }
    }

    public void AddGoldRing()
    {
        currentGoldRings += 1;
        uIGoldRings_Script_.CheckGoldRings(currentGoldRings);
        if((currentGoldRings >= 3) && (isExtraHealthActivated == false))
        {
            ActivateExtraHealth();
            currentGoldRings = 0;
        }
        else if ((currentGoldRings >= 3) && (isExtraHealthActivated == true))
        {
                //AÑADIR UNA VIDA
                currentGoldRings = 0;
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
        currentPlayerHealth *= maxPlayerHealth/100;
        totalPlayerHealth = maxPlayerHealth;
        uIHealthBar_Script_.StartCoroutine("ActivateExtraHealthUI"); //TODO: crear método que inicie la corrutina
    }

    public void IncreaseHealth(float healthwillIncrease)
    {
        if (currentPlayerHealth < totalPlayerHealth)
        {
            currentPlayerHealth += healthwillIncrease;

            uIHealthBar_Script_.StartCoroutine("IncreaseBarSize", (healthwillIncrease)); //SE LO PASO A LA BARRA DE SALUD PARA QUE CREZCA //TODO: crear método que inicie la corrutina

            if (currentPlayerHealth > totalPlayerHealth)
            {
                currentPlayerHealth = totalPlayerHealth;
            }
            CheckHealth();
        }
    }

    public void DecreaseHealth(float healthWillDecrease)
    {
        if(currentPlayerHealth > 0)
        {
            currentPlayerHealth -= healthWillDecrease;

           
           gameObject.GetComponent<CinemachineImpulseSource>().GenerateImpulse(); //CAMERA SHAKE!!

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
        
       else if (currentPlayerHealth <= 0)
       {
            currentPlayerHealth = 0;
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

                EnemyHealth_Script enemyHealth_Script_ = collision.gameObject.GetComponent<EnemyHealth_Script>();
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

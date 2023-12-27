using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth_Script : MonoBehaviour
{

    bool isBossDead = false;
    bool isRightPartDestroyed = false;
    bool isLeftUpPartDestroyed = false;
    bool isLeftDownPartDestroyed = false;
    int partsDestroyed = 0;
    float timeToExplodeCounter = 0;
    
    
    UIBossHealth_Script uIBossHealth_Script_;


    [Header("Boss Health Parameters")]
    [SerializeField] float bossHealth = 100;
    [SerializeField] int hitsWillAdd = 10;
    [SerializeField] float timeToExplodeAfterDead = 5f;

    [Space]
    [Header("Boss Explosion Prefab References")]
    [SerializeField] GameObject waterExplosion;
    [SerializeField] GameObject bigExplosion;

    [Space]
    [Header("Boss Parts References")]
    [SerializeField] BoxCollider bossMainBodyCollider;
    [SerializeField] BoxCollider colliderPartRight;
    [SerializeField] BoxCollider colliderPartLeft_Up;
    [SerializeField] BoxCollider colliderPartLeft_Down;
    [Tooltip("Para que se vuelva rojo al daño")]
    [SerializeField] List<Animator> animators = new List<Animator>();


    [Space]
    [Header("Enemy Spawn Locations")]
    [SerializeField] Transform leftSpawn_UP;
    [SerializeField] Transform leftSpawn_DOWN;
    [SerializeField] Transform rightSpawn;

    [Space]
    [Header("Enemy Prefab References")]
    [SerializeField] List<GameObject> enemiesList = new List<GameObject>();
    
    

    [Header("Sound")]
    [SerializeField] AudioSource bossAudioSource;
    [SerializeField] AudioClip bossExplosion_sound;


    public int PartsDestroyed
    {
        get { return partsDestroyed; }
        set { partsDestroyed = value; }
    }

    public bool RightPartDestroyed
    {
        get { return isRightPartDestroyed; }
        set { isRightPartDestroyed = value; }
    }
    public bool LeftDownPartDestroyed
    {
        get { return isLeftDownPartDestroyed; }
        set { isLeftDownPartDestroyed = value; }
    }
    public bool LeftUpPartDestroyed
    {
        get { return isLeftUpPartDestroyed; }
        set { isLeftUpPartDestroyed = value; }
    }

    private void Awake()
    {
        
        bossMainBodyCollider = gameObject.GetComponent<BoxCollider>();

        foreach (Animator animator in animators)
        {
            Animator _animator = animator.gameObject.GetComponent<Animator>();
        }

        uIBossHealth_Script_ = gameObject.GetComponentInParent<BossMovement_Script>().UIBossHealthScript.GetComponent<UIBossHealth_Script>();
        uIBossHealth_Script_.BossHealthScript_ = GetComponent<BossHealth_Script>();
        bossAudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(isBossDead)
        {
            timeToExplodeCounter += Time.deltaTime;
            if(timeToExplodeCounter >= timeToExplodeAfterDead)
            {
                //Explode
                ExplodeBoss();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //TODO: FALTA AÑADIR TODO EL ARMAMENTO PARA DAÑO (Bombas, charged sphere, explosions...)
        if (other.tag == "LaserBeam")
        {
            DepleteBossHealth(other.gameObject.GetComponent<Beam_Script>().damagePoints);
            
            
        }
        if((other.tag == "Water") && (isBossDead == true))
        {
            Debug.Log("He tocado agua");
            Instantiate(waterExplosion, this.transform.position, new Quaternion(0, 0, 0, 0), null);

            ExplodeBoss();
        }

    }

    private void ExplodeBoss()
    {
        GameObject.Find("UIBombEffectPanel").GetComponent<UIBombEffect_Script>().PlayUIBombAnimation(); //TODO: CAMBIAR A SU PROPIO EFECTO
        GameObject bigExplosion_ = Instantiate(bigExplosion, this.transform.position, new Quaternion(0, 0, 0, 0), null) as GameObject;
        bigExplosion_.transform.localScale *= 5;
        var mainParticles = bigExplosion_.GetComponent<ParticleSystem>().main;
        mainParticles.simulationSpeed = 0.5f;
        bossAudioSource.clip = bossExplosion_sound;
        bossAudioSource.Play();
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    public void DepleteBossHealth(float damage)
    {
        if(bossHealth > 0 )
        {
            if(isBossDead == false)
            {
                bossHealth -= damage;
                uIBossHealth_Script_.UpdateCurrentDamage(damage);
                int i = 0;
                foreach(Animator animator in animators)
                {
                    animators[i].Play("Boss_Damage_Right_anim");
                    i++;
                }

                if (bossHealth <= 0 && partsDestroyed == 3)
                {
                    //MUERTE BOSS
                    KillBoss();
                }

            }
            
        }
        

    }

    //MUERTE BOSS
    private void KillBoss()
    {
        //Suelto el boss del player y que explote al impactar contra el suelo o tras un tiempo
        bossHealth = 0;
        isBossDead = true;
        GameObject.Find("ScoreManager").GetComponent<ScoreManager_Script>().AddHits(hitsWillAdd);
        
        this.GetComponent<Animator>().StopPlayback();
        Camera.main.gameObject.GetComponent<MusicSelector_Script>().StopMusic();
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
       
    }

   

    public void BossFinalForm()
    {
        bossHealth = 100;
        ActivateBossCollider();
        this.gameObject.transform.GetChild(11).gameObject.SetActive(false);
    }
    
    public void ActivateBossCollider()
    {
        //this.GetComponent<BoxCollider>().enabled = true;
        bossMainBodyCollider.enabled = true;
    }




    //LAS LLAMO MEDIANTE ANIMATION EVENTS

    void SpawnEnemiesLeft()
    {
        //SELECCIÓN DE ENEMIGOS
        int e = Random.Range(0, enemiesList.Count);
        // e = 0 = gangaFighter, e = 1 = dragonFighter

        if (e == 0)
        {
            int i = Random.Range(0, 2);
            // i = 0 = leftSpawnUP, i = 1 = leftSpawnDown,
            
            
            if(isLeftUpPartDestroyed == true)
            {
                i = 1;
                
            }
            else if(isLeftDownPartDestroyed == true)
            {
                i = 0;
            }
           
            if (i == 0)
            {
                if (isLeftUpPartDestroyed == false)
                {
                   GameObject enemyInstanced = (GameObject)Instantiate(enemiesList[e], leftSpawn_UP.transform.position, leftSpawn_UP.transform.rotation, null);
                    enemyInstanced.GetComponent<EnemyMovementManager>().MustFollowTrack = false;
                }
            }
            else //if (i == 1)
            {
                if (isLeftDownPartDestroyed == false)
                {
                    GameObject enemyInstanced = (GameObject)Instantiate(enemiesList[e], leftSpawn_DOWN.transform.position, leftSpawn_DOWN.transform.rotation, null);
                    enemyInstanced.GetComponent<EnemyMovementManager>().MustFollowTrack = false;
                }
            }

        }
        else //if (e == 1)
        {
            int i = Random.Range(0, 2);

            // i = 0 = leftSpawnUP, i = 1 = leftSpawnDown,
            Debug.Log(i);

            if (i == 0)
            {
                if (isLeftUpPartDestroyed == false)
                {
                    GameObject enemyInstanced = (GameObject)Instantiate(enemiesList[e], leftSpawn_UP.transform.position, leftSpawn_UP.transform.rotation, null);
                    enemyInstanced.GetComponent<EnemyMovementManager>().MustFollowTrack = false;
                }
            }
            else //if (i == 1)
            {
                if (isLeftDownPartDestroyed == false)
                {
                    GameObject enemyInstanced = (GameObject)Instantiate(enemiesList[e], leftSpawn_DOWN.transform.position, leftSpawn_DOWN.transform.rotation, null);
                    enemyInstanced.GetComponent<EnemyMovementManager>().MustFollowTrack = false;
                }
            }


        }



    }

    void SpawnEnemiesRight()
    {
        if (isRightPartDestroyed == false)
        {
            //SELECCIÓN DE ENEMIGOS
            int e = Random.Range(0, 2);
            // e = 0 = gangaFighter, e = 1 = dragonFighter
            GameObject enemyInstanced = (GameObject)Instantiate(enemiesList[e], rightSpawn.transform.position, rightSpawn.transform.rotation, null);
            enemyInstanced.GetComponent<EnemyMovementManager>().MustFollowTrack = false;
        }
    }


    public void EnableRightCollider()
    {
        if(colliderPartRight != null)
        {
            colliderPartRight.enabled = true;
        }       
    }
    public void DisableRightCollider()
    {
        if(colliderPartRight != null)
        {
            colliderPartRight.enabled = false;            
        }
    }

    public void EnableLeftCollider()
    {
        if(colliderPartLeft_Up != null)
        {
            colliderPartLeft_Up.enabled = true;
        }
        if(colliderPartLeft_Down != null)
        {
            colliderPartLeft_Down.enabled = true;            
        }
    }
    public void DisableLeftCollider()
    {
        if (colliderPartLeft_Up != null)
        {
            colliderPartLeft_Up.enabled = false;
        }
        if (colliderPartLeft_Down != null)
        {
            colliderPartLeft_Down.enabled = false;
        }
    }
}

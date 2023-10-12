using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth_Script : MonoBehaviour
{

    public bool isBossDead = false;
    public float bossHealth = 100;
    public float maxBossHealth;
    public int partsDestroyed = 0;
    public int hitsWillAdd = 10;

    public bool isRightPartDestroyed = false;
    public bool isLeftUpPartDestroyed = false;
    public bool isLeftDownPartDestroyed = false;

    public GameObject waterExplosion;
    public GameObject bigExplosion;


    public BoxCollider bossCollider;
    public BoxCollider colliderRight;
    public BoxCollider colliderLeft_Up;
    public BoxCollider colliderLeft_Down;


    public Transform leftSpawn_UP;
    public Transform leftSpawn_DOWN;
    public Transform rightSpawn;

    public GameObject ganga;
    public GameObject dragon;
    private GameObject enemyInstanced;

    public List<Animator> animators = new List<Animator>();

    
    public UIBossHealth_Script uIBossHealth_Script_;
    public AudioSource bossAudioSource;

    [Header("Sound")]
    public AudioClip bossExplosion_sound;


    private void Awake()
    {
        maxBossHealth = bossHealth;
        bossCollider = this.gameObject.GetComponent<BoxCollider>();

        foreach (Animator animator in animators)
        {
            Animator _animator = animator.gameObject.GetComponent<Animator>();
        }

        uIBossHealth_Script_ = this.gameObject.transform.parent.GetComponent<BossMovement_Script>().uIBossHealth_Script_.GetComponent<UIBossHealth_Script>();
        uIBossHealth_Script_.bossHealth_Script_ = this.gameObject.GetComponent<BossHealth_Script>();
        bossAudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        //FALTA AÑADIR TODO EL ARMAMENTO PARA DAÑO
        if (other.tag == "LaserBeam")
        {
            DepleteBossHealth(other.gameObject.GetComponent<Beam_Script>().damagePoints);
            
            Debug.Log(this.gameObject.name);
        }
        if((other.tag == "Water") && (isBossDead == true))
        {
            Debug.Log("He tocado agua");
            Instantiate(waterExplosion, this.transform.position, new Quaternion(0, 0, 0, 0), null);
            
            GameObject bigExplosion_ = Instantiate(bigExplosion, this.transform.position, new Quaternion(0, 0, 0, 0), null) as GameObject;
            bigExplosion_.transform.localScale *= 5;
            var mainParticles = bigExplosion_.GetComponent<ParticleSystem>().main;
            mainParticles.simulationSpeed = 0.5f;
            bossAudioSource.clip = bossExplosion_sound;
            bossAudioSource.Play();
            Destroy(this.gameObject.transform.parent);
        }

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
        bossHealth = 0;
        isBossDead = true;
        GameObject.Find("ScoreManager").GetComponent<ScoreManager_Script>().AddHits(hitsWillAdd);
        GameObject.Find("UIBombEffectPanel").GetComponent<UIBombEffect_Script>().PlayUIBombAnimation();
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
        bossCollider.enabled = true;
    }




    //LAS LLAMO MEDIANTE ANIMATION EVENTS

    void SpawnEnemiesLeft()
    {
        //SELECCIÓN DE ENEMIGOS
        int e = Random.Range(0, 2);
        // e = 0 = gangaFighter, e = 1 = dragonFighter

        if (e == 0)
        {

            //Vector3 enemySpawnPos;

            int i = Random.Range(0, 2);

            // i = 0 = leftSpawnUP, i = 1 = leftSpawnDown,
            Debug.Log(i);
            
            if(isLeftUpPartDestroyed == true)
            {
                i = 1;
                
            }
            else if(isLeftDownPartDestroyed == true)
            {
                i = 0;
            }
            Debug.Log(i);
            if (i == 0)
            {
                if (isLeftUpPartDestroyed == false)
                {
                    //PRUEBAS PARA ALIGERAR CÓDIGO
                    //enemySpawnPos = leftSpawn_UP.transform.position;

                    enemyInstanced = (GameObject)Instantiate(ganga, leftSpawn_UP.transform.position, leftSpawn_UP.transform.rotation, null);
                    enemyInstanced.GetComponent<EnemyMovement_Script>().mustFollowRoute = false;
                }
            }
            else //if (i == 1)
            {
                if (isLeftDownPartDestroyed == false)
                {
                    //PRUEBAS PARA ALIGERAR CÓDIGO
                    //enemySpawnPos = leftSpawn_DOWN.transform.position;

                    enemyInstanced = (GameObject)Instantiate(ganga, leftSpawn_DOWN.transform.position, leftSpawn_DOWN.transform.rotation, null);
                    enemyInstanced.GetComponent<EnemyMovement_Script>().mustFollowRoute = false;
                }
            }

            //PRUEBAS PARA ALIGERAR CÓDIGO
            //enemyInstanced = (GameObject)Instantiate(ganga, enemySpawnPos, leftSpawn_UP.transform.rotation, null);
            //enemyInstanced.GetComponent<EnemyMovement_Script>().mustFollowRoute = false;

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
                    //PRUEBAS PARA ALIGERAR CÓDIGO
                    //enemySpawnPos = leftSpawn_UP.transform.position;

                    enemyInstanced = (GameObject)Instantiate(dragon, leftSpawn_UP.transform.position, leftSpawn_UP.transform.rotation, null);
                    enemyInstanced.GetComponent<EnemyMovement_Script>().mustFollowRoute = false;
                }
            }
            else //if (i == 1)
            {
                if (isLeftDownPartDestroyed == false)
                {
                    //PRUEBAS PARA ALIGERAR CÓDIGO
                    //enemySpawnPos = leftSpawn_DOWN.transform.position;

                    enemyInstanced = (GameObject)Instantiate(dragon, leftSpawn_DOWN.transform.position, leftSpawn_DOWN.transform.rotation, null);
                    enemyInstanced.GetComponent<EnemyMovement_Script>().mustFollowRoute = false;
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
            if (e == 0)
            {

                enemyInstanced = (GameObject)Instantiate(ganga, rightSpawn.transform.position, rightSpawn.transform.rotation, null);
                enemyInstanced.GetComponent<EnemyMovement_Script>().mustFollowRoute = false;

            }
            else if (e == 1)
            {

                enemyInstanced = (GameObject)Instantiate(dragon, rightSpawn.transform.position, rightSpawn.transform.rotation, null);
                enemyInstanced.GetComponent<EnemyMovement_Script>().mustFollowRoute = false;

            }
        }




    }


    public void EnableRightCollider()
    {
        if(colliderRight != null)
        {
            colliderRight.enabled = true;
        }
        //Debug.Log("activo collider DERECha");
    }
    public void DisableRightCollider()
    {
        if(colliderRight != null)
        {
            colliderRight.enabled = false;
            
        }
        //Debug.Log("desactivo collider DERECha");
    }

    public void EnableLeftCollider()
    {
        if(colliderLeft_Up != null)
        {
            colliderLeft_Up.enabled = true;

        }
        if(colliderLeft_Down != null)
        {
            colliderLeft_Down.enabled = true;
            
        }

    }
    public void DisableLeftCollider()
    {
        if (colliderLeft_Up != null)
        {
            colliderLeft_Up.enabled = false;

        }
        if (colliderLeft_Down != null)
        {
            colliderLeft_Down.enabled = false;

        }

    }
}

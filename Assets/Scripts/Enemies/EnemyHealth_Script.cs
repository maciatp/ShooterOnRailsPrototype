using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth_Script : MonoBehaviour
{
    [Header("Bools")]
    public bool isShotDown = false;
    public bool isInvulnerable = false;
    

    [Header("Parameters")]
    public float healthPoints = 3;
    public int numOfHitsWillAdd = 1;
    public float slowMoPointsWillIncrease = 0.2f;
    
    public float enemyShotDownInvulnerabilityTime = 0.3f;
    public float explodeCounter = 0;
    public float timeToExplode = 3;
    public float rotationSpeed = 2;



    [Header("Public references")]
    public Material enemyMaterial;
    public Rigidbody enemyRB;
    public BoxCollider enemyBoxCollider;
    public Transform surfaceCollided = null;

    public Animator enemyAnimator;

    [Header("Public Script references")]
    public Beam_Script beam_Script_;
    public ChargedLaserSphere_Script chargedLaserSphere_Script_;
   
    public TankMovement_Script tankMovement_Script_;
    public EnemyShooting_Script enemyShooting_Script_;
    public Cinemachine.CinemachineDollyCart cinemachineDollyCart_;

   
    

    

    public GameObject vehicleExplosionFX;
    public GameObject vehicleExplosion_WaterFX;
    public GameObject waterSplash;
    [SerializeField] GameObject smokeTrailFX;
    public GameObject smokeFXAfterImpact;
    GameObject smokeFXAfterImpact_InScene;
    public GameObject tinyFlames;

    [Header("Object Selector")]
    public int objectSelector = 0;
    public GameObject ring;
    public GameObject bomb;

    public bool shootDownEnemy = false;

    private void Awake()
    {          
        enemyAnimator = gameObject.GetComponent<Animator>(); 

        if(gameObject.GetComponent<EnemyShooting_Script>() != null) //Para los enemigos que no disparan
        {
            enemyShooting_Script_ = gameObject.GetComponent<EnemyShooting_Script>();
        }

        if(gameObject.GetComponent<Cinemachine.CinemachineDollyCart>() != null)
        {
            cinemachineDollyCart_ = gameObject.GetComponent<Cinemachine.CinemachineDollyCart>();
        }

    }
    

    // Update is called once per frame
    void Update()
    {
       if((isShotDown == true) && (explodeCounter <= timeToExplode))
        {
            //ROTACIÓN PARA CUANDO CAE
            float finalRotationSpeed = Random.RandomRange(rotationSpeed / 2, rotationSpeed);
            transform.Rotate(Vector3.forward, finalRotationSpeed * Time.deltaTime);


            explodeCounter += Time.deltaTime;
            if(explodeCounter >= timeToExplode)
            {
                ExplodeShipModel();
                explodeCounter = 0;
            }
        }        
    }


    void SpawnPowerUp()
    {
        if (objectSelector == 0) // EN 0 ES ALEATORIO
        {
            int i = Random.Range(1, 2);
            if (i == 1)
            {
                Instantiate(ring, this.transform.position, new Quaternion(0, 0, 0, 0), null);
            }
            else if (i == 2)
            {
                Instantiate(bomb, this.transform.position, new Quaternion(0, 0, 0, 0), null);
            }
           
        }
        else if (objectSelector == -1)
        {
            //NO INSTANCIAR NINGÚN OBJETO
        }
        else if (objectSelector == 1)
        {
            Instantiate(ring, this.transform.position, new Quaternion(0, 0, 0, 0), null);
        }
        else if (objectSelector == 2)
        {
            Instantiate(bomb, this.transform.position, new Quaternion(0, 0, 0, 0), null);
        }
        
    }
    private void HurtEnemyWithLaser(Collision collision)
    {
        beam_Script_ = collision.gameObject.GetComponent<Beam_Script>();
        healthPoints -= beam_Script_.damagePoints;
        if (healthPoints > 0)
        {
            RumbleUponReceivingDamage();
            Instantiate(tinyFlames, transform, false);
        }
        else
        {
            StartCoroutine(ShotDown());
        }
    }

    //ENEMIGO CAYENDO
    public IEnumerator ShotDown()
    {
        isShotDown = true;
        enemyRB.useGravity = true;
        enemyAnimator.enabled = false;

        if(cinemachineDollyCart_ != null)
        {
            enemyRB.velocity = transform.forward * cinemachineDollyCart_.m_Speed/2; //  para que caiga al ser derribado
            cinemachineDollyCart_.enabled = false;
        }
        smokeTrailFX = Instantiate(smokeTrailFX, transform) as GameObject;
       
        GameObject flames_GO = Instantiate(tinyFlames, transform, false) as GameObject;

        isInvulnerable = true;

        if ((enemyShooting_Script_ != null) && (enemyShooting_Script_.enabled == true)) // DESACTIVO ENEMY  Shooting PARA QUE SE CAIGA Y NO mire al player
        {
            enemyShooting_Script_.enabled = false;
        }

        yield return new WaitForSecondsRealtime(enemyShotDownInvulnerabilityTime); //ATENCIÓN: para que espere waitForSeconds hay que usar yield return new WaitForSeconds()

        isInvulnerable = false;

        yield return null;

    }

    public void ExplodeShipModel()
    {
        smokeTrailFX.transform.SetParent(null);
      
        smokeFXAfterImpact_InScene = Instantiate(smokeFXAfterImpact, transform) as GameObject; //TODO CHECK ROTATION
        smokeFXAfterImpact_InScene.transform.SetParent(null);
        smokeFXAfterImpact_InScene.transform.rotation = Quaternion.identity;
        
       
        if(surfaceCollided != null)
        {
            //EXPLOSIONES DIFERENTES
            if (surfaceCollided.gameObject.layer == 4) //EXPLOSION ON WATER
            {
                Instantiate(waterSplash, this.transform.position, this.transform.rotation);
                Instantiate(vehicleExplosion_WaterFX, this.transform.position, this.transform.rotation);
            }
            else if (surfaceCollided.gameObject.layer == 14)  //EXPLOSION ON GROUND
            {
                Instantiate(vehicleExplosionFX, this.transform.position, this.transform.rotation);
            }


            //EXPLOSION de todo lo demás
            else if ((surfaceCollided.gameObject.tag == "LaserBeam") || (surfaceCollided.gameObject.tag == "ChargedLaserSphere") || (surfaceCollided.gameObject.tag == "Explosion") || (surfaceCollided.gameObject.tag == "Bomb") || (surfaceCollided.gameObject.tag == "SmartBombExplosion") || (surfaceCollided.gameObject.tag == "Player"))
            {
                Instantiate(vehicleExplosionFX, this.transform.position, this.transform.rotation);
                //DESTRUYO NAVE SI LE DISPARO CON ALGO
            }
        }
        else
        {
            Instantiate(vehicleExplosionFX, this.transform.position, this.transform.rotation);
            //DESTRUYO NAVE POR CONTADOR DE TIEMPO SIN CHOCAR CON NADA
        }

        SpawnPowerUp();
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>().AddHits(numOfHitsWillAdd);
        GameObject.Find("Player").GetComponent<TimeManager_Script>().AddSlowMoPoints(slowMoPointsWillIncrease);

        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        surfaceCollided = collision.gameObject.transform;

        if (((collision.gameObject.layer == 14) || (collision.gameObject.layer == 4)) && (isShotDown == true)) //14 es el num de LAYER que tiene el escenario, 4 es el agua
        {

            ExplodeShipModel();
        }
       else if ((collision.gameObject.tag == "LaserBeam") && (isInvulnerable == false))
        {
            if (isShotDown == true)//cuando le das con el láser cuando ya está shotdown
            {
                Instantiate(tinyFlames, this.transform, false);
                ExplodeShipModel();
            }

            else if (isShotDown == false)
            {
                //HURT WITH LASER
                HurtEnemyWithLaser(collision);             
            }          
            Destroy(collision.gameObject); //TODO: Pasar gestión de destrucción de láseres a los láseres
        }
        else if ((collision.gameObject.tag == "ChargedLaserSphere")  && (isInvulnerable == false))
        {
            if(isShotDown == false)
            {
                chargedLaserSphere_Script_ = collision.gameObject.GetComponent<ChargedLaserSphere_Script>();
                healthPoints -= chargedLaserSphere_Script_.damagePoints;
                if (healthPoints <= 0) // así ChargedLaserSphere no destruye directamente, así puedes quitarle vida y no matarlo de golpe.( por si hay enemigos más resistentes)
                {
                    ExplodeShipModel(); ;
                }
              
                RumbleUponReceivingDamage();
            }
            else //SHOT DOWN TRUE
            {
                ExplodeShipModel();
            }

           
        }
        else if ((collision.gameObject.tag == "Bomb")  && (isInvulnerable == false))
        {
            ExplodeShipModel(); // EL ENEMIGO EXPLOTA CON LA BOMBA
        }
      
    }

    


    private void OnTriggerEnter(Collider other)
    {
        surfaceCollided = other.gameObject.transform;
          if ((other.gameObject.tag == "Explosion")  && (isInvulnerable == false))
          {
            
            if(isShotDown == true)
            {
                ExplodeShipModel();
            }
            else
            {                
                SelectRandomDestruction();
            }
          }
         if ((other.gameObject.tag == "SmartBombExplosion") && (isInvulnerable == false))
        {            
            SelectRandomDestruction();
        }
    }

    private void SelectRandomDestruction()
    {
        int i = Random.Range(0, 1);
        if (i == 0)
        {
            StartCoroutine(ShotDown());
        }
        else
        {
            ExplodeShipModel();
        }
    }   

    public void RumbleUponReceivingDamage()
    {       
        enemyAnimator.Play("ReceiveDamage_Anim");
    }
}

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
    public EnemyMovement_Script enemyMovement_Script_;
    public TankMovement_Script tankMovement_Script_;
    public EnemyShooting_Script enemyShooting_Script_;
    public Cinemachine.CinemachineDollyCart cinemachineDollyCart_;

   
    public ScoreManager_Script scoreManager_Script_;

    public TimeManager_Script timeManager_Script_;

    public GameObject vehicleExplosionFX;
    public GameObject vehicleExplosion_WaterFX;
    public GameObject waterSplash;
    public GameObject smokeFX;
    public GameObject smokeFX_GO;
    public GameObject tinyFlames;

    [Header("Object Selector")]
    public int objectSelector = 0;
    public GameObject ring;
    public GameObject bomb;

    public bool shootDownEnemy = false;

    private void Awake()
    {
        enemyMovement_Script_ = this.GetComponent<EnemyMovement_Script>();
        //enemyMaterial = this.GetComponent<MeshRenderer>();  
        enemyRB = this.gameObject.GetComponent<Rigidbody>();
        enemyBoxCollider = this.gameObject.GetComponent<BoxCollider>();
        scoreManager_Script_ = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>();
        // timeManager_Script_ = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager_Script>();
        timeManager_Script_ = GameObject.Find("Player").GetComponent<TimeManager_Script>();
        enemyAnimator = this.gameObject.GetComponent<Animator>(); //DESCOMENTAR CUANDO TODOS LOS ENEMIGOS TENGAN ANIMATOR
        if(this.gameObject.GetComponent<EnemyShooting_Script>() != null) //Para los enemigos que no disparan
        {
            enemyShooting_Script_ = this.gameObject.GetComponent<EnemyShooting_Script>();
        }

        if(this.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>() != null)
        {
            cinemachineDollyCart_ = this.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>();
        }

        //ESTO SE PODRÍA QUITAR
        if (this.gameObject.GetComponent<TankMovement_Script>() != null) //Para los enemigos que no disparan
        {
            tankMovement_Script_ = this.gameObject.GetComponent<TankMovement_Script>();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shootDownEnemy == true)
        {
            StartCoroutine("ShotDown");
        }

        if((healthPoints <= 0)&&(isShotDown == false))
        {

            StartCoroutine("ShotDown");
            Debug.Log("He llamado a Shotdown");
        }

       if((isShotDown == true) && (explodeCounter <= timeToExplode))
        {
            //ROTACIÓN PARA CUANDO CAE
            this.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);


            explodeCounter += Time.deltaTime;
            if(explodeCounter >= timeToExplode)
            {
                DestroyShipLogic();
                //ExplodeShipModel();
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

    
    //SE USABA PARA BORRAR LOS WAYPOINTS. SI NO HACE FALTA, MOVER LO INDISPENSABLE ENTRE SHOTDOWN Y EXPLODE
    public void DestroyShipLogic()
    {
        
        SpawnPowerUp();
        //Debug.Log("Voy a añadir puntos");
        scoreManager_Script_.AddHits(numOfHitsWillAdd); //ASEGURARME DE QUE SOLO AÑADE UN PUNTO
        timeManager_Script_.AddSlowMoPoints(slowMoPointsWillIncrease);
       
       
       
        ExplodeShipModel();

        /*
         *  if (enemyMovement_Script_.beginShootingLocation != null)
        {
            enemyMovement_Script_.DestroyWayPoints();
        }

        Destroy(this.gameObject);
         * */

    }

    private void ExplodeShipModel()
    {
        if(smokeFX_GO != null)
        {

        smokeFX_GO.transform.parent = null;
        }
       
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
        

        //ESTO SE PODRÍA BORRAR
        //if (enemyMovement_Script_ != null)
        //{
        //    enemyMovement_Script_.DestroyWayPoints();
        //}
        
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        surfaceCollided = collision.gameObject.transform;

        if (((collision.gameObject.layer == 14) || (collision.gameObject.layer == 4)) && (isShotDown == true)) //14 es el num de LAYER que tiene el escenario, 4 es el agua
        {
            
            DestroyShipLogic();
        }
       else if ((collision.gameObject.tag == "LaserBeam") && (isInvulnerable == false))
        {
              if (isShotDown == true)//cuando le das con el láser cuando ya está shotdown
            {
                Instantiate(tinyFlames, this.transform, false);
                DestroyShipLogic();
                Debug.Log("HE DESTRUIDO LA LOGICA");
            }

            else if (isShotDown == false)
            {
                //HURT WITH LASER
                HurtEnemyWithLaser(collision);
                Debug.Log("HE HERIDO AL ENEMIGO");
            }

           

            Destroy(collision.gameObject);
        }
        else if ((collision.gameObject.tag == "ChargedLaserSphere")  && (isInvulnerable == false))
        {
            if(isShotDown == false)
            {
                chargedLaserSphere_Script_ = collision.gameObject.GetComponent<ChargedLaserSphere_Script>();
                healthPoints -= chargedLaserSphere_Script_.damagePoints;
                //chargedLaserSphere_Script_.Explode();
                if (healthPoints <= 0) // así ChargedLaserSphere no destruye directamente, así puedes quitarle vida y no matarlo de golpe.( por si hay enemigos más resistentes)
                {
                    // SelectRandomDestruction();
                    DestroyShipLogic();
                }
               // Debug.Log("He destruido con la esfera");
                RumbleUponReceivingDamage();
            }
            else //SHOT DOWN TRUE
            {
                DestroyShipLogic();
            }

           
        }
        else if ((collision.gameObject.tag == "Bomb")  && (isInvulnerable == false))
        {
            DestroyShipLogic(); // EL ENEMIGO EXPLOTA CON LA BOMBA
        }
      
      

        
       
        
    }

    private void HurtEnemyWithLaser(Collision collision)
    {
        beam_Script_ = collision.gameObject.GetComponent<Beam_Script>();
        healthPoints -= beam_Script_.damagePoints;
        // Debug.Log("He restado " + beam_Script_.damagePoints);
        if (healthPoints > 0)
        {
            RumbleUponReceivingDamage();
            Instantiate(tinyFlames, this.transform, false);
            //enemyRB.AddExplosionForce(1000, this.transform.position, 100);
        }

        //else if (healthPoints <= 0)
        //{
        //    Debug.Log("VOY A EMPEZAR LA CORUTINA SHOTDOWN");
        //    StartCoroutine("ShotDown");
        //}
        
    }


    private void OnTriggerEnter(Collider other)
    {
        surfaceCollided = other.gameObject.transform;
          if ((other.gameObject.tag == "Explosion")  && (isInvulnerable == false))
          {
            
            if(isShotDown == true)
            {
                //ExplodeShipModel();
               DestroyShipLogic();
            }
            else
            {
                //StartCoroutine("ShotDown");
                //RumbleUponReceivingDamage();
                ////scoreManager_Script_.AddHits(1);
                SelectRandomDestruction();
            }

            //Debug.Log("He recibido de la explosion");
            // RumbleUponReceivingDamage();


          }
         if ((other.gameObject.tag == "SmartBombExplosion") && (isInvulnerable == false))
        {

            //DestroyShipLogic(); // EL ENEMIGO EXPLOTA CON LA BOMBA

            SelectRandomDestruction();

            //(OLD)StartCoroutine("ShotDown"); Si quieres que el enemigo sea derribado en lugar de explotar directamente.
        }
    }

    private void SelectRandomDestruction()
    {
        int i = Random.Range(0, 1);
        if (i == 0)
        {
            StartCoroutine("ShotDown");
        }
        else
        {
            DestroyShipLogic();
        }
    }

    //ENEMIGO CAYENDO
    public IEnumerator ShotDown()
    {
        isShotDown = true;
        enemyRB.useGravity = true;
        //enemyRB.velocity = Vector3.forward * 100;
        //Debug.Log(this.transform.localPosition);
        cinemachineDollyCart_.enabled = false;
        //Debug.Log(this.transform.localPosition);
        //if(this.gameObject.GetComponent<EnemyPositionUpdate_Script>() != null)
        //{
        //this.gameObject.GetComponent<EnemyPositionUpdate_Script>().enabled = false;

        //}
        


        smokeFX_GO = Instantiate(smokeFX, this.transform) as GameObject;
        GameObject flames_GO = Instantiate(tinyFlames, this.transform, false)as GameObject;
        //enemyBoxCollider.enabled = false;
        isInvulnerable = true;

        //Tengo destroy ship logic comentado porque se llama cuando está shotdown y le da a algo
       // DestroyShipLogic();

        if ((enemyShooting_Script_ != null) && (enemyShooting_Script_.enabled == true)) // DESACTIVO ENEMY  Shooting PARA QUE SE CAIGA Y NO mire al player
        {
            enemyShooting_Script_.enabled = false;
        }


       
        //Debug.Log("NO olvides cambiar WaitForSeconds a RealTime cuando hayas arreglado el enemigo hits");
        yield return new WaitForSecondsRealtime(enemyShotDownInvulnerabilityTime); //ATENCIÓN: para que espere waitForSeconds hay que usar yield return new WaitForSeconds()
        

        //enemyBoxCollider.enabled = true;
        isInvulnerable = false;
        
        
    


        yield return null;
       
                
    }

    public void RumbleUponReceivingDamage()
    {
        // enemyRB.transform.eulerAngles = new Vector3(0, -20, 0);
        enemyAnimator.Play("ReceiveDamage_Anim");
    }
}

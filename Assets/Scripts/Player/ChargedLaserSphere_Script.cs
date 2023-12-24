using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.SceneManagement;

public class ChargedLaserSphere_Script : MonoBehaviour
{
    public Transform parent;
    public float sphereSpeed;

    public int damagePoints = 5;

    public bool isSphereShot = false;


    public bool isSphereExploded = false;

    public bool canExplode = false;

    
   [Header("Public References")]
    private Rigidbody rb_sphere;
    public GameObject objectLocked;
    public GameObject trailRenderer_GameObject;
    public GameObject trailSlowMo;
    public SphereCollider sphereCollider;
    public ParticleSystem chargedLaserSphereParticleSystem;

    public AudioSource chargedLaserAudio;
    public AudioClip chargedReady;
    public AudioClip chargedShot;


   


    public Transform surfaceCollided = null;

    [Space]
    public GameObject chargedLaserExplosionFX;
    public GameObject waterSplash;

    [Header("Script References")]
    public PlayerShooting_Script playerShooting_Script_;
    public GuidedLaserTrigger_Script guidedLaserTrigger_Script_;
    public Billboard_Script billBoardLocked;
    public CinemachineImpulseSource cinemachineImpulse_;
    public TimeManager_Script timeManager_Script_;
    [SerializeField] ChargingLaserAbsorb_Script absorberScript;

    [Header("Parameters")]
    public float conteoExplode = 0;


    public float conteoExplodeTimeSpan = 1.5f;

    void Awake()
    {
        this.transform.GetChild(1).gameObject.SetActive(false); //desativo shockwavetrail por si me lo he dejado
        this.transform.GetChild(2).gameObject.SetActive(false); // desactivo campo de fuerza por si me lo he dejado activado
        parent = this.transform.parent;
        trailRenderer_GameObject = this.transform.GetChild(0).gameObject;
        trailSlowMo = this.transform.GetChild(1).gameObject;
        //rb_sphere = GetComponent<Rigidbody>();
        playerShooting_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting_Script>();
        guidedLaserTrigger_Script_ = GameObject.Find("GuidedChargedLaserTrigger").GetComponent<GuidedLaserTrigger_Script>();
        sphereCollider = this.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        chargedLaserAudio = this.GetComponent<AudioSource>();
        chargedLaserSphereParticleSystem = this.GetComponent<ParticleSystem>();
        
        cinemachineImpulse_ = this.GetComponent<CinemachineImpulseSource>();
        //timeManager_Script_ = GameObject.FindGameObjectWithTag("TimeManager").GetComponent<TimeManager_Script>();
        timeManager_Script_ = GameObject.Find("Player").gameObject.GetComponent<TimeManager_Script>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
        isSphereShot = false;
        chargedLaserAudio.clip = chargedReady;
        chargedLaserAudio.Play();
        // trailRenderer_GameObject.SetActive(true); //ENCIENDO EL TRAILRENDERER CUANDO CARGA





    }

    // Update is called once per frame
    void Update()
    {

        //DISPARADO CON LOCK
        if ((guidedLaserTrigger_Script_.objectLocked != null) && (isSphereShot == false))
        {
            objectLocked = guidedLaserTrigger_Script_.objectLocked;
        }

        if (isSphereShot == true)
        {
            trailRenderer_GameObject.SetActive(true); //ENCIENDO EL TRAILRENDERER CUANDO DISPARO
              //DISPARADO SIN LOCK
            if (objectLocked == null)
            {
                //ésta forma de mover la bola no tenía en cuenta TimeScale
                //rb_sphere.velocity = transform.forward * sphereSpeed;

                //Con ésta forma sí se tiene en cuenta TimeScale
                rb_sphere.position += transform.forward * sphereSpeed * Time.unscaledDeltaTime;
                rb_sphere.isKinematic = false;
                canExplode = true;

            }

          
            else if (objectLocked != null)
            {
                rb_sphere.position += new Vector3(objectLocked.transform.position.x - this.transform.position.x, objectLocked.transform.position.y - this.transform.position.y, objectLocked.transform.position.z - this.transform.position.z).normalized * sphereSpeed * Time.unscaledDeltaTime;

                if (objectLocked.gameObject.tag == "BillBoard")
                {
                    CheckLockedBillboard();
                    Debug.Log("Checkeo Billboard");
                }

            }
        }

        if (canExplode == true)
        {
            conteoExplode += Time.unscaledDeltaTime;
        }

        if (conteoExplode > conteoExplodeTimeSpan)
        {
            Explode();
        }
    }


    private void OnCollisionEnter(Collision collision) //COLISIONES
    {

        surfaceCollided = collision.transform;

        //LAYER 14 = SCENARIO //LAYER 4 = WATER     IR AÑADIENDO AQUÍ CONTRA LO QUE PUEDE EXPLOTAR
        if ((collision.gameObject == objectLocked) || (collision.gameObject.tag == "Enemy") || (collision.gameObject.layer == 14) || (collision.gameObject.layer == 4) || (collision.gameObject.tag == "Scenario") || (collision.gameObject.tag == "BillBoard") || (collision.gameObject.tag == "Wall_BrokenObj") || (collision.gameObject.tag == "Obstacle") || (collision.gameObject.tag == "Button"))
        {
            canExplode = true;
            Explode();
        }
        
        
    }

    public void ShootChargedLaser()
    {
        //APAGO EL TRIGGER ATTRACTOR
        absorberScript.triggerCollider.gameObject.SetActive(false);
        absorberScript.enabled = false;

        this.transform.parent = null;
        this.gameObject.layer = 11; //cambio de layer a la que toca para que no moleste antes de lanzarla
        sphereCollider.enabled = true;
        rb_sphere = gameObject.AddComponent<Rigidbody>();
        rb_sphere.useGravity = false;

        if (objectLocked != null)
        {

            canExplode = false;
            if((objectLocked.tag == "BillBoard") && (billBoardLocked == null))
            {
                billBoardLocked = objectLocked.gameObject.transform.parent.GetComponent<Billboard_Script>();
            }
        }
        if (objectLocked == null)
        {
            canExplode = true;
        }

        chargedLaserAudio.clip = chargedShot;
        chargedLaserAudio.Play();
        isSphereShot = true;

        //Debug.Log("isSphereShot is " + isSphereShot);
        //Debug.Log("has disparado chargedLAser");

        chargedLaserSphereParticleSystem.Play();

        //this.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().simulationSpace = ParticleSystemSimulationSpace.World; //hago que lightning sea WorldSpace cuando disparo
        this.transform.GetChild(2).gameObject.SetActive(true); // SE activa el campo de fuerza cuando disparas
        if (timeManager_Script_.isSlowMotionActivated == true)
        {
            this.transform.GetChild(1).gameObject.SetActive(true); //Activación Shockwave_trail

        }

    }

    public void Explode()
    {
        if (canExplode == true)
        {
            trailRenderer_GameObject.transform.parent = null;

            if(this.transform.GetChild(1).gameObject.activeInHierarchy == true)
            {
                trailSlowMo.transform.parent = null; // seguir comprobando cómo hacer que el trail slowmo no desaparezca
            }
            
            isSphereExploded = true;
            playerShooting_Script_.isChargedLaserInstanced = false;
            ExplosionFXSelect();

            cinemachineImpulse_.GenerateImpulse();//CAMERA SHAKE
            
            Destroy(this.gameObject);

        }
    }

    private void ExplosionFXSelect()
    {
        //SELECCIÓN DE EXPLOSIÓN SEGÚN A LO QUE IMPACTA
        if(surfaceCollided != null)
        {
            if (surfaceCollided.gameObject.layer == 4) //WATER
            {
                Instantiate(waterSplash, this.transform.position, new Quaternion(0, 0, 0, 0), null);
                chargedLaserExplosionFX = (GameObject)Instantiate(chargedLaserExplosionFX, this.transform.position, this.transform.rotation);

            }
            //LAYER 14 = SCENARIO //LAYER 4 = WATER IR AÑADIENDO AQUÍ CONTRA LO QUE PUEDE EXPLOTAR. SI FALTA ALGO, FALLA TODO
            else if ((surfaceCollided.gameObject == objectLocked) || (surfaceCollided.gameObject.tag == "Enemy") || (surfaceCollided.gameObject.tag == "Obstacle") || (surfaceCollided.gameObject.layer == 14) || (surfaceCollided.gameObject.tag == "Button") || (surfaceCollided.gameObject.tag == "Wall_BrokenObj") || (surfaceCollided.gameObject.tag == "BillBoard"))
            {
                //Debug.Log(surfaceCollided);
                chargedLaserExplosionFX = (GameObject)Instantiate(chargedLaserExplosionFX, this.transform.position, this.transform.rotation);
            }
        }
        else
        {
            chargedLaserExplosionFX = (GameObject)Instantiate(chargedLaserExplosionFX, this.transform.position, this.transform.rotation);
        }
        
    }

    public void Deactivate()
    {
        Destroy(gameObject);

    }

   

    

    public void CheckLockedBillboard()
    {
        if(billBoardLocked.isBillBoardActivated == true)
        {
            canExplode = true;
        }
    }
}

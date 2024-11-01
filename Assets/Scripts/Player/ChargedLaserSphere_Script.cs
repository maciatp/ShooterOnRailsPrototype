﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ChargedLaserSphere_Script : MonoBehaviour
{
    Transform parent;
    bool isSphereShot = false;
    bool isSphereExploded = false;
    bool canExplode = false;
    Transform surfaceCollided = null;
    float conteoExplode = 0;

    [Header("Parameters")]
    [SerializeField] float conteoExplodeTimeSpan = 1.5f;
    [SerializeField] float sphereSpeed;
    [SerializeField] int damagePoints = 5;
    [Tooltip("Damage that will do")]





    
   [Header("Local References")]
    private Rigidbody rb_sphere;
    GameObject objectLocked;
    [SerializeField] GameObject trailRenderer_GameObject;
    [SerializeField] GameObject trailSlowMo;
    [SerializeField] GameObject forceField;

    SphereCollider sphereCollider;
    ParticleSystem chargedLaserSphereParticleSystem;
    CinemachineImpulseSource cinemachineImpulse_;
    
    [Space]
    [Header("Audio References")]
    [SerializeField] AudioClip chargedReady;
    [SerializeField] AudioClip chargedShot;
    AudioSource chargedLaserAudioSource;


   



    [Space]
    [Header("Explosion Prefab References")]
    [SerializeField] GameObject chargedLaserExplosionFX;
    [SerializeField] GameObject waterSplash;

    [Space]
    [Header("External References")]
    PlayerShooting_Script playerShooting_Script_;
    GuidedLaserTrigger_Script guidedLaserTrigger_Script_;
    Billboard_Script billBoardLocked;
    TimeManager_Script timeManager_Script_;
    ChargingLaserAbsorb_Script absorberScript;

    public bool IsSphereShot
    {
        get { return isSphereShot; }
        set { isSphereShot = value; }
    }
    public int DamagePoints
    {
        get { return damagePoints; }
    }



    void Awake()
    {
        trailSlowMo.SetActive(false); //desativo shockwavetrail por si me lo he dejado
        
        parent = this.transform.parent;
        
        
        
        //rb_sphere = GetComponent<Rigidbody>(); //YA ESTABA
        playerShooting_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting_Script>();
        guidedLaserTrigger_Script_ = GameObject.Find("GuidedChargedLaserTrigger").GetComponent<GuidedLaserTrigger_Script>();

       //Local references
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        chargedLaserAudioSource = GetComponent<AudioSource>();
        chargedLaserSphereParticleSystem = GetComponent<ParticleSystem>();
        cinemachineImpulse_ = GetComponent<CinemachineImpulseSource>();
        absorberScript = GetComponent<ChargingLaserAbsorb_Script>();
        
        timeManager_Script_ = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<TimeManager_Script>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
        isSphereShot = false;
        chargedLaserAudioSource.clip = chargedReady;
        chargedLaserAudioSource.Play();
       
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

        chargedLaserAudioSource.clip = chargedShot;
        chargedLaserAudioSource.Play();
        isSphereShot = true;

        //Debug.Log("isSphereShot is " + isSphereShot);
        //Debug.Log("has disparado chargedLAser");

        chargedLaserSphereParticleSystem.Play();

        
        forceField.SetActive(true); // SE activa el campo de fuerza cuando disparas
        if (timeManager_Script_.IsSlowMoActivated == true)
        {
            trailSlowMo.SetActive(true); //Activación Shockwave_trail

        }

    }

    public void Explode()
    {
        if (canExplode == true)
        {
            trailRenderer_GameObject.transform.parent = null;

            if(trailSlowMo.activeInHierarchy == true)
            {
                trailSlowMo.transform.parent = null; // seguir comprobando cómo hacer que el trail slowmo no desaparezca
            }
            
            isSphereExploded = true;
            playerShooting_Script_.IsChargedLaserInstanced = false;
            ExplosionFXSelect();

            cinemachineImpulse_.GenerateImpulse();//CAMERA SHAKE
            
            Destroy(gameObject);

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

   

    

    
}

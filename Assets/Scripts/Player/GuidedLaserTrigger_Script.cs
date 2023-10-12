﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedLaserTrigger_Script : MonoBehaviour
{
    [Header("Colliders")]
    public BoxCollider triggerCollidernear;
    public SphereCollider triggerSphereCollider;
    [Space]
    

    [Space]
    [Header("Mirilla_Externa")]
    public GameObject mirilla_Externa_GO;
   
    public Mirilla_Externa_Script mirilla_Externa_Script_INSCENE;


    [Space]
    [Header("Public References")]
    public PlayerShooting_Script playerShooting_Script_;
  
    public Mirillas_Script mirillas_Script_;
    public GameObject objectLocked;
    public Billboard_Script billboardLocked;

    [Space]
    [Header("Parameters")]
    public float mirillaAugmentation = 5;

    

    public bool isObjectLocked = false;



    //private Vector3 originalMirillaLejosLocalPosition;
    //private Vector3 originalLocalScale;


    

    private void Awake()
    {
        triggerCollidernear = this.gameObject.GetComponent<BoxCollider>();
        triggerSphereCollider = this.gameObject.GetComponent<SphereCollider>();
      
        playerShooting_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting_Script>();
      

        
        mirillas_Script_ = GameObject.Find("Mirilla_Lejos").GetComponent<Mirillas_Script>();

    }
    // Start is called before the first frame update
    void Start()
    {
        triggerCollidernear.enabled = false;
        triggerSphereCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        //ARREGLAR ROTACION DE LA MIRILLA

        if(playerShooting_Script_.isLaserCharged == true)
        {
            ActivateColliders();

        }
        if ((playerShooting_Script_.isLaserCharged == false))
        {
            DeactivateColliders();

            SetMirillaToDefault();
        }


        if ((isObjectLocked == true) && (objectLocked != null))
        {
            if(objectLocked.gameObject.tag == "Enemy")
            {
                if(objectLocked.gameObject.GetComponent<EnemyHealth_Script>().isShotDown)
                {
                    isObjectLocked = false;
                    objectLocked = null;
                    mirillas_Script_.ReturnToDefaultFar();
                    mirillas_Script_.MakeREDOnceCharged();
                    mirilla_Externa_Script_INSCENE.DestroyMirilla_Externa();
                    Debug.Log("He estado aquí");
                }
            }
            //rb_Mirilla_Externa.transform.position = objectLocked.transform.position;
            //rb_Mirilla_Externa.transform.parent = null;
            //IR PROBANDO
            //rb_MirillaLejos.transform.localRotation = Quaternion.FromToRotation(rb_MirillaLejos.transform.position, Camera.main.transform.position); // new Quaternion(0,0,0,0); //QUEDA HACER QUE MIREN A CÁMARA SIEMPRE
            //Instantiate(mirilla_Externa_GO, objectLocked.transform.position, new Quaternion(0, 0, 0, 0));


        }
        if ((objectLocked == null) && (playerShooting_Script_.isLaserCharged == true))
        {
            mirillas_Script_.mirilla_Animator.SetBool("mirillaLejosIsBumping", true);
        }

        if ((objectLocked == null) && (playerShooting_Script_.isLaserCharged == false))
        {
            isObjectLocked = false;
            SetMirillaToDefault();

        }
        if((isObjectLocked == true) && (objectLocked == null))
        {
            isObjectLocked = false;
            SetMirillaToDefault();
        }
        
    }

    private void DeactivateColliders()
    {
        triggerCollidernear.enabled = false;
        triggerSphereCollider.enabled = false;
    }

    private void ActivateColliders()
    {
        triggerCollidernear.enabled = true;
        triggerSphereCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BillBoard")
        {
             billboardLocked = other.transform.parent.GetComponent<Billboard_Script>();
        }

        if ((isObjectLocked == false) && (((other.tag == "Enemy") && (other.gameObject.GetComponent<EnemyHealth_Script>().isShotDown == false)) || (other.tag == "SpecialBox") || ((other.tag == "Button") && (other.gameObject.GetComponent<Button_Script>().isButtonActivated == false)) || ((other.tag == "Button") && (other.gameObject.GetComponent<Button_Script>().isButtonLockableWhenActivated == true)) || ((other.tag == "BillBoard") && (billboardLocked.isBillBoardActivated != true))))
        {
            LockObject(other);
            
        }
        
    }

    public void SetMirillaToDefault()
    {
        // rb_Mirilla_Externa.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        //rb_Mirilla_Externa.transform.localPosition = originalMirillaLejosLocalPosition;
        //rb_Mirilla_Externa.transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y);

        mirillas_Script_.ReturnToDefaultFar();

       // mirilla_Externa_Script_INSCENE.DestroyMirilla_Externa();

        objectLocked = null;
        isObjectLocked = false;
    }

    

    private void LockObject(Collider other)
    {
        mirillas_Script_.mirilla_Animator.SetBool("mirillaLejosIsBumping", false);
        
        mirillas_Script_.DeactivateFarWhileObjectLocked();

        isObjectLocked = true;
        objectLocked = other.gameObject;
        
       Instantiate(mirilla_Externa_GO, objectLocked.transform.position, new Quaternion(0, 0, 0, 0));
        mirilla_Externa_Script_INSCENE = GameObject.FindGameObjectWithTag("Mirilla_Externa").gameObject.GetComponent<Mirilla_Externa_Script>();
        playerShooting_Script_.mirilla_Externa_Script_INSCENE = GameObject.FindGameObjectWithTag("Mirilla_Externa").gameObject.GetComponent<Mirilla_Externa_Script>();

       
    }
}

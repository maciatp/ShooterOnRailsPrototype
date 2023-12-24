using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedLaserTrigger_Script : MonoBehaviour
{
    [Header("Colliders")]
    public BoxCollider triggerColliderAttractor;
    
    [Space]
    

    [Space]
    [Header("Mirilla_Externa")]
    public GameObject mirilla_Externa_GO;
   
    Mirilla_Externa_Script mirilla_Externa_Script_INSCENE;
    public Mirilla_Externa_Script MirillaExterna
    {
        get { return mirilla_Externa_Script_INSCENE; }
        set { mirilla_Externa_Script_INSCENE = value; }
    }

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




    

    private void Awake()
    {
        triggerColliderAttractor = this.gameObject.GetComponent<BoxCollider>();
       
      
        playerShooting_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting_Script>();
      

        
        mirillas_Script_ = GameObject.Find("Mirilla_Lejos").GetComponent<Mirillas_Script>();

    }
    // Start is called before the first frame update
    void Start()
    {
        triggerColliderAttractor.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        //ARREGLAR ROTACION DE LA MIRILLA

        if(playerShooting_Script_.IsLaserCharged == true)
        {
            ActivateColliders();

        }
        if ((playerShooting_Script_.IsLaserCharged == false))
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
        }
        if ((objectLocked == null) && (playerShooting_Script_.IsLaserCharged == true))
        {
            mirillas_Script_.MirillasAnimator.SetBool("mirillaLejosIsBumping", true);
        }

        if ((objectLocked == null) && (playerShooting_Script_.IsLaserCharged == false))
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
        triggerColliderAttractor.enabled = false;
        
    }

    private void ActivateColliders()
    {
        triggerColliderAttractor.enabled = true;
       
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
        mirillas_Script_.ReturnToDefaultFar();      

        objectLocked = null;
        isObjectLocked = false;
    }

    

    private void LockObject(Collider other)
    {
        mirillas_Script_.MirillasAnimator.SetBool("mirillaLejosIsBumping", false);
        
        mirillas_Script_.DeactivateFarWhileObjectLocked();

        isObjectLocked = true;
        objectLocked = other.gameObject;
        
       Instantiate(mirilla_Externa_GO, objectLocked.transform.position, new Quaternion(0, 0, 0, 0));
        mirilla_Externa_Script_INSCENE = GameObject.FindGameObjectWithTag("Mirilla_Externa").gameObject.GetComponent<Mirilla_Externa_Script>();
        playerShooting_Script_.MirillaExterna = GameObject.FindGameObjectWithTag("Mirilla_Externa").gameObject.GetComponent<Mirilla_Externa_Script>();

       
    }
}

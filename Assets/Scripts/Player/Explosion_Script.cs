using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class Explosion_Script : MonoBehaviour
{
    private BoxCollider explosionCollider;
    private ParticleSystem explosionParticles;

   

    public float explosionForce = 10;
    public float explosionRadius = 3;
    
    [Header("Script References")]
    public Billboard_Script billboard_Script_;
    public Button_Script button_Script_;
    public DoorHouse_Script doorHouse_Script_;

    [SerializeField] float explosionDuration = 0.5f;
    float explosionCounter = 0;
    

    public float maxSize;
    public float colliderIncreasingRate = 1.04f;

    public int hitCounter = 0;

    private void Awake()
    {
        explosionCollider = this.gameObject.GetComponent<BoxCollider>();
        explosionParticles = this.gameObject.GetComponent<ParticleSystem>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
       if(this.gameObject.tag == "SmartBombExplosion")
        {
            float origChrom = 3;
            float endChrom = 0;
            float origDistortion = -50;
            float endDistorton = 0;


            //float zoom = state ? -7 : 0;
            PlayerMovement_Script playerMovement_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement_Script>();

            DOVirtual.Float(origChrom, endChrom, 3, playerMovement_Script_.Chromatic);

            DOVirtual.Float(origDistortion, endDistorton, 3, playerMovement_Script_.DistortionAmount);
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        if (explosionCollider.size.x <= maxSize)
        {
            explosionCollider.size += new Vector3((explosionCollider.size.x * colliderIncreasingRate * Time.deltaTime) ,(explosionCollider.size.y * colliderIncreasingRate * Time.deltaTime) , (explosionCollider.size.z * colliderIncreasingRate * Time.deltaTime));

        }

        explosionCounter += Time.deltaTime;
        if(explosionCounter >= explosionDuration)
        {
            explosionCollider.enabled = false;
            explosionCounter = 0;
        }
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BillBoard")
        {
            if (other.gameObject.transform.GetComponentInParent<Billboard_Script>().IsBillboardActivated == false)
            {
                other.gameObject.transform.GetComponentInParent<Billboard_Script>().ActivateBillBoard();
            }
           
        }
        if (other.tag == "Button")
        {
            other.gameObject.GetComponent<Button_Script>();
            
            if(button_Script_.IsButtonActivated == false)
            {
                button_Script_.ActivateButton();
            }
            
        }

        if(other.gameObject.tag == "Enemy")
        {
            if(other.gameObject.GetComponent<EnemyHealth_Script>().isShotDown == false)
            {
                //TEXTO HIT +
                hitCounter += other.gameObject.GetComponent<EnemyHealth_Script>().numOfHitsWillAdd;

                if (hitCounter > 0)
                {
                    //ACTIVATE HIT TEXT
                    GameObject.Find("UIHitFloatingText").gameObject.GetComponent<UIHitCombo_Script>().ActivateUIHitText(hitCounter, other.gameObject.transform.position);
                }
            }
            
        }

        if(this.gameObject.tag == "SmartBombExplosion")
        {
            if (((other.gameObject.name == "RightDoor") || (other.gameObject.name == "LeftDoor")) && (other.gameObject.transform.parent.parent.name == "DoorHouse_Bomb"))
            {
               other.transform.parent.parent.GetComponent<DoorHouse_Script>().OpenDoor();
            }
        }
        


        /*if ((this.gameObject.tag == "SmartBombExplosion"))
        {
            if((other.GetComponentInParent<DoorHouse_Script>().isBombHouse == true))
            {
                Debug.Log(other.name);
                other.gameObject.transform.GetComponentInParent<DoorHouse_Script>().OpenDoor();
            } 
            
           
        }
        /*if ((this.gameObject.tag == "SmartBombExplosion") && (other.GetComponentInParent<DoorHouse_Script>().isBombHouse == false))
        {
            Debug.Log("NO ES DOORHOUSE BOMB");
        }*/
        if (other.tag == "Wall_BrokenObj")
        {
            Debug.Log("He añadido fuerza a la pared");
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0, ForceMode.Impulse);
        }

    }
    void OnTriggerStay(Collider other)
    {
        if((gameObject.tag == "Explosion") && (((other.gameObject.name == "RightDoor") 
      || (other.gameObject.name == "LeftDoor")) && (other.gameObject.transform.parent.parent.name == "DoorHouse")))
        {
            //ABRO DOORHOUSE CON CHARGEDLASEREXPLOSION PERO NO CON SMARTBOMBEXPLOSION
            other.gameObject.transform.parent.parent.GetComponent<DoorHouse_Script>().ReceiveDamage();
            Debug.Log("he añadido daño a la DoorHouse con la ChargedLaserExplosion");
        }
        
       
    }

}

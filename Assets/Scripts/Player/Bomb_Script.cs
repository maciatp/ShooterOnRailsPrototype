using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bomb_Script : MonoBehaviour
{    
    public bool canExplode = false;
    public bool isExploded = false;
   
    
    public Rigidbody rb_Bomb;
    public ParticleSystem bombExplosionFX;
    public BoxCollider bombCollider;


    public float bombSpeed = 50f;

    public float conteoBomb = 0;

    public float bombTimeToExplode = 2f;

    public GameObject bombExplosion_GO;
    public PlayerShooting_Script playerShooting_; //se encuentra solo en el Awake
    public GameObject playerInScene;  //se encuentra solo en el Awake
    public GuidedLaserTrigger_Script guidedLaserTrigger_Script_;
    public UIBombEffect_Script uIBombEffect_Script_;
    public CinemachineImpulseSource cinemachineImpulse_;

    public GameObject enemyLocked;

   



    private void Awake()
    {
        
        playerInScene = GameObject.FindGameObjectWithTag("Player");
        playerShooting_ = playerInScene.GetComponent<PlayerShooting_Script>();

        guidedLaserTrigger_Script_ = playerInScene.transform.Find("GuidedChargedLaserTrigger").GetComponent<GuidedLaserTrigger_Script>();
        uIBombEffect_Script_ = GameObject.Find("UIBombEffectPanel").GetComponent<UIBombEffect_Script>();
        cinemachineImpulse_ = this.GetComponent<CinemachineImpulseSource>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if(guidedLaserTrigger_Script_.isObjectLocked == false)
        {
            canExplode = true;
        }
        if (guidedLaserTrigger_Script_.isObjectLocked == true)
        {
            canExplode = false;
            enemyLocked = guidedLaserTrigger_Script_.objectLocked;
            guidedLaserTrigger_Script_.SetMirillaToDefault();
            guidedLaserTrigger_Script_.MirillaExterna.DestroyMirilla_Externa();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canExplode)
        {
            conteoBomb += Time.unscaledDeltaTime;
        }

        if ((isExploded != true) && (canExplode == true))
        {
            //UNSCALED PARA EL TIEMPO BALA
            transform.position += transform.forward.normalized * bombSpeed * Time.unscaledDeltaTime;
        }

        if(enemyLocked != null)
        {
            transform.position  += new Vector3(enemyLocked.transform.position.x - this.transform.position.x, enemyLocked.transform.position.y - this.transform.position.y, enemyLocked.transform.position.z - this.transform.position.z).normalized * bombSpeed * Time.unscaledDeltaTime;
        }
       
        if((conteoBomb > bombTimeToExplode) &&(isExploded==false) && (canExplode = true))
        {
            Explode();
           
        } 
       

    }

    

    public void Explode()
    {
        cinemachineImpulse_.GenerateImpulse();
        isExploded = true;
        canExplode = false;
        playerShooting_.IsBombShot = false;
        conteoBomb = 0;
        playerShooting_.ConteoBomb = 0;
        Instantiate(bombExplosion_GO, this.gameObject.transform.position, this.gameObject.transform.rotation, null);
        uIBombEffect_Script_.PlayUIBombAnimation();
        Destroy(gameObject);       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.layer == 14) || (collision.gameObject.layer == 4)) //LAYER 14 == SCENARIO, 4 == WATER
        {
            Explode();
        }
       else if((collision.gameObject.tag == "Enemy") || (collision.gameObject.tag == "Obstacle") || (collision.gameObject.tag == "Scenario") || (collision.gameObject.tag == "Wall_BrokenObj") || (collision.gameObject.tag == "BillBoard") || collision.gameObject.tag == "Button")
        {
            
            Explode();
        }
    }


}

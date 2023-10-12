using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHouse_Script : MonoBehaviour
{
    [Header("Paramaters")]
    public bool isBombHouse = false;
   
    public bool isDoorClosed = true;
    public int damageToOpen = 3;
    public int currentDamage = 0;

    [Header("References")]
    public Animator doorHouseAnimator;
    public BoxCollider leftDoorCollider;
    public BoxCollider rightDoorCollider;
    public Transform objectSpawnLocation;
    public int objectSelector;
    public GameObject laserPowerUp;
    public GameObject bombPowerUp;
    public GameObject silverRing;
    public GameObject goldRing;

    [Header("Script References")]
    public Button_Script button_Script_;
    public SpriteRenderer bombSprite;
    public ScoreManager_Script scoreManager_Script_;

   

    private void Awake()
    {
        objectSpawnLocation = this.transform.GetChild(3).gameObject.transform;// donde va a spawnearse el objeto
        scoreManager_Script_ = GameObject.Find("ScoreManager").GetComponent<ScoreManager_Script>();
        leftDoorCollider = this.gameObject.transform.GetChild(1).GetChild(0).GetComponent<BoxCollider>();
        rightDoorCollider = this.gameObject.transform.GetChild(2).GetChild(0).GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        doorHouseAnimator = this.GetComponent<Animator>();


        if (this.gameObject.name == "DoorHouse_Button")
        {
            button_Script_ = this.transform.GetChild(3).GetComponent<Button_Script>();
        }
       if(this.gameObject.name == "DoorHouse_Bomb")
        {
            bombSprite = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        }
        
    }

    

    public void ReceiveDamage()
    {
        currentDamage += 1;
        doorHouseAnimator.Play("ReceiveDamageDoor");
        if (currentDamage >= damageToOpen)
        {
            OpenDoor();

        }

    }

    void SpawnObject()
    {
        if (objectSelector == 0) // EN 0 ES ALEATORIO
        {
            
            int i = Random.Range(1, 4);
            if (i == 1)
            {
                Instantiate(laserPowerUp, objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);
            }
            else if (i == 2)
            {
                Instantiate(bombPowerUp, objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);
            }
            else if (i == 3)
            {
                Instantiate(silverRing, objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);
            }
            else if (i == 4)
            {
                Instantiate(goldRing, objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);
            }
        }
        else if(objectSelector == -1)
        {
            //NO INSTANCIAR NINGÚN OBJETO
        }
        else if (objectSelector == 1)
        {
           
                Instantiate(laserPowerUp, objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);
            
           
        }
        else if (objectSelector == 2)
        {

            Instantiate(bombPowerUp, objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);


        }
       else if (objectSelector == 3)
        {

            Instantiate(silverRing, objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);


        }
       else if (objectSelector == 4)
        {

            Instantiate(goldRing, objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);


        }
    }

    public void OpenDoor()
    {
       if(isDoorClosed == true)
        {
            Destroy(rightDoorCollider);
            Destroy(leftDoorCollider);
            SpawnObject();
            if (this.gameObject.name == "DoorHouse_Bomb")
            {
                bombSprite.enabled = false;
            }

            isDoorClosed = false;
            doorHouseAnimator.SetBool("isDoorClosed", false);
            doorHouseAnimator.Play("Opening");
            scoreManager_Script_.AddHits(1);
        }
        
        
    }

  
    
    private void OnCollisionEnter(Collision collision)
    {
        if (this.gameObject.name == "DoorHouse")// SÓLO FUNCIONA SI SE LLAMA EXACTAMENTE IGUAL (CON CLONE INCLUIDO)
        {
            if(collision.gameObject.tag == "LaserBeam")
            {
                if(isDoorClosed == true)
                {
                    ReceiveDamage();
                }
               
                // Debug.Log("estoy recibiendo daño puerta");
            }
            else if ((collision.gameObject.tag == "ChargedLaserSphere"))
            {
                if (isDoorClosed == true)
                {
                    OpenDoor();
                }
                
            }

        }
             
        else if ((this.gameObject.name == "DoorHouse_Bomb") && (collision.gameObject.tag == "Bomb"))// SÓLO FUNCIONA SI SE LLAMA EXACTAMENTE IGUAL (CON CLONE INCLUIDO)
        {
            bombSprite.enabled = false;
            if (isDoorClosed == true)
            {
                OpenDoor();
            }
            // Debug.Log("la EXPLOSION DE LA BOMBA ha abierto la puerta");
        }
    }

    /* esto se llama desde explosion_script
     * 
    private void OnTriggerEnter(Collider other)
    {
        if((this.gameObject.name == "DoorHouse_Bomb") && (other.gameObject.tag == "SmartBombExplosion"))
        {
            bombSprite.enabled = false;
            OpenDoor();
        }
    }
    */
}

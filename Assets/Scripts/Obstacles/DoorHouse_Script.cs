using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHouse_Script : MonoBehaviour
{
    [Header("Paramaters")]
    [SerializeField] int damageToOpen = 3;
    [SerializeField] bool isBombHouse = false;
    [Tooltip("-1:Random, -2:none, range from below")]
    [SerializeField] int objectSelector;
    [SerializeField] List<GameObject> powerUpsList;
   
    bool isDoorClosed = true;
    int currentDamage = 0;

    [Header("References")]
    
    [SerializeField] BoxCollider leftDoorCollider;
    [SerializeField] BoxCollider rightDoorCollider;
    [SerializeField] Transform objectSpawnLocation;

    

    public void ReceiveDamage()
    {
        currentDamage += 1;
        GetComponent<Animator>().Play("ReceiveDamageDoor");
        if (currentDamage >= damageToOpen)
        {
            OpenDoor();

        }

    }

    void SpawnObject()
    {
        if (objectSelector == -1) // EN -1 ES ALEATORIO
        {
            
            int i = Random.Range(0, powerUpsList.Count);
            Instantiate(powerUpsList[i], objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);
        }
        else if(objectSelector == -2) // -2 NO INSTANCIA NADA
        {
            //NO INSTANCIAR NINGÚN OBJETO
        }
        else
        {
            Instantiate(powerUpsList[objectSelector], objectSpawnLocation.transform.position, new Quaternion(0, 0, 0, 0), null);
        }
    }      

    public void OpenDoor()
    {
       if(isDoorClosed == true)
        {
            Animator doorHouseAnimator = GetComponent<Animator>();

            isDoorClosed = false;
            rightDoorCollider.enabled = false;
            leftDoorCollider.enabled = false;
            doorHouseAnimator.SetBool("isDoorClosed", false);
            doorHouseAnimator.Play("Opening");
            GameObject.Find("ScoreManager").GetComponent<ScoreManager_Script>().AddHits(1);
            SpawnObject();

            if (gameObject.name == "DoorHouse_Bomb")
            {
                //DESACTIVO SPRITE DE BOMBA
                GetComponentInChildren<SpriteRenderer>().enabled = false;
            }

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
            
            if (isDoorClosed == true)
            {
                OpenDoor();
            }
            
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

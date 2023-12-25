using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MonoBehaviour
{   
    bool isButtonActivated = false;
    [SerializeField] bool isButtonLockableWhenActivated = false;
    
    public bool IsButtonActivated
    {
        get { return isButtonActivated; }
        set { isButtonActivated = value; }
    }
    public bool IsButtonLockableWhenActivated
    {
        get { return isButtonLockableWhenActivated; }
        set { isButtonLockableWhenActivated = value; }
    }


    private void Awake()
    {   
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger botón por explosion");
        if ((isButtonActivated == false) && ((other.gameObject.tag == "Explosion") || (other.gameObject.tag == "SmartBombExplosion")))
        {
            ActivateButton();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colisión botón");
        if ((isButtonActivated == false) && ((collision.gameObject.tag == "LaserBeam") || (collision.gameObject.tag == "ChargedLaserSphere") || (collision.gameObject.tag == "Bomb")))
        {
            ActivateButton();

        }
    }

    public void ActivateButton()
    {
        Debug.Log("He activado botón");        
        isButtonActivated = true;
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>().AddHits(1);
        GetComponent<MeshRenderer>().material.color = Color.green;
        
        if(transform.parent.name == "DoorHouse_Button") // SÓLO FUNCIONA SI SE LLAMA EXACTAMENTE IGUAL (CON CLONE INCLUIDO)
        {
            transform.parent.GetComponent<DoorHouse_Script>().OpenDoor();
        }
        if(transform.parent.name == "Column_Button_Prefab")
        {
            transform.parent.GetComponent<ColumnPrefab_Script>().PlayAnimationColumn();
        }
        if(transform.name == "Button_TrackChanger")
        {
            transform.parent.GetChild(1).gameObject.GetComponent<TrackChanger_Trigger_Script>().EnableTrigger();
        }
    }
}

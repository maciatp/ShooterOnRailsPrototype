using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MonoBehaviour
{
    public bool isButtonUsable = true;
    public bool isButtonActivated = false;
    public bool isButtonLockableWhenActivated = false;
    public MeshRenderer buttonMesh;
    public ScoreManager_Script scoreManager_Script_;
    //public BoxCollider buttonCollider;

    private void Awake()
    {
        scoreManager_Script_ = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager_Script>();
        //buttonCollider = this.gameObject.GetComponent<BoxCollider>();
        buttonMesh = this.gameObject.GetComponent<MeshRenderer>();
        buttonMesh.material.color = Color.red;
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
            Debug.Log("He activado botón");

        }
    }

    public void ActivateButton()
    {
        //buttonCollider.enabled = false;
        isButtonActivated = true;
        scoreManager_Script_.AddHits(1);
        buttonMesh.material.color = Color.green;
        isButtonUsable = false;
        if(this.transform.parent.name == "DoorHouse_Button") // SÓLO FUNCIONA SI SE LLAMA EXACTAMENTE IGUAL (CON CLONE INCLUIDO)
        {
            this.transform.parent.GetComponent<DoorHouse_Script>().OpenDoor();
        }
        if(this.transform.parent.name == "Column_Button_Prefab")
        {
            this.transform.parent.GetComponent<ColumnPrefab_Script>().PlayAnimationColumn();
        }
        if(this.transform.name == "Button_TrackChanger")
        {
            this.transform.parent.GetChild(1).gameObject.GetComponent<TrackChanger_Trigger_Script>().EnableTrigger();
        }
    }
}

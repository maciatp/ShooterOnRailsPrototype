using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TrackChanger_Trigger_Script : MonoBehaviour
{
    
    public int currentTriggersActivated = 0;
   
    public BoxCollider changerTrigger;

    public bool hasToActivateChat_WhenCompleted = false;
    public List<GameObject> chatFinishTriggersToActivate = new List<GameObject>();
   // public GameObject chatTriggerToActivate_WhenCompleted;

    public bool hasToActivateMIDdleChat = false;
    public int midChat = 0;
    public List<GameObject> chatTriggersToActivate_During = new List<GameObject>();
    //public GameObject chatTriggerToActivate_During;

    public Button_Script button_Script_;

    public List<BoxCollider> triggersToActivate = new List<BoxCollider>();


    private void Awake()
    {
        if(this.gameObject.transform.parent.name == "DollyTrack_Changer_Button")
        {
            changerTrigger = this.gameObject.transform.parent.GetChild(1).gameObject.GetComponent<BoxCollider>();

            changerTrigger.enabled = false;

            button_Script_ = this.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<Button_Script>();
        }
        else if(this.gameObject.transform.parent.name == "DollyTrack_Changer_Trigger")
        {
            changerTrigger = this.gameObject.transform.parent.GetChild(0).gameObject.GetComponent<BoxCollider>();

            changerTrigger.enabled = false;
            foreach(BoxCollider trigger in triggersToActivate)
            {
              BoxCollider  _triggerCollider = trigger.gameObject.GetComponent<BoxCollider>();
            }

            if (hasToActivateChat_WhenCompleted)
            {
                foreach (GameObject chatTriggerWhenEnded in chatFinishTriggersToActivate)
                {
                    chatTriggerWhenEnded.SetActive(false);
                }
               // chatTriggerToActivate_WhenCompleted.gameObject.SetActive(false);
            }
            if(hasToActivateMIDdleChat)
            {
                foreach (GameObject chatTriggerMIDDLE in chatTriggersToActivate_During)
                {
                    chatTriggerMIDDLE.SetActive(false);
                }
                //chatTriggerToActivate_During.SetActive(false);
            }

        }
    }
    

    public void EnableTrigger()
    {
        changerTrigger.enabled = true;
    }

    public void DisableTrigger()
    {
        changerTrigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (this.gameObject.transform.parent.name == "DollyTrack_Changer_Button")
            {
                //Debug.Log("HE sido activado!" +  other.name);

                //DESACTIVADO MIENTRAS TESTEO RAILCHANGER. AÑADIR NEXT TRACK DEL ARRAY
                //GameObject.Find("GameplayPlane").gameObject.GetComponent<RailChanger_Script>().ChangeTrack(this.gameObject.transform.parent.gameObject.GetComponent<CinemachineSmoothPath>());
                GameObject.Find("GameplayPlane").gameObject.GetComponent<RailChanger_Script>().ChangeExtraTrack();
                DisableTrigger();
            }
            else if (this.gameObject.transform.parent.name == "DollyTrack_Changer_Trigger")
            {
                ////ACTIVA EL TRIGGER DEL CHAT CUANDO COMPLETAS EL REQUISITO
                //if(hasToActivateChat_WhenCompleted)
                //{
                //    foreach(GameObject chatTriggerWhenEnded in chatFinishTriggersToActivate)
                //    {
                //        chatTriggerWhenEnded.SetActive(true);
                //    }
                //    //chatTriggerToActivate_WhenCompleted.SetActive(true);
                //}
                
                Debug.Log("Casi he cambiado de carril");
                if(currentTriggersActivated == triggersToActivate.Count)
                {
                    GameObject.Find("GameplayPlane").gameObject.GetComponent<RailChanger_Script>().ChangeExtraTrack();
                    DisableTrigger();
                    Debug.Log("He Cambiado de carril");

                    //chatTriggerToActivate_WhenCompleted.gameObject.SetActive(true);

                    

                }
            }
        }
    }
}

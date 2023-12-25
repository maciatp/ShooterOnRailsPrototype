using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TrackChanger_Trigger_Script : MonoBehaviour
{
    BoxCollider changerTrigger;
    
    [Header("Triggers To activate rail change")]
    int currentTriggersActivated = 0;
    [SerializeField] public List<BoxCollider> triggersToActivate = new List<BoxCollider>();
   
    [Header("Chat when completed")]
    [SerializeField] bool hasToActivateChat_WhenCompleted = false;
    [SerializeField] List<GameObject> chatFinishTriggersToActivate = new List<GameObject>();

    [Space]
    [Header("Chat During Sequence")]
    [SerializeField] bool hasToActivateMidChat = false; //activa chat cuando has pasado por un mínimo de triggers
    [Tooltip("Número de Triggers (de la lista de Triggers to activate) por los que hay que pasar para activar el mid chat.")] 
    [SerializeField] int activateChatTriggerAfterTheseTriggersCrossed = 0;
    [Tooltip("El trigger de chat en la escena que se va a activar DURANTE la secuencia")]
    [SerializeField] List<GameObject> chatTriggersToActivate_During = new List<GameObject>();
    
    //para cambiar de rail con botones //TODO
    Button_Script button_Script_;


    public int MidChat
    {
        get { return activateChatTriggerAfterTheseTriggersCrossed; }
        set { activateChatTriggerAfterTheseTriggersCrossed = value; }
    }
    public int CurrentTriggersActivated
    {
        get { return currentTriggersActivated; }    
        set { currentTriggersActivated = value; }
    }
    public bool ActivatesChatWhenCompleted
    {
        get { return hasToActivateChat_WhenCompleted; }
        set { hasToActivateChat_WhenCompleted = value; }
    }
    public List<GameObject> ChatFinishTriggersList
    {
        get { return chatFinishTriggersToActivate; }
        set { chatFinishTriggersToActivate = value;}
    }
    public bool ActivatesChatOnCertainTrigger
    {
        get { return hasToActivateMidChat; }
        set { hasToActivateMidChat = value; }
    }
    public List<GameObject> ChatMiddleTriggersList
    {
        get { return chatTriggersToActivate_During; }
        set { chatTriggersToActivate_During = value; }
    }

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
               
            }
            if(hasToActivateMidChat)
            {
                foreach (GameObject chatTriggerMIDDLE in chatTriggersToActivate_During)
                {
                    chatTriggerMIDDLE.SetActive(false);
                }
               
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
                GameObject.Find("GameplayPlane").gameObject.GetComponent<TrackManager>().ChangeExtraTrack();
                DisableTrigger();
            }
            else if (this.gameObject.transform.parent.name == "DollyTrack_Changer_Trigger")
            {
                Debug.Log("Casi he cambiado de carril");
                if(currentTriggersActivated == triggersToActivate.Count)
                {
                    GameObject.Find("GameplayPlane").gameObject.GetComponent<TrackManager>().ChangeExtraTrack();
                    DisableTrigger();
                    Debug.Log("He Cambiado de carril");


                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToActivateExtraPath_Script : MonoBehaviour
{
    bool isActivated = false;
    
    TrackChanger_Trigger_Script trackChanger_Trigger_Script_;


    private void Awake()
    {
        
        trackChanger_Trigger_Script_ = this.gameObject.transform.parent.GetChild(0).GetComponent<TrackChanger_Trigger_Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isActivated = true;
            trackChanger_Trigger_Script_.CurrentTriggersActivated++;
            
            if (trackChanger_Trigger_Script_.CurrentTriggersActivated >= (trackChanger_Trigger_Script_.triggersToActivate.Count))
            {
                trackChanger_Trigger_Script_.EnableTrigger();
                Debug.Log("He activado el trigger de cambio carril");

                //ACTIVA EL TRIGGER DEL CHAT CUANDO COMPLETAS EL REQUISITO
                if (trackChanger_Trigger_Script_.ActivatesChatWhenCompleted)
                {
                    foreach (GameObject chatTriggerWhenEnded in trackChanger_Trigger_Script_.ChatFinishTriggersList)
                    {
                        chatTriggerWhenEnded.SetActive(true);
                    }
                   
                }

            }

            //ACTIVA EL TRIGGER DE PISTA (SE ACTIVA CUANDO LLEVAS X TRIGGERS) -> LUEGO HAY QUE PASAR POR EL TRIGGER! 
            if ((trackChanger_Trigger_Script_.ActivatesChatOnCertainTrigger) && (trackChanger_Trigger_Script_.CurrentTriggersActivated >= trackChanger_Trigger_Script_.MidChat))
            {

                foreach (GameObject chatTriggerMIDDLE in trackChanger_Trigger_Script_.ChatMiddleTriggersList)
                {
                    chatTriggerMIDDLE.SetActive(true);
                }
                
                Debug.Log("He activado el trigger de chat MId extra-sequence");
            }


            gameObject.SetActive(false);
        }
    }

}

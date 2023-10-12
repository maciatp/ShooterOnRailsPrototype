using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToActivateExtraPath_Script : MonoBehaviour
{
    [SerializeField] bool isActivated = false;
    BoxCollider triggerCollider;
    TrackChanger_Trigger_Script trackChanger_Trigger_Script_;


    private void Awake()
    {
        triggerCollider = this.gameObject.GetComponent<BoxCollider>();
        trackChanger_Trigger_Script_ = this.gameObject.transform.parent.GetChild(0).GetComponent<TrackChanger_Trigger_Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isActivated = true;
            trackChanger_Trigger_Script_.currentTriggersActivated++;
            //if(trackChanger_Trigger_Script_.currentTriggersActivated >= 3)
            //{
              //TEST PARA CUANDO PUEDA METER DIÁLOGOS A LOS CAMBIOS DE CARRIL.
            //}
            if (trackChanger_Trigger_Script_.currentTriggersActivated >= (trackChanger_Trigger_Script_.triggersToActivate.Count))
            {
                trackChanger_Trigger_Script_.EnableTrigger();
                Debug.Log("He activado el trigger de cambio carril");

                //ACTIVA EL TRIGGER DEL CHAT CUANDO COMPLETAS EL REQUISITO
                if (trackChanger_Trigger_Script_.hasToActivateChat_WhenCompleted)
                {
                    foreach (GameObject chatTriggerWhenEnded in trackChanger_Trigger_Script_.chatFinishTriggersToActivate)
                    {
                        chatTriggerWhenEnded.SetActive(true);
                    }
                   
                }

            }

            //ACTIVA EL TRIGGER DE PISTA (SE ACTIVA CUANDO LLEVAS X TRIGGERS) -> LUEGO HAY QUE PASAR POR EL TRIGGER! (O LO ACTIVO directamente DESDE AQUÍ? por ahora hay que pasar por el trigger))
            if ((trackChanger_Trigger_Script_.hasToActivateMIDdleChat) && (trackChanger_Trigger_Script_.currentTriggersActivated >= trackChanger_Trigger_Script_.midChat))
            {

                foreach (GameObject chatTriggerMIDDLE in trackChanger_Trigger_Script_.chatTriggersToActivate_During)
                {
                    chatTriggerMIDDLE.SetActive(true);
                }
                
                Debug.Log("He activado el trigger intermedio");
            }


            this.gameObject.SetActive(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger_Script : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Voy a encender la luz");
            Debug.Log(other.gameObject.name);
            //Debug.Log(other.transform.Find("Directional Light").gameObject.name);
            other.transform.Find("Light").gameObject.SetActive(true);//.GetComponent<Light>().enabled = true;
            Debug.Log("Y se hizo la luz!");
        }

        if (other.CompareTag("Wingman"))
        {
            Debug.Log(other.name);
            other.transform.Find("Light").gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.Find("Light").gameObject.SetActive(false); //GetComponent<Light>().enabled = false;
        }
        if (other.CompareTag("Wingman"))
        {
            Debug.Log(other.name);
            other.transform.Find("Light").gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Wingman_Trigger_Script : MonoBehaviour
{

    public bool activateFalco = false;
    public bool activateSlippy = false;
    public bool activatePeppy = false;


    public GameObject wingmanFalcoInScene;
    public GameObject wingmanSlippyInScene;
    public GameObject wingmanPeppyInScene;
    public CinemachineSmoothPath wingmanSmoothPath;
    public float speed = 2;

    public Transform launchPosition_Falco;
    public Transform launchPosition_Slippy;
    public Transform launchPosition_Peppy;

    public Vector3 offset_Falco;
    public Vector3 offset_Slippy;
    public Vector3 offset_Peppy;

    private void Awake()
    {
        wingmanSmoothPath = this.gameObject.transform.GetChild(0).gameObject.GetComponent<CinemachineSmoothPath>();
        
        
        
        launchPosition_Falco = this.gameObject.transform.GetChild(0).Find("launchPosition_Falco").gameObject.transform;
        launchPosition_Slippy = this.gameObject.transform.GetChild(0).Find("launchPosition_Slippy").gameObject.transform;
        launchPosition_Peppy = this.gameObject.transform.GetChild(0).Find("launchPosition_Peppy").gameObject.transform;

        wingmanFalcoInScene = GameObject.Find("Wingman_Falco").gameObject;
        wingmanSlippyInScene = GameObject.Find("Wingman_Slippy").gameObject;
        wingmanPeppyInScene = GameObject.Find("Wingman_Peppy").gameObject;

        offset_Falco = launchPosition_Falco.position - wingmanSmoothPath.transform.position;
        offset_Slippy = launchPosition_Slippy.position - wingmanSmoothPath.transform.position;
        offset_Peppy = launchPosition_Peppy.position - wingmanSmoothPath.transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Debug.Log("Entro");
            ActivateWingmanRoute(activateFalco,activateSlippy,activatePeppy);

            //desactivo componente box collider para no activar el trigger otra vez.
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }


    void ActivateWingmanRoute(bool falco, bool slippy, bool peppy)
    {
        if (falco)
            ActivateRoute(wingmanFalcoInScene, offset_Falco);
        if (slippy)
            ActivateRoute(wingmanSlippyInScene, offset_Slippy);
        if (peppy)
            ActivateRoute(wingmanPeppyInScene, offset_Peppy);
        //Debug.Log("Activo a Peppy");
    }

    void ActivateRoute(GameObject wingman, Vector3 offset)
    {
        wingman.gameObject.SetActive(true);
        wingman.transform.SetParent(wingmanSmoothPath.gameObject.transform);

        //SETEO POSICIÓN DEL GameObj a localpos 0 0 0 
        wingman.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
        wingman.GetComponent<CinemachineDollyCart>().m_Path = wingmanSmoothPath;
        wingman.GetComponent<Wingman_Script>().offset = offset;
        wingman.GetComponent<CinemachineDollyCart>().m_Speed = speed;
        wingman.GetComponent<CinemachineDollyCart>().m_Position = 0;

        wingman.transform.GetComponent<Wingman_Script>().OverwriteWingmanPosition();
        
        //ACITVO LA SEÑAL DEL COMPAÑERO
       // wingman.transform.GetComponent<Wingman_Script>().ActivateUIWingmanIndicator(); //TODO: POR QUÉ COMENTADO?? borrar
    }
}

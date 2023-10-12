using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Wingman_Trigger_Script : MonoBehaviour
{

    public bool activateFalco = false;
    public bool activateSlippy = false;
    public bool activatePeppy = false;


    public GameObject wingmanFalco;
    public GameObject wingmanSlippy;
    public GameObject wingmanPeppy;
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

        wingmanFalco = GameObject.Find("Wingman_Falco").gameObject;
        wingmanSlippy = GameObject.Find("Wingman_Slippy").gameObject;
        wingmanPeppy = GameObject.Find("Wingman_Peppy").gameObject;

        Debug.LogWarning(wingmanFalco.name, wingmanFalco);
        Debug.LogWarning(wingmanSlippy.name, wingmanSlippy);
        Debug.LogWarning(wingmanPeppy.name, wingmanPeppy);

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
            ActivateRoute(wingmanFalco, offset_Falco);
        if (slippy)
            ActivateRoute(wingmanSlippy, offset_Slippy);
        if (peppy)
            ActivateRoute(wingmanPeppy, offset_Peppy);
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
        //wingman.transform.GetChild(0).GetComponent<Wingman_Script>().offset = offset;
        wingman.GetComponent<CinemachineDollyCart>().m_Speed = speed;
        wingman.GetComponent<CinemachineDollyCart>().m_Position = 0;

        wingman.transform.GetComponent<Wingman_Script>().OverwriteWingmanPosition();
        //wingman.transform.GetChild(0).GetComponent<Wingman_Script>().Overwrite


        //ACITVO LA SEÑAL DEL COMPAÑERO
       // wingman.transform.GetComponent<Wingman_Script>().ActivateUIWingmanIndicator();
    }
}

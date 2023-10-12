using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Wingman_Script : MonoBehaviour
{
    public Vector3 offset;
    public GameObject uIWingmanIndicator;

    private void Awake()
    {
        if (this.name.Contains("Falco"))
        {
            uIWingmanIndicator = GameObject.Find("UIWingmanIndicators").gameObject.transform.Find("Falco_Indicator").gameObject;
        }
        if (this.name.Contains("Slippy"))
        {
            uIWingmanIndicator = GameObject.Find("UIWingmanIndicators").gameObject.transform.Find("Slippy_Indicator").gameObject;

            if (this.name.Contains("Peppy"))
            {
                uIWingmanIndicator = GameObject.Find("UIWingmanIndicators").gameObject.transform.Find("Peppy_Indicator").gameObject;
            }
        }

       
    }
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    //void LateUpdate()
    //{
    //    if(gameObject.transform.parent.GetComponent<Cinemachine.CinemachineDollyCart>().enabled && gameObject.transform.parent.GetComponent<CinemachineDollyCart>().m_Path != null)
    //    {
    //        //OverwriteWingmanPosition();
    //        //Debug.Log("Voy a actualizar posicion de " + gameObject.name);

    //        //offset = this.gameObject.GetComponent<CinemachineDollyCart>().m_Path.GetComponent<CinemachineSmoothPath>().m_Waypoints[0].position - this.transform.position;

    //    }
    //}

    public void ActivateUIWingmanIndicator()
    {
        uIWingmanIndicator.SetActive(true);
    }
    public void DeactivateUIWingmanIndicator()
    {
        uIWingmanIndicator.SetActive(false);
    }

    public void OverwriteWingmanPosition()
      {

        //uIWingmanIndicator.SetActive(true);
        transform.GetChild(0).transform.localPosition += offset;


        
          
     }
    public void OnBecameVisible()
    {
        uIWingmanIndicator.SetActive(true);
        Debug.Log("ON BECAME VISIBLE");
    }
    public void OnBecameInvisible()
    {
        uIWingmanIndicator.SetActive(false);
        Debug.Log("ON BECAME INVISIBLE");
    }

    private void Update()
    {
        if(this.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path != null)
        {
            if (this.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position >= this.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path.PathLength)
            {
               
                this.gameObject.SetActive(false);
            }
        }
        
    }
}

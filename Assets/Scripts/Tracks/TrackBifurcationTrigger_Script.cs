using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TrackBifurcationTrigger_Script : MonoBehaviour
{
    //ESTE PREFAB SE AÑADE él SOLO AL FINAL DE CADA CINEMACHINEPATH marcado con IsBifurcating, AL INICIAR EL NIVEL. NO HACE FALTA TOCAR NADA

    public GameObject chooseWayArrows;
    public UIArrows_Script uIArrows_Script_;

    public CinemachinePathBase affectedTrack;


    private void Awake()
    {
        uIArrows_Script_ = GameObject.Find("UIArrows").GetComponent<UIArrows_Script>();
        chooseWayArrows = GameObject.Find("UIChooseWayArrows").gameObject;
        affectedTrack = this.gameObject.transform.parent.gameObject.GetComponent<CinemachineSmoothPath>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //DisableChooseWayArrows();
        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EnableChooseWayArrows();
            uIArrows_Script_.DisableLeftArrow();
            uIArrows_Script_.DisableRightArrow();
        }
    }

   
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            DisableChooseWayArrows();
        }
    }

    public void ChooseRightPath()
    {
        chooseWayArrows.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ChooseLeftPath()
    {
        chooseWayArrows.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void EnableChooseWayArrows()
    {
        chooseWayArrows.SetActive(true);
    }


    private void DisableChooseWayArrows()
    {
        chooseWayArrows.SetActive(false);
    }

}

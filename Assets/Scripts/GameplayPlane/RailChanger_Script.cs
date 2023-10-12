using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class RailChanger_Script : MonoBehaviour
{
   
    public int currentTrack = 0;
    public List<CinemachineSmoothPath> tracks = new List<CinemachineSmoothPath>();
    public List<CinemachineSmoothPath> extraTracks = new List<CinemachineSmoothPath>();

    public CinemachineDollyCart dolly;
    public PlayerMovement_Script playerMovement_Script_;
    public GameObject bifurcationTrigger_GO;

    
    //Primero hay que disparar al botón para activarlo (sirve como acción X que activa el cambio). El botón activa el trigger que está al inicio de la pista secundaria, y debe ramificarse DESDE la principal)
    //Cuando el player cruza el trigger activo, se cambia de pista instantáneamente, situándolo al inicio de la pista extra, que como ramifica de la primera, no debería notarse mucho el cambio).
    //Una de las dos clases (RailChanger o el script que lleve el trigger, deben pedir el cambio al gestor de Rutas TrackManager (todavía no lo he hecho).

    private void Awake()
    {
        //button_Script_ =;
        playerMovement_Script_ = GameObject.Find("Player").gameObject.GetComponent<PlayerMovement_Script>();

        foreach (CinemachineSmoothPath track in tracks)
        {
           Bifurcation_Script bifurcation_Script_ = track.gameObject.GetComponent<Bifurcation_Script>();

            if (bifurcation_Script_.isBifurcating == true)
            {
                Vector3 triggerPos = track.m_Waypoints[track.m_Waypoints.Length - 1].position;
                Instantiate(bifurcationTrigger_GO, triggerPos, bifurcationTrigger_GO.transform.rotation, track.gameObject.transform);
            }
        }

    }


    
    void Update()
    {
        if(dolly.m_Position >= dolly.m_Path.PathLength)
        {
            //TRACK NORMAL
            if((dolly.m_Path.gameObject.GetComponent<Bifurcation_Script>().isBifurcating == false) && (dolly.m_Path.gameObject.GetComponent<Bifurcation_Script>().isExtraTrack == false))
            {

                //get current track in the array and get the next one
                ChangeTrack(tracks[currentTrack + 1]);
                currentTrack++;
                
            }

            //CHOOSE WAY
            else if( dolly.m_Path.gameObject.GetComponent<Bifurcation_Script>().isBifurcating == true)
            {
                ChooseWay();
            }

            //CONTINUE IN TRACK SPECIAL
            if((dolly.m_Path.gameObject.GetComponent<Bifurcation_Script>().isBifurcating == false) && (dolly.m_Path.gameObject.GetComponent<Bifurcation_Script>().isExtraTrack == true))
            {
                ChangeTrack(extraTracks[currentTrack + 1]);
                currentTrack++;
            }       
            
        }
    }
    //LOS WAYPOINTS DEBEN ESTAR ALINEADOS EN LA X Y LA Y. (DA IGUAL A LO LEJOS: Z)


    //NEXT TRACK NORMAL
    public void ChangeTrack(CinemachineSmoothPath nextTrack)
    {
        nextTrack.gameObject.GetComponent<CinemachineSmoothPath>().m_Waypoints[0].position.z = this.transform.position.z;
        dolly.m_Path = nextTrack;
        dolly.m_Position = 0;
        //currentTrack++; ya he cambiado antes de llamar a la función

        Debug.Log("He cambiado de pista");
    }


    //CHANGE FROM NORMAL TO EXTRA
    public void ChangeExtraTrack()
    {
        extraTracks[0].GetComponent<CinemachineSmoothPath>().m_Waypoints[0].position.z = this.transform.position.z;
        dolly.m_Path = extraTracks[0];
        dolly.m_Position = 0;
        currentTrack = 0;

    }


    public void ChooseWay()
    {
        //He probado con WorldToViewport (va de 0 a 1), mirar en el futuro en caso de necesitarlo
       
        //CHOOSE RIGHT PATH
        if (playerMovement_Script_.transform.localPosition.x >= 0)  
        {
            dolly.m_Path.gameObject.transform.GetChild(0).gameObject.GetComponent<TrackBifurcationTrigger_Script>().ChooseRightPath();
            ChangeTrack(tracks[currentTrack + 1]);
            currentTrack += 2;
        }
        else //CHOOSE LEFT PATH
        {
            dolly.m_Path.gameObject.transform.GetChild(0).gameObject.GetComponent<TrackBifurcationTrigger_Script>().ChooseLeftPath();
            ChangeTrack(tracks[currentTrack + 2]);
            currentTrack += 2;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardTrigger_Script : MonoBehaviour
{

   
    private void OnCollisionEnter(Collision collision)
    {
        Billboard_Script billboard_Script_ = GetComponentInParent<Billboard_Script>();
        if ((billboard_Script_.IsBillboardActivated == false) && ((collision.gameObject.tag == "LaserBeam") || (collision.gameObject.tag == "ChargedLaserSphere") || (collision.gameObject.tag == "Bomb")))
        {
            billboard_Script_.ActivateBillBoard();
            
        }
    }
}





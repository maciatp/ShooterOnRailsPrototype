using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUpdatePosition_Script : MonoBehaviour
{

    public Vector3 offset = Vector3.zero;

   

    // Update is called once per frame
    void Update()
    {
        //if((this.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().enabled != false) && (this.gameObject.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path != null))
        //{
        //    OverWriteEnemyPosition();
        //}
    }


   public void OverWriteEnemyPosition()
    {
       transform.GetChild(0).transform.localPosition +=  offset;
    }
}

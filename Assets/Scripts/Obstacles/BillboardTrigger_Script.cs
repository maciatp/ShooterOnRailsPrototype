using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardTrigger_Script : MonoBehaviour
{
    public BoxCollider triggerCollider;

    public Billboard_Script billboard_Script_;

    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = this.gameObject.GetComponent<BoxCollider>();
        billboard_Script_ = this.transform.parent.gameObject.GetComponent<Billboard_Script>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


   
    private void OnCollisionEnter(Collision collision)
    {
        if ((billboard_Script_.isBillBoardActivated == false) && ((collision.gameObject.tag == "LaserBeam") || (collision.gameObject.tag == "ChargedLaserSphere") || (collision.gameObject.tag == "Bomb")))
        {
            billboard_Script_.ActivateBillBoard();
            //triggerCollider.enabled = false; para que puedas seguir lockándolo cuando ya lo has activado (comentar sólo para testing, descomentar en build)
        }
    }
}





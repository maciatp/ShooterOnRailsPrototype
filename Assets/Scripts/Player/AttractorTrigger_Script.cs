using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorTrigger_Script : MonoBehaviour
{

    public BoxCollider attractorCollider;
    public ChargingLaserAbsorb_Script chargingLaserAbsorb_Script_;


    private void Awake()
    {
        attractorCollider = this.gameObject.GetComponent<BoxCollider>();
        chargingLaserAbsorb_Script_ = this.transform.GetComponentInParent<ChargingLaserAbsorb_Script>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        
        if ((other.tag == "PowerUp") && (other.name != "SpecialBox"))
        {
           chargingLaserAbsorb_Script_.AddPowerUpToList(other.gameObject);
          

           

          
        }
    }
}

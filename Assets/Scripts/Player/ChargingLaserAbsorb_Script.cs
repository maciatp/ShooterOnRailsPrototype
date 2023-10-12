using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingLaserAbsorb_Script : MonoBehaviour
{

    public float attractorForce = 3;
    public float conteoToBeginAttraction = 0;
    public float beginAttractionTimeSpan = 0.3f;

    public List<Rigidbody> rB_ObsAbsorbing = new List<Rigidbody>();


    
    public BoxCollider triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        triggerCollider = gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider>();
        conteoToBeginAttraction = 0;
    }

    // Update is called once per frame
    void Update()
    {

        conteoToBeginAttraction += Time.deltaTime;
        
        foreach(Rigidbody rigidbody in rB_ObsAbsorbing)
        {
            if((rigidbody != null) && (conteoToBeginAttraction > beginAttractionTimeSpan))
            {
                rigidbody.transform.position -= (new Vector3(rigidbody.transform.position.x - this.transform.position.x, rigidbody.transform.position.y - this.transform.position.y, rigidbody.transform.position.z - this.transform.position.z) *attractorForce* Time.deltaTime);
            }
            
           
        }

    }

     public void AddPowerUpToList(GameObject gameObjectToAdd)
    {
        
        rB_ObsAbsorbing.Add(gameObjectToAdd.GetComponent<Rigidbody>());
    }
}

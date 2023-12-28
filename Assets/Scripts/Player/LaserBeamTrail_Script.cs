using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamTrail_Script : MonoBehaviour
{
    float conteoInstantiate;
    [Header("Parameters")]
    [SerializeField] float distance = 3f;
    [SerializeField] float instantiateTimeSpan = 0.35f;
    
    [Space]
    [Header("External Prefab References")]
    [SerializeField] GameObject groundTrail;
    [SerializeField] GameObject waterTrail;

    
    void FixedUpdate()
    {
        conteoInstantiate += Time.unscaledDeltaTime;

        if(conteoInstantiate >= instantiateTimeSpan)
        {
            Vector3 dwn = transform.TransformDirection(Vector3.down);
            RaycastHit hit;



            if (Physics.Raycast(transform.position, dwn, out hit, distance))
            {
                //print("There is something in front of the object!");
                if (hit.transform.tag == "Scenario")
                {
                    Instantiate(groundTrail, hit.point, new Quaternion(0, 0, 0, 0), null);
                }
                if (hit.transform.tag == "Water")
                {
                    Instantiate(waterTrail, hit.point, new Quaternion(0, 0, 0, 0), null);
                }
            }

            conteoInstantiate = 0;
        }


        
    }
}

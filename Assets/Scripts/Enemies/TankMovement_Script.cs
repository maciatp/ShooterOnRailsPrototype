using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement_Script : MonoBehaviour
{
    public Rigidbody rb_Tank;
    public float speed = 10;

    public Transform initialPoint;
    public Transform goalPoint;

    public bool isGoingForward = true;
    // Start is called before the first frame update
    void Start()
    {
        rb_Tank = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // if( this.transform.position != goalPoint.transform.position)
        {
            if(isGoingForward == true)
            {
                rb_Tank.velocity =  this.transform.forward * speed;
            }
            if(isGoingForward == false)
            {
                rb_Tank.velocity = this.transform.forward * -speed;
            }
            
           
        }




    }
}

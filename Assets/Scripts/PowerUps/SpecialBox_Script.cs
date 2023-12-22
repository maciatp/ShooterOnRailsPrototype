using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBox_Script : MonoBehaviour
{
    public float rotationSpeed = 50;

    private Rigidbody rb_Box;

    //public GameObject randomItemGenerator; TODO

    public GameObject laserUpgrade_GO;


    private void Awake()
    {
        rb_Box = this.gameObject.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.localEulerAngles += new Vector3(0, rotationSpeed, 0)*Time.deltaTime;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if((collision.gameObject.tag == "LaserBeam") || (collision.gameObject.tag == "ChargedLaserSphere") || (collision.gameObject.tag == "Bomb")) 
        {
            Instantiate(laserUpgrade_GO, this.gameObject.transform.position, this.gameObject.transform.rotation, null);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Explosion")
        {
            Instantiate(laserUpgrade_GO, this.gameObject.transform.position, this.gameObject.transform.rotation, null);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucerRotation_Script : MonoBehaviour
{

    //public Rigidbody saucerRb;

    public float saucerRotationSpeed = 10;

    private void Awake()
    {
        //saucerRb = this.transform.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.eulerAngles +=  new Vector3(0, Vector3.up.y * saucerRotationSpeed * Time.deltaTime , 0);
        this.transform.Rotate(Vector3.up, saucerRotationSpeed * Time.deltaTime);
    }
}

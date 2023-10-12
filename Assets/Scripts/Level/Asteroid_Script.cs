using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Script : MonoBehaviour
{
    public Rigidbody asteroid_RB;
    public CapsuleCollider asteroid_Collider;
    public float speed = 5;
    public float rotationSpeed = 0.5f;

    private void Awake()
    {
        asteroid_RB = this.gameObject.GetComponent<Rigidbody>();
        asteroid_Collider = this.gameObject.GetComponent<CapsuleCollider>();

        asteroid_RB.velocity = Vector3.forward * speed;

    }

  

    // Update is called once per frame
    void Update()
    {
        //asteroid_RB.rotation = Quaternion.FromToRotation(asteroid_RB.transform.position, new Vector3(asteroid_RB.transform.position.x, asteroid_RB.transform.position.y, asteroid_RB.transform.position.z -5)); 
        this.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            
    }
}

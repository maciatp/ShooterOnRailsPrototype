using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedLaserExplosion_Script : MonoBehaviour
{
    private BoxCollider chargedLaserCollider;
    private ParticleSystem chargedLaserExplosionParticles;


   
    public float maxSize;
    public float colliderIncreasingRate = 1.04f;

    private void Awake()
    {
        chargedLaserCollider = this.gameObject.GetComponent<BoxCollider>();
        chargedLaserExplosionParticles = this.gameObject.GetComponent<ParticleSystem>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (chargedLaserCollider.size.x <= maxSize)
        {
            chargedLaserCollider.size = new Vector3((chargedLaserCollider.size.x) * colliderIncreasingRate, (chargedLaserCollider.size.y) * colliderIncreasingRate, (chargedLaserCollider.size.z) * colliderIncreasingRate);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Billboard")
        {
           other.gameObject.GetComponent<Billboard_Script>().ActivateBillBoard();
        }
        if (other.tag == "Button")
        {
            other.gameObject.GetComponent<Button_Script>().ActivateButton();
        }
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.name == "DoorHouse")
        {
            other.gameObject.GetComponent<DoorHouse_Script>().ReceiveDamage();
        }
        if (other.tag == "Button")
        {
            other.gameObject.GetComponent<Button_Script>().ActivateButton();
            Debug.Log("activo botón");
        }
    }
}

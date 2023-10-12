using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestruction_Script : MonoBehaviour


{
    public GameObject brokenObject;

    public float objActualHealth = 5;

    public MeshRenderer objRenderer;

    private void Awake()
    {
        objRenderer = this.GetComponent<MeshRenderer>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "LaserBeam")
        {
            DepleteHealth(collision.gameObject.GetComponent<Beam_Script>().damagePoints);

        }
        if ((collision.gameObject.tag == "ChargedLaserSphere")  || (collision.gameObject.tag == "Bomb") )
        {
            DestroyAndInstantiate();
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if((other.gameObject.tag == "SmartBombExplosion") || (other.gameObject.tag == "Explosion"))
        {
            DestroyAndInstantiate();
        }
    }

    void DepleteHealth(float healthPointsToDeplete)
    {
        objActualHealth -= healthPointsToDeplete;
        objRenderer.material.color += Color.white * 0 + Color.red * 2f;
        if (objActualHealth <= 0)
        {
            DestroyAndInstantiate();
        }
    }

    private void DestroyAndInstantiate()
    {
        //Instantiate(brokenObject, this.transform.position, this.transform.localRotation, null);
        Instantiate(brokenObject, this.transform.position, new Quaternion(0, 0, 0, 0), null);
        Destroy(this.gameObject);
    }
}

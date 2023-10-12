using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Script : MonoBehaviour
{

    private Rigidbody rb;

    public float beam_Speed;
    public bool isShot = false;

    public float damagePoints = 1f;
    public float conteoToBeDestroyed = 0;
    public float laserLifeTime = 4;

    public GameObject laserImpact;
    public GameObject laserAdvancedImpact;
    public GameObject damageImpact; // todo lo que sea interactivo (Puertas, buttons, wall, enemigos,etc.)
    public GameObject waterSplash;

     void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }



    // Start is called before the first frame update
    void Start()
    {
       
        isShot = true;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.localScale += new Vector3(0,0,(-Time.deltaTime*16.5f)) ; AVERIGUAR SI STARFOX REDUCE TAMAÑO DE DISPAROS (HACER CON TRAILS??)
        if(isShot==true)
        {
            conteoToBeDestroyed += Time.unscaledDeltaTime;
            rb.position += transform.forward.normalized * beam_Speed * Time.unscaledDeltaTime;
        }

        if(conteoToBeDestroyed > laserLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.layer == 4 ) //Colisión con agua
        {
            Instantiate(waterSplash, this.transform.position, new Quaternion(0, 0, 0, 0), null);

        }
       if((collision.gameObject.layer == 14) || (collision.gameObject.tag == "Scenario")) // colisión con scenario
        {
            if((this.gameObject.name == "LaserBeam") || (this.gameObject.name == "TwinLasers"))
            {
                Instantiate(laserImpact, this.transform.position, new Quaternion(0,0,0,0), null);
                //Debug.Log("greenImpact");
            }
            else if(this.name  == "HyperLasers")
            {
                Instantiate(laserAdvancedImpact, this.transform.position, new Quaternion(0, 0, 0, 0), null);
                //Debug.Log("blueImpact");
            }


        }
        if ((collision.gameObject.tag == "Enemy") || (collision.gameObject.tag == "Obstacle")) // colisión con enemigo (ya se resta salud en el enemigo, aquí sólo partículas)
        {
          
                Instantiate(damageImpact, this.transform.position, new Quaternion(0, 0, 0, 0), null);
            

        }
        /*
        if(this.gameObject.name == "LaserBeam")
        {
            transform.GetChild(0).transform.SetParent(null);
            Destroy(this.gameObject.transform.GetChild(1));
        }
        else if ((this.gameObject.name == "TwinLasers") || (this.gameObject.name == "HyperLasers"))
        {
            this.transform.GetChild(0).transform.GetChild(0).transform.SetParent(null);
            this.transform.GetChild(1).transform.GetChild(0).transform.SetParent(null);
            Destroy(this.gameObject.transform.GetChild(0).transform.GetChild(1));
            Destroy(this.gameObject.transform.GetChild(1).transform.GetChild(1));
        }*/

        
        Destroy(this.gameObject);
        
    }
}

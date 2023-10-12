using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeam_Script : MonoBehaviour
{

    private Rigidbody rb_EnemyBeam;
    //private PlayerHealth_Script playerHealth_Script_;

    public float enemyBeam_Speed = 40;
    public bool isEnemyBeamShot = false;

    public float damagePointsToPlayer = 1f;
    public float conteoToBeDestroyedEnemyBeam = 0;
    public float enemyLaserLifeTime = 4;


    public GameObject laserImpact;
    
    public GameObject waterSplash;

    void Awake()
    {
        rb_EnemyBeam = GetComponent<Rigidbody>();
        //playerHealth_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth_Script>();
    }



    // Start is called before the first frame update
    void Start()
    {
        rb_EnemyBeam.velocity = transform.forward * enemyBeam_Speed;
        isEnemyBeamShot = true;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.localScale += new Vector3(0,0,(-Time.deltaTime*16.5f)) ; AVERIGUAR SI STARFOX REDUCE TAMAÑO DE DISPAROS (HACER CON TRAILS??)
        if (isEnemyBeamShot == true)
        {
            conteoToBeDestroyedEnemyBeam += Time.deltaTime;
        }

        if (conteoToBeDestroyedEnemyBeam > enemyLaserLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
       //la colisión con el player la controlo en PlayerHealth_Script 
     
        if(collision.gameObject.layer == 14)  //colision con scenario
        {
            
            Instantiate(laserImpact, this.transform.position, new Quaternion(0, 0, 0, 0), null);
            Destroy(this.gameObject);

        }
        else if( collision.gameObject.layer == 4) //colision con agua
        {
            Instantiate(waterSplash, this.transform.position, new Quaternion(0, 0, 0, 0), null);
            Destroy(this.gameObject);
        }

        
    }
    
}

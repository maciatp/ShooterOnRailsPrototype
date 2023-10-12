using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting_Script : MonoBehaviour
{

    public GameObject enemyBeam_GO;
    public GameObject playerInScene;
    public Rigidbody rb_EnemyBeam;
    public Transform enemyLaserSpawn;
    

    public EnemyHealth_Script enemyHealth_Script_;


    public bool isShooting = false;

    public float conteoShooting = 0;
    public float conteoFireTimeSpan = 2;

    private void Awake()
    {
        playerInScene = GameObject.FindGameObjectWithTag("Player");
        enemyHealth_Script_ = this.gameObject.GetComponent<EnemyHealth_Script>();
        rb_EnemyBeam = this.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting != false)
        {
            if(this.transform.position.z - playerInScene.transform.position.z > 0)
            {
                this.transform.LookAt(playerInScene.transform.position); // para que apunte directamente al player
                                                                         //this.transform.LookAt(playerInScene.transform.position + new Vector3(0,0,5)); //para que apunte un poco más adelante que el player (para cuando avance)
                                                                         //this.transform.rotation = Quaternion.FromToRotation( this.transform., playerInScene.gameObject.transform.position);



                conteoShooting += Time.deltaTime;

                if (conteoShooting > conteoFireTimeSpan)
                {
                    GameObject _enemyBeam = Instantiate(enemyBeam_GO, enemyLaserSpawn.position, this.gameObject.transform.rotation) as GameObject;
                    conteoShooting = 0;
                }

            }
            else
            {
                isShooting = false;
            }



        }
    }
}

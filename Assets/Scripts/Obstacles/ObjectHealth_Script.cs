using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth_Script : MonoBehaviour
{
    [Header("Parameters")]
    public float healthPoints = 1;
    public int numOfHitsWillAdd = 1;
    public BoxCollider enemyBoxCollider;


    [Header("Public References")]
    public Beam_Script beam_Script_;
    public ChargedLaserSphere_Script chargedLaserSphere_Script_;
    public ScoreManager_Script scoreManager_Script_;


    [Header("Instances")]
    public GameObject ring;
    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {

        if ((collision.gameObject.tag == "LaserBeam") || (collision.gameObject.tag == "ChargedLaserSphere") || (collision.gameObject.tag == "Explosion") || (collision.gameObject.tag == "SmartBombExplosion"))
        {

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

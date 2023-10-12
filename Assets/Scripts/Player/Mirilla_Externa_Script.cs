using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirilla_Externa_Script : MonoBehaviour
{
    //public Rigidbody rB_Mirilla_Externa;

    public bool isPositioned = false;
    public Animator mirillaExterna_Animator;
    public GameObject objectLocked;

    [Header("Public References")]
    public GuidedLaserTrigger_Script guidedLaserTrigger_Script_;
    [Header("Audio")]
    public AudioSource guidedLaserAudioSource;

    private void Awake()
    {
        guidedLaserAudioSource = this.GetComponent<AudioSource>();
        
        //rB_Mirilla_Externa = this.GetComponent<Rigidbody>();
        mirillaExterna_Animator = this.GetComponent<Animator>();
        guidedLaserTrigger_Script_ = GameObject.FindGameObjectWithTag("GuidedChargedTrigger").GetComponent<GuidedLaserTrigger_Script>();
        objectLocked = guidedLaserTrigger_Script_.objectLocked;
        this.transform.position = objectLocked.transform.position;
        if (objectLocked != null)
        {
            Positionate();

            // this.transform.LookAt(Camera.main.transform);
            //Debug.Log(transform.eulerAngles);
            this.transform.eulerAngles = new Vector3(0, Vector3.Angle(Vector3.forward, Camera.main.transform.forward), -180);
            //Debug.Log(Vector3.Angle(Vector3.forward, Camera.main.transform.forward));
            //Debug.Log(this.transform.eulerAngles);


            // this.transform.rotation.eulerAngles = this.transform.LookAt(Camera.main.transform);

            //this.transform.eulerAngles = new Vector3(0, //posición del jugador , 0);
        }

    }


  

    // Update is called once per frame
    void Update()
    {
        if (objectLocked != null)
        {
           this.transform.position = objectLocked.transform.position; //para que siga a objectLocked
            
            if(isPositioned == false)
            {
                Positionate();
            }
            
            
        }
        if( objectLocked == null)
        {
            DestroyMirilla_Externa();
        }

    }

    public void DestroyMirilla_Externa()
    {
       
            Destroy(this.gameObject);
       
        
    }
    public void Positionate()
    {
        isPositioned = true;
       // this.transform.rotation = new Quaternion(0, 0, 0, 0);
        /*if(objectLocked != null)
        {
            this.transform.position = objectLocked.transform.position;
        }*/
    }

}

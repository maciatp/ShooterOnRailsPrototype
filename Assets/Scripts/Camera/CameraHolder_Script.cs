using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder_Script : MonoBehaviour
{
    

    public GameObject cameraHolder;

    public Animator cameraHolder_Animator;
    public PlayerMovement_Script playerMovement_Script_;


    void Awake()
    {
        cameraHolder_Animator = this.gameObject.GetComponent<Animator>();

        //NEW INPUT SYSTEM

        playerMovement_Script_ = GameObject.Find("Player").GetComponent<PlayerMovement_Script>();


     }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        
            cameraHolder_Animator.SetFloat("cameraFloat",playerMovement_Script_.moveDirection.x);
        
        cameraHolder_Animator.SetFloat("cameraVerticalFloat",playerMovement_Script_.moveDirection.y);


        /*
                if (Input.GetAxis ("Horizontal") > 0)
            {
            // this.transform.Rotate(0, 10, 0);
            }
                if (Input.GetAxis("Horizontal") < 0.1f)
                 {
                   this.transform.Rotate(0, 0, 0);
                 }

                if (Input.GetAxis("Horizontal") < 0)
                {
                        this.transform.Rotate(0, -10, 0);
                }
                if ((Input.GetAxis("Horizontal") > -0.1f) && (Input.GetAxis("Horizontal") < 0))
                {
                        this.transform.Rotate(0, 0, 0);

                }

                }*/
    }
}
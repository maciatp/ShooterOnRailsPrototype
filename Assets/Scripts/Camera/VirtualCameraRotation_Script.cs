using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraRotation_Script : MonoBehaviour
{

    public PlayerControls controls;

    public Animator vCamera_Animator;

    public PlayerMovement_Script playerMovement_Script_;

    private void Awake()
    {
        vCamera_Animator = this.GetComponent<Animator>();

        controls = GameObject.Find("Player").GetComponent<PlayerShooting_Script>().controls;

        playerMovement_Script_ = GameObject.Find("Player").GetComponent<PlayerMovement_Script>();

    }

  

    // Update is called once per frame
    void Update()

        //ROTATE CAMERA WHEN NOT TILTING
    {
        if (playerMovement_Script_.IsTilting == false)
        {
            vCamera_Animator.SetFloat("cameraFloat_Dutch", playerMovement_Script_.moveDirection.x);
            vCamera_Animator.SetFloat("cameraVerticalFloat", playerMovement_Script_.moveDirection.y);
        }
        else
        {
            //RETURN CAMERA TO IDLE WHEN TILTING
            vCamera_Animator.SetFloat("cameraFloat_Dutch", 0);
        }
        
       

    }

  
}

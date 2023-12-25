using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder_Script : MonoBehaviour
{
    Animator cameraHolder_Animator;
    [SerializeField] PlayerMovement_Script playerMovement_Script_;

    private void Start()
    {
        cameraHolder_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {      
        cameraHolder_Animator.SetFloat("cameraFloat",playerMovement_Script_.moveDirection.x);
        
        cameraHolder_Animator.SetFloat("cameraVerticalFloat",playerMovement_Script_.moveDirection.y);

    }
}
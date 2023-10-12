using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ColumnPrefab_Script : MonoBehaviour
{
   
    public BoxCollider columnCollider;
    public Animator column_Animator;
    public Rigidbody columnRigidBody;
    public CinemachineImpulseSource cinemachineImpulse_;
   

    private void Awake()
    {
        cinemachineImpulse_ = this.GetComponent<CinemachineImpulseSource>();
        columnCollider = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<BoxCollider>();
        column_Animator = this.gameObject.GetComponent<Animator>();
        columnRigidBody = this.transform.GetChild(0).GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimationColumn()
    {
        column_Animator.SetBool("isColumnActivatedToFall", true);
    }

    private void OnTriggerEnter(Collider other)
    {

      
        if(other.tag == "Player")
        {
            Debug.Log("He entrado en la columna");
        }
    }


    public void ShakeCamera()
    {
        cinemachineImpulse_.GenerateImpulse();
    }

}

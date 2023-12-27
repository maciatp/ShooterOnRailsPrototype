using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirillas_Script : MonoBehaviour
{
    [Header("Public References")]
    [SerializeField] SpriteRenderer mirillaCerca;
    [SerializeField] SpriteRenderer mirillaLejos;
    [SerializeField] PlayerShooting_Script playerShooting_Script_;
    
    
    [Space]
    [Header("Parameters")]
    
    [Space]
    [Header("Animation")]
    Animator mirilla_Animator;

    public Animator MirillasAnimator
    {
        get { return mirilla_Animator; }
        set { mirilla_Animator = value; }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.name == "Mirilla_Cerca")
        {
            mirillaCerca = GetComponent<SpriteRenderer>();
            mirillaLejos = null;
        }
       if(this.gameObject.name == "Mirilla_Lejos")
        {
            mirillaLejos = GetComponent<SpriteRenderer>();
            mirilla_Animator = GetComponent<Animator>();
            mirillaCerca = null;
        }

        
    }

    // Update is called once per frame
    void Update()
    {

        
        if((this.gameObject.name == "Mirilla_Cerca") && (playerShooting_Script_.IsChargedLaserInstanced == true))
        {            
            if (mirillaCerca.material.color != Color.yellow)
            {
                MakeNEARYellowWhenCharged();
                
            }
            
        }
        if ((this.gameObject.name == "Mirilla_Cerca") && (playerShooting_Script_.IsLaserCharging == false) && (playerShooting_Script_.IsLaserCharged == false))
        {
            ReturnToWhiteNear();            
        }
        if ((this.gameObject.name == "Mirilla_Lejos") && (playerShooting_Script_.IsChargedLaserInstanced == true))
        {
            MakeREDOnceCharged();
        }

        if ((this.gameObject.name == "Mirilla_Lejos") && (playerShooting_Script_.IsLaserCharged == false))
        {
            ReturnToDefaultFar();

        }


    }

    public void MakeNEARYellowWhenCharged()
    {
        mirillaCerca.material.color = Color.yellow;
    }
    public void MakeREDOnceCharged()
    {
        mirillaLejos.material.color = Color.red;
        mirilla_Animator.SetBool("mirillaLejosIsBumping", true);
    }
    public void ReturnToWhiteNear()
    {
        mirillaCerca.material.color = Color.white;

       
    }
    public void ReturnToDefaultFar()
    {
        mirillaLejos.enabled = true;
        mirillaLejos.transform.localRotation = new Quaternion(0, 0, 0, 0);

        mirillaLejos.material.color = Color.white;
        mirilla_Animator.SetBool("mirillaLejosIsBumping", false);
        //mirilla_Animator.SetBool("objectLocked", false);
    }

    public void DeactivateFarWhileObjectLocked()
    {
        mirillaLejos.enabled = false;
    }

}

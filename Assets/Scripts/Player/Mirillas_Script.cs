using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirillas_Script : MonoBehaviour
{
    [Header("Public References")]
    public SpriteRenderer mirillaCerca;
    public SpriteRenderer mirillaLejos;
    public PlayerShooting_Script playerShooting_Script_;
    //public PlayerMovement_Script playerMovement_Script_; NO LO USO
    
    [Space]
    [Header("Parameters")]
    public float conteoToPaintMirilla = 0;
    public float PaintMirillaTimeSpan = 0.2f;
    [Space]
    [Header("Animation")]
    public Animator mirilla_Animator;


    private void Awake()
    {
        playerShooting_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting_Script>();
        //playerMovement_Script_ = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement_Script>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.name == "Mirilla_Cerca")
        {
            mirillaCerca = this.gameObject.GetComponent<SpriteRenderer>();
            mirillaLejos = null;
        }
       if(this.gameObject.name == "Mirilla_Lejos")
        {
            mirillaLejos = this.gameObject.GetComponent<SpriteRenderer>();
            mirilla_Animator = this.GetComponent<Animator>();
            mirillaCerca = null;
        }

        
    }

    // Update is called once per frame
    void Update()
    {

        
        if((this.gameObject.name == "Mirilla_Cerca") && (playerShooting_Script_.isLaserCharging == true))
        {
            conteoToPaintMirilla += Time.deltaTime;

            if ((conteoToPaintMirilla > PaintMirillaTimeSpan) && (mirillaCerca.material.color != Color.yellow))
            {
                MakeNEARYellowWhileCharging();
                conteoToPaintMirilla = 0;
            }
            
        }
        if ((this.gameObject.name == "Mirilla_Cerca") && (playerShooting_Script_.isLaserCharging == false) && (playerShooting_Script_.isLaserCharged == false))
        {
            ReturnToWhiteNear();
            conteoToPaintMirilla = 0;
        }
        if ((this.gameObject.name == "Mirilla_Lejos") && (playerShooting_Script_.isLaserCharged == true))
        {
            MakeREDOnceCharged();
            

        }
        /*
        if ((this.gameObject.name == "Mirilla_Lejos") && (playerShooting_Script_. == false) )
        {
            ReturnToDefaultFar();
            
        }
        */

    }

    public void MakeNEARYellowWhileCharging()
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

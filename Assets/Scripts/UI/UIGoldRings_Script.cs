using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGoldRings_Script : MonoBehaviour
{
    public Animator goldring1;
    public Animator goldring2;
    public Animator goldring3;

    

    


    private void Awake()
    {
        goldring1 = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        goldring2 = this.gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>();
        goldring3 = this.gameObject.transform.GetChild(2).gameObject.GetComponent<Animator>();

    }
   

    public void CheckGoldRings(int actualGoldRings)
    {
        goldring1.SetInteger("ActualGoldRings", actualGoldRings);
        goldring2.SetInteger("ActualGoldRings", actualGoldRings);
        

        if(actualGoldRings == 1)
        {
            goldring1.SetTrigger("ActivateRing");
            goldring2.Play("UIGoldRing_Inactive_anim");
            goldring3.Play("UIGoldRing_Inactive_anim");
        }
        else if(actualGoldRings == 2)
        {
            goldring1.Play("GoldRing_UI_IdleActive");
            goldring2.Play("GoldRing_UI_IdleActive");
            //goldring2.SetTrigger("ActivateRing");
        }
        else if(actualGoldRings == 3)
        {
            goldring1.Play("GoldRing_UI_IdleActive");
            goldring2.Play("GoldRing_UI_IdleActive");
            goldring3.SetTrigger("ActivateRing");

            


        }
    }

   
}

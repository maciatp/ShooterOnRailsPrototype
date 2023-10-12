using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingChat_Behaviour : StateMachineBehaviour
{
    public UIChat_Script uIChat_Script_;
    public AudioClip uIChatImageClosingSound;

    private void Awake()
    {
        uIChat_Script_ = GameObject.Find("UIChat").gameObject.GetComponent<UIChat_Script>();
    }


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        uIChat_Script_.chatImageAnimator.gameObject.GetComponent<AudioSource>().clip = uIChatImageClosingSound;
        uIChat_Script_.chatImageAnimator.gameObject.GetComponent<AudioSource>().Play();
        //uIChat_Script_.audioSource.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

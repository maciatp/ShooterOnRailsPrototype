using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatImageBehaviour : StateMachineBehaviour
{
    public UIChat_Script uIChat_Script_;
    public AudioClip uIChatImageOpeningSound;
    public DialogueData dialogue;
    public int conversationBlockint;

    private void Awake()
    {
        uIChat_Script_ = GameObject.Find("UIChat").gameObject.GetComponent<UIChat_Script>();
        
    }

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        uIChat_Script_.chatImageAnimator.gameObject.GetComponent<AudioSource>().clip = uIChatImageOpeningSound;
        uIChat_Script_.chatImageAnimator.gameObject.GetComponent<AudioSource>().Play();
        //uIChat_Script_.audioSource.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        uIChat_Script_.audioSource.Play();
        uIChat_Script_.ReadDialogue(dialogue.conversationBlock[conversationBlockint]);
       // uIChat_Script_.gameObject.GetComponent<Animator>().Play("UIChatBox_Opening"); se puede borrar porq comentado aún funciona todo
    }

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

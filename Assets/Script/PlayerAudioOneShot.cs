using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioOneShot : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1f;
    public float delayTime = 1f;
    public float elapseDelayTime = 0;
    public float timeSinceEnter = 0;



    public bool playOnEnter = true, playOnExit = false, playOnDelay = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
        {
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position);
        }
        timeSinceEnter = 0f;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnDelay)
        {
            timeSinceEnter += Time.deltaTime;
            if (timeSinceEnter > delayTime)
            {
                timeSinceEnter = 0;
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
        {
            if (playOnEnter)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position);
            }
        }
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

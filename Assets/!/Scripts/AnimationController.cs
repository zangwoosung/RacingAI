using UnityEngine;

public class AnimationController : MonoBehaviour
{
    AnimationController anim;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(IAgentAIState state)
    {
        switch (state)
        {
            case IdleState idleState:
                Debug.Log("Playing Idle Animation");

               
                animator.Play("Idle_Guard_AR"); // Use the exact name of the state

                break;
            case AlertState:
                
                Debug.Log("Playing Patrol Animation");
                animator.Play("Idle_gunMiddle_AR"); //
                break;
            case AttackState attackState:
                 Debug.Log("Playing Attack Animation");
                animator.Play("Shoot_Autoshot_AR");
                break;
            case DeadState deadState:
                animator.Play("Die");
                break;
            default:
                Debug.Log("Unknown animation state");
                break;
        }

    }

}

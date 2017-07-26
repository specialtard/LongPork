using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper
{
    public static IEnumerator DelayAnimation(Transform transform, int delay,string animation)
    {
        Animation animator = transform.GetComponent<Animation>();

        yield return new WaitForSeconds(delay);

        animator.enabled = true;
    }

    public static bool AnimationState(Transform transform, string animation)
    {
        var animator = transform.GetComponent<Animator>();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation))
        {
            return true;
        }
        return false;
    }

}

using UnityEngine;
using System.Collections;
using System;

namespace Arena
{
    public class AnimationController : Controller<AnimationMessage>
    {
        public override void OnNotification(AnimationMessage message, GameObject obj, params object[] opData)
        {
            switch (message)
            {
                case AnimationMessage.WALK:
                    HandleWalk(obj, (float)opData[0]);
                    break;
            }
        }

        void HandleWalk(GameObject obj, float movement)
        {
            Animator anim = obj.GetComponent<Animator>();
            if (anim)
            {
                anim.SetFloat("motion", movement);
            } else
            {
                Debug.LogError(obj.name + " is trying to play a walk animation but doesn't have an animator");
            }
        }
    }
}
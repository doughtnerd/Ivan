using UnityEngine;
using System.Collections;
using System;

namespace Arena
{
    public class CyclopsBehavior : EnemyBehavior
    {
        
        protected override void Move()
        {
            app.NotifyAction(ActionMessage.MOVE, gameObject, direction);
            app.NotifyAnimation(AnimationMessage.WALK, gameObject, direction.magnitude);
        }

        public override void OnDamage()
        {
        }

        public override void OnDeath()
        {
            this.SetBehaviorEnabled(false);
            Destroy(gameObject);
        }

        protected override void Attack()
        {
            app.NotifyAction(ActionMessage.SLASH, gameObject, direction);
        }

        protected override void Update()
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, detectionLayer);
            if (player)
            {
                GameObject g = player.gameObject;
                target = g.transform.position;
                direction = (target - transform.position).normalized;
            }
            else
            {
                target = Vector3.zero;
            }
            if (target != Vector3.zero)
            {
                if(Vector3.Distance(target, transform.position) <= 2)
                {
                    Attack();
                } else
                {
                    Move();
                }
                LookInDirection(target);
            }
        }
    }
}

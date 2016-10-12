using UnityEngine;
using System.Collections;
using System;

namespace Arena
{
    public class ChangeController : Controller<ChangeMessage>
    {
        public override void OnNotification(ChangeMessage message, GameObject obj, params object[] opData)
        {
            switch (message)
            {
                case ChangeMessage.DAMAGE:
                    HandleDamage(obj, (int) opData[0]);
                    break;
                case ChangeMessage.MOVE_SPEED:
                    HandleMoveSpeed(obj, (bool)opData[0], (float)opData[1]);
                    break;
            }
        }

        void HandleMoveSpeed(GameObject obj, bool reset, float speed)
        {
            MovingCharacter move = obj.GetComponent<MovingCharacter>();
            if (move)
            {
                if (reset)
                {
                    move.ResetSpeed();
                } else
                {
                    move.SetMoveSpeed(speed);
                }
            }
        }

        void HandleDamage(GameObject obj, int amount)
        {
            Damageable dam = obj.GetComponent<Damageable>();
            if (dam)
            {
                if(dam.IsBlocking() && dam.GetShieldPoints() > 0)
                {
                    dam.DamageShield(amount);
                } else
                {
                    dam.ChangeHealth(-amount);
                }
            } else
            {
                Debug.Log(obj.name + " was scheduled for damage calculations but doesn't have a damageable component.");
            }
        }
    }
}

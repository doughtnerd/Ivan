using UnityEngine;
using System.Collections;
using System;

namespace Arena
{
    public class ActionController : Controller<ActionMessage>
    {
        public override void OnNotification(ActionMessage message, GameObject obj, params object[] opData)
        {
            switch (message)
            {
                case ActionMessage.MOVE:
                    HandleMove(obj, (Vector3)opData[0]);
                    break;
                case ActionMessage.SHOOT:
                    HandleShoot(obj, (Vector3)opData[0]);
                    break;
                case ActionMessage.SLASH:
                    HandleSlash(obj, (Vector3) opData[0]);
                    break;
                case ActionMessage.DIE:
                    HandleDeath(obj);
                    break;
            }
        }

        void HandleSlash(GameObject obj, Vector3 direction)
        {
            Slasher slash = obj.GetComponent<Slasher>();
            if (slash) {
                slash.Slash(direction);
            } else
            {
                Debug.Log(obj.name + " is attempting to Slash but does not have a Slasher component.");
            }

        }

        void HandleDeath(GameObject obj)
        {
            Destroy(obj);
        }

        void HandleShoot(GameObject obj, Vector3 direction)
        {
            Shooter shoot = obj.GetComponent<Shooter>();
            if (shoot)
            {
                shoot.PrimaryShoot(direction);
            } else
            {

            }
        }

        void HandleMove(GameObject obj, Vector3 direction)
        {
            MovingCharacter move = obj.GetComponent<MovingCharacter>();
            if (move)
            {
                move.SetMoveDirection(direction);
            } else
            {
                Debug.Log(obj.name + " is trying to move but doesn't have a MovingCharacter componenet");
            }
        }
    }
}

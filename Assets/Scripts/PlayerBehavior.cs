using UnityEngine;
using System.Collections.Generic;
using System;

namespace Arena
{
    /// <summary>
    /// Makes the object this is attached to controllable by the player.
    /// </summary>
    public class PlayerBehavior : Behavior, IOnDeathBehavior
    {
        /// <summary>
        /// The direction from the player character that the mouse is positioned at.
        /// </summary>
        private Vector3 shootDirection;

        /// <summary>
        /// Dummy variable to enable toggling of the shield.
        /// </summary>
        private bool shield = false;

        private bool paused = false;

        protected override void Behave()
        {
            Move();
            shootDirection = GetPointingDirection();
            HandleControls(shootDirection);
        }

        void Move()
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
            app.NotifyAction(ActionMessage.MOVE, gameObject, moveDirection);
        }

        private Vector3 GetPointingDirection()
        {
            Vector3 temp = Input.mousePosition;
            return Camera.main.ScreenToWorldPoint(temp) - gameObject.transform.position;
        }

        void HandleControls(Vector3 direction)
        {
            if (Input.GetButton("Fire1"))
            {
                app.NotifyAction(ActionMessage.SHOOT, gameObject, shootDirection);
                DisableShield();
            }
            if (Input.GetButton("Fire2"))
            {
                app.NotifyAction(ActionMessage.SLASH, gameObject, shootDirection);
                DisableShield();
            }
            /*
            if (Input.GetButtonDown("Shield"))
            {
                shield = !shield;
                app.NotifyAction(ActionMessage.SHIELD, gameObject, shield);
            }
            */
            if (Input.GetButtonDown("Pause"))
            {
                paused = !paused;
                app.NotifyGame(GameMessage.PAUSE_GAME, gameObject, paused);
            }
        }

        /// <summary>
        /// Tells the game manager to disable the players shield and resets the toggle boolean.
        /// </summary>
        private void DisableShield()
        {
            app.NotifyAction(ActionMessage.SHIELD, gameObject, false);
            shield = false;
        }

        public void OnDeath()
        {
            this.SetBehaviorEnabled(false);
            Destroy(gameObject, 1.5f);
        }
    }
}

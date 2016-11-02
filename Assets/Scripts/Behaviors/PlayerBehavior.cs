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
            if (!Camera.main.orthographic)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float zPlanePos = 0;
                Vector3 posAtZ = ray.origin + ray.direction * (zPlanePos - ray.origin.z) / ray.direction.z;
                Vector2 point = new Vector2(posAtZ.x, posAtZ.y);
                return new Vector3(point.x, point.y, 0) - gameObject.transform.position;
            }
            Vector3 temp = Input.mousePosition;
            return Camera.main.ScreenToWorldPoint(temp) - gameObject.transform.position;
        }

        void HandleControls(Vector3 direction)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                app.NotifyAction(ActionMessage.SHOOT, gameObject, shootDirection);
                app.NotifyAnimation(AnimationMessage.TRIGGER, gameObject, "ranged");
            }
            if (Input.GetButtonDown("Fire2"))
            {
                app.NotifyAction(ActionMessage.SLASH, gameObject, shootDirection);
                app.NotifyAnimation(AnimationMessage.TRIGGER, gameObject, "melee");
            }
            if (Input.GetButtonDown("Pause"))
            {
                paused = !paused;
                app.NotifyGame(GameMessage.PAUSE_GAME, gameObject, paused);
            }
        }

        public void OnDeath()
        {
            this.SetBehaviorEnabled(false);
            Destroy(gameObject, 1.5f);
            app.NotifyGame(GameMessage.NEW_GAME, null);
        }
    }
}

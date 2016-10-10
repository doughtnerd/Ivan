using UnityEngine;
using System.Collections;
using System;

namespace Arena
{
    /// <summary>
    /// Represents a GameObject that is able to be damaged.
    /// </summary>
    public class Damageable : Messager
    {
        /// <summary>
        /// The max health this damageable object can have.
        /// </summary>
        [SerializeField]
        private int maxHealth = 3;

        /// <summary>
        /// The current health of the object.
        /// </summary>
        [SerializeField]
        private int health = 3;

        /// <summary>
        /// The max shield points this object can have.
        /// </summary>
        [SerializeField]
        private int maxShieldPoints = 3;

        /// <summary>
        /// The current shield points of this object.
        /// </summary>
        [SerializeField]
        private int shieldPoints = 3;

        /// <summary>
        /// Amount to increase movement speed by when shield is active.
        /// </summary>
        [SerializeField]
        private float shieldSpeedIncrease = 5f;

        /// <summary>
        /// The recharge speed of the object's shield points.
        /// </summary>
        [SerializeField]
        private float shieldRechargeSpeed = 20f;

        /// <summary>
        /// The amount of time the gameobject has before it is able to take damage again.
        /// </summary>
        [SerializeField]
        private float iFrameTime = .5f;

        /// <summary>
        /// Time when the iFrames stop.
        /// </summary>
        private float iFrameStop;
        
        /// <summary>
        /// Whether or not this object is currently using its shield functionality, handled internally by a controller.
        /// </summary>
        private bool isBlocking;

        /// <summary>
        /// Time when the next shield point is regenerated.
        /// </summary>
        private float nextChargeTime;

        /// <summary>
        /// Whether or not the object has died.
        /// </summary>
        private bool didDie = false;

        /// <summary>
        /// Sets this objects max health.
        /// </summary>
        /// <param name="health">New value for this objects max health.</param>
        public void SetMaxHealth(int health)
        {
            this.maxHealth = health;
        }

        /// <summary>
        /// Sets this objects health.
        /// </summary>
        /// <param name="health">The new value for this objects health.</param>
        public void SetHealth(int health)
        {
            this.health = health;
        }


        /// <summary>
        /// Changes the object's health by the specified amount.
        /// If amount is less than 0, the change is considered damage. 
        /// If amount is greater than 0, the change is considered healing.
        /// </summary>
        /// <param name="amount">The positive or negative amount to change this object's health by.</param>
        public void ChangeHealth(int amount)
        {
            int newHealth = health + amount;
            if (amount < 0)
            {
                if (Time.time > iFrameStop)
                {
                    //app.NotifyAnim(AnimationMessage.DAMAGE, gameObject);
                    health = newHealth <= 0 ? 0 : newHealth;
                    iFrameStop = Time.time + iFrameTime;
                }
            }
            else if (amount > 0)
            {
                health = newHealth > maxHealth ? maxHealth : newHealth;
            }
        }

        /// <summary>
        /// Returns this objects max health
        /// </summary>
        /// <returns>This object's max health.</returns>
        public int GetMaxHealth()
        {
            return this.maxHealth;
        }

        /// <summary>
        /// Returns this objects current health.
        /// </summary>
        /// <returns>This objects current health.</returns>
        public int GetHealth()
        {
            return this.health;
        }

        /// <summary>
        /// Whether or not this objects health is less than or equal to zero.
        /// </summary>
        /// <returns>True if health is less than or equal to 0, false otherwise.</returns>
        public bool IsDead()
        {
            return this.health <= 0;
        }

        /// <summary>
        /// Damages this objects shield.
        /// </summary>
        /// <param name="damage">the amount to damage the shield by.</param>
        public void DamageShield(int damage)
        {
            if(Time.time > iFrameStop)
            {
                this.shieldPoints -= damage;
                if (shieldPoints <= 0)
                {
                    this.SetBlocking(false);
                }
                nextChargeTime = Time.time + this.shieldRechargeSpeed;
            }
        }

        /// <summary>
        /// Sets this objects max shield points.
        /// </summary>
        /// <param name="points">The new value for this objects max shield.</param>
        public void SetMaxShieldPoints(int points)
        {
            this.maxShieldPoints = points;
        }

        /// <summary>
        /// Returns this objects max shield points.
        /// </summary>
        /// <returns>This objects max shield points.</returns>
        public int GetMaxShieldPoints()
        {
            return this.maxShieldPoints;
        }

        /// <summary>
        /// Sets this objects shield points.
        /// </summary>
        /// <param name="points">The new value for this objects shield points.</param>
        public void SetShieldPoints(int points)
        {
            this.shieldPoints = points;
        }

        /// <summary>
        /// Returns this objects current shield points.
        /// </summary>
        /// <returns>This objects shield points.</returns>
        public int GetShieldPoints()
        {
            return this.shieldPoints;
        }

        /// <summary>
        /// Returns whether or not this object is currently blocking.
        /// </summary>
        /// <returns>True if it is blocking, false otherwise.</returns>
        public bool IsBlocking()
        {
            return this.isBlocking;
        }

        /// <summary>
        /// Sets whether this object is blocking or not.
        /// </summary>
        /// <param name="blocking">New value for blocking</param>
        public void SetBlocking(bool blocking)
        {
            this.isBlocking = shieldPoints>0 && blocking;
            if(isBlocking)
            {
                //app.NotifyChange(ChangeMessage.MOVE_SPEED, gameObject, false, shieldSpeedIncrease);
            } else
            {
                //app.NotifyChange(ChangeMessage.MOVE_SPEED, gameObject, true, 0f);
            }
            //app.NotifyAnim(AnimationMessage.SHIELD, gameObject, isBlocking);
        }

        /// <summary>
        /// Handles checking if this object is dead and shield recharge.
        /// </summary>
        void Update()
        {
            if (IsDead())
            {
                if (!didDie)
                {
                    //app.NotifyAnim(AnimationMessage.DIE, gameObject);
                    app.NotifyAction(ActionMessage.DIE, gameObject);
                    didDie = true;
                }
            }
            if(Time.time >= nextChargeTime && this.shieldPoints<maxShieldPoints)
            {
                this.shieldPoints++;
                nextChargeTime = Time.time + this.shieldRechargeSpeed;
            }
        }
    }
}

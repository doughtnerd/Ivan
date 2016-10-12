using UnityEngine;
using System.Collections;

namespace Arena{

    /// <summary>
    /// Gives the object this class is attached to melee abilities.
    /// </summary>
    public class Slasher : Damager
    {
        /// <summary>
        /// The size of the box raycast to shoot forward.
        /// </summary>
        [SerializeField]
        private Vector2 hitBoxSize;

        /// <summary>
        /// Rate at which the melee ability can be used.
        /// </summary>
        [SerializeField]
        private float slashRate = .5f;

        /// <summary>
        /// Time until the next slash.
        /// </summary>
        private float nextSlashTime;

        /// <summary>
        /// Whether or not to knock back the target when hit.
        /// </summary>
        [SerializeField]
        private bool knockbackOnHit = false;

        /// <summary>
        /// How far to know the target back upon hitting.
        /// </summary>
        [SerializeField]
        private int knockbackDistance = 3;

        /// <summary>
        /// Returns the box size for the raycast.
        /// </summary>
        /// <returns>This objects hit box size.</returns>
        public Vector2 GetBoxSize()
        {
            return this.hitBoxSize;
        }

        /// <summary>
        /// Causes this object to melee attack using the desired hit box size, direction, and targeting this objects targetLayer.
        /// </summary>
        /// <param name="direction">Direction to slash in.</param>
        /// <param name="layer">Target layer of objects that can be attacked.</param>
        public void Slash(Vector3 direction)
        {
            if(Time.time > nextSlashTime)
            {
                //app.NotifyAnim(AnimationMessage.SLASH, gameObject);
                RaycastHit2D[] arr = Physics2D.BoxCastAll(gameObject.transform.position, hitBoxSize, 0f, direction, detectionRange, damageableLayer);
                for (int i = 0; i < arr.Length; i++)
                {
                    Collider2D coll = arr[i].collider;
                    GameObject obj = coll.gameObject;
                    if (knockbackOnHit)
                    {
                        obj.transform.position = (obj.transform.position - transform.position).normalized * knockbackDistance + transform.position;
                    }
                    base.OnOverlap(coll);
                }
                nextSlashTime = Time.time + slashRate;
            }
        }

        protected override void Update()
        {
        }
    }
}

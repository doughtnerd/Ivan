using UnityEngine;
using System.Collections;

namespace Arena
{
    /// <summary>
    /// Gives an object shootable properties, behavior, and functionality.
    /// </summary>
    public class Shootable : Damager
    {
        /// <summary>
        /// How quickly this object travels.
        /// </summary>
        [SerializeField]
        private float speed = 1f;

        /// <summary>
        /// The range of this object. 
        /// After this object travels the specified range, this object will destroy itself.
        /// </summary>
        private float range;

        /// <summary>
        /// The direction this object should travel in.
        /// </summary>
        private Vector3 direction;

        /// <summary>
        /// This objects origin location.
        /// </summary>
        private Vector3 origin;

        /// <summary>
        /// This objects velocity.
        /// </summary>
        private Vector3 vel;

        /// <summary>
        /// Sets this objects direction. 
        /// </summary>
        /// <param name="direction">The new direction value for this object.</param>
        public void SetDirection(Vector3 direction)
        {
            this.direction = direction;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        /// <summary>
        /// Sets the bullet's travel speed.
        /// </summary>
        /// <param name="speed"></param>
        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        /// <summary>
        /// Sets this objects range.
        /// </summary>
        /// <param name="range">The new value for this objects range.</param>
        public void SetRange(float range)
        {
            this.range = range;
        }

        void Start()
        {
            origin = this.transform.position;
            vel = direction.normalized * speed;
        }

        protected override void OnOverlap(Collider2D coll)
        {
            base.OnOverlap(coll);
            Destroy(gameObject);
        }

        void FixedUpdate()
        {
            float distance = Mathf.Abs(Vector3.Distance(this.transform.position, origin));
            transform.position = transform.position + vel;
            if (distance >= range)
            {
                Destroy(gameObject);
            }
        }
    }
}

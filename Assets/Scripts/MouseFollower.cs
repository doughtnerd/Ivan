using UnityEngine;
using System.Collections;

namespace Arena
{
    public class MouseFollower : MonoBehaviour
    {
        public Quaternion rotation { get; set; }

        // Update is called once per frame
        void Update()
        {
            Vector3 temp = Input.mousePosition;
            SetDirection(Camera.main.ScreenToWorldPoint(temp));
        }

        public void SetDirection(Vector3 direction)
        {
            float angleRad = Mathf.Atan2(direction.y - transform.position.y, direction.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad;
            rotation = Quaternion.Euler(0, 0, angleDeg);
            this.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
    }
}

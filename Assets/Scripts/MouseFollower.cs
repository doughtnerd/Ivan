using UnityEngine;
using System.Collections;

namespace Arena
{
    public class MouseFollower : MonoBehaviour
    {
        void Update()
        {
            Vector3 temp = Input.mousePosition;
            SetDirection(Camera.main.ScreenToWorldPoint(temp));
        }

        public void SetDirection(Vector3 location)
        {
            float angleRad = Mathf.Atan2(location.y - transform.position.y, location.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * (angleRad - Mathf.PI/2);
            this.transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }
    }
}

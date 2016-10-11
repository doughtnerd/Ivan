using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Arena
{
    public abstract class StatUpdater : MonoBehaviour
    {
        [SerializeField]
        private Image icon;

        [SerializeField]
        private float yOffset = 0;

        [SerializeField]
        private float xOffSet = .5f;

        [SerializeField]
        private float xPad = 1.5f;

        protected GameObject player;
        private List<Image> icons;
        private Canvas canvas;

        private int prevAmount;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            icons = new List<Image>();
            canvas = GetComponent<Canvas>();
        }

        public void UpdateIcons(int points)
        {
            if (points != prevAmount)
            {
                prevAmount = points;
                for (int i = 0; i < icons.Count; i++)
                {
                    DestroyImmediate(icons[i].gameObject);
                }
                icons.Clear();

                for (int i = 0; i < points; i++)
                {
                    Image image = Instantiate<Image>(icon);
                    image.transform.SetParent(gameObject.transform, false);
                    image.rectTransform.position = new Vector3(image.transform.position.x + ((i * image.rectTransform.rect.width * canvas.scaleFactor) + xOffSet) * xPad, image.transform.position.y - (yOffset * image.rectTransform.rect.height * canvas.scaleFactor), 0);
                    icons.Add(image);
                }
            }
        }

        protected virtual void Update()
        {
            if (!player)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZoaProto
{
    public class ScoreUpdater : MonoBehaviour
    {
        private int currentScore = 0;

        [SerializeField]
        private Text scoreText;

        private Text instance;

        void Start()
        {
            scoreText = Instantiate<Text>(scoreText);
            scoreText.transform.SetParent(gameObject.transform, false);
        }

        public void UpdateScore(int newScore)
        {
            this.currentScore = newScore;
            scoreText.text = "Score: " + currentScore;
        }
    }
}

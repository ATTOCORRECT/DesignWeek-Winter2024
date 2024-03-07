using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace team28 
{
    public class DisplayManager : MicrogameEvents
    {
        public Color[] colors;

        public string[] sadExpressions;
        public string[] nutralExpressions;
        public string[] happyExpressions;

        string textQueue;

        public ScoreManager scoreManager;

        int score;

        TextMesh text;
        // Start is called before the first frame update
        void Start()
        {
            text = gameObject.GetComponent<TextMesh>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateDisplayScore()
        {
            score = scoreManager.score + 1;

            text.text = score.ToString();

            int happiness = Mathf.Clamp(score / 5, 0, 2);
            
            
            if (score > 0)
            {
                FlashText(GetRandomFace(happiness));
            }
            text.color = colors[happiness];
        }

        private void FlashText(string flashText)
        {
            textQueue = text.text;
            text.text = flashText;

            Invoke("setTextToQueue",0.5f);
        }

        private void setTextToQueue()
        {
            text.text = textQueue;
        }

        private string GetRandomFace(int happines)
        {
            int randomIndex;
            switch (happines)
            {
                case 0:
                    randomIndex = Random.Range(0, sadExpressions.Length);
                    return sadExpressions[randomIndex];
                case 1:
                    randomIndex = Random.Range(0, nutralExpressions.Length);
                    return nutralExpressions[randomIndex];
                case 2:
                    randomIndex = Random.Range(0, happyExpressions.Length);
                    return happyExpressions[randomIndex];
                default:
                    return ":|";
            }
        }
    }
}



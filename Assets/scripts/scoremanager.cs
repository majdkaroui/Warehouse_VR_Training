  
    using TMPro;
    using UnityEngine;

    public class scoremanager : MonoBehaviour
    {
        public static float score;
        public   TMP_Text score_text;
        public int correctplacements=0;
        public int wrongplacements = 0;
        public static int numberoferrors = 0;


        // Start is called before the first frame update
        public void InitializeScore()
        {
            score = 0;
            numberoferrors = 0;
        }
         public void IncrementScore()
        {
            score += 10;
            correctplacements += 1;
        }
        public void Decrement_Score()
        {
            score -= 10;
            wrongplacements += 1;
            numberoferrors += 1;




        }
        public void Decrement_Wrong_placements() {
            wrongplacements -= 1;
        }




        // Update is called once per frame
        void Update()
        {
            score_text.text = "Score : "+score.ToString();


        }
    }

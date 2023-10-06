using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GetFinalScore : MonoBehaviour
{  
    public TMP_Text Final_Score;
    // Start is called before the first frame update
    void Start()
    {
        Final_Score.text="Final Score :"+scoremanager.score.ToString();
    }

    // Update is called once per frame
    
}

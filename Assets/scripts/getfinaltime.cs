using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GetFinalTime : MonoBehaviour
{
    public TMP_Text Final_Time;
    // Start is called before the first frame update
    void Start()
    {
        Final_Time.text = "Time Elapsed :" + TimerScript.current_time;
    }

    // Update is called once per frame

}

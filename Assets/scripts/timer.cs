using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public static string current_time;
    private float startTime;
    static bool isTimerRunning = false;

    public TMP_Text timerText; // Reference to the TextMeshPro component

    // Start is called before the first frame update

    
    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            // Calculate the elapsed time
            float currentTime = Time.time - startTime;

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            // Update the TextMeshPro component to display the timer
            current_time = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = "Time: "+ string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    

    // Function to start the timer
    public void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    // Function to stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Function to reset the timer
    public void ResetTimer()
    {
        startTime = 0f;
        timerText.text = "00:00"; // Reset the TextMeshPro display
        isTimerRunning = false;
    }

    // Function to get the current time in seconds
    public float GetTime()
    {   
        return Time.time - startTime;
    }
}

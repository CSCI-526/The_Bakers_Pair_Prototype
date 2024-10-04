using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Import this to use UI elements
using TMPro;

public class GameManager : MonoBehaviour
{
    public WinCon winCon; // Reference to the WinCon script
    public LoseCon loseCon; // Reference to the LoseCon script
    public TMP_Text timerText; // Reference to the Timer Text UI element

    private float timerDuration = 60f; // 1 minute timer
    private float timer = 60.0f; // Timer variable

    void Start()
    {
        timer = timerDuration; // Initialize timer
        StartCoroutine(TimerCountdown()); // Start the timer countdown
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator TimerCountdown()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime; // Decrease the timer
            UpdateTimerUI(); // Update the UI text
            yield return null; // Wait for the next frame
        }
        // Timer has run out
        loseCon.DisplayLoseUI(); // Call lose condition
    }

    private void UpdateTimerUI()
    {
        timerText.text = "Time Left: " + Mathf.Ceil(timer).ToString() + "s"; // Update the text
    }
}

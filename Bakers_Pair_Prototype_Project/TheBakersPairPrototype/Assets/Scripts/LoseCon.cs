using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class LoseCon : MonoBehaviour
{
    public GameObject loseUI;               // Reference to the "You Lose" UI
    public TextMeshProUGUI timerText;       // Reference to the UI Text element for displaying the timer (TextMeshPro)
    public float gameDuration = 60f;        // Game duration in seconds
    private float timer;

    void Start()
    {
        timer = gameDuration;               // Initialize the timer
        loseUI.SetActive(false);            // Ensure the lose UI is hidden at the start
        StartCoroutine(TimerCountdown());    // Start the countdown timer
    }

    IEnumerator TimerCountdown()
    {
        while (timer > 0)
        {
            timer -= Time.deltaTime;          // Decrease the timer by delta time
            UpdateTimerDisplay();             // Update the displayed timer value
            yield return null;                // Wait until the next frame
        }

        // When the timer runs out, show the "You Lose" UI
        ShowLoseUI();
    }

    // Method to update the timer text in the UI
    void UpdateTimerDisplay()
    {
        // Display the timer as minutes:seconds format
        int minutes = Mathf.FloorToInt(timer / 60f); // Get the minutes
        int seconds = Mathf.FloorToInt(timer % 60f); // Get the remaining seconds

        // Update the timerText to display remaining time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool IsLoseConditionMet()
    {
        return timer <= 0;                    // Returns true if the timer has run out
    }

    void ShowLoseUI()
    {
        loseUI.SetActive(true);               // Show the "You Lose" UI
        // Additional logic for losing (like stopping the game) can be added here
    }

    // Method to display lose UI manually (optional, can be triggered from other scripts)
    public void DisplayLoseUI()
    {
        loseUI.SetActive(true);               // Activate the lose UI
    }
}

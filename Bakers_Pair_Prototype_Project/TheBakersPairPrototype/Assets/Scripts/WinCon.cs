using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCon : MonoBehaviour
{
    public GameObject tower;          // Reference to the tower (parent object)
    public GameObject victoryUI;      // Reference to the victory UI
    public float topOfScreenY = -2f;   // Y-coordinate that represents the top of the screen

    void Start()
    {
        victoryUI.SetActive(false);   // Ensure the win UI is hidden at the start
    }

    void Update()
    {
        CheckTowerHeight();           // Continuously check if the tower has reached the top
    }

    void CheckTowerHeight()
    {
        // Get the highest block's Y-position in the tower (assuming blocks are children of the tower)
        foreach (Transform block in tower.transform)
        {
            if (block.position.y >= topOfScreenY)
            {
                ShowWinUI();
                break;
            }
        }
    }

    void ShowWinUI()
    {
        victoryUI.SetActive(true);     // Display the win UI when the tower reaches the top
        // Additional logic for winning (e.g., stopping the game) can be added here
    }
}

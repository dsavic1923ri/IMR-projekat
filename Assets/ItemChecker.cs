using UnityEngine;
using TMPro; // 1. This allows us to control TextMeshPro UI elements

public class ItemChecker : MonoBehaviour
{
    public int score = 0;
    public int lives = 3; // Fixed: Set the starting lives right here

    // References to your UI text components
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI gameOverText; 

    private bool isGameOver = false;

    void Start()
    {
        // Hide the win/lose text when the game starts
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        UpdateUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the game is already over, ignore any more items hitting the player
        if (isGameOver) return;

        if (other.CompareTag("Good"))
        {
            score += 5;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Gooder"))
        {
            score += 10;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Bad"))
        {
            lives--;
            Destroy(other.gameObject);
        }

        // Update our display texts and check if the game should end
        UpdateUI();
        CheckGameStatus();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (livesText != null) livesText.text = "Lives: " + lives;
    }

    void CheckGameStatus()
    {
        if (lives <= 0)
        {
            EndGame("GAME OVER: You Died!");
        }
        else if (score >= 100)
        {
            EndGame("YOU WIN!");
        }
    }

    void EndGame(string message)
    {
        isGameOver = true;
        
        if (gameOverText != null)
        {
            gameOverText.text = message;
            gameOverText.gameObject.SetActive(true);
        }

        // Freeze time so everything stops moving/falling
        Time.timeScale = 0f; 
    }
}
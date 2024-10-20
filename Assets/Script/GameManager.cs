using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    private int score = 0;
    private int highScore = 0;

    [Header("Health")]
    public Image[] heartImages;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;
    public int maxHealth = 3;
    private int currentHealth;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Load the high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateScoreDisplay();
        UpdateHighScoreDisplay();
        InitializeHealth();
    }

    // Score Management
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();

        if (score > highScore)
        {
            SetHighScore(score);
        }
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    void SetHighScore(int newHighScore)
    {
        highScore = newHighScore;
        PlayerPrefs.SetInt("HighScore", highScore);
        UpdateHighScoreDisplay();
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    // Health Management
    void InitializeHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public void UpdateHealthDisplay()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Add your game over logic here
    }
}
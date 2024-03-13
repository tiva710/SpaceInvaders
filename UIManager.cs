using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    
    private int score = 0;
    private int highScore = 0;
    public GameObject introPointsUI;

    void Start()
    {
        // Initialize high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
        UpdateHighScoreUI();
        
        StartCoroutine(HideIntro(5f));
    }
    
    IEnumerator HideIntro(float delay)
    {
        yield return new WaitForSeconds(delay);
        introPointsUI.SetActive(false); 
    }

    public void AddScore(int points)
    {
        score += points;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    void UpdateScoreUI()
    {
        // Update current score text with leading zeros
        scoreText.text = $"Score: {score:0000}";
    }

    void UpdateHighScoreUI()
    {
        // Update high score text with leading zeros
        highScoreText.text = $"High Score: {highScore:0000}";
    }
}

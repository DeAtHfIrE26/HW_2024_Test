using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        Debug.Log("ScoreManager Awake called");
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("ScoreManager instance set");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate ScoreManager destroyed");
        }
    }

    void Start()
    {
        Debug.Log("ScoreManager Start called");
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
            if (scoreText == null)
            {
                Debug.LogError("ScoreText object not found in the scene!");
            }
            else
            {
                Debug.Log("ScoreText object assigned successfully.");
            }
        }
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score updated: {score}");
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        Debug.Log("Score reset to 0");
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
            Debug.Log($"ScoreText updated: {scoreText.text}");
        }
        else
        {
            Debug.LogError("ScoreText is not assigned!");
        }
    }
}
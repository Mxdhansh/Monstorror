using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;

    public TMP_Text scoreText;
    public TMP_Text winText;
    public TMP_Text timerText;

    private float timeRemaining = 300f; // 5 minutes
    private bool gameEnded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        score = 0;

        scoreText.text = "Humans Eaten: 0 / 26";
        winText.text = "";
        timerText.text = "Time: 5:00";
    }

    private void Update()
    {
        if (gameEnded)
            return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0)
            timeRemaining = 0;

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("Time: {0}:{1:00}", minutes, seconds);

        if (timeRemaining <= 0)
        {
            gameEnded = true;
            winText.text = "TIME UP!\n\nThe Humans Escaped!";
            Time.timeScale = 0f;
        }
    }

    public void AddScore()
    {
        if (gameEnded)
            return;

        score++;

        scoreText.text = "Humans Eaten: " + score + " / 26";

        if (score >= 26)
        {
            gameEnded = true;
            winText.text = "YOU WIN!\n\nAll Humans Have Been Eaten!";
            Time.timeScale = 0f;
        }
    }
}
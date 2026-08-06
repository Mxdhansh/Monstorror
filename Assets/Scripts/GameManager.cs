using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;

    public TMP_Text scoreText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreText.text = "Score: 0";
    }

    public void AddScore()
    {
        score++;

        scoreText.text = "Score: " + score;

        if (score >= 40)
        {
            scoreText.text = "YOU WIN!";
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;

    public TMP_Text scoreText;
    public TMP_Text winText;
    public TMP_Text timerText;

    public GameObject mainMenu;

    private float timeRemaining = 300f;
    private bool gameEnded = false;
    private bool gameStarted = false;

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

        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        mainMenu.SetActive(true);

        Time.timeScale = 0f;
    }

    private void Update()
    {
        // Start game
        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStarted = true;

                mainMenu.SetActive(false);

                scoreText.gameObject.SetActive(true);
                timerText.gameObject.SetActive(true);

                Time.timeScale = 1f;
            }

            return;
        }

        // Restart game
        if (gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            return;
        }

        timeRemaining -= Time.deltaTime;

        if (timeRemaining < 0)
            timeRemaining = 0;

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("Time: {0}:{1:00}", minutes, seconds);

        if (timeRemaining <= 0)
        {
            gameEnded = true;
            winText.text = "TIME UP!\n\nThe Humans Escaped!\n\nPress R to Restart";
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
            winText.text = "YOU WIN!\n\nAll Humans Have Been Eaten!\n\nPress R to Restart";
            Time.timeScale = 0f;
        }
    }
}
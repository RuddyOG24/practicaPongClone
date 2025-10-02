using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    public Text scoreText;          // "L : R"
    public Text highScoreText;      // opcional: texto para mostrar High Score

    [Header("Límites de gol")]
    public float goalX = 9f;        // mismo valor que usabas (±9f)

    [Header("Referencias")]
    public BallController ball;     // arrastra tu Ball en el inspector

    private int leftScore = 0;
    private int rightScore = 0;

    // Evita sumar múltiples veces en el mismo “cruce” de la pelota
    private bool goalCooldown = false;

    void Start()
    {
        // Mostrar High Score guardado (si no existe, 0)
        int high = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
            highScoreText.text = "High Score: " + high;

        UpdateScoreUI();
    }

    void Update()
    {
        if (ball == null) return;

        // Revisa si la pelota sale por los lados (derecha = punto para left; izquierda = punto para right)
        if (!goalCooldown && ball.transform.position.x > goalX)
        {
            leftScore++;
            OnScore();
        }
        else if (!goalCooldown && ball.transform.position.x < -goalX)
        {
            rightScore++;
            OnScore();
        }
    }

    void OnScore()
    {
        UpdateScoreUI();
        SaveHighScoreIfNeeded();

        // Resetea la pelota y activa un breve cooldown para evitar dobles conteos
        ResetBall();
        StartCoroutine(GoalCooldown());
    }

    IEnumerator GoalCooldown()
    {
        goalCooldown = true;
        yield return new WaitForSeconds(0.5f); // medio segundo alcanza
        goalCooldown = false;
    }

    void ResetBall()
    {
        // reposiciona y relanza usando tu BallController
        ball.transform.position = Vector3.zero;
        ball.ResetBall(); // ✅ ya lo tenías funcionando
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = leftScore + " : " + rightScore;
    }

    void SaveHighScoreIfNeeded()
    {
        // Métrica simple: suma total del marcador
        int total = leftScore + rightScore;
        int currentHigh = PlayerPrefs.GetInt("HighScore", 0);

        if (total > currentHigh)
        {
            PlayerPrefs.SetInt("HighScore", total);
            PlayerPrefs.Save();

            if (highScoreText != null)
                highScoreText.text = "High Score: " + total;
        }
    }
}

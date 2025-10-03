using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("UI")]
    public Text scoreText;       // "L : R"
    public Text highScoreText;   // opcional
    public float serveDelay = 0.75f;

    [Header("Referencias")]
    public BallController ball;

    private int leftScore = 0;
    private int rightScore = 0;

    void Start()
    {
        UpdateUI();
        if (ball != null) ball.ServeRandom(); // primer saque aleatorio
    }

    // Llamado por GoalZone: isRightGoal=true si anotaron en el arco derecho
    public void RegisterGoal(bool isRightGoal)
    {
        if (isRightGoal)
            leftScore++;    // si entró en el arco derecho, punto para la izquierda
        else
            rightScore++;   // si entró en el arco izquierdo, punto para la derecha

        UpdateUI();

        // Detener y re-servir
        ball.DeactivateOnGoal();
        StartCoroutine(ServeAfterDelayRandom());
    }

    IEnumerator ServeAfterDelayRandom()
    {
        yield return new WaitForSeconds(serveDelay);
        ball.ServeRandom(); // siempre desde el centro, izquierda o derecha aleatorio
    }

    void UpdateUI()
    {
        if (scoreText) scoreText.text = $"{leftScore} : {rightScore}";
        if (highScoreText) highScoreText.text = $"High: {Mathf.Max(leftScore, rightScore)}";
    }

    public void ResetMatch()
    {
        leftScore = rightScore = 0;
        UpdateUI();
        StopAllCoroutines();
        ball.DeactivateOnGoal();
        StartCoroutine(ServeAfterDelayRandom());
    }
}

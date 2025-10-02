using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int leftScore = 0;
    private int rightScore = 0;

    public BallController ball;

    void Update()
    {
        if (!ball) return;

        // Revisar si la pelota salió
        if (ball.transform.position.x > 9f) // lado derecho
        {
            leftScore++;
            EndGame();
        }
        else if (ball.transform.position.x < -9f) // lado izquierdo
        {
            rightScore++;
            EndGame();
        }

        scoreText.text = leftScore + " : " + rightScore;
    }

    void EndGame()
    {
        ball.StopBall();
        Debug.Log("Juego terminado");
        // Aquí podrías mostrar un panel de "Game Over" si quieres
    }
}

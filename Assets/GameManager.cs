using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Refs")]
    public Ball ball;
    public PaddleController leftPaddle;
    public PaddleController rightPaddle;

    [Header("Score")]
    public int scoreLeft = 0;
    public int scoreRight = 0;
    public int targetScore = 0; // 0 = sin límite

    public static event Action<int, int> OnScoreChanged;

    void OnEnable()
    {
        GoalZone.OnGoal += OnGoal;
    }

    void OnDisable()
    {
        GoalZone.OnGoal -= OnGoal;
    }

    void Start()
    {
        NotifyScore();
    }

    void OnGoal(GoalSide side)
    {
        // Si la bola entra en GoalLeft, punto para Right; y viceversa
        if (side == GoalSide.Left) scoreRight++;
        if (side == GoalSide.Right) scoreLeft++;

        NotifyScore();

        // ¿Game Over?
        if (targetScore > 0 && (scoreLeft >= targetScore || scoreRight >= targetScore))
        {
            // Aquí podrías congelar o cargar otra escena
            // Por ahora, reiniciamos marcador
            scoreLeft = scoreRight = 0;
            NotifyScore();
        }

        // Relanzar hacia quien recibió el gol (quien acaba de perder)
        int launchDir = (side == GoalSide.Left) ? -1 : 1; // hacia la izquierda si gol en izquierda, etc.
        ball.ResetAndLaunch(launchDir);
    }

    void NotifyScore() => OnScoreChanged?.Invoke(scoreLeft, scoreRight);
}

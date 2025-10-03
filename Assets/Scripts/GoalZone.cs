using UnityEngine;

public class GoalZone : MonoBehaviour
{
    public bool isRightGoal = false; // true = arco de la derecha

    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.GetComponent<BallController>();
        if (ball == null) return;

        // Notifica al marcador que hubo gol en este arco
        var score = FindObjectOfType<ScoreManager>();
        if (score != null)
        {
            score.RegisterGoal(isRightGoal);
        }
    }
}

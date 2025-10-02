using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody2D rb;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void LaunchBall()
    {
        if (isMoving) return;

        transform.position = Vector3.zero;
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(-1f, 1f);
        Vector2 direction = new Vector2(x, y).normalized;

        rb.velocity = direction * speed;
        isMoving = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving) return;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Rebotar con ángulo según punto de contacto
            float y = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            Vector2 dir = new Vector2(-rb.velocity.x, y).normalized;
            rb.velocity = dir * speed;
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleHeight)
    {
        return (ballPos.y - paddlePos.y) / paddleHeight;
    }

    public void StopBall()
    {
        rb.velocity = Vector2.zero;
        isMoving = false;
    }

    public void ResetBall(int direction = 0)
    {
        // Detener y centrar
        rb.velocity = Vector2.zero;
        isMoving = false;
        transform.position = Vector3.zero;

        // Relanzar (opcionalmente hacia un lado específico)
        int dirX = direction != 0 ? direction : (Random.value < 0.5f ? -1 : 1);
        float y = Random.Range(-1f, 1f);
        Vector2 dir = new Vector2(dirX, y).normalized;

        rb.velocity = dir * speed;
        isMoving = true;
    }
}

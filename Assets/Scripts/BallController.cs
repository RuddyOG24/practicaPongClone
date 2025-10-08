using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BallController : MonoBehaviour
{
    [Header("Velocidad")]
    public float initialSpeed = 7f;
    public float speedIncrement = 0.5f;
    public float maxSpeed = 14f;

    private Rigidbody2D rb;
    private Collider2D col;
    private Vector2 dir;
    private float currentSpeed;
    public bool IsActive { get; private set; } = false;

    public Vector2 Velocity => rb ? rb.velocity : Vector2.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Start()
    {
        ServeRandom(); // primer saque
    }

    /// <summary>
    /// Saque desde el centro, recto en X; dirX: -1 izq, +1 der.
    /// </summary>
    public void Serve(int dirX)
    {
        gameObject.SetActive(true);
        transform.position = Vector3.zero;

        currentSpeed = initialSpeed;
        float sx = Mathf.Sign(dirX) >= 0 ? 1f : -1f;
        dir = new Vector2(sx, 0f); // y = 0 (recto)
        rb.velocity = dir * currentSpeed;

        IsActive = true;
        col.enabled = true;
    }

    /// <summary>
    /// Saque aleatorio: izquierda o derecha, recto.
    /// </summary>
    public void ServeRandom()
    {
        int dir = Random.value < 0.5f ? -1 : 1;
        Serve(dir);
    }

    /// <summary>
    /// Usado SOLO cuando hay gol.
    /// </summary>
    public void DeactivateOnGoal()
    {
        IsActive = false;
        rb.velocity = Vector2.zero;
        col.enabled = false;
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (!IsActive) return;

        // ¿Golpeó paleta?
        bool hitPaddle = c.collider.CompareTag("Paddle") ||
                         c.collider.GetComponent<PaddleControllerSimple>() != null;

        if (hitPaddle)
        {
            // Ángulo según punto de impacto
            float y = HitFactor(transform.position, c.transform.position, c.collider.bounds.size.y);

            // Invertir X; aplicar componente Y
            dir = new Vector2(-Mathf.Sign(rb.velocity.x), y).normalized;

            // Aumentar velocidad con tope
            currentSpeed = Mathf.Min(currentSpeed + speedIncrement, maxSpeed);
            rb.velocity = dir * currentSpeed;
        }
        else
        {
            // Paredes u otros: mantener norma de velocidad
            if (rb.velocity.sqrMagnitude > 0.0001f)
                dir = rb.velocity.normalized;

            rb.velocity = dir * currentSpeed;
        }
    }

    // -1 abajo .. 1 arriba
    float HitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleHeight)
    {
        return Mathf.Clamp((ballPos.y - paddlePos.y) / (paddleHeight * 0.5f), -1f, 1f);
    }
}

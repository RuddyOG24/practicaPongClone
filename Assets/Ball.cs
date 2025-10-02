using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [Header("Speed")]
    public float initialSpeed = 7f;
    public float speedIncrement = 0.5f;
    public float maxSpeed = 14f;

    private Rigidbody2D rb;
    private Vector2 dir;
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ResetAndLaunch();
    }

    public void ResetAndLaunch(int launchDir = 0) // 0 = random, -1 left, 1 right
    {
        transform.position = Vector3.zero;
        currentSpeed = initialSpeed;

        int sx = launchDir != 0 ? launchDir : (Random.value < 0.5f ? -1 : 1);
        float sy = Random.Range(-0.35f, 0.35f);

        dir = new Vector2(sx, sy).normalized;
        rb.velocity = dir * currentSpeed;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        // Si golpea paleta, agrega componente vertical según punto de impacto
        if (c.collider.GetComponent<PaddleController>() != null)
        {
            float y = HitFactor(transform.position, c.transform.position, c.collider.bounds.size.y);
            dir = new Vector2(Mathf.Sign(dir.x) * -1f, y).normalized;

            currentSpeed = Mathf.Min(currentSpeed + speedIncrement, maxSpeed);
            rb.velocity = dir * currentSpeed;
        }
        else
        {
            // Recalcular velocidad tras paredes por seguridad
            rb.velocity = rb.velocity.normalized * currentSpeed;
        }
    }

    float HitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleHeight)
    {
        // -1 (abajo) .. 1 (arriba)
        return Mathf.Clamp((ballPos.y - paddlePos.y) / (paddleHeight * 0.5f), -1f, 1f);
    }
}

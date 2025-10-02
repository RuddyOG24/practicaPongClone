using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleController : MonoBehaviour
{
    [Header("Config")]
    public bool isLeftPaddle = true;
    public float speed = 10f;
    public float clampY = 4.2f; // límite vertical según tu cámara

    private Rigidbody2D rb;
    private Vector2 targetPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = rb.position;
    }

    void FixedUpdate()
    {
        float input = 0f;

        if (isLeftPaddle)
        {
            // W/S
            input = (Input.GetKey(KeyCode.W) ? 1f : 0f) + (Input.GetKey(KeyCode.S) ? -1f : 0f);
        }
        else
        {
            // Up/Down
            input = (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f) + (Input.GetKey(KeyCode.DownArrow) ? -1f : 0f);
        }

        float newY = Mathf.Clamp(rb.position.y + input * speed * Time.fixedDeltaTime, -clampY, clampY);
        targetPos = new Vector2(rb.position.x, newY);
        rb.MovePosition(targetPos);
    }
}

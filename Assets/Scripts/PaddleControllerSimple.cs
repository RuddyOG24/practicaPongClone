using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PaddleControllerSimple : MonoBehaviour
{
    [Header("Jugador 1 = W/S; Jugador 2 = ↑/↓")]
    public bool isLeftPaddle = true;

    [Header("Movimiento")]
    public float speed = 10f;
    public float clampY = 4.5f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
    }

    void Update()
    {
        float move = 0f;

        if (isLeftPaddle)
        {
            if (Input.GetKey(KeyCode.W)) move = 1f;
            if (Input.GetKey(KeyCode.S)) move = -1f;
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow)) move = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) move = -1f;
        }

        Vector2 pos = rb.position;
        pos.y += move * speed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -clampY, clampY);
        rb.MovePosition(pos);
    }
}

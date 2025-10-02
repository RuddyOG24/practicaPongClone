using UnityEngine;

public class PaddleControllerSimple : MonoBehaviour
{
    public float speed = 10f;
    public bool isPlayerOne = true; // true = W/S, false = 8/2

    void Update()
    {
        float move = 0f;

        if (isPlayerOne)
        {
            if (Input.GetKey(KeyCode.W)) move = 1f;
            if (Input.GetKey(KeyCode.S)) move = -1f;
        }
        else
        {
            if (Input.GetKey(KeyCode.Keypad8)) move = 1f;
            if (Input.GetKey(KeyCode.Keypad2)) move = -1f;
        }

        transform.Translate(0, move * speed * Time.deltaTime, 0);

        // Limitar la raqueta dentro de la cámara
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -4.5f, 4.5f);
        transform.position = pos;
    }
}

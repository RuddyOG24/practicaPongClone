using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Cargar la escena del Pong
    public void StartGame()
    {
        SceneManager.LoadScene("PongScene"); // el nombre debe coincidir con tu escena
    }

    // Salir del juego
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // si est�s en el editor
#else
        Application.Quit(); // si est�s en el build
#endif
    }
}

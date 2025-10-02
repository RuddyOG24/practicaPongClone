using UnityEngine;
using UnityEngine.UI;

public class MainMenuHighScore : MonoBehaviour
{
    public Text highScoreText;

    void Start()
    {
        int high = PlayerPrefs.GetInt("HighScore", 0);
        if (highScoreText != null)
            highScoreText.text = "High Score: " + high;
    }
}

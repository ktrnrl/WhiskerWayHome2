using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Працює тільки в білді
    }

    public void Options()
    {
        Debug.Log("Options will be implemented later.");
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeTrigger : MonoBehaviour
{
    public Text victoryText;
    public Text failureText;
    public int requiredMemories = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.Instance.memoriesCollected >= requiredMemories)
        {
            victoryText.gameObject.SetActive(true);
            Invoke(nameof(LoadNextLevel), 3f);
        }
        else
        {
            failureText.gameObject.SetActive(true);
            Invoke(nameof(ReloadLevel), 3f);
        }
    }

void ReloadLevel()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

void LoadNextLevel()
{
    string currentScene = SceneManager.GetActiveScene().name;

    if (currentScene == "Level1")
        SceneManager.LoadScene("Level2");
    else if (currentScene == "Level2")
        SceneManager.LoadScene("Level3");
    else
        SceneManager.LoadScene("Level1"); // або MainMenu
}

}

using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int memoriesCollected = 0;
    public int totalMemories = 3;

    public Text memoryCounterText;
    public Text feedbackText; // UI текст для позитивних/негативних фраз
    public Image screenOverlay; // UI Image (Canvas, full screen) для затемнення
    public Transform player;
    public Vector3 respawnPosition;

    private float negativeMemoryCount = 0;
    private float maxDarknessAlpha = 0.8f; // максимальна затемненість

    private string[] goodMessages =
    {
        "You remembered something beautiful.",
        "A warm feeling returns...",
        "This memory feels safe."
    };

    private string[] badMessages =
    {
        "A shadow creeps in...",
        "That was not what you hoped for...",
        "The world feels colder."
    };

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
        respawnPosition = player.position;
        screenOverlay.color = new Color(0, 0, 0, 0); // прозорий початково
        feedbackText.text = "";
    }

    public void CollectMemory(GameObject memory)
    {
        memoriesCollected++;
        Destroy(memory);
        ShowFeedback(true);
        UpdateUI();
    }

    public void RemoveMemory(GameObject fakeMemory)
    {
        memoriesCollected = Mathf.Max(0, memoriesCollected - 1);
        negativeMemoryCount++;
        Destroy(fakeMemory);
        ShowFeedback(false);
        UpdateDarkness();
        UpdateUI();
    }

    void UpdateUI()
    {
        memoryCounterText.text = "Memory: " + memoriesCollected + "/" + totalMemories;
    }

    void ShowFeedback(bool isGood)
    {
        string[] source = isGood ? goodMessages : badMessages;
        string message = source[Random.Range(0, source.Length)];
        feedbackText.text = message;
        CancelInvoke("ClearFeedback");
        Invoke("ClearFeedback", 2f);
    }

    void ClearFeedback()
    {
        feedbackText.text = "";
    }

    void UpdateDarkness()
    {
        float alpha = Mathf.Clamp01(negativeMemoryCount / totalMemories) * maxDarknessAlpha;
        screenOverlay.color = new Color(0, 0, 0, alpha);
    }
}

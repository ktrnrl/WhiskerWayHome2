using UnityEngine;

public class MemoryItem : MonoBehaviour
{
    public bool isFake = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (isFake)
        {
            GameManager.Instance.RemoveMemory(gameObject);
        }
        else
        {
            GameManager.Instance.CollectMemory(gameObject);
        }
    }
}

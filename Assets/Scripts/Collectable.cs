using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public string currentID = string.Empty;
    public UnityEvent onCollect;
    bool alreadyCollected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadyCollected) return;
        alreadyCollected = true;
        onCollect?.Invoke();
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.currentCollectables++;
        if (currentID != string.Empty)
        {
            player.collectedIDs.Add(currentID);
        }
        Destroy(this.gameObject);
    }
}

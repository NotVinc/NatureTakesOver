using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public string currentID = string.Empty;
    public UnityEvent onCollect;
    bool alreadyCollected;
    public AudioSource collectSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player") return;

        if (alreadyCollected) return;
        alreadyCollected = true;
        onCollect?.Invoke();
        collectSound?.Play();
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.currentCollectables++;
        if (currentID != string.Empty)
        {
            player.collectedIDs.Add(currentID);
        }
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool canInteract = true;
    public UnityEvent onInteract;
    public virtual void Interact(PlayerController trigger)
    {
        if(canInteract)
            onInteract?.Invoke();
    }
}

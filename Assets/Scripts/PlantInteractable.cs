using NUnit.Framework;
using UnityEngine;

public class PlantInteractable : Interactable
{
    public string correctID;
    public bool alreadyInteracted = false;
    public SpriteRenderer plantsToChange;
    public override void Interact()
    {
        //base.Interact();
        if(!canInteract) return;
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (alreadyInteracted)
        {

        }

        alreadyInteracted = true;
        foreach (string id in player.collectedIDs)
        {
            if (id == correctID)
            {
                player.collectedIDs.Remove(id);
                onInteract?.Invoke();
            }
        }

    }
}

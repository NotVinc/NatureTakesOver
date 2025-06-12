using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class PlantInteractable : Interactable
{
    public string correctID;
    public bool alreadyInteracted = false;
    public CorruptedPlant[] plantsToChange;


    public override void Interact(PlayerController trigger)
    {
        //base.Interact();
        if(!canInteract) return;

        if(correctID == string.Empty)
        {

            onInteract?.Invoke();
            if (!alreadyInteracted)
            {
                foreach (CorruptedPlant plant in plantsToChange)
                {
                    plant.Healplant();
                }
            }
            alreadyInteracted = true;
        }
        else
        {
            foreach (var id in trigger.collectedIDs)
            {
                if (id == correctID)
                {
                    trigger.collectedIDs.Remove(id);
                    onInteract?.Invoke();
                    if (!alreadyInteracted)
                    {
                        foreach (var plant in plantsToChange)
                        {
                            plant.Healplant();
                        }
                    }
                    alreadyInteracted = true;
                }
            }
        }

    }
}

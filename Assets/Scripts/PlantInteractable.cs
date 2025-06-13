using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class PlantInteractable : Interactable
{
    public string correctID;
    public bool alreadyInteracted = false;
    public float clearRadius = 10f;


    public override void Interact(PlayerController trigger)
    {
        //base.Interact();
        if(!canInteract) return;

        if(correctID == string.Empty)
        {

            onInteract?.Invoke();
            if (!alreadyInteracted)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, clearRadius);
                foreach (Collider2D col in colliders)
                {
                    CorruptedPlant plant = col.gameObject.GetComponent<CorruptedPlant>();
                    if (plant != null)
                    {
                        plant.Healplant(true);
                    }
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
                        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, clearRadius );
                        foreach (Collider2D col in colliders)
                        {
                            CorruptedPlant plant = col.gameObject.GetComponent<CorruptedPlant>();
                            if (plant != null)
                            {
                                plant.Healplant(true);
                            }
                        }
                    }
                    alreadyInteracted = true;
                }
            }
        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, clearRadius);

    }
}

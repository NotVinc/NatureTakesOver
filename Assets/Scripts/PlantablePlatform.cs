using System.Collections;
using UnityEngine;

public class PlantablePlatform : MonoBehaviour
{
    public float clearRadius = 15f;
    private Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }


    public void GiveMyEffect(float percentage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, clearRadius * percentage);
        foreach (Collider2D col in colliders)
        {
            CorruptedPlant plant = col.gameObject.GetComponent<CorruptedPlant>();
            if (plant != null)
            {
                plant.Healplant(false);
            }
        }
    }

    public void RemoveMyEffect(float percentage = 1f)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, clearRadius * percentage);
        foreach (Collider2D col in colliders)
        {
            CorruptedPlant plant = col.gameObject.GetComponent<CorruptedPlant>();
            if (plant != null)
            {
                plant.Infectplant();
            }
        }
    }

    public void ActivateMyCollider()
    {
        col.enabled = true;
    }

    public void DestoryMe()
    {
        RemoveMyEffect();
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, clearRadius);

    }

}

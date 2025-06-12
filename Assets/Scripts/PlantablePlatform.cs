using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void ActivateMyCollider()
    {
        col.enabled = true;
    }

    public void DestoryMe()
    {
        Destroy(this.gameObject);
    }


}

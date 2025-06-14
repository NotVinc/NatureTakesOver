using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform player; 
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        transform.position = player.position + offset;
    }
}

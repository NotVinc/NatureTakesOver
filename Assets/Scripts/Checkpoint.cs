using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform checkpointPoint;
    public bool isActive = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        anim.SetBool("isActive", isActive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (checkpointPoint != null && player != null)
        {
            if(player.lastCheckPoint != null)
                player.lastCheckPoint.isActive = false;

            player.SetCheckpoint(checkpointPoint.position);
            isActive = true;
            player.lastCheckPoint = this;
        }
    }
}

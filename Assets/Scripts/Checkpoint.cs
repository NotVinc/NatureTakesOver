using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform checkpointPoint;
    public bool isActive = false;
    public bool isCorrupted = false;
    public SpriteRenderer render;
   

    private Animator anim;

    public void HealMe()
    {
        isCorrupted = false;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        anim.SetBool("isActive", isActive);
        anim.SetBool("isCorrupted", isCorrupted);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (checkpointPoint != null && player != null && !isCorrupted)
        {
            if(player.lastCheckPoint != null && player.lastCheckPoint != this)
                player.lastCheckPoint.isActive = false;

            player.SetCheckpoint(checkpointPoint.position);
            if(!isActive) GetComponent<AudioSource>().Play();
            isActive = true;
            player.lastCheckPoint = this;
        }
    }
}

using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    public Collider2D col;
    Animator animator;
    bool isTriggerd =false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !isTriggerd)
        {
            isTriggerd = true;
            animator.SetTrigger("trigger");

        }

    }

    public void EnableCollider(){ col.enabled = true; isTriggerd = false; }
    public void DisableCollider(){ col.enabled = false; isTriggerd = true; }
}

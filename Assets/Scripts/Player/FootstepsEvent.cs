
using System;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsEvent : MonoBehaviour
{
    public List<GroundAudio> audioGround;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void Footstep()
    {
        string tag = string.Empty;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerController.groundCheck.position, playerController.groundCheckRadius);
        foreach (Collider2D col in colliders)
        {
            foreach(GroundAudio audio in audioGround)
            {
                if (col.tag == audio.tag)
                {
                    GetComponent<AudioSource>().clip = audio.audioClips[UnityEngine.Random.Range(0, audio.audioClips.Length)];
                    break;

                }
            }
        }

        GetComponent<AudioSource>().Play();
    }

    public void AllowMovement()
    {
        playerController.SwitchMove(true);
    }

    public void DenyMovement()
    {
        playerController.SwitchMove(false);

    }

    public void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerController.interactStart.position, playerController.interactRadius);
        foreach (Collider2D col in colliders)
        {
            Interactable interactable = col.gameObject.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.Interact(playerController);
            }
        }
    }

}

[Serializable]
public class GroundAudio
{
    public string tag;
    public AudioClip[] audioClips;
}

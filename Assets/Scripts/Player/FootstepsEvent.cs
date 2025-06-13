
using System;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsEvent : MonoBehaviour
{
    public List<AudioClip> footSteps;
    public AudioClip[] pingPongClip;
    public AudioSource pingPong;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void Footstep()
    {
        GetComponent<AudioSource>().clip = footSteps[UnityEngine.Random.Range(0, footSteps.Count)];
        GetComponent<AudioSource>().Play();
    }

    public void PingPong()
    {
        pingPong.clip = pingPongClip[UnityEngine.Random.Range(0,pingPongClip.Length)];
        pingPong.Play();
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

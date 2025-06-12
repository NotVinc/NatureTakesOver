using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public AudioClip[] clickSounds;
    public AudioClip[] hoverSounds;
    public AudioSource clickSource;
    public AudioSource hoverSource;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    public void ClickSound()
    {
        if (!button.enabled) return;
        clickSource.clip = clickSounds[Random.Range(0, clickSounds.Length)];
        clickSource.Play();
    }

    public void HoverSound()
    {
        if (!button.enabled) return;

        hoverSource.clip = hoverSounds[Random.Range(0, hoverSounds.Length)];
        hoverSource.Play();
    }
}

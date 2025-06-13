using System.Collections;
using UnityEngine;

public class CorruptedPlant : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite infectedSprite;
    private SpriteRenderer renderer;
    public ParticleSystem effect;
    bool foreverHealedBool = false;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        if(!foreverHealedBool) renderer.sprite = infectedSprite;
    }
    public void Healplant(bool foreverHealed = true)
    {
        if(!foreverHealedBool) foreverHealedBool = foreverHealed;
        effect.Play();
        renderer.sprite = normalSprite;
    }

    public void Infectplant()
    {
        if(foreverHealedBool) return;
        renderer.sprite = infectedSprite;

    }

}

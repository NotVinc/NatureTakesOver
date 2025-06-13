using System.Collections;
using UnityEngine;

public class CorruptedPlant : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite infectedSprite;
    private SpriteRenderer renderer;
    public GameObject effect;
    bool foreverHealedBool = false;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        if(!foreverHealedBool) renderer.sprite = infectedSprite;
    }
    public void Healplant(bool foreverHealed = true)
    {
        if(!foreverHealedBool) foreverHealedBool = foreverHealed;
        effect.gameObject.SetActive(true);
        StartCoroutine(clearEffect());
        renderer.sprite = normalSprite;
    }

    public void Infectplant()
    {
        if(foreverHealedBool) return;
        renderer.sprite = infectedSprite;

    }

    IEnumerator clearEffect()
    {
        yield return new WaitForSeconds(.3f);
        effect.gameObject.SetActive(false);
    }
}

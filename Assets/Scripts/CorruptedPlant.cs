using UnityEngine;

public class CorruptedPlant : MonoBehaviour
{
    public Sprite normalSprite;
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    public void Healplant()
    {
        renderer.sprite = normalSprite;
    }
}

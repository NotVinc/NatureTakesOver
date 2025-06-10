using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        if(instance != null) Destroy(instance.gameObject);
        instance = this;
    }


    
    /// <summary>
    /// From black to white
    /// </summary>
    public void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }



    /// <summary>
    /// From white to Black
    /// </summary>
    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }
}

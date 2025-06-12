using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    private Animator _animator;
    public Image image;
    public bool playOnAwake = true;
    Coroutine fadeCoro;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();

        if(instance != null) Destroy(instance.gameObject);
        instance = this;

        if(!playOnAwake) image.gameObject.SetActive(false);
    }


    
    /// <summary>
    /// From black to white
    /// </summary>
    public void FadeIn()
    {
        image.gameObject.SetActive(true);
        _animator.SetTrigger("FadeIn");
        if (fadeCoro != null) StopCoroutine(fadeCoro);
        fadeCoro = StartCoroutine(FadeOutEnumerator());
    }



    /// <summary>
    /// From white to Black
    /// </summary>
    public void FadeOut()
    {
        image.gameObject.SetActive(true);
        _animator.SetTrigger("FadeOut");
    }

    public IEnumerator FadeOutEnumerator()
    {
        yield return new WaitForSeconds(1.4f);
        image.gameObject.SetActive(false);

    }



}

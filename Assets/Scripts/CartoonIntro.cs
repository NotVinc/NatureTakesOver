
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CartoonIntro : MonoBehaviour
{
    public List<Sprite> slides;
    public Image display; 
    int index = 0;
    public string SceneName = "SampleScene";

    private void Awake()
    {
        display.sprite = slides[0];
    }

    public void NextSlide()
    {
        index++;
        if(index <= slides.Count)
        {
            display.sprite = slides[index];
        }
        else
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}

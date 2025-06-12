using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(waitIntro());
    }

    IEnumerator waitIntro()
    {
        yield return new WaitForSeconds(9);
        SceneManager.LoadScene("MainMenu");
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel = "SampleScene";

    public void StartGame()
    {
        StartCoroutine(waitBeforeGoInLevel());
    }

    public void QuitGame()
    {
        StartCoroutine(waitBeforeQuit());
    }

    IEnumerator waitBeforeQuit()
    {
        yield return new WaitForSeconds(1.7f);
        Application.Quit();
    }

    IEnumerator waitBeforeGoInLevel()
    {
        yield return new WaitForSeconds(1.7f);
        SceneManager.LoadScene(firstLevel);
    }
}

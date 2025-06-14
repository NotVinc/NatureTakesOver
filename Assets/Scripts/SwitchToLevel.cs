using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToLevel : MonoBehaviour
{
    bool started = false;
    public void SwitchLevel()
    {
        if(!started)StartCoroutine(switchScene());
        started = true;
    }

    IEnumerator switchScene()
    {
        yield return new WaitForSeconds(2.3f);
        FadeManager.instance.FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Outro");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

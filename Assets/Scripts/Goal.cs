using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string Level = "Level2";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1");
        if(collision.tag == "Player")
        {
            Debug.Log("2");
            SceneManager.LoadScene(Level);
        }
    }
}

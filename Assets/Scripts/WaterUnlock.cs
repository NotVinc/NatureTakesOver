using UnityEngine;

public class WaterUnlock : MonoBehaviour
{
    public GameObject pressF;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().unlockedPlanting = true;
            pressF.SetActive(true);
        }
    }
}

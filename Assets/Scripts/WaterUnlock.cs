using UnityEngine;

public class WaterUnlock : MonoBehaviour
{
    public GameObject pressF;
    bool alreadyInteracted = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(!alreadyInteracted) collision.GetComponent<PlayerController>().RefillWater();
            pressF.SetActive(false);
            pressF.SetActive(true);
            alreadyInteracted = true;
        }
    }
}

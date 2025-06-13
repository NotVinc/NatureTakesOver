using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlantInteractable[] allPlants;
    public GameObject wayBlockade;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance.gameObject);
        instance = this;

        allPlants = FindObjectsByType<PlantInteractable>(FindObjectsSortMode.None);
    }

    private void FixedUpdate()
    {
        bool plantedAll = true;

        foreach(PlantInteractable plant in allPlants)
        {
            if (!plant.alreadyInteracted) plantedAll = false;
        }

        wayBlockade.SetActive(!plantedAll);
    }
}

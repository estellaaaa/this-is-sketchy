using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // Reference to the item prefab
    public Transform[] spawnPoints; // Array of spawn points
    private int currentSpawnIndex = 0;
    private GameObject currentStick;
    private bool canSpawn = false;

    void Start()
    {
        // Set the goal for sticks
        TaskBoxController.Instance.SetItemGoal("Stick", spawnPoints.Length);
    }

    public void StartSpawningSticks()
    {
        canSpawn = true;
        SpawnNextItem();
    }

    public void SpawnNextItem()
    {
        if (!canSpawn)
        {
            Debug.Log("Spawning is not enabled yet.");
            return;
        }

        if (currentStick != null)
        {
            Debug.Log("A stick is already spawned, waiting for it to be collected.");
            return;
        }

        if (currentSpawnIndex < spawnPoints.Length)
        {
            Debug.Log($"Spawning stick at spawn point {currentSpawnIndex}");
            currentStick = Instantiate(itemPrefab, spawnPoints[currentSpawnIndex].position, spawnPoints[currentSpawnIndex].rotation);
            currentSpawnIndex++;
        }
        else
        {
            Debug.Log("No more spawn points available");
        }
    }

    public void StickCollected()
    {
        currentStick = null;
    }
}
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; // Reference to the item prefab
    public Transform[] spawnPoints; // Array of spawn points
    private int currentSpawnIndex = 0;
    private GameObject currentStick;

    void Start()
    {
        // Set the goal for sticks
        TaskBoxController.Instance.SetItemGoal("Stick", spawnPoints.Length);
        SpawnNextItem();
    }

    public void SpawnNextItem()
    {
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
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
        currentSpawnIndex = 0; // Ensure we're starting fresh from the first spawn point
        Debug.Log("Started spawning sticks.");
        SpawnNextItem(); // Immediately spawn the first stick
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
            Debug.Log("No more spawn points available for sticks.");
        }
    }

    public void StickCollected()
    {
        currentStick = null;

        // Check if the goal for sticks is reached
        if (TaskBoxController.Instance.IsGoalReached("Stick"))
        {
            Debug.Log("All sticks collected. Advancing to the next task.");
            WizardScript.Instance.AdvanceToNextTask(); // Ensure Wizard_Script has a singleton instance
        }
    }
}
using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject movingObjectPrefab; // Reference to the moving object prefab
    public GameObject objectToLeavePrefab; // Reference to the object to leave behind
    public Transform[] spawnPoints; // Array of spawn points for the moving objects
    public Transform[] leavePoints; // Array of points where objects will be left behind

    public float moveSpeed = 2.0f; // Speed at which the objects move
    public float delayBetweenSpawns = 1.0f; // Delay between spawning each object

    public void StartSpawning()
    {
        Debug.Log("Starting to spawn objects");
        StartCoroutine(SpawnAndMoveObjects());
    }

    public IEnumerator SpawnAndMoveObjects()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Debug.Log($"Spawning moving object at spawn point {i}");
            // Spawn the moving object
            GameObject movingObject = Instantiate(movingObjectPrefab, spawnPoints[i].position, spawnPoints[i].rotation);

            // Move the object to the leave point
            yield return StartCoroutine(MoveObject(movingObject, leavePoints[i].position));

            Debug.Log($"Leaving object at leave point {i}");
            // Leave the object behind
            Instantiate(objectToLeavePrefab, leavePoints[i].position, leavePoints[i].rotation);

            // Destroy the moving object
            Destroy(movingObject);

            // Wait before spawning the next object
            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }

    private IEnumerator MoveObject(GameObject obj, Vector3 targetPosition)
    {
        Debug.Log($"Moving object from {obj.transform.position} to {targetPosition}");
        while (Vector3.Distance(obj.transform.position, targetPosition) > 0.1f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Object reached target position");
    }
}
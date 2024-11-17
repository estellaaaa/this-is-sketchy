using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject movingObjectPrefab;
    public GameObject objectToLeavePrefab;
    public Transform[] spawnPoints;
    public Transform[] leavePoints;

    public float moveSpeed = 2.0f;
    public float delayBetweenSpawns = 1.0f;

    public bool isSpawningComplete = false;

    public void StartSpawning()
    {
        Debug.Log("Starting to spawn objects");
        isSpawningComplete = false;
        StartCoroutine(SpawnAndMoveObjects());
    }

    public IEnumerator SpawnAndMoveObjects()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Debug.Log($"Spawning moving object at spawn point {i}");
            GameObject movingObject = Instantiate(movingObjectPrefab, spawnPoints[i].position, spawnPoints[i].rotation);

            yield return StartCoroutine(MoveObject(movingObject, leavePoints[i].position));

            Debug.Log($"Leaving object at leave point {i}");
            Instantiate(objectToLeavePrefab, leavePoints[i].position, leavePoints[i].rotation);

            Destroy(movingObject);

            yield return new WaitForSeconds(delayBetweenSpawns);
        }

        isSpawningComplete = true;
        Debug.Log("Spawning and moving complete");
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
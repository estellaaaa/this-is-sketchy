using UnityEngine;

public class Stick : MonoBehaviour
{
    private bool isNearPlayer = false;

    private void Update()
    {
        if (isNearPlayer && Input.GetButtonDown("Interact"))
        {
            Debug.Log("Player interacted with the stick");
            CollectStick();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered stick's trigger");
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited stick's trigger");
            isNearPlayer = false;
        }
    }

    private void CollectStick()
    {
        // Notify the ItemSpawner to spawn the next item
        ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
        if (itemSpawner != null)
        {
            Debug.Log("Notifying ItemSpawner to spawn the next stick.");
            itemSpawner.StickCollected();
            itemSpawner.SpawnNextItem();
        }

        // Update the task box counter for sticks
        TaskBoxController.Instance.UpdateItemCounter("Stick");

        // Destroy the stick
        Debug.Log("Destroying the stick.");
        Destroy(gameObject);
    }

}
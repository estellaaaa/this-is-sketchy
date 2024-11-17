// using UnityEngine;

// public class Rubber : MonoBehaviour
// {
//     private bool isNearPlayer = false;

//     private void Update()
//     {
//         if (isNearPlayer && Input.GetButtonDown("Interact"))
//         {
//             Debug.Log("Player interacted with the rubber");
//             CollectRubber();
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Player entered rubber's trigger");
//             isNearPlayer = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Player exited rubber's trigger");
//             isNearPlayer = false;
//         }
//     }

//     private void CollectRubber()
//     {
//         // Notify the ItemSpawner to spawn the next rubber
//         ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
//         if (itemSpawner != null)
//         {
//             Debug.Log("Notifying ItemSpawner to spawn the next rubber");
//             itemSpawner.RubberCollected();
//             itemSpawner.SpawnNextRubber();
//         }

//         // Update the task box counter for rubber
//         TaskBoxController.Instance.UpdateItemCounter("Rubber");

//         // Destroy the rubber
//         Debug.Log("Destroying the rubber");
//         Destroy(gameObject);
//     }
// }
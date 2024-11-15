using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskBoxController : MonoBehaviour
{
    public static TaskBoxController Instance { get; private set; }

    public GameObject taskBox; // Reference to the task box
    public TextMeshProUGUI taskField; // Text display for the task

    private bool taskBoxShown = false; // Flag to track if the task box has been shown

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Ensure the task box is initially disabled
        taskBox.SetActive(false);
    }

    public void ShowTaskBox(string taskText)
    {
        if (!taskBoxShown)
        {
            taskBox.SetActive(true);
            taskField.text = taskText;
            taskBoxShown = true; // Set the flag to true to prevent showing the task box again
        }
    }
}
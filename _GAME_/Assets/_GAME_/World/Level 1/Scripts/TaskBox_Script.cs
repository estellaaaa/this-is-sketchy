using UnityEngine;
using TMPro;

public class TaskBoxController : MonoBehaviour
{
    public static TaskBoxController Instance { get; private set; }

    public GameObject taskBox; // Reference to the task box
    public TextMeshProUGUI taskField; // Text display for the task

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
        taskBox.SetActive(true);
        taskField.text = taskText;
    }
}
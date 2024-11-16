using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TaskBoxController : MonoBehaviour
{
    public static TaskBoxController Instance { get; private set; }

    public GameObject taskBox; // Reference to the task box
    public TextMeshProUGUI taskField; // Text display for the task

    private Dictionary<string, int> itemCounters = new Dictionary<string, int>();
    private Dictionary<string, int> itemGoals = new Dictionary<string, int>();

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

    public void HideTaskBox()
    {
        taskBox.SetActive(false);
    }

    public void SetItemGoal(string itemName, int goal)
    {
        if (!itemGoals.ContainsKey(itemName))
        {
            itemGoals[itemName] = goal;
            itemCounters[itemName] = 0;
        }
    }

    public void UpdateItemCounter(string itemName)
    {
        if (itemCounters.ContainsKey(itemName))
        {
            itemCounters[itemName]++;
            UpdateTaskBoxText();
        }
    }

    public bool IsGoalReached(string itemName)
    {
        return itemCounters.ContainsKey(itemName) && itemCounters[itemName] >= itemGoals[itemName];
    }

    public void UpdateTaskBoxText()
    {
        string taskText = "Collected:\n";
        foreach (var item in itemCounters)
        {
            taskText += $"{item.Key}: {item.Value}/{itemGoals[item.Key]}\n";
        }
        taskField.text = taskText;
    }

    
}
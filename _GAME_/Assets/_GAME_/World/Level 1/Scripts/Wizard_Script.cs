using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WizardScript : MonoBehaviour
{
    private string[] wizardTexts = new string[]
    {
        "Welcome, hero!",
        "I have been waiting for you.",
        "Your journey is just beginning.",
        "You must defeat the evil that plagues this land.",
        "For that, you will need to create tools",
        "and gather items",
        "unfortunately, the pencil which I used to create you broke during the process",
        "so you will have to find the pieces to repair it",
        "walk around the to find the pieces of wood",
    };

    private string[] newTaskTexts = new string[]
    {
        "Great job collecting the sti... wait NO!",
        "The evil are back leaving their mark",
        "Be quick, we have to stop them",
        "For that we need to collect pieced of rubber",
        "to craft a tool to defeat them",
        "THE RUBBER!",
        "QUICKLY!!! FIND THE PIECES!1!!!!1!",
    };

    private int currentTextIndex = 0;
    private bool nearWizard = false;
    public float interactionDistance = 2.0f; // Distance within which the player can interact
    private Transform playerTransform;
    private bool isDisplayingText = false;
    private bool initialTaskStarted = false;

    public static WizardScript Instance { get; private set; }

    public ObjectSpawner objectSpawner; // Reference to the ObjectSpawner

    private List<Task> tasks = new List<Task>();
    private int currentTaskIndex = 0;

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
        Debug.Log("WizardScript Start");
        // Find the player GameObject by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            Debug.Log("Player found: " + player.name);
        }
        else
        {
            Debug.LogError("Player not found");
        }

        // Initialize tasks
        InitializeTasks();

        // Show the initial task box
        UpdateTaskBox();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Check the distance between the player and the wizard
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            nearWizard = distance <= interactionDistance;

            if (nearWizard && !isDisplayingText)
            {
                Debug.Log("Player is near the wizard");

                // Check if the "Interact" button is pressed
                if (Input.GetButtonDown("Interact"))
                {
                    Debug.Log("E key pressed near wizard");
                    CheckTaskCompletion();
                    DisplayWizardText();
                }
            }

            // Advance text on any key press if text is being displayed
            if (isDisplayingText && Input.anyKeyDown)
            {
                currentTextIndex++;
                if (currentTextIndex < wizardTexts.Length)
                {
                    DisplayWizardText();
                }
                else
                {
                    // Hide the text box when all texts have been displayed
                    Debug.Log("Hiding text box after displaying all texts");
                    TextBoxController.Instance.HideTextBox();
                    isDisplayingText = false;
                    currentTextIndex = 0; // Reset the index for future interactions

                    // Update the task box text based on the current task
                    UpdateTaskBox();
                }
            }
        }
    }

    void DisplayWizardText()
    {
        Debug.Log("DisplayWizardText called");
        if (currentTextIndex < wizardTexts.Length)
        {
            Debug.Log($"Displaying wizard text[{currentTextIndex}]: {wizardTexts[currentTextIndex]}");

            // Check TextBoxController references
            if (TextBoxController.Instance != null)
            {
                // Hide the task box if it is active
                if (TaskBoxController.Instance != null && TaskBoxController.Instance.taskBox.activeSelf)
                {
                    Debug.Log("Task box is active, hiding it now");
                    TaskBoxController.Instance.HideTaskBox();
                    Debug.Log("Task box hidden");
                }
                else
                {
                    Debug.Log("Task box is not active or TaskBoxController.Instance is null");
                }

                Debug.Log("Showing text box");
                TextBoxController.Instance.ShowTextBox(wizardTexts[currentTextIndex]);
                Debug.Log($"Wizard text shown: {wizardTexts[currentTextIndex]}");
                isDisplayingText = true;
            }
            else
            {
                Debug.LogError("TextBoxController Instance is null!");
            }
        }
    }

    void CheckTaskCompletion()
    {
        if (TaskBoxController.Instance.IsGoalReached(tasks[currentTaskIndex].itemName))
        {
            Debug.Log($"Task completed: {tasks[currentTaskIndex].description}");
            wizardTexts = newTaskTexts;
            currentTextIndex = 0;

            // Hide the old task box
            TaskBoxController.Instance.HideTaskBox();

            // Start spawning objects before showing the new task
            Debug.Log("Starting to spawn objects before showing the new task");
            StartCoroutine(WaitForObjectsToFinishSpawning());
        }
        else
        {
            Debug.Log("Task not completed yet.");

            // Start the initial task if it hasn't been started yet
            if (!initialTaskStarted)
            {
                initialTaskStarted = true;
                StartInitialTask();
            }
        }
    }

    IEnumerator WaitForObjectsToFinishSpawning()
    {
        // Wait for the objects to finish spawning
        Debug.Log("Waiting for objects to finish spawning");
        yield return StartCoroutine(objectSpawner.SpawnAndMoveObjects());

        // Wait until the spawning is complete
        while (!objectSpawner.isSpawningComplete)
        {
            yield return null;
        }

        // Advance to the next task
        AdvanceToNextTask();
    }

    void UpdateTaskBox()
    {
        if (TaskBoxController.Instance != null)
        {
            if (TaskBoxController.Instance.IsGoalReached(tasks[currentTaskIndex].itemName))
            {
                // Check if the objects have finished moving
                if (objectSpawner.isSpawningComplete)
                {
                    TaskBoxController.Instance.ShowTaskBox(tasks[currentTaskIndex].description);
                }
                else
                {
                    Debug.Log("Waiting for objects to finish moving before showing the next task.");
                }
            }
            else
            {
                TaskBoxController.Instance.ShowTaskBox(tasks[currentTaskIndex].description);
            }
        }
    }

    void StartInitialTask()
    {
        // Notify the ItemSpawner to start spawning sticks
        ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
        if (itemSpawner != null)
        {
            itemSpawner.StartSpawningSticks();
        }
    }

    void InitializeTasks()
    {
        tasks.Add(new Task("Stick", 4, "find the 4 sticks to fix the pencil"));
        tasks.Add(new Task("Rubber", 15, "find the magic stone to complete the pencil"));
        // Add more tasks as needed
    }

    void AdvanceToNextTask()
    {
        if (currentTaskIndex < tasks.Count - 1)
        {
            currentTaskIndex++;
            TaskBoxController.Instance.SetItemGoal(tasks[currentTaskIndex].itemName, tasks[currentTaskIndex].goal);
            UpdateTaskBox();
        }
        else
        {
            Debug.Log("All tasks completed!");
        }
    }
}

public class Task
{
    public string itemName;
    public int goal;
    public string description;

    public Task(string itemName, int goal, string description)
    {
        this.itemName = itemName;
        this.goal = goal;
        this.description = description;
    }
}